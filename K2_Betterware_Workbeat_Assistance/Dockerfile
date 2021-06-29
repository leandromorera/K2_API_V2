
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY K2_Betterware_Workbeat_Assistance.Api/*.csproj ./K2_Betterware_Workbeat_Assistance.Api/
COPY K2_Betterware_Workbeat_Assistance.Core/*.csproj ./K2_Betterware_Workbeat_Assistance.Core/
COPY K2_Betterware_Workbeat_Assistance.Infraestructure/*.csproj ./K2_Betterware_Workbeat_Assistance.Infraestructure/
RUN dotnet restore "K2_Betterware_Workbeat_Assistance.Api/K2_Betterware_Workbeat_Assistance.Api.csproj"
COPY . .
WORKDIR "/src/K2_Betterware_Workbeat_Assistance.Api"
RUN dotnet build "K2_Betterware_Workbeat_Assistance.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "K2_Betterware_Workbeat_Assistance.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "K2_Betterware_Workbeat_Assistance.Api.dll", "--server.urls", "http://0.0.0.0:5000"]
