#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Discord.Bot.TextCommandProcessingService/Discord.Bot.TextCommandProcessingService.csproj", "Discord.Bot.TextCommandProcessingService/"]
COPY ["Discord.Bot.Shared/Discord.Bot.Shared.csproj", "Discord.Bot.Shared/"]
RUN dotnet restore "Discord.Bot.TextCommandProcessingService/Discord.Bot.TextCommandProcessingService.csproj"
COPY . .
WORKDIR "/src/Discord.Bot.TextCommandProcessingService"
RUN dotnet build "Discord.Bot.TextCommandProcessingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discord.Bot.TextCommandProcessingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discord.Bot.TextCommandProcessingService.dll"]