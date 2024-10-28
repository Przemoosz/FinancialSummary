FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/FinancialSummary.Api/FinancialSummary.Api.csproj", "Source/FinancialSummary.Api/"]
COPY ["Source/FinancialSummary.Application/FinancialSummary.Application.csproj", "Source/FinancialSummary.Application/"]
COPY ["Source/FinancialSummary.Domain/FinancialSummary.Domain.csproj", "Source/FinancialSummary.Domain/"]
COPY ["Source/FinancialSummary.Shared/FinancialSummary.Shared.csproj", "Source/FinancialSummary.Shared/"]
COPY ["Source/FinancialSummary.Infrastructure/FinancialSummary.Infrastructure.csproj", "Source/FinancialSummary.Infrastructure/"]
COPY ["Source/FinancialSummary.Presentation/FinancialSummary.Presentation.csproj", "Source/FinancialSummary.Presentation/"]
RUN dotnet restore "Source/FinancialSummary.Api/FinancialSummary.Api.csproj"
COPY . .
WORKDIR "/src/Source/FinancialSummary.Api"
RUN dotnet build "FinancialSummary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinancialSummary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialSummary.Api.dll"]
