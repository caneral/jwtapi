FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Jwt.WebApi/Jwt.WebApi.csproj", "Jwt.WebApi/"]
RUN dotnet restore "Jwt.WebApi/Jwt.WebApi.csproj"
COPY . .
WORKDIR "/src/Jwt.WebApi"
RUN dotnet build "Jwt.WebApi.csproj" -c Release -o /app/build




FROM build AS publish
RUN dotnet publish "Jwt.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jwt.WebApi.dll"]