import { getUser } from './utils.js';
import { apiFetch } from './api.js';
import { refreshAccessToken } from './auth.js';


export function myBookingsApp() {
  return {
    username: '',
    userRole: '',
    bookings: [],

    async init() {

      try {

        const user = getUser();

        if (!user) {
            refreshAccessToken();
            return;
        }

        this.username = user.UserName;
        this.userRole = user.Role

        const response = await apiFetch(`http://localhost:5138/api/Bookings/userBookings/${user.Id}`);
        if (!response.ok) throw new Error(`HTTP error ${response.status}`);
        const data = await response.json();

        this.bookings = data.map(b => ({
          ...b,
          totalPrice: b.totalPrice.toLocaleString('uk-UA'),
          checkInDate: new Date(b.checkInDate).toLocaleDateString('uk-UA'),
          checkOutDate: new Date(b.checkOutDate).toLocaleDateString('uk-UA')
        }));
      } catch (err) {
        console.error('Error fetching bookings:', err);
        alert('Error loading bookings');
      }
    }
  }
}

document.addEventListener('alpine:init', () => {
  Alpine.data('myBookingsApp', myBookingsApp);
});
