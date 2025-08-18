FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY . .
RUN dotnet restore ./_OrderAPI/_OrderAPI.csproj
RUN dotnet publish ./_OrderAPI/_OrderAPI.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "_OrderAPI.dll"]