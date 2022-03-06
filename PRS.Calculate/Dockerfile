##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
#WORKDIR /src
#COPY ["PRS.Calculate/PRS.Calculate.csproj", "PRS.Calculate/"]
#COPY ["PRS.Utils/PRS.Utils.csproj", "PRS.Utils/"]
#COPY ["PRS.Models/PRS.Models.csproj", "PRS.Models/"]
#COPY ["PRS.Repository/PRS.Repository.csproj", "PRS.Repository/"]
#RUN dotnet restore "PRS.Calculate/PRS.Calculate.csproj"
#COPY . .
#WORKDIR "/src/PRS.Calculate"
#RUN dotnet build "PRS.Calculate.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "PRS.Calculate.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "PRS.Calculate.dll"]