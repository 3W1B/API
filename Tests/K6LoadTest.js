import http from 'k6/http'
import { check } from 'k6';
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export const options = {
  thresholds: {
    http_req_failed: ['rate<0.01'],
  },
};

export const stages = [
    { duration: '5m', target: 200 },
    { duration: '10m', target: 200 },
    { duration: '5m', target: 0 },
];

export default function () {
    const data = { id: 1 };
    let res = http.post(`${__ENV.HOSTING}/logger/read`, JSON.stringify(data),
        {
            headers: { 'Content-Type': 'application/json' }
        }
    );
    const checkSuccess = check(res, { 'Logger Read Success': (r) => r.status === 200 });
    if (!checkSuccess) fail('Logger Read Failed');
}

export function handleSummary(data) {
    return {
        "/Tests/K6Reports/LoadTestSummary.html": htmlReport(data),
    };
}
