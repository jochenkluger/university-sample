#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UniversitySample.App/Server/UniversitySample.App.Server.csproj", "UniversitySample.App/Server/"]
COPY ["UniversitySample.App/Client/UniversitySample.App.Client.csproj", "UniversitySample.App/Client/"]
COPY ["Services/UniversitySample.Students.Domain/UniversitySample.Students.Domain.csproj", "Services/UniversitySample.Students.Domain/"]
COPY ["UniversitySample.Shared/UniversitySample.Shared.csproj", "UniversitySample.Shared/"]
COPY ["UniversitySample.App/Shared/UniversitySample.App.Shared.csproj", "UniversitySample.App/Shared/"]
RUN dotnet restore "UniversitySample.App/Server/UniversitySample.App.Server.csproj"
COPY . .
WORKDIR "/src/UniversitySample.App/Server"
RUN dotnet build "UniversitySample.App.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniversitySample.App.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniversitySample.App.Server.dll"]