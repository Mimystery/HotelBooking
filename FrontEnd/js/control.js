import { apiFetch } from './api.js';
import { getUser } from './utils.js';
import { refreshAccessToken } from './auth.js';

export function hotelManager() {
    return {
    username: '',
    hotel: {
        hotelName: '',
        city: '',
        address: '',
        description: ''
    },
        hotelAdded: false,
        hotelId: null,
        rooms: [],
        showRoomModal: false,
    newRoom: {
        roomName: '',
        roomSize: '',
        pricePerNight: ''
    },

    init() {
        const user = getUser();

        if (!user) {
            refreshAccessToken();
            return;
        }

        this.username = user.UserName;
    },

    async addHotel() {
        if (!this.hotel.hotelName || !this.hotel.city || !this.hotel.address) {
            alert('Please fill in all required hotel fields');
            return;
        }

        try {
            const response = await apiFetch('http://localhost:5138/api/Hotels/addHotel', {
                method: 'POST',
                body: JSON.stringify(this.hotel)
            });

            const data = await response.json();
            this.hotelId = data.hotelId;
            this.hotelAdded = true;

            alert('Hotel added successfully!');
        } catch (err) {
            console.error('Add hotel error:', err);
            alert('Error adding hotel');
        }
    },

    async createRoom() {
        if (!this.newRoom.roomName || !this.newRoom.roomSize || !this.newRoom.pricePerNight) {
            alert('Please fill in all room fields');
            return;
        }

        try {

            const payload = {
                hotelId: this.hotelId,
                roomName: this.newRoom.roomName,
                roomSize: Number(this.newRoom.roomSize),
                pricePerNight: Number(this.newRoom.pricePerNight)
            };

            const response = await apiFetch('http://localhost:5138/api/Rooms/addRoom', {
                method: 'POST',
                body: JSON.stringify(payload)
            });

            const data = await response.json();

            const roomResponse = await apiFetch(`http://localhost:5138/api/Rooms/roomById/${data.roomId}`, {
                method: 'GET'
            });

            const room = await roomResponse.json();

            data.isEditing = false;
            this.rooms.push(room);

            this.newRoom = { roomName: '', roomSize: '', pricePerNight: '' };
            this.showRoomModal = false;
        } catch (err) {
            console.error('Add room error:', err);
            alert('Error creating room');
        }
    },

    async deleteRoom(roomId) {
        if (!confirm('Are you sure you want to delete this room?')) return;

        try {
            await apiFetch(`http://localhost:5138/api/Rooms/deleteRoom/${roomId}`, {
                method: 'DELETE'
            });
                this.rooms = this.rooms.filter(r => r.roomId !== roomId);
            } catch (err) {
                console.error('Delete room error:', err);
                alert('Error deleting room');
            }
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
window.hotelManager = hotelManager;