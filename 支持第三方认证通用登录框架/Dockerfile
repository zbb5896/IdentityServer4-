#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["支持第三方认证通用登录框架/支持第三方认证通用登录框架.csproj", "支持第三方认证通用登录框架/"]
RUN dotnet restore "支持第三方认证通用登录框架/支持第三方认证通用登录框架.csproj"
COPY . .
WORKDIR "/src/支持第三方认证通用登录框架"
RUN dotnet build "支持第三方认证通用登录框架.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "支持第三方认证通用登录框架.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "支持第三方认证通用登录框架.dll"]