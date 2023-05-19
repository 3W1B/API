import http from 'k6/http'
import { check } from 'k6';
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export const options = {
    threshold: {
        http_req_failed: ['rate<0.01'],
        http_req_duration: ['p(95)<200']
    },
    duration: '5s',
    vus: 10,
    stages:
        [
            { duration: '20s', target: 15 },
            { duration: '20s', target: 15 },
            { duration: '20s', target: 20 },
            { duration: '20s', target: 20 },
            { duration: '20s', target: 50 },
            { duration: '20s', target: 50 },
            { duration: '20s', target: 0 },
        ]
}

export default function () {
    const data = { id: 1 };
    let res = http.post(`http://${__ENV.HOSTING}/log/readall`, JSON.stringify(data),
        {
            headers: { 'Content-Type': 'application/json' }
        }
    );
    const checkSuccess = check(res, { 'Task Read Success': (r) => r.status === 200 });
    if (!checkSuccess) fail('Task Read Failed');
}

export function handleSummary(data) {
    return {
        "K6Reports/StressTestSummary.html": htmlReport(data),
    };
}