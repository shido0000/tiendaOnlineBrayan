-- ============================================
-- SCRIPT SQL - MÓDULO DE CONTABILIDAD
-- Base de Datos para SQL Server
-- ============================================

-- Crear tabla de Cuentas Contables
CREATE TABLE [dbo].[CuentasContables] (
    [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
    [Codigo] [nvarchar](50) NOT NULL,
    [Nombre] [nvarchar](255) NOT NULL,
    [EsActivo] [bit] NOT NULL DEFAULT (1),
    [EsDeMovimiento] [bit] NOT NULL DEFAULT (1),
    [CuentaPadreId] [uniqueidentifier] NULL,
    [FechaCreacion] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
    [FechaModificacion] [datetime2](7) NULL,
    [UsuarioCreacion] [nvarchar](256) NULL,
    [UsuarioModificacion] [nvarchar](256) NULL,
    CONSTRAINT [PK_CuentasContables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CuentasContables_CuentasPadre] FOREIGN KEY ([CuentaPadreId])
        REFERENCES [dbo].[CuentasContables] ([Id]),
    CONSTRAINT [UQ_CuentasContables_Codigo] UNIQUE ([Codigo])
);

-- Crear tabla de Asientos Contables
CREATE TABLE [dbo].[AsientosContables] (
    [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
    [Fecha] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
    [Descripcion] [nvarchar](max) NOT NULL,
    [ReferenciaId] [uniqueidentifier] NOT NULL,
    [TipoReferencia] [nvarchar](50) NOT NULL,
    [FechaCreacion] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
    [FechaModificacion] [datetime2](7) NULL,
    [UsuarioCreacion] [nvarchar](256) NULL,
    [UsuarioModificacion] [nvarchar](256) NULL,
    CONSTRAINT [PK_AsientosContables] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Crear tabla de Movimientos Contables
CREATE TABLE [dbo].[MovimientosContables] (
    [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
    [AsientoContableId] [uniqueidentifier] NOT NULL,
    [CuentaContableId] [uniqueidentifier] NOT NULL,
    [Debe] [decimal](18, 2) NOT NULL DEFAULT (0),
    [Haber] [decimal](18, 2) NOT NULL DEFAULT (0),
    [FechaCreacion] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
    [FechaModificacion] [datetime2](7) NULL,
    CONSTRAINT [PK_MovimientosContables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MovimientosContables_AsientoContable] FOREIGN KEY ([AsientoContableId])
        REFERENCES [dbo].[AsientosContables] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovimientosContables_CuentaContable] FOREIGN KEY ([CuentaContableId])
        REFERENCES [dbo].[CuentasContables] ([Id])
);

-- ============================================
-- CREAR ÍNDICES
-- ============================================

-- Índices para CuentasContables
CREATE NONCLUSTERED INDEX [IX_CuentasContables_CuentaPadreId]
    ON [dbo].[CuentasContables] ([CuentaPadreId] ASC);

CREATE NONCLUSTERED INDEX [IX_CuentasContables_EsActivo]
    ON [dbo].[CuentasContables] ([EsActivo] ASC);

CREATE NONCLUSTERED INDEX [IX_CuentasContables_Codigo]
    ON [dbo].[CuentasContables] ([Codigo] ASC);

-- Índices para AsientosContables
CREATE NONCLUSTERED INDEX [IX_AsientosContables_Fecha]
    ON [dbo].[AsientosContables] ([Fecha] ASC);

CREATE NONCLUSTERED INDEX [IX_AsientosContables_TipoReferencia]
    ON [dbo].[AsientosContables] ([TipoReferencia] ASC);

CREATE NONCLUSTERED INDEX [IX_AsientosContables_ReferenciaId]
    ON [dbo].[AsientosContables] ([ReferenciaId] ASC);

-- Índices para MovimientosContables
CREATE NONCLUSTERED INDEX [IX_MovimientosContables_AsientoContableId]
    ON [dbo].[MovimientosContables] ([AsientoContableId] ASC);

CREATE NONCLUSTERED INDEX [IX_MovimientosContables_CuentaContableId]
    ON [dbo].[MovimientosContables] ([CuentaContableId] ASC);

-- ============================================
-- INSERTAR DATOS DE EJEMPLO
-- ============================================

-- Cuentas Padre
DECLARE @ActivosId UNIQUEIDENTIFIER = NEWID();
DECLARE @PasivosId UNIQUEIDENTIFIER = NEWID();
DECLARE @CapitalId UNIQUEIDENTIFIER = NEWID();
DECLARE @IngresosId UNIQUEIDENTIFIER = NEWID();
DECLARE @GastosId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[CuentasContables]
([Id], [Codigo], [Nombre], [EsActivo], [EsDeMovimiento], [CuentaPadreId])
VALUES
(@ActivosId, '1', 'Activos', 1, 0, NULL),
(@PasivosId, '2', 'Pasivos', 1, 0, NULL),
(@CapitalId, '3', 'Capital', 1, 0, NULL),
(@IngresosId, '4', 'Ingresos', 1, 0, NULL),
(@GastosId, '5', 'Gastos', 1, 0, NULL);

-- Subcuentas de Activos
DECLARE @CajaId UNIQUEIDENTIFIER = NEWID();
DECLARE @BancoId UNIQUEIDENTIFIER = NEWID();
DECLARE @InventarioId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[CuentasContables]
([Id], [Codigo], [Nombre], [EsActivo], [EsDeMovimiento], [CuentaPadreId])
VALUES
(@CajaId, '1.1', 'Caja General', 1, 1, @ActivosId),
(@BancoId, '1.2', 'Banco del Caribe', 1, 1, @ActivosId),
(@InventarioId, '1.3', 'Inventario de Productos', 1, 1, @ActivosId);

-- Subcuentas de Pasivos
DECLARE @CuentasPorPagarId UNIQUEIDENTIFIER = NEWID();
INSERT INTO [dbo].[CuentasContables]
([Id], [Codigo], [Nombre], [EsActivo], [EsDeMovimiento], [CuentaPadreId])
VALUES
(@CuentasPorPagarId, '2.1', 'Cuentas por Pagar', 1, 1, @PasivosId);

-- Subcuentas de Ingresos
DECLARE @IngresosVentasId UNIQUEIDENTIFIER = NEWID();
DECLARE @DescuentosId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[CuentasContables]
([Id], [Codigo], [Nombre], [EsActivo], [EsDeMovimiento], [CuentaPadreId])
VALUES
(@IngresosVentasId, '4.1', 'Ingresos por Ventas', 1, 1, @IngresosId),
(@DescuentosId, '4.2', 'Descuentos Concedidos', 1, 1, @IngresosId);

-- Subcuentas de Gastos
DECLARE @GastosFletesId UNIQUEIDENTIFIER = NEWID();
DECLARE @GastosAdministrativosId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[CuentasContables]
([Id], [Codigo], [Nombre], [EsActivo], [EsDeMovimiento], [CuentaPadreId])
VALUES
(@GastosFletesId, '5.1', 'Gastos de Fletes', 1, 1, @GastosId),
(@GastosAdministrativosId, '5.2', 'Gastos Administrativos', 1, 1, @GastosId);

-- ============================================
-- INSERTAR ASIENTOS DE EJEMPLO
-- ============================================

DECLARE @Asiento1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Asiento2Id UNIQUEIDENTIFIER = NEWID();

-- Asiento 1: Venta de productos por 1000
INSERT INTO [dbo].[AsientosContables]
([Id], [Fecha], [Descripcion], [ReferenciaId], [TipoReferencia])
VALUES
(@Asiento1Id, '2025-01-15 10:30:00', 'Venta de productos - Orden #001',
 '550e8400-e29b-41d4-a716-446655440000', 'Venta');

-- Movimientos del Asiento 1
INSERT INTO [dbo].[MovimientosContables]
([AsientoContableId], [CuentaContableId], [Debe], [Haber])
VALUES
(@Asiento1Id, @CajaId, 1000, 0),
(@Asiento1Id, @IngresosVentasId, 0, 1000);

-- Asiento 2: Pago de gastos por 200
INSERT INTO [dbo].[AsientosContables]
([Id], [Fecha], [Descripcion], [ReferenciaId], [TipoReferencia])
VALUES
(@Asiento2Id, '2025-01-16 14:15:00', 'Pago de gastos administrativos',
 '550e8400-e29b-41d4-a716-446655440001', 'Gasto');

-- Movimientos del Asiento 2
INSERT INTO [dbo].[MovimientosContables]
([AsientoContableId], [CuentaContableId], [Debe], [Haber])
VALUES
(@Asiento2Id, @GastosAdministrativosId, 200, 0),
(@Asiento2Id, @CajaId, 0, 200);

-- ============================================
-- VISTAS PARA REPORTES (OPCIONAL)
-- ============================================

-- Vista de Estado de Cuentas
CREATE VIEW [dbo].[vw_EstadoDeCuentas] AS
SELECT
    c.Id,
    c.Codigo,
    c.Nombre,
    ISNULL(SUM(CASE WHEN m.Debe > 0 THEN m.Debe ELSE 0 END), 0) AS TotalDebe,
    ISNULL(SUM(CASE WHEN m.Haber > 0 THEN m.Haber ELSE 0 END), 0) AS TotalHaber,
    ISNULL(SUM(CASE WHEN m.Debe > 0 THEN m.Debe ELSE 0 END), 0) -
    ISNULL(SUM(CASE WHEN m.Haber > 0 THEN m.Haber ELSE 0 END), 0) AS Saldo
FROM [dbo].[CuentasContables] c
LEFT JOIN [dbo].[MovimientosContables] m ON c.Id = m.CuentaContableId
WHERE c.EsActivo = 1
GROUP BY c.Id, c.Codigo, c.Nombre;

-- Vista de Resumen por Tipo de Referencia
CREATE VIEW [dbo].[vw_ResumenPorTipo] AS
SELECT
    a.TipoReferencia,
    COUNT(DISTINCT a.Id) AS CantidadAsientos,
    ISNULL(SUM(m.Debe), 0) AS TotalDebe,
    ISNULL(SUM(m.Haber), 0) AS TotalHaber,
    ISNULL(SUM(m.Debe), 0) + ISNULL(SUM(m.Haber), 0) AS MontoTotal
FROM [dbo].[AsientosContables] a
LEFT JOIN [dbo].[MovimientosContables] m ON a.Id = m.AsientoContableId
GROUP BY a.TipoReferencia;

-- Vista de Balance de Prueba
CREATE VIEW [dbo].[vw_BalanceDePrueba] AS
SELECT
    c.Codigo,
    c.Nombre,
    ISNULL(SUM(CASE WHEN m.Debe > 0 THEN m.Debe ELSE 0 END), 0) AS Debe,
    ISNULL(SUM(CASE WHEN m.Haber > 0 THEN m.Haber ELSE 0 END), 0) AS Haber
FROM [dbo].[CuentasContables] c
LEFT JOIN [dbo].[MovimientosContables] m ON c.Id = m.CuentaContableId
WHERE c.EsActivo = 1 AND c.EsDeMovimiento = 1
GROUP BY c.Codigo, c.Nombre
ORDER BY c.Codigo;

-- ============================================
-- PROCEDIMIENTOS ALMACENADOS (OPCIONAL)
-- ============================================

-- Procedimiento para obtener asientos con filtros
CREATE PROCEDURE [dbo].[sp_ObtenerAsientos]
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @Descripcion NVARCHAR(MAX) = NULL,
    @TipoReferencia NVARCHAR(50) = NULL,
    @FechaInicio DATETIME2 = NULL,
    @FechaFin DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SkipCount INT = (@PageNumber - 1) * @PageSize;

    SELECT
        a.Id,
        a.Fecha,
        a.Descripcion,
        a.ReferenciaId,
        a.TipoReferencia,
        a.FechaCreacion,
        (SELECT COUNT(*) FROM [dbo].[AsientosContables]
         WHERE (@Descripcion IS NULL OR Descripcion LIKE '%' + @Descripcion + '%')
         AND (@TipoReferencia IS NULL OR TipoReferencia = @TipoReferencia)
         AND (@FechaInicio IS NULL OR Fecha >= @FechaInicio)
         AND (@FechaFin IS NULL OR Fecha <= @FechaFin)) AS TotalCount
    FROM [dbo].[AsientosContables] a
    WHERE (@Descripcion IS NULL OR a.Descripcion LIKE '%' + @Descripcion + '%')
    AND (@TipoReferencia IS NULL OR a.TipoReferencia = @TipoReferencia)
    AND (@FechaInicio IS NULL OR a.Fecha >= @FechaInicio)
    AND (@FechaFin IS NULL OR a.Fecha <= @FechaFin)
    ORDER BY a.Fecha DESC
    OFFSET @SkipCount ROWS FETCH NEXT @PageSize ROWS ONLY;
END;

-- ============================================
-- VERIFICACIÓN FINAL
-- ============================================

-- Verificar que las tablas fueron creadas
SELECT
    OBJECT_NAME(OBJECT_ID) AS NombraTabla,
    COUNT(*) AS CantidadRegistros
FROM [sys].[dm_db_index_usage_stats]
WHERE database_id = DB_ID() AND OBJECT_ID > 0
GROUP BY OBJECT_ID;

-- Verificar estructura de cuentas
SELECT
    c.Codigo,
    c.Nombre,
    c.EsActivo,
    c.EsDeMovimiento,
    CASE WHEN c.CuentaPadreId IS NULL THEN 'Padre' ELSE 'Subcuenta' END AS Tipo
FROM [dbo].[CuentasContables] c
ORDER BY c.Codigo;

-- Verificar asientos con movimientos
SELECT
    a.Fecha,
    a.Descripcion,
    a.TipoReferencia,
    COUNT(m.Id) AS CantidadMovimientos,
    SUM(m.Debe) AS TotalDebe,
    SUM(m.Haber) AS TotalHaber
FROM [dbo].[AsientosContables] a
LEFT JOIN [dbo].[MovimientosContables] m ON a.Id = m.AsientoContableId
GROUP BY a.Id, a.Fecha, a.Descripcion, a.TipoReferencia;

-- ============================================
-- FIN DEL SCRIPT
-- ============================================
