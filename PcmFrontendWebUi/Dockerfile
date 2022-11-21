FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PcmFrontendWebUi/PcmFrontendWebUi.csproj", "PcmFrontendWebUi/"]
RUN dotnet restore "PcmFrontendWebUi/PcmFrontendWebUi.csproj"
COPY . .
WORKDIR "/src/PcmFrontendWebUi"
RUN dotnet build "PcmFrontendWebUi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PcmFrontendWebUi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PcmFrontendWebUi.dll"]
