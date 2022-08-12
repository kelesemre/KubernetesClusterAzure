#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["KubernetesCluster.Client/KubernetesCluster.Client.csproj", "KubernetesCluster.Client/"]
RUN dotnet restore "KubernetesCluster.Client/KubernetesCluster.Client.csproj"
COPY . .
WORKDIR "/src/KubernetesCluster.Client"
RUN dotnet build "KubernetesCluster.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KubernetesCluster.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KubernetesCluster.Client.dll"]