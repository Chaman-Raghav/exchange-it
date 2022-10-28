#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.1-alpine3.10 AS base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS base
RUN apk add curl
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

#FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101-alpine3.10 AS build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS build
WORKDIR /src
COPY Api/ .
RUN dotnet build -c Release
RUN dotnet publish -c Release --no-restore --no-build -o /publish


FROM base AS final
WORKDIR /publish
COPY --from=build /publish .
# CMD tail -f /dev/null
ENTRYPOINT ["dotnet", "Api.dll", "--urls", "https://0.0.0.0:5001;https://0.0.0.0:5000", "-r", "linux-x64"]


# #FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.1-alpine3.10 AS base
# FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11
# RUN apk add curl
# WORKDIR /app
# EXPOSE 5000
# EXPOSE 5001

# #FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101-alpine3.10 AS build
# WORKDIR /app
# COPY Api/ .
# RUN dotnet build -v n -c Release
# RUN dotnet publish -c Release -v n --no-restore --no-build -o /publish/

# WORKDIR /publish
# #ENTRYPOINT [ "/bin/sh" ]
# ENTRYPOINT ["dotnet", "Api.dll", "--urls", "https://0.0.0.0:5001;https://0.0.0.0:5000", "-r", "linux-x64"]