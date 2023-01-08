FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApi.SuporteTecnico/WebApi.SuporteTecnico.csproj", "WebApi.SuporteTecnico/"]
RUN dotnet restore "WebApi.SuporteTecnico/WebApi.SuporteTecnico.csproj"
COPY . .
WORKDIR "/src/WebApi.SuporteTecnico"
RUN dotnet build "WebApi.SuporteTecnico.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.SuporteTecnico.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.SuporteTecnico.dll"]
