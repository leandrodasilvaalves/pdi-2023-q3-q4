FROM mcr.microsoft.com/dotnet/aspnet:7.0 As base
WORKDIR /app
RUN apt-get update -y && apt-get install -y curl

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY Star/Star.Entries/Star.Entries.csproj ./Star/Star.Entries/Star.Entries.csproj
COPY Shared/Shared.csproj ./Shared/Shared.csproj

RUN dotnet restore ./Star/Star.Entries/Star.Entries.csproj

COPY Star/Star.Entries/. ./Star/Star.Entries
COPY Shared/. ./Shared/
RUN dotnet publish ./Star/Star.Entries/Star.Entries.csproj -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Star.Entries.dll"]