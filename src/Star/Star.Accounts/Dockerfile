FROM mcr.microsoft.com/dotnet/aspnet:7.0 As base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY Star/Star.Accounts/Star.Accounts.csproj ./Star/Star.Accounts/Star.Accounts.csproj
COPY Shared/Shared.csproj ./Shared/Shared.csproj

RUN dotnet restore ./Star/Star.Accounts/Star.Accounts.csproj

COPY Star/Star.Accounts/. ./Star/Star.Accounts
COPY Shared/. ./Shared/
RUN dotnet publish ./Star/Star.Accounts/Star.Accounts.csproj -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Star.Accounts.dll"]