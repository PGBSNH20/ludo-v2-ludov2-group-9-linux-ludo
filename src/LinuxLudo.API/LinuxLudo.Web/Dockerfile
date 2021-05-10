FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LinuxLudo.Web/LinuxLudo.Web.csproj", "LinuxLudo.Web/"]
RUN dotnet restore "LinuxLudo.Web/LinuxLudo.Web.csproj"
COPY . .
WORKDIR "/src/LinuxLudo.Web"
RUN dotnet build "LinuxLudo.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinuxLudo.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinuxLudo.Web.dll"]
