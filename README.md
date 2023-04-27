# Project: Discite backend

## Elérhetőség


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

### Admin bejelentkezés:
1. regisztrálás a weboldalon
2. phpmyadmin user résznél az id átállítása 0-ra

### Bearer key megszerzése a swagger-nél:
1. bejelentkezés admin-ként
2. az oldalon jobb klikk-> elem vizsgálata -> hálózat -> user -> fejléc és a "Authorization" megkeresése -> Bearer kihagyása és csak az utána lévő másolása pl: eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwibmJmIjoxNjgyNTgwMzM5LCJleHAiOjE2ODI1ODM5MzksImlhdCI6MTY4MjU4MDMzOX0.fzAevm-E9DL4lYymQRmb6CGZcrCDsLbKY9K0bgnM1KiO0OMMPxIlLhZDeNYPmgQAkdBr28mKKAhwgSmxXgG_uw
