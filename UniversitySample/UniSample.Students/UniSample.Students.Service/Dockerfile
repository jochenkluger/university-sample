#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UniSample.Students/UniSample.Students.Service/UniSample.Students.Service.csproj", "UniSample.Students/UniSample.Students.Service/"]
COPY ["UniSample.Students/UniSample.Students.Domain/UniSample.Students.Domain.csproj", "UniSample.Students/UniSample.Students.Domain/"]
COPY ["UniSample.Common/UniSample.Common.csproj", "UniSample.Common/"]
COPY ["UniSample.Common.WebApi/UniSample.Common.WebApi.csproj", "UniSample.Common.WebApi/"]
RUN dotnet restore "UniSample.Students/UniSample.Students.Service/UniSample.Students.Service.csproj"
COPY . .
WORKDIR "/src/UniSample.Students/UniSample.Students.Service"
RUN dotnet build "UniSample.Students.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniSample.Students.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniSample.Students.Service.dll"]