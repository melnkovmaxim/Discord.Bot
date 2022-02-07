#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Discord.Bot.NotificationService/Discord.Bot.NotificationService.csproj", "Discord.Bot.NotificationService/"]
COPY ["Discord.Bot.Shared/Discord.Bot.Shared.csproj", "Discord.Bot.Shared/"]
RUN dotnet restore "Discord.Bot.NotificationService/Discord.Bot.NotificationService.csproj"
COPY . .
WORKDIR "/src/Discord.Bot.NotificationService"
RUN dotnet build "Discord.Bot.NotificationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discord.Bot.NotificationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discord.Bot.NotificationService.dll"]