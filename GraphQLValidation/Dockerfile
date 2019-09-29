FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build
WORKDIR /src
COPY ["GraphQLValidation.csproj", ""]
RUN dotnet restore "./GraphQLValidation.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "GraphQLValidation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GraphQLValidation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQLValidation.dll"]