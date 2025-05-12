# Executar a migração para criar o banco de dados

cd C:\Dev\SalesOrderManagement\src\api\SalesOrderManagement.API.Infra

dotnet ef migrations add InitialCreate -o Migrations

dotnet ef database update