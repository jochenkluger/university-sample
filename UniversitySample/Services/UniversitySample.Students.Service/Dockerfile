#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/UniversitySample.Students.Service/UniversitySample.Students.Service.csproj", "Services/UniversitySample.Students.Service/"]
COPY ["UniversitySample.Shared/UniversitySample.Shared.csproj", "UniversitySample.Shared/"]
COPY ["Services/UniversitySample.Students.Domain/UniversitySample.Students.Domain.csproj", "Services/UniversitySample.Students.Domain/"]
RUN dotnet restore "Services/UniversitySample.Students.Service/UniversitySample.Students.Service.csproj"
COPY . .
WORKDIR "/src/Services/UniversitySample.Students.Service"
RUN dotnet build "UniversitySample.Students.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniversitySample.Students.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniversitySample.Students.Service.dll"]