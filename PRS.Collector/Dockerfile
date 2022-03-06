##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
#WORKDIR /src
#COPY ["PRS.Collector/PRS.Collector.csproj", "PRS.Collector/"]
#COPY ["PRS.Utils/PRS.Utils.csproj", "PRS.Utils/"]
#COPY ["PRS.Models/PRS.Models.csproj", "PRS.Models/"]
#COPY ["PRS.Repository/PRS.Repository.csproj", "PRS.Repository/"]
#COPY ../cnsettings.json .
#COPY ../dbsettings.json .
#RUN dotnet restore "PRS.Collector/PRS.Collector.csproj"
#COPY . .
#WORKDIR "/src/PRS.Collector"
#RUN dotnet build "PRS.Collector.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "PRS.Collector.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY ../cnsettings.json ../
#COPY ../dbsettings.json ../
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "PRS.Collector.dll"]