#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/UniversitySample.Library.Service/UniversitySample.Library.Service.csproj", "Services/UniversitySample.Library.Service/"]
RUN dotnet restore "Services/UniversitySample.Library.Service/UniversitySample.Library.Service.csproj"
COPY . .
WORKDIR "/src/Services/UniversitySample.Library.Service"
RUN dotnet build "UniversitySample.Library.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniversitySample.Library.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniversitySample.Library.Service.dll"]