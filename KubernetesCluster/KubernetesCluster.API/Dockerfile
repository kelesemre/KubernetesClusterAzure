#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["KubernetesCluster.API/KubernetesCluster.API.csproj", "KubernetesCluster.API/"]
RUN dotnet restore "KubernetesCluster.API/KubernetesCluster.API.csproj"
COPY . .
WORKDIR "/src/KubernetesCluster.API"
RUN dotnet build "KubernetesCluster.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KubernetesCluster.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KubernetesCluster.API.dll"]