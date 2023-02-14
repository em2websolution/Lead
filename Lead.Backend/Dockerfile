#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lead.Backend/Lead.Backend.csproj", "Lead.Backend/"]
COPY ["Adapter/Adapter.csproj", "Adapter/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
RUN dotnet restore "Lead.Backend/Lead.Backend.csproj"
COPY . .
WORKDIR "/src/Lead.Backend"
RUN dotnet build "Lead.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lead.Backend.csproj" -c Release -o /app/publish

# Set the environment variable for RabbitMQ host
ENV RABBITMQ_HOST="localhost"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lead.Backend.dll"]