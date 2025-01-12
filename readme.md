## Setup
1. install k6
2. dotnet run ./10k-rq/10k-rq.csproj -c release
3. k6 run ./loadtest.js


## k6 result:
-vus 200

|               |memory list|   |   |   |
|---------------|-----------|---|---|---|
| iterations/s  |   72833   |   |   |   |
| avg duration  |  2.67ms   |   |   |   |
| p(95)         |   5ms     |   |   |   |