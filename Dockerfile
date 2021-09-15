FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ./src/Tiradentes.CobrancaAtiva.Api/Tiradentes.CobrancaAtiva.Api.csproj ./src/Tiradentes.CobrancaAtiva.Api/
COPY ./src/Tiradentes.CobrancaAtiva.Application/Tiradentes.CobrancaAtiva.Application.csproj ./src/Tiradentes.CobrancaAtiva.Application/
COPY ./src/Tiradentes.CobrancaAtiva.CrossCutting.IoC/Tiradentes.CobrancaAtiva.CrossCutting.IoC.csproj ./src/Tiradentes.CobrancaAtiva.CrossCutting.IoC/
COPY ./src/Tiradentes.CobrancaAtiva.Domain/Tiradentes.CobrancaAtiva.Domain.csproj ./src/Tiradentes.CobrancaAtiva.Domain/
COPY ./src/Tiradentes.CobrancaAtiva.Infrastructure/Tiradentes.CobrancaAtiva.Infrastructure.csproj ./src/Tiradentes.CobrancaAtiva.Infrastructure/
COPY ./src/Tiradentes.CobrancaAtiva.Services/Tiradentes.CobrancaAtiva.Services.csproj ./src/Tiradentes.CobrancaAtiva.Services/
COPY ./tests/Tiradentes.CobrancaAtiva.Unit/Tiradentes.CobrancaAtiva.Unit.csproj ./tests/Tiradentes.CobrancaAtiva.Unit/
RUN dotnet restore

# copy everything else and build app
COPY . ./
WORKDIR /app/src/Tiradentes.CobrancaAtiva.Api
RUN dotnet publish -c Release -o publish 

FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY --from=build /app/src/Tiradentes.CobrancaAtiva.Api/publish  ./
ENV ASPNETCORE_URLS=http://+:80 
ENTRYPOINT ["dotnet", "Tiradentes.CobrancaAtiva.Api.dll"]