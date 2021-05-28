# General information

## Documentation

  - [Technical Spec](general.md)
  - [Authentication](auth.md)
  - [SignalR / Websockets](async.md)
  - [Responsive design](responsive.md)
  - [Deployment](deploy.md)

## Tools and Frameworks used
  - Tailwind
  - SignalR
  - Postgres
  - ASP.NET
  - EntityFramework
  - Docker
  
## Project layout
```
├── docker
│   ├── docker-compose.prod.yml - Deployment for production
│   └── docker-compose.yml - Deployment for testing and development
│
└── src
    ├── LinuxLudo.API - Rest API and Websockets
    ├── LinuxLudo.Core - Generic Models between API & Web
    ├── LinuxLudo.Test - UnitTests
    └── LinuxLudo.Web - Web application in blazor
```
