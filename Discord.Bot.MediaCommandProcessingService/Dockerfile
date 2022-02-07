FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Discord.Bot.MusicCommandHandling/Discord.Bot.MusicCommandHandling.csproj", "Discord.Bot.MusicCommandHandling/"]
RUN dotnet restore "Discord.Bot.MusicCommandHandling/Discord.Bot.MusicCommandHandling.csproj"
COPY . .
WORKDIR "/src/Discord.Bot.MusicCommandHandling"
RUN dotnet build "Discord.Bot.MusicCommandHandling.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discord.Bot.MusicCommandHandling.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discord.Bot.MusicCommandHandling.dll"]
