import http from 'k6/http';

export const options = {
  vus: 200,
  duration: '30s',
};
let seatCounter = 1;
export default function () {
  const payload = JSON.stringify({
    seatNumber: seatCounter++,
    eventId: 1,
    customerId: 3
  });
  const headers = { 'Content-Type': 'application/json' };
  http.post('http://localhost:5200/bookEventsDb', payload, { headers });
}