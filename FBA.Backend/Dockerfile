FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FBA.Backend/FBA.Backend.csproj", "FBA.Backend/"]
RUN dotnet restore "FBA.Backend/FBA.Backend.csproj"
COPY . .
WORKDIR "/src/FBA.Backend"
RUN dotnet build "FBA.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FBA.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FBA.Backend.dll"]
