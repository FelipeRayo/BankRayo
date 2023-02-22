#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet build "BankRayo.csproj" -c Release -o /app/build
RUN dotnet publish "BankRayo.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS publish
WORKDIR /app
COPY --from=base /app/publish .
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "BankRayo.dll"]