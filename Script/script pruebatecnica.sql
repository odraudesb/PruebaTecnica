
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PruebaTecnicaDB')
BEGIN
    CREATE DATABASE PruebaTecnicaDB;
END
GO
USE PruebaTecnicaDB;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Personas' AND xtype='U')
BEGIN
    CREATE TABLE Personas (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nombres NVARCHAR(100) NOT NULL,
        Apellidos NVARCHAR(100) NOT NULL,
        NumeroIdentificacion NVARCHAR(50) UNIQUE NOT NULL,
        Email NVARCHAR(150) UNIQUE NOT NULL,
        TipoIdentificacion NVARCHAR(10) NOT NULL,
        FechaCreacion DATETIME DEFAULT GETDATE(),
        NumeroIdentificacionCompleto AS (TipoIdentificacion + '-' + NumeroIdentificacion) PERSISTED,
        NombreCompleto AS (Nombres + ' ' + Apellidos) PERSISTED
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuarios' AND xtype='U')
BEGIN
    CREATE TABLE Usuarios (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UsuarioNombre NVARCHAR(50) UNIQUE NOT NULL,
        Pass NVARCHAR(255) NOT NULL, -- Guardaremos el hash de la contraseña
        FechaCreacion DATETIME DEFAULT GETDATE()
    );
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ObtenerPersonas')
BEGIN
    DROP PROCEDURE sp_ObtenerPersonas;
END
GO

CREATE PROCEDURE sp_ObtenerPersonas
AS
BEGIN
    SELECT Id, Nombres, Apellidos, NumeroIdentificacion, Email, TipoIdentificacion, FechaCreacion, 
           NumeroIdentificacionCompleto, NombreCompleto
    FROM Personas;
END;
GO
