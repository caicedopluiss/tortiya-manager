FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app-build
# Copy everything from root/ directory (elements inside .dockerignore are excluded)
# Context must be parent folder (root/) to be able to copy its content
COPY ../../TortiYaManager.sln ./
COPY ../../server/ ./server

RUN dotnet restore TortiYaManager.sln && \
  dotnet publish -c Release -o published server/TortiYaManager.WebAPI/TortiYaManager.WebAPI.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /app-build/published .
ENTRYPOINT ["dotnet"]
CMD ["TortiYaManager.WebAPI.dll"]