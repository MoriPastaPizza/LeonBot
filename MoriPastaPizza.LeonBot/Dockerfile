FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app
COPY ./bin/release/net9.0/publish/ .
ENTRYPOINT ["dotnet", "MoriPastaPizza.LeonBot.dll"]