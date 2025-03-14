#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Customer.Identity.Microservice.API/Customer.Identity.Microservice.API.csproj", "Customer.Identity.Microservice.API/"]
COPY ["Customer.Identity.Microservice.App/Customer.Identity.Microservice.App.csproj", "Customer.Identity.Microservice.App/"]
COPY ["Customer.Microservice/Customer.Identity.Microservice.Domain.csproj", "Customer.Microservice/"]
COPY ["Customer.Identity.Microservice.Infrastructure/Customer.Identity.Microservice.Infrastructure.csproj", "Customer.Identity.Microservice.Infrastructure/"]
RUN dotnet restore "Customer.Identity.Microservice.API/Customer.Identity.Microservice.API.csproj"
COPY . .
WORKDIR "/src/Customer.Identity.Microservice.API"
RUN dotnet build "Customer.Identity.Microservice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.Identity.Microservice.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.Identity.Microservice.API.dll"]