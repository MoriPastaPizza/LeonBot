FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app
COPY ./bin/Release/net9.0/publish/ .
ENTRYPOINT ["dotnet", "MoriPastaPizza.LeonBot.dll"]