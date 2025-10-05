import { redirectToLogin } from './utils.js';

document.addEventListener("DOMContentLoaded", () => {
  const loginForm = document.getElementById("loginForm");
  const registerForm = document.getElementById("registerForm");

  if (loginForm) {
    loginForm.addEventListener("submit", async (e) => {
      e.preventDefault();
      const email = document.getElementById("email").value;
      const password = document.getElementById("password").value;

      const res = await fetch("http://localhost:5138/api/Identity/login", {
        method: "POST",
        credentials: "include",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      if (res.ok) {

        const data = await res.json();

        alert("Login successful!");

        sessionStorage.setItem("accessToken", data.accessToken);

        window.location.href = "index.html";
      } else {
        alert("Login failed");
      }
    });
  }

  if (registerForm) {
    registerForm.addEventListener("submit", async (e) => {
      e.preventDefault();
      const payload = {
        username: document.getElementById("username").value,
        firstName: document.getElementById("firstName").value,
        lastName: document.getElementById("lastName").value,
        email: document.getElementById("email").value,
        password: document.getElementById("password").value,
        confirmPassword: document.getElementById("confirmPassword").value,
      };

      const res = await fetch("http://localhost:5138/api/Identity/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      if (res.ok) {
        alert("Registration successful!");
        window.location.href = "login.html";
      } else {
        alert("Email is already in use!");
      }
    });
  }
});

export async function refreshAccessToken() {
  try {
    const response = await fetch('http://localhost:5138/api/Identity/refresh-token', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
    });

    if (!response.ok){
      redirectToLogin();
      throw new Error('Failed to refresh');
    } 

    const data = await response.json();

    return data.accessToken;
  } catch (err) {
    console.error('Error refresh:', err);
    redirectToLogin();
  }
}
