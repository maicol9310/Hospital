# Hospital API

## Descripción

Hospital API es un servicio REST desarrollado con **ASP.NET Core 8** siguiendo los principios de **Clean Architecture** y **CQRS** mediante **MediatR**. La API permite gestionar órdenes hospitalarias creando ordenes que luego son llamadas y procesadas cada 40 segundos, cuenta con autenticación mediante JWT, Serilog para los logs, un Worker services background para procesar las ordenes, cuenta con validaciones abstractas de fluent y manejo de excepciones globales con un Middleware implementado en sharedkernel llamado ExceptionMiddleware, consulta de ordenes individuales o por filtros de prioridad o paciente al igual que crea ordenes, se crearon 6 Test con NUnit y un archivo para integración continua totalmente funcional en el proyecto.

## Tecnologías utilizadas

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* MediatR
* JWT Authentication
* Swagger / OpenAPI
* NUnit
* Fluent
* Serilog
* Middleware
* Worker Background

---

# Pasos de ejecución

## 1. Clonar el repositorio

```bash
git clone https://github.com/maicol9310/Hospital.git
cd Hospital
```

## 2. Restaurar las dependencias

```bash
dotnet restore
```

## 3. Configurar la base de datos

Modificar el archivo:

```
src/Hospital.API/appsettings.json
```

configurando la cadena de conexión correspondiente.
Adicional "path": "Logs/hospital-log-.txt" que es donde se guardara el archivo log automaticamente en el proyecto API

## 4. Ejecutar las migraciones

Si existen migraciones pendientes:

```bash
 dotnet ef database update --context HospitalDbContext --project Hospital.Infrastructure --startup-project Hospital.API
```

De lo contrario y lo que quiere hacer desde 0, Borres toda la data de la carpeta Migrations en Persistence, cree la DB manual solo la DB Hospital y ejecute:

```bash
 dotnet ef migrations add User --context HospitalDbContext --project Hospital.Infrastructure --startup-project Hospital.API --output-dir Persistence/Migrations
```

```bash
 dotnet ef database update --context HospitalDbContext --project Hospital.Infrastructure --startup-project Hospital.API
```

## 5. Ejecutar la aplicación

```bash
dotnet run --project src/Hospital.API
```

La API quedará disponible en:

```
https://localhost:5001
```

o

```
http://localhost:5000
```

## 6. Acceder a Swagger

Una vez iniciada la aplicación:

```
https://localhost:7097/swagger/index.html
```

---

# Configuración

## Base de datos

El proyecto utiliza **SQL Server** como motor de base de datos.

Crear previamente una base de datos con el nombre:

```
Hospital
```

o modificar la cadena de conexión para utilizar otra base existente.

---

## Variables de entorno

La aplicación utiliza la configuración estándar de ASP.NET Core.

Las principales configuraciones se encuentran en:

```
appsettings.json
```
Para la configuración de la cadena de conexión, el secret del token y la ruta del log.

Configuraciones relevantes:

* ConnectionStrings
* Jwt
* Logging

---

## JWT

La autenticación se realiza mediante JWT.

Ejemplo de configuración:

```json
"Jwt": {
  "Key": "YourSecretKeyHere",
  "Issuer": "HospitalAPI",
  "Audience": "HospitalAPI",
  "ExpireMinutes": 60
}
```

Una vez autenticado mediante:

```
POST /api/Auth/login
```

el token deberá enviarse en todas las peticiones protegidas Swagger esta configurado para usar tokens:

```
Authorization: Bearer {token}
```
El usuario y la contraseña se debe crear manual en base de datos para poder utilizar el endpoint de Login y generar el token
---

# Cadenas de conexión

Ejemplo de configuración:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=HospitalDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Para SQL Server con autenticación SQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=HospitalDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
}
```

La cadena puede modificarse directamente en:

```
src/Hospital.API/appsettings.json
```

o mediante variables de entorno utilizando:

```
ConnectionStrings__DefaultConnection
```

---

# Ejecución de pruebas

Para ejecutar las pruebas unitarias:

Click derecho sobre el proyecto pruebas y ejecutar pruebas

---

# Arquitectura

La solución está organizada siguiendo Clean Architecture:

```
Hospital.API
Hospital.Application
Hospital.Domain
Hospital.Infrastructure
Hospital.SharedKernel
Hospital.Application.Tests
```

* **Hospital.API**: Endpoints REST, JWT con politicas de roles, autenticación, serilog y configuración.
* **Hospital.Application**: Casos de uso, CQRS, validaciones y autenticación.
* **Hospital.Domain**: Entidades y reglas de negocio.
* **Hospital.Infrastructure**: Persistencia, Entity Framework y el Worker Background.
* **Hospital.SharedKernel**: Middleware de manejo de excepciones globales con ExceptionMiddleware y Validator
* **Test**: Pruebas unitarias.

---

# Documentación

La documentación OpenAPI está disponible mediante Swagger en este repositorio, tambien se puede encontrar cuando se ejecuta el proyecto.
