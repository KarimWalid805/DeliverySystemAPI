
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore


COPY . ./


RUN dotnet publish -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/publish .

EXPOSE 10000


ENTRYPOINT ["dotnet", "DeliverySystemAPI.dll"] 
