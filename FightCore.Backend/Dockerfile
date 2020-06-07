FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env 
WORKDIR /app
COPY ["FightCore.Backend/FightCore.Backend.csproj", "FightCore.Backend/"]
COPY ["FightCore.Data/FightCore.Data.csproj", "FightCore.Data/"]
COPY ["FightCore.Models/FightCore.Models.csproj", "FightCore.Models/"]
COPY ["FightCore.Configuration/FightCore.Configuration.csproj", "FightCore.Configuration/"]
COPY ["FightCore.Services/FightCore.Services.csproj", "FightCore.Services/"]
COPY ["FightCore.Repository/FightCore.Repositories.csproj", "FightCore.Repository/"]
RUN dotnet restore "FightCore.Backend/FightCore.Backend.csproj"
COPY . .
COPY entrypoint.sh /app/out/entrypoint.sh
RUN dotnet build "FightCore.Backend/FightCore.Backend.csproj" -c Release -o /app/build

RUN dotnet publish "FightCore.Backend/FightCore.Backend.csproj" -c Release -o /app/out

EXPOSE 80/tcp

WORKDIR /app/out
#COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "FightCore.Backend.dll"]
