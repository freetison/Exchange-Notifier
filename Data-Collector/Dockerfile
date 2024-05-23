# Utiliza la imagen SDK de .NET 8.0 basada en Ubuntu 22.04 (Jammy Jellyfish)
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build

WORKDIR /src
COPY ["Data-Collector.sln", "./"]

# Copia los archivos de proyecto .csproj y restaura las dependencias de NuGet
COPY ["ExchangeHttpWorker/ExchangeHttpWorker.csproj", "ExchangeHttpWorker/"]
COPY ["HttpServiceProvider/HttpServiceProvider.csproj", "HttpServiceProvider/"]
COPY ["RabbitMqProvider/RabbitMqProvider.csproj", "RabbitMqProvider/"]
RUN dotnet restore

# Copia el resto de los archivos de la solución al contenedor
COPY . .

# Publica la aplicación principal, asegurándose de que todas las dependencias se construyan
RUN dotnet publish "ExchangeHttpWorker/ExchangeHttpWorker.csproj" -c Release -o /app/publish

# Utiliza la imagen de tiempo de ejecución de ASP.NET 8.0 basada en Ubuntu 22.04 (Jammy Jellyfish)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ExchangeHttpWorker.dll"]
