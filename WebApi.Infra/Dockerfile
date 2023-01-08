FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApi.Infra/WebApi.Infra.csproj", "WebApi.Infra/"]
RUN dotnet restore "WebApi.Infra/WebApi.Infra.csproj"
COPY . .
WORKDIR "/src/WebApi.Infra"
RUN dotnet build "WebApi.Infra.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.Infra.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.Infra.dll"]
