FROM mcr.microsoft.com/dotnet/aspnet:7.0 As base
WORKDIR /app
RUN apt-get update -y && apt-get install -y curl

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY Star/Star.Claims/Star.Claims.csproj ./Star/Star.Claims/Star.Claims.csproj
COPY Shared/Shared.csproj ./Shared/Shared.csproj

RUN dotnet restore ./Star/Star.Claims/Star.Claims.csproj

COPY Star/Star.Claims/. ./Star/Star.Claims
COPY Shared/. ./Shared/
RUN dotnet publish ./Star/Star.Claims/Star.Claims.csproj -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Star.Claims.dll"]