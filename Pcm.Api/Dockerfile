FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pcm.Api/Pcm.Api.csproj", "Pcm.Api/"]
RUN dotnet restore "Pcm.Api/Pcm.Api.csproj"
COPY . .
WORKDIR "/src/Pcm.Api"
RUN dotnet build "Pcm.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pcm.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pcm.Api.dll"]
