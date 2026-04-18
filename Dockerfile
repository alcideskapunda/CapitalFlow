# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /source

COPY ./src .

RUN dotnet restore 'CapitalFlow.Api'

RUN dotnet publish 'CapitalFlow.Api' -c Release -o /publish

# =========================
# RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0
ENV ASPNETCORE_HTTP_PORTS=3333
EXPOSE 3333
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "CapitalFlow.Api.dll"]