#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UniSample.Library/UniSample.Library.Service/UniSample.Library.Service.csproj", "UniSample.Library/UniSample.Library.Service/"]
COPY ["UniSample.Library/UniSample.Library.Domain/UniSample.Library.Domain.csproj", "UniSample.Library/UniSample.Library.Domain/"]
COPY ["UniSample.Common/UniSample.Common.csproj", "UniSample.Common/"]
COPY ["UniSample.Common.WebApi/UniSample.Common.WebApi.csproj", "UniSample.Common.WebApi/"]
RUN dotnet restore "UniSample.Library/UniSample.Library.Service/UniSample.Library.Service.csproj"
COPY . .
WORKDIR "/src/UniSample.Library/UniSample.Library.Service"
RUN dotnet build "UniSample.Library.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniSample.Library.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniSample.Library.Service.dll"]