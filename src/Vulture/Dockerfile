FROM mcr.microsoft.com/dotnet/aspnet:7.0 As base
WORKDIR /app
RUN apt-get update -y && apt-get install -y curl

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY Vulture/Vulture.csproj ./Vulture/Vulture.csproj
COPY Shared/Shared.csproj ./Shared/Shared.csproj

RUN dotnet restore ./Vulture/Vulture.csproj

COPY Vulture/. ./Vulture
COPY Shared/. ./Shared/
RUN dotnet publish ./Vulture/Vulture.csproj -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Vulture.dll"]