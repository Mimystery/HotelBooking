FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV JwtOptions__Issuer=HotelBookingAPI
ENV JwtOptions__Audience=HotelBookingClient
ENV JwtOptions__SecretKey=5i936cf0vfb6vcjzyucdpxyrh5es6oj8
ENV JwtOptions__ExpiresInMinutes=10

ENV AllowedOrigins=https://hotelbooking-1-u50e.onrender.com

EXPOSE 5000
ENTRYPOINT ["dotnet", "HotelBooking.API.dll"]
