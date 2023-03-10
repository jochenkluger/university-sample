#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/UniversitySample.Courses.Service/UniversitySample.Courses.Service.csproj", "Services/UniversitySample.Courses.Service/"]
COPY ["Services/UniversitySample.Courses.Domain/UniversitySample.Courses.Domain.csproj", "Services/UniversitySample.Courses.Domain/"]
RUN dotnet restore "Services/UniversitySample.Courses.Service/UniversitySample.Courses.Service.csproj"
COPY . .
WORKDIR "/src/Services/UniversitySample.Courses.Service"
RUN dotnet build "UniversitySample.Courses.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniversitySample.Courses.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniversitySample.Courses.Service.dll"]