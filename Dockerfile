FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.props .
COPY *.sln .

COPY WebApp/*.csproj ./WebApp/
COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY App.Contracts.BLL/*.csproj ./App.Contracts.BLL/
COPY App.Contracts.DAL/*.csproj ./App.Contracts.DAL/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY App.Domain/*.csproj ./App.Domain/
COPY App.Domain.Enums/*.csproj ./App.Domain.Enums/
COPY App.Public.DTO/*.csproj ./App.Public.DTO/

COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.DAL/*.csproj ./Base.DAL/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Extensions/*.csproj ./Base.Extensions/
COPY Base.Resources/*.csproj ./Base.Resources/

COPY Base.Contracts/Base.Contracts/*.csproj ./Base.Contracts/Base.Contracts/
COPY Base.Contracts/Base.Contracts.BLL/*.csproj ./Base.Contracts/Base.Contracts.BLL/
COPY Base.Contracts/Base.Contracts.DAL/*.csproj ./Base.Contracts/Base.Contracts.DAL/
COPY Base.Contracts/Base.Contracts.Domain/*.csproj ./Base.Contracts/Base.Contracts.Domain/

RUN dotnet restore

COPY WebApp/. ./WebApp/
COPY App.BLL/. ./App.BLL/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY App.Contracts.BLL/. ./App.Contracts.BLL/
COPY App.Contracts.DAL/. ./App.Contracts.DAL/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY App.Domain/. ./App.Domain/
COPY App.Domain.Enums/. ./App.Domain.Enums/
COPY App.Public.DTO/. ./App.Public.DTO/

COPY Base.BLL/. ./Base.BLL/
COPY Base.DAL/. ./Base.DAL/
COPY Base.DAL.EF/. ./Base.DAL.EF/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Extensions/. ./Base.Extensions/
COPY Base.Resources/. ./Base.Resources/

COPY Base.Contracts/Base.Contracts/. ./Base.Contracts/Base.Contracts/
COPY Base.Contracts/Base.Contracts.BLL/. ./Base.Contracts/Base.Contracts.BLL/
COPY Base.Contracts/Base.Contracts.DAL/. ./Base.Contracts/Base.Contracts.DAL/
COPY Base.Contracts/Base.Contracts.Domain/. ./Base.Contracts/Base.Contracts.Domain/

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]