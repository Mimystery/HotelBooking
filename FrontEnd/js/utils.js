export function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);

  if (parts.length === 2) {
    return parts.pop().split(';').shift();
  }
}

export function setCookie(name, value, days) {
  const expires = new Date(Date.now() + days * 864e5).toUTCString();
  document.cookie = `${name}=${value}; expires=${expires}; path=/; Secure; SameSite=Lax`;
}

export function redirectToLogin() {
  window.location.href = '/login.html';
}

export function parseJwt(token) {
  if (!token) return null;

  try {
    const payloadBase64 = token.split('.')[1];

    const base64 = payloadBase64.replace(/-/g, '+').replace(/_/g, '/');

    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );

    return JSON.parse(jsonPayload);
  } catch (err) {
    console.error('Error:', err);
    return null;
  }
}

export function getUser(){
    const token = sessionStorage.getItem('accessToken');
    const userData = parseJwt(token);
    return userData;
}