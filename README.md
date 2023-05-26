# Project: Discite backend

## Elérhetőség
https://discite.jedlik.cloud/api/swagger/index.html

# Szerepkörök
Admin: 
email: admin@gmail.com
jelszó: Admin123

User:
email: testuser@gmail.com
jelszó: Testuser123

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
A '/api/user/register' vagy a '/api/user/login' vépontokat használva a válaszban érkező json objektum "token" mezőjének értékét kell bemásolni az oldal tetején található "Authorize" feliratú gombra kattintva megjelenő ablak beviteli mezőjébe.
