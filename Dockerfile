FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore SampleApi.API/SampleApi.API.csproj

WORKDIR /src/SampleApi.API
RUN dotnet build SampleApi.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SampleApi.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SampleApi.API.dll"]
