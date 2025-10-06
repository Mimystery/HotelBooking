import { getCookie, redirectToLogin } from './utils.js';
import { refreshAccessToken } from './auth.js';

export async function apiFetch(url, options = {}, retry = true) {
  let accessToken = getCookie('accessToken');

  const headers = {
    ...(options.headers || {}),
    'Content-Type': 'application/json',
  };

  if (accessToken) {
    headers['Authorization'] = `Bearer ${accessToken}`;
  }

  try {
    const response = await fetch(url, {
      ...options,
      headers,
      credentials: 'include',
    });

    if (response.status === 401 && retry && !url.includes('/refresh-token')) {
      const newToken = await refreshAccessToken();

      if (!newToken) {
        redirectToLogin();
        throw new Error('Unauthorized - redirecting to login');
      }

      const newHeaders = {
        ...(options.headers || {}),
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${newToken}`
      };

      return await fetch(url, {
        ...options,
        headers: newHeaders,
        credentials: 'include'
      });
    }

    if (!response.ok) throw new Error(`Error request: ${response.status}`);
    return response;
  } catch (err) {
    console.error('Error:', err);
    throw err;
  }
}
