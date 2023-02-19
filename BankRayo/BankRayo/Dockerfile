#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BankRayo/BankRayo.csproj", "BankRayo/"]
RUN dotnet restore "BankRayo/BankRayo.csproj"
COPY . .
WORKDIR "/src/BankRayo"
RUN dotnet build "BankRayo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankRayo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankRayo.dll"]