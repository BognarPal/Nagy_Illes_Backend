# Project: Discite backend

## Elérhetőség
Swagger auth key: eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwibmJmIjoxNjgyNTc4Njk0LCJleHAiOjE2ODI1ODIyOTQsImlhdCI6MTY4MjU3ODY5NH0.6qXknd2QROcQqUxrB_sS_jotJb-lz7wSow8GxufRRjkSIAAjacB48P-LIZUHcBBYsrkwiENGPQiW4cDu_uPNxQ

## Üzemeltetés
### Követelmények
- lokálisan futó mysql kiszolgáló
- .NET 6.0 SDK (https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

### Build/Indítás
1. mysql kiszolgáló elindítása
2. Visual Studio PM konzolba: `Update-Database -StartUpProject Discite.Data -project Discite.Data`
3. Discite.API kiválasztása fő projektként
4. projekt indítása
vagy
- start.bat futtatása
