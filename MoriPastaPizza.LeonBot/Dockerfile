FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app
COPY ./bin/release/net10.0/linux-x64/publish/ .
ENTRYPOINT ["dotnet", "MoriPastaPizza.LeonBot.dll"]