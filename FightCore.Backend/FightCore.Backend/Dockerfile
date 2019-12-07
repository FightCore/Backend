FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-stretch AS build
WORKDIR /src
COPY ["FightCore.Backend/FightCore.Backend.csproj", "FightCore.Backend/"]
COPY ["FightCore.Data/FightCore.Data.csproj", "FightCore.Data/"]
COPY ["FightCore.Models/FightCore.Models.csproj", "FightCore.Models/"]
COPY ["FightCore.Configuration/FightCore.Configuration.csproj", "FightCore.Configuration/"]
COPY ["FightCore.Services/FightCore.Services.csproj", "FightCore.Services/"]
COPY ["FightCore.Repository/FightCore.Repositories.csproj", "FightCore.Repository/"]
RUN dotnet restore "FightCore.Backend/FightCore.Backend.csproj"
COPY . .
WORKDIR "/src/FightCore.Backend"
RUN dotnet build "FightCore.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FightCore.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FightCore.Backend.dll"]