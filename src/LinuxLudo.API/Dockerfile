FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LinuxLudo.API/LinuxLudo.API.csproj", "LinuxLudo.API/"]
RUN dotnet restore "LinuxLudo.API/LinuxLudo.API.csproj"
COPY . .
WORKDIR "/src/LinuxLudo.API"
RUN dotnet build "LinuxLudo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinuxLudo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinuxLudo.API.dll"]
