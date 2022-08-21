# Developer Guide

---

### Configure docker machine

```shell
podman machine stop
podman machine rm
podman machine init --cpus 8 --memory 8096 --disk-size 50
podman machine start
```

### Start Local MSSQL Database

```shell
docker pull mcr.microsoft.com/mssql/server:2019-latest
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=AdmIn@123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

### Configure Migration

```shell
dotnet tool install --global dotnet-ef
cd Mango.Services.ProductAPI
dotnet ef dbcontext info 
dotnet ef migrations add AddProductModelToDb
dotnet ef database update
```

### Configure Duende Identity Server

https://docs.duendesoftware.com/identityserver/v6/quickstarts/0_overview/

```shell
dotnet new --install Duende.IdentityServer.Templates
cd
```