FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app_build
COPY *.csproj ./
RUN dotnet restore
COPY ./ ./
RUN dotnet publish -c Release -o build_out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY bin/Release/net6.0/publish/ .
ENTRYPOINT ["dotnet", "Card.dll"]