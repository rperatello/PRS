##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
#WORKDIR /src
#COPY ["PRS.Main/PRS.Main.csproj", "PRS.Main/"]
#COPY ["PRS.Models/PRS.Models.csproj", "PRS.Models/"]
#COPY ../cnsettings.json .
#RUN dotnet restore "PRS.Main/PRS.Main.csproj"
#COPY . .
#WORKDIR "/src/PRS.Main"
#RUN dotnet build "PRS.Main.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "PRS.Main.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY ../cnsettings.json .
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "PRS.Main.dll"]