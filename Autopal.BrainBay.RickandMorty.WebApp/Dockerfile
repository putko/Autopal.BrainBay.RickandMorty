#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Autopal.BrainBay.RickandMorty.WebApp/Autopal.BrainBay.RickandMorty.WebApp.csproj", "Autopal.BrainBay.RickandMorty.WebApp/"]
RUN dotnet restore "Autopal.BrainBay.RickandMorty.WebApp/Autopal.BrainBay.RickandMorty.WebApp.csproj"
COPY . .
WORKDIR "/src/Autopal.BrainBay.RickandMorty.WebApp"
RUN dotnet build "Autopal.BrainBay.RickandMorty.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Autopal.BrainBay.RickandMorty.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Autopal.BrainBay.RickandMorty.WebApp.dll"]