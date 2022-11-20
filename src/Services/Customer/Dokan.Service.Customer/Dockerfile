#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Customer/Dokan.Service.Customer/Dokan.Service.Customer.csproj", "src/Services/Customer/Dokan.Service.Customer/"]
RUN dotnet restore "src/Services/Customer/Dokan.Service.Customer/Dokan.Service.Customer.csproj"
COPY . .
WORKDIR "/src/src/Services/Customer/Dokan.Service.Customer"
RUN dotnet build "Dokan.Service.Customer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dokan.Service.Customer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dokan.Service.Customer.dll"]