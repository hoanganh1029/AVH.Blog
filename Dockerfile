# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /source
COPY ["src/AVBlog.WebApp/AVBlog.WebApp.csproj", "src/AVBlog.WebApp/"]
COPY ["src/AVBlog.Infrastructure/AVBlog.Infrastructure.csproj", "src/AVBlog.Infrastructure/"]
COPY ["src/AVBlog.Application/AVBlog.Application.csproj", "src/AVBlog.Application/"]
COPY ["src/AVBlog.Domain/AVBlog.Domain.csproj", "src/AVBlog.Domain/"]
RUN dotnet restore "./src/AVBlog.WebApp/AVBlog.WebApp.csproj"
COPY . .
WORKDIR "/source/src/AVBlog.WebApp"
RUN dotnet build "./AVBlog.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AVBlog.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AVBlog.WebApp.dll"]