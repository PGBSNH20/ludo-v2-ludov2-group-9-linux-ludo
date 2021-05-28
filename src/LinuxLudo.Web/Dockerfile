# Specify Image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Set work dir
WORKDIR /src

# Copy csproj files into container
COPY LinuxLudo.Web/LinuxLudo.Web.csproj ./LinuxLudo.Web/
COPY LinuxLudo.Core/LinuxLudo.Core.csproj ./LinuxLudo.Core/

# Restore (resolve deps)
RUN dotnet restore "LinuxLudo.Web/LinuxLudo.Web.csproj"

# Copy source code into build
COPY LinuxLudo.Web ./LinuxLudo.Web/
COPY LinuxLudo.Core ./LinuxLudo.Core/

# Build project and output to /build
RUN dotnet build "LinuxLudo.Web/LinuxLudo.Web.csproj" -c Release -o /build

FROM build AS publish
RUN dotnet publish "LinuxLudo.Web/LinuxLudo.Web.csproj" -c Release -o /publish
RUN ls /publish

FROM nginx:alpine AS final
WORKDIR /var/www/web
COPY --from=publish /publish/wwwroot .
COPY LinuxLudo.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
