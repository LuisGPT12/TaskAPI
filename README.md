# 📝 TaskAPI

> API REST construida con **ASP.NET Core (.NET 8.0)** para la gestión de tareas (*Tasks*) e ítems (*Items*).

---

## 📋 Requisitos previos

| Herramienta | Versión mínima | Descarga |
|---|---|---|
| .NET SDK | 8.0 | [dotnet.microsoft.com](https://dotnet.microsoft.com) |
| SQL Server / SQL Express | Cualquiera | [microsoft.com/sql-server](https://www.microsoft.com/sql-server) |
| IDE *(opcional)* | — | Visual Studio 2022+ · VS Code |

---

## ⚙️ Configuración

### 1. Cadena de conexión

la cadena de conexion se encuentra en el archivo appsettings.json
estara definida como : Server=.\SQLEXPRESS;Database=TaskDb;Trusted_Connection=True;TrustServerCertificate=True;

> **⚠️ Nota:*si necesitas cambiarla puedes hacerlo desde ahi(appsettings.json)* .

### 2. Base de datos

Ejecuta el script SQL incluido en el repositorio para crear la base de datos `TaskDb` y sus tablas:

```sql
-- Ubicación: TaskAPI/TaskDb_Script.sql
```

---

## 🚀 Ejecución

### Opción A — Visual Studio

1. Abre la solución (`.sln`) en Visual Studio.
2. Establece **TaskAPI** como proyecto de inicio.
3. Presiona `F5` (con depuración) o `Ctrl + F5` (sin depuración).
4. Swagger se abrirá automáticamente en `https://localhost:{puerto}/swagger`.

### Opción B — CLI / PowerShell

Ejecuta los siguientes comandos desde la **raíz del repositorio**:

```
# 1. Restaurar dependencias
dotnet restore

# 2. Compilar
dotnet build

# 3. Ejecutar
dotnet run --project TaskAPI
```

La consola mostrará la URL asignada, por ejemplo:

```
Now listening on: https://localhost:7042
```

---

## 📌 Endpoints

**URL base por defecto:** `https://localhost:7042`  
*(el puerto puede variar; usa el que muestre la consola al ejecutar)*

### Tasks

| Método | Ruta | Descripción |
|--------|------|-------------|
| `GET` | `/api/tasks` | Obtener todas las tareas |
| `GET` | `/api/tasks/{id}` | Obtener una tarea por ID |
| `POST` | `/api/tasks` | Crear una nueva tarea |
| `PUT` | `/api/tasks/{id}` | Actualizar una tarea existente |
| `DELETE` | `/api/tasks/{id}` | Eliminar una tarea |
| `POST` | `/api/tasks/{taskId}/items` | Agregar un ítem a una tarea |

### Items

| Método | Ruta | Descripción |
|--------|------|-------------|
| `PUT` | `/api/items/{id}` | Actualizar un ítem |
| `DELETE` | `/api/items/{id}` | Eliminar un ítem |

> **💡 Swagger UI** — Disponible en `/swagger` para explorar y probar todos los endpoints de forma interactiva.

---

## 🧪 Pruebas con Postman

Importa la colección de Postman incluida para tener todos los endpoints preconfigurados y listos para usar.

> 📎 **https://documenter.getpostman.com/view/54272267/2sBXwqrAcT**

---

## 🗂️ Estructura del proyecto

```
TaskAPI/
├── Context/
│   └── TaskDbContext.cs        # Configuración de EF Core y contexto de la Db
├── Controllers/
│   ├── TasksController.cs      # Endpoints de tareas
│   └── ItemsController.cs      # Endpoints de ítems
├── Models/
│   ├── Task.cs
│   └── Item.cs
├── scripts/
│   └── TaskDb_Script.sql       # Script de creación de base de datos
├── appsettings.json
└── Program.cs
```

---

## 🛠️ Tecnologías utilizadas

- [ASP.NET Core 8.0](https://learn.microsoft.com/aspnet/core)
- [Entity Framework Core](https://learn.microsoft.com/ef/core)
- [SQL Server / SQL Express](https://www.microsoft.com/sql-server)
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)