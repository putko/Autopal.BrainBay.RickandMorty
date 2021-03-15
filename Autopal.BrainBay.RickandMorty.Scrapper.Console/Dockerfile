#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Autopal.BrainBay.RickandMorty.Scrapper.Console/Autopal.BrainBay.RickandMorty.Scrapper.Console.csproj", "Autopal.BrainBay.RickandMorty.Scrapper.Console/"]
COPY ["Autopal.BrainBay.RickandMorty.Scrapper.Business/Autopal.BrainBay.RickandMorty.Scrapper.Business.csproj", "Autopal.BrainBay.RickandMorty.Scrapper.Business/"]
COPY ["Autopal.BrainBay.RickandMorty.Scrapper.Connector/Autopal.BrainBay.RickandMorty.Scrapper.Connector.csproj", "Autopal.BrainBay.RickandMorty.Scrapper.Connector/"]
RUN dotnet restore "Autopal.BrainBay.RickandMorty.Scrapper.Console/Autopal.BrainBay.RickandMorty.Scrapper.Console.csproj"
COPY . .
WORKDIR "/src/Autopal.BrainBay.RickandMorty.Scrapper.Console"
RUN dotnet build "Autopal.BrainBay.RickandMorty.Scrapper.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Autopal.BrainBay.RickandMorty.Scrapper.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Autopal.BrainBay.RickandMorty.Scrapper.Console.dll"]
