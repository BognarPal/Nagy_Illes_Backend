@echo off

mysql -u root < .\project_discite.sql

dotnet build
.\Discite.API\bin\Debug\net6.0\Discite.API.exe
