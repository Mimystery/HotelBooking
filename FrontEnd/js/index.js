import Alpine from 'https://cdn.jsdelivr.net/npm/alpinejs@3.x.x/dist/module.esm.js';
import { apiFetch } from './api.js';
import { getUser } from './utils.js';

window.hotelApp = function () {
  return {
    hotels: [],
    search: '',
    city: '',
    username: '',
    userRole: '',
    startDate: '',
    endDate: '',
    bookings: [],
    bookingsByRoom: {},

    get filteredHotels() {
        const searchTerm = this.search.toLowerCase();
        const cityTerm = this.city.toLowerCase();

        let hotelsFiltered = this.hotels.filter(h =>
            h.hotelName.toLowerCase().includes(searchTerm) &&
            h.city.toLowerCase().includes(cityTerm)
        );

        if (this.startDate && this.endDate) {
            const start = new Date(this.startDate);
            const end = new Date(this.endDate);

            hotelsFiltered = hotelsFiltered.filter(hotel =>
                hotel.rooms.some(room => {
                    const bookingsForRoom = this.bookingsByRoom[room.roomId] || [];
                    return !bookingsForRoom.some(b =>
                        (start < new Date(b.checkOutDate)) &&
                        (new Date(b.checkInDate) < end)
                    );
                })
            );
        }

        return hotelsFiltered;
    },

    async init() {

      try {

        const response1 = await apiFetch(`https://hotelbooking-0bnw.onrender.com/api/Bookings/allBookings`);

        this.bookings = await response1.json();

        this.bookingsByRoom = {};
        this.bookings.forEach(b => {
            if (!this.bookingsByRoom[b.roomId]) this.bookingsByRoom[b.roomId] = [];
            this.bookingsByRoom[b.roomId].push(b);
        });


        const response = await apiFetch('https://hotelbooking-0bnw.onrender.com/api/Hotels/allHotels');
        if (!response.ok) throw new Error(`HTTP error ${response.status}`);
        this.hotels = await response.json();

        const user = getUser();
        this.userRole = user.Role;
        this.username = user.UserName;

        flatpickr(this.$refs.startDatePicker, {
            dateFormat: "Y-m-d",
            onChange: (selectedDates) => {
                this.startDate = selectedDates[0] ? selectedDates[0].toISOString().split('T')[0] : '';
            }
            });

            flatpickr(this.$refs.endDatePicker, {
            dateFormat: "Y-m-d",
            onChange: (selectedDates) => {
                this.endDate = selectedDates[0] ? selectedDates[0].toISOString().split('T')[0] : '';
            }
            });
        
      } catch (err) {
        console.error('Error loading hotels:', err);
      }
    },

    selectHotel(hotel) {
      sessionStorage.setItem('selectedHotel', JSON.stringify(hotel));
      window.location.href = 'booking.html';
    },

    async removeHotel(id) {
        this.hotels = this.hotels.filter(h => h.hotelId !== id);

        try {

            await apiFetch(`https://hotelbooking-0bnw.onrender.com/api/Hotels/deleteHotel/${id}`, {
                method: 'DELETE'
            });

        } catch (err) {
            console.error('Delete room error:', err);
            alert('Error deleting room');
        }

    }
  };
};

window.Alpine = Alpine;
Alpine.start();
