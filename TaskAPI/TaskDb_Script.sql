-- =============================================
-- Script SQL Server - TaskDb
-- Modelo de Datos: Task & Items
-- =============================================

-- Crear base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TaskDb')
BEGIN
    CREATE DATABASE TaskDb;
END
GO

USE TaskDb;
GO

-- =============================================
-- Crear tabla Tasks
-- =============================================
IF OBJECT_ID('dbo.Tasks', 'U') IS NOT NULL
    DROP TABLE dbo.Tasks;
GO

CREATE TABLE dbo.Tasks (
    Id          INT             NOT NULL IDENTITY(1,1),
    Title       NVARCHAR(100)   NOT NULL,
    Description NVARCHAR(500)   NULL,
    CreatedAt   DATETIME        NOT NULL CONSTRAINT DF_Tasks_CreatedAt DEFAULT GETDATE(),
    IsCompleted BIT             NOT NULL CONSTRAINT DF_Tasks_IsCompleted DEFAULT 0,

    CONSTRAINT PK_Tasks PRIMARY KEY (Id)
);
GO

-- =============================================
-- Crear tabla Items
-- =============================================
IF OBJECT_ID('dbo.Items', 'U') IS NOT NULL
    DROP TABLE dbo.Items;
GO

CREATE TABLE dbo.Items (
    Id      INT             NOT NULL IDENTITY(1,1),
    TaskId  INT             NOT NULL,
    Name    NVARCHAR(100)   NOT NULL,
    IsDone  BIT             NOT NULL CONSTRAINT DF_Items_IsDone DEFAULT 0,

    CONSTRAINT PK_Items  PRIMARY KEY (Id),
    CONSTRAINT FK_Items_Tasks FOREIGN KEY (TaskId)
        REFERENCES dbo.Tasks (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

-- =============================================
-- Insertar registros de prueba - Tasks
-- =============================================
INSERT INTO dbo.Tasks (Title, Description, CreatedAt, IsCompleted)
VALUES
    (
        'Configurar entorno de desarrollo',
        'Instalar y configurar todas las herramientas necesarias para el proyecto.',
        GETDATE(),
        0
    ),
    (
        'Diseñar base de datos',
        'Crear el modelo relacional y los scripts de creación de tablas.',
        DATEADD(DAY, -2, GETDATE()),
        1
    ),
    (
        'Implementar API REST',
        'Desarrollar los endpoints CRUD para Tasks e Items.',
        DATEADD(DAY, -1, GETDATE()),
        0
    );
GO

-- =============================================
-- Insertar registros de prueba - Items
-- =============================================
INSERT INTO dbo.Items (TaskId, Name, IsDone)
VALUES
    -- Items de Task 1: Configurar entorno
    (1, 'Instalar Visual Studio 2022',        1),
    (1, 'Instalar SQL Server 2022',           1),
    (1, 'Instalar .NET 8 SDK',               0),
    (1, 'Configurar variables de entorno',    0),

    -- Items de Task 2: Diseñar base de datos
    (2, 'Definir modelo entidad-relación',    1),
    (2, 'Crear script de tablas',             1),
    (2, 'Insertar datos de prueba',           1),

    -- Items de Task 3: Implementar API REST
    (3, 'Crear proyecto Web API',             0),
    (3, 'Implementar endpoint GET /tasks',    0),
    (3, 'Implementar endpoint POST /tasks',   0),
    (3, 'Implementar endpoint DELETE /tasks', 0);
GO

-- =============================================
-- Verificar datos insertados
-- =============================================
SELECT
    t.Id          AS TaskId,
    t.Title,
    t.Description,
    t.CreatedAt,
    t.IsCompleted,
    i.Id          AS ItemId,
    i.Name        AS ItemName,
    i.IsDone
FROM dbo.Tasks  t
LEFT JOIN dbo.Items i ON i.TaskId = t.Id
ORDER BY t.Id, i.Id;
GO
