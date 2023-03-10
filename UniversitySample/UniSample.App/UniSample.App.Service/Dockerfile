#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UniSample.App/UniSample.App.Service/UniSample.App.Service.csproj", "UniSample.App/UniSample.App.Service/"]
COPY ["UniSample.App/UniSample.App.Client/UniSample.App.Client.csproj", "UniSample.App/UniSample.App.Client/"]
COPY ["UniSample.Students/UniSample.Students.Domain/UniSample.Students.Domain.csproj", "UniSample.Students/UniSample.Students.Domain/"]
COPY ["UniSample.Common/UniSample.Common.csproj", "UniSample.Common/"]
COPY ["UniSample.Library/UniSample.Library.Domain/UniSample.Library.Domain.csproj", "UniSample.Library/UniSample.Library.Domain/"]
COPY ["UniSample.Courses/UniSample.Courses.Domain/UniSample.Courses.Domain.csproj", "UniSample.Courses/UniSample.Courses.Domain/"]
RUN dotnet restore "UniSample.App/UniSample.App.Service/UniSample.App.Service.csproj"
COPY . .
WORKDIR "/src/UniSample.App/UniSample.App.Service"
RUN dotnet build "UniSample.App.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniSample.App.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniSample.App.Service.dll"]