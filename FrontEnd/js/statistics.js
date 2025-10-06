import { getUser } from './utils.js';
import { apiFetch } from './api.js';
import { refreshAccessToken } from './auth.js';

export function statisticsApp() {
  return {
    username: '',
    bookings: [],
    statistics: [],
    chart: null,

    async init() {

      try {
        const response = await apiFetch(`https://hotelbooking-0bnw.onrender.com/api/Bookings/allBookings`);

        const user = getUser();
        if (!user) {
            refreshAccessToken();
            return;
        }
        this.username = user.UserName;

        const data = await response.json();
        this.bookings = data.map(b => ({
          ...b,
          totalPrice: b.totalPrice.toLocaleString('uk-UA'),
          checkInDate: new Date(b.checkInDate).toLocaleDateString('uk-UA'),
          checkOutDate: new Date(b.checkOutDate).toLocaleDateString('uk-UA')
        }));

        const statsResp = await apiFetch(`https://hotelbooking-0bnw.onrender.com/api/Bookings/bookingsStatistics`);
        const statsData = await statsResp.json();

        this.statistics = statsData.map(s => ({
            date: new Date(s.date).toLocaleDateString('ua-UK'),
            count: Number(s.count)
        }));

    //     this.statistics = [
    //     { date: '10/01/2025', count: 1 },
    //     { date: '10/02/2025', count: 3 },
    //     { date: '10/03/2025', count: 2 },
    //     { date: '10/04/2025', count: 5 },
    //     { date: '10/05/2025', count: 4 },
    //   ];


        this.renderChart();

      } catch (err) {
        console.error('Error fetching bookings:', err);
        alert('Error loading bookings');
      }
    },

    renderChart() {
      const ctx = document.getElementById('bookingsChart').getContext('2d');

      if (this.chart) {
        this.chart.destroy();
      }

      this.chart = new Chart(ctx, {
        type: 'bar',
        data: {
          labels: this.statistics.map(s => s.date),
          datasets: [{
            data: this.statistics.map(s => s.count),
            backgroundColor: 'rgba(59, 130, 246, 1)',
            borderRadius: 5
          }]
        },
        options: {
          responsive: true,
          plugins: {
            legend: { display: false }
          },
          scales: {
            y: {
                beginAtZero: true,
                ticks: {
                    callback: function(value) {
                    return Number(value);
                    },
                }, 
                title: { display: true, text: 'Number of Bookings' }
            },
            x: {
                title: { display: true, text: 'Date' }
            }
          }
        }
      });
    }

  }
}

document.addEventListener('alpine:init', () => {
  Alpine.data('statisticsApp', statisticsApp);
});
