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
