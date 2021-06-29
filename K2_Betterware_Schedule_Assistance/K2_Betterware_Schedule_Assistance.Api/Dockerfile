#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["K2_Betterware_Schedule_Assistance.Api/K2_Betterware_Schedule_Assistance.Api.csproj", "K2_Betterware_Schedule_Assistance.Api/"]
COPY ["K2_Betterware_Schedule_Assistance.Infraestructure/K2_Betterware_Schedule_Assistance.Infraestructure.csproj", "K2_Betterware_Schedule_Assistance.Infraestructure/"]
COPY ["K2_Betterware_Schedule_Assistance.Core/K2_Betterware_Schedule_Assistance.Core.csproj", "K2_Betterware_Schedule_Assistance.Core/"]
RUN dotnet restore "K2_Betterware_Schedule_Assistance.Api/K2_Betterware_Schedule_Assistance.Api.csproj"
COPY . .
WORKDIR "/src/K2_Betterware_Schedule_Assistance.Api"
RUN dotnet build "K2_Betterware_Schedule_Assistance.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "K2_Betterware_Schedule_Assistance.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "K2_Betterware_Schedule_Assistance.Api.dll"]