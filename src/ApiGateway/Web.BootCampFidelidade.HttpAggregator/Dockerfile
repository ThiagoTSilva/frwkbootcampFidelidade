#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Web.BootCampFidelidade.HttpAggregator/Web.BootCampFidelidade.HttpAggregator.csproj", "Web.BootCampFidelidade.HttpAggregator/"]
COPY ["FrwkBootCampFidelidade.DTO/FrwkBootCampFidelidade.DTO.csproj", "FrwkBootCampFidelidade.DTO/"]
COPY ["FrwkBootCampFidelidade.Dominio/FrwkBootCampFidelidade.Dominio.csproj", "FrwkBootCampFidelidade.Dominio/"]
COPY ["FrwkBootCampFidelidade.Aplicacao/FrwkBootCampFidelidade.Aplicacao.csproj", "FrwkBootCampFidelidade.Aplicacao/"]
COPY ["FrwkBootCampFidelidade.IOC/FrwkBootCampFidelidade.Infraestrutura.IOC.csproj", "FrwkBootCampFidelidade.IOC/"]
COPY ["FrwkBootCampFidelidade.Infraestrutura/FrwkBootCampFidelidade.Infraestrutura.Data.csproj", "FrwkBootCampFidelidade.Infraestrutura/"]
RUN dotnet restore "Web.BootCampFidelidade.HttpAggregator/Web.BootCampFidelidade.HttpAggregator.csproj"
COPY . .
WORKDIR "/src/Web.BootCampFidelidade.HttpAggregator"
RUN dotnet build "Web.BootCampFidelidade.HttpAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.BootCampFidelidade.HttpAggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.BootCampFidelidade.HttpAggregator.dll"]