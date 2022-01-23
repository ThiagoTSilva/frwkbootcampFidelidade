#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FrwkBootCampFidelidade.Order.API/FrwkBootCampFidelidade.Order.API.csproj", "FrwkBootCampFidelidade.Order.API/"]
COPY ["FrwkBootCampFidelidade.Infraestrutura/FrwkBootCampFidelidade.Infraestrutura.Data.csproj", "FrwkBootCampFidelidade.Infraestrutura/"]
COPY ["FrwkBootCampFidelidade.DTO/FrwkBootCampFidelidade.DTO.csproj", "FrwkBootCampFidelidade.DTO/"]
COPY ["FrwkBootCampFidelidade.Dominio/FrwkBootCampFidelidade.Dominio.csproj", "FrwkBootCampFidelidade.Dominio/"]
COPY ["FrwkBootCampFidelidade.IOC/FrwkBootCampFidelidade.Infraestrutura.IOC.csproj", "FrwkBootCampFidelidade.IOC/"]
COPY ["FrwkBootCampFidelidade.Aplicacao/FrwkBootCampFidelidade.Aplicacao.csproj", "FrwkBootCampFidelidade.Aplicacao/"]
RUN dotnet restore "FrwkBootCampFidelidade.Order.API/FrwkBootCampFidelidade.Order.API.csproj"
COPY . .
WORKDIR "/src/FrwkBootCampFidelidade.Order.API"
RUN dotnet build "FrwkBootCampFidelidade.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FrwkBootCampFidelidade.Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FrwkBootCampFidelidade.Order.API.dll"]