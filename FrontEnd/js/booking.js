import { getUser, getCookie, redirectToLogin } from './utils.js';
import { refreshAccessToken } from './auth.js';
import { apiFetch } from './api.js';

async function ensureToken() {
  const accessToken = getCookie('accessToken');
  if (!accessToken) {
    const newToken = await refreshAccessToken();
    if (!newToken) {
      redirectToLogin();
      return false;
    }
  }
  return true;
}

export function hotelPage() {

  const storedHotel = JSON.parse(sessionStorage.getItem('selectedHotel'));

  const user = getUser();

  return {
    userRole: user.Role,
    username: user.UserName,
    currentImage: 0,
    images: storedHotel.images || [1, 2, 3, 4],
    hotel: {
    },
    rooms: [],

    showBookingModal: false,
    selectedRoom: null,
    startDate: '',
    endDate: '',
    summary: 0,
    bookedRanges: [],

    async init() {
      ensureToken()

      const storedHotel = JSON.parse(sessionStorage.getItem('selectedHotel'));
      if (!storedHotel) {
        alert('No hotel selected');
        return;
      }

      try {
        const hotelResp = await apiFetch(`http://localhost:5138/api/Hotels/hotelById/${storedHotel.hotelId}`);
        const hotelData = await hotelResp.json();

        this.hotel = {
          hotelId: hotelData.hotelId,
          hotelName: hotelData.hotelName,
          city: hotelData.city,
          address: hotelData.address,
          description: hotelData.description || 'No description.'
        };

        this.images = hotelData.images || [1,2,3,4];

        this.rooms = (hotelData.rooms || []).map(r => ({ ...r, isEditing: false }));

      } catch (err) {
        console.error('Error loading hotel/rooms:', err);
        alert('Failed to load hotel data');
      }
    },

    async bookRoom() {
      if (!this.startDate || !this.endDate) {
        alert('Please select start and end dates.');
        return;
      }

      const start = new Date(this.startDate);
      const end = new Date(this.endDate);
      if (end <= start) {
        alert('End date must be after start date.');
        return;
      }

      const bookingData = {
        userId: getUser().Id,
        roomId: this.selectedRoom.roomId,
        hotelId: this.hotel.hotelId,
        hotelName: this.hotel.hotelName,
        city: this.hotel.city,
        roomName: this.selectedRoom.roomName,
        firstName: getUser().FirstName || 'Unknown',
        lastName: getUser().LastName || 'Unknown',
        checkInDate: new Date(this.startDate).toISOString(),
        checkOutDate: new Date(this.endDate).toISOString(),
        totalPrice: this.summary,
    };


    try {
        const response = await apiFetch('http://localhost:5138/api/Bookings/addBooking', {
          method: 'POST',
          body: JSON.stringify(bookingData)
        });

        if (response.ok) {
          alert("Booking confirmed!");
          this.showBookingModal = false;
        } else {
          const text = await response.text();
          alert('Failed to book: ' + text);
        }

      } catch (err) {
        console.error('Booking error:', err);
        alert('An error occurred while booking. Please try again later.');
      }
    },

    async openBookingModal(room) {
      this.selectedRoom = room;
      this.showBookingModal = true;
      this.startDate = '';
      this.endDate = '';
      this.summary = 0;

      await this.loadBookedDates(room.roomId);

      this.$nextTick(() => {
        const disabledRanges = this.bookedRanges.map(b => ({
          from: b.start,
          to: b.end
        }));

        flatpickr(this.$refs.startPicker, {
          dateFormat: 'Y-m-d',
          disable: disabledRanges,
          minDate: 'today',
          onChange: (selectedDates) => {
            this.startDate = selectedDates[0] ? selectedDates[0].toISOString().split('T')[0] : '';
            this.calculateSummary();
          }
        });

        flatpickr(this.$refs.endPicker, {
          dateFormat: 'Y-m-d',
          disable: disabledRanges,
          minDate: 'today',
          onChange: (selectedDates) => {
            this.endDate = selectedDates[0] ? selectedDates[0].toISOString().split('T')[0] : '';
            this.calculateSummary();
          }
        });
      });
    },

    async loadBookedDates(roomId) {

      try {
        const response = await apiFetch(`http://localhost:5138/api/Bookings/roomBookings/${roomId}`);
        if (!response.ok) throw new Error('Failed to load bookings');
        const data = await response.json();

        this.bookedRanges = data.map(b => ({
          start: b.checkInDate.split('T')[0],
          end: b.checkOutDate.split('T')[0]
        }));
      } catch (err) {
        console.error('Error loading booked dates:', err);
        this.bookedRanges = [];
      }
    },

    calculateSummary() {
      if (!this.startDate || !this.endDate) return;
      const start = new Date(this.startDate);
      const end = new Date(this.endDate);

      if (end <= start) {
        this.summary = 0;
        return;
      }

      const nights = Math.ceil((end - start) / (1000 * 60 * 60 * 24));
      this.summary = (nights * this.selectedRoom.pricePerNight).toFixed(2);
    },

    nextImage() {
      this.currentImage = (this.currentImage + 1) % this.images.length;
    },

    prevImage() {
      this.currentImage = (this.currentImage - 1 + this.images.length) % this.images.length;
    },
    
    async updateRoom(room) {
        try {
            await apiFetch(`http://localhost:5138/api/Rooms/updateRoom/${room.roomId}`, {
            method: 'PUT',
            body: JSON.stringify({
                roomName: room.roomName,
                roomSize: room.roomSize,
                pricePerNight: room.pricePerNight
            })
            });

            room.isEditing = false;
            alert('Room updated successfully!');
        } catch (err) {
            console.error('Update room error:', err);
            alert('Error updating room');
        }
    }
  };
}

window.hotelPage = hotelPage;
