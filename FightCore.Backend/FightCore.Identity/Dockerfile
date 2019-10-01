FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["FightCore.Identity/FightCore.Identity.csproj", "FightCore.Identity/"]
COPY ["FightCore.Models/FightCore.Models.csproj", "FightCore.Models/"]
COPY ["FightCore.Data/FightCore.Data.csproj", "FightCore.Data/"]
COPY ["FightCore.Configuration/FightCore.Configuration.csproj", "FightCore.Configuration/"]
RUN dotnet restore "FightCore.Identity/FightCore.Identity.csproj"
COPY . .
WORKDIR "/src/FightCore.Identity"
RUN dotnet build "FightCore.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FightCore.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FightCore.Identity.dll"]