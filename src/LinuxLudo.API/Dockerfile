FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# Specify Image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Set work dir
WORKDIR /src

# Copy csproj files into container
COPY LinuxLudo.API/LinuxLudo.API.csproj ./LinuxLudo.API/
COPY LinuxLudo.Core/LinuxLudo.Core.csproj ./LinuxLudo.Core/

# Restore (resolve deps)
RUN dotnet restore "LinuxLudo.API/LinuxLudo.API.csproj"

# Copy source code into build
COPY LinuxLudo.API ./LinuxLudo.API/
COPY LinuxLudo.Core ./LinuxLudo.Core/

# Build project and output to /build
RUN dotnet build "LinuxLudo.API/LinuxLudo.API.csproj" -c Release -o /build

FROM build AS publish
RUN dotnet publish "LinuxLudo.API/LinuxLudo.API.csproj" -c Release -o /publish

FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "LinuxLudo.API.dll"]
