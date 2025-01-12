import http from 'k6/http';

export const options = {
  vus: 200,
  duration: '30s',
};

export default function () {
  const payload = JSON.stringify({
    seatNumber: 1,
    eventId: 2,
    customerId: 3
  });
  const headers = { 'Content-Type': 'application/json' };
  http.post('http://localhost:5200/bookEvents', payload, { headers });
}