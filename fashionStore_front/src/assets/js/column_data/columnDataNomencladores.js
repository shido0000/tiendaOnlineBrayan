// #region NOMENCLADORES COLUMN DATA
/**************************************************************************************************
 *                                     NOMENCLADORES COLUMN DATA                                        *
 **************************************************************************************************/

// #region CATEGORIA DE PRODUCTO
/*********************************************************************
 *                CATEGORIA DE PRODUCTO COLUMN DATA                     *
 *********************************************************************/

export const dataColumnCategoriaProducto = Object.freeze([
    {
        name: 'nombre',
        align: 'center',
        label: 'Código',
        field: 'nombre',
        sortable: true
    },
    {
        name: "descripcion",
        align: "center",
        label: "Descripción",
        field: "descripcion",
        sortable: true,
    },

    /*{
        name: 'activo',
        align: 'center',
        label: 'Activo',
        field: 'activo',
        sortable: true
    },*/
    {
        name: "action",
        align: "center",
        label: "Acciones",
        field: "action",
        sortable: true,
    },
]);

export const dataColumnMoneda = Object.freeze([
    {
        name: 'codigo',
        align: 'center',
        label: 'Código',
        field: 'codigo',
        sortable: true
    },
    {
        name: "descripcion",
        align: "center",
        label: "Descripción",
        field: "descripcion",
        sortable: true,
    },
    {
        name: "tasaCambio",
        align: "center",
        label: "Tasa de Cambio",
        field: "tasaCambio",
        sortable: true,
    },
    {
        name: 'esActiva',
        align: 'center',
        label: 'Activo',
        field: 'esActiva',
        sortable: true
    },
    {
        name: "action",
        align: "center",
        label: "Acciones",
        field: "action",
        sortable: true,
    },
]);


export const dataColumnProducto = Object.freeze([
    {
        name: 'codigo',
        align: 'center',
        label: 'Código',
        field: 'codigo',
        sortable: true
    },
    {
        name: 'sku',
        align: 'center',
        label: 'SKU',
        field: 'sku',
        sortable: true
    },
    {
        name: "descripcion",
        align: "center",
        label: "Descripción",
        field: "descripcion",
        sortable: true,
    },
    {
        name: "precioCosto",
        align: "center",
        label: "Precio de Costo",
        field: "precioCosto",
        sortable: true,
    },
    {
        name: "precioVenta",
        align: "center",
        label: "Precio de Venta",
        field: "precioVenta",
        sortable: true,
    },
    {
        name: "monedaCodigo",
        align: "center",
        label: "Moneda",
        field: "monedaCodigo",
        sortable: true,
    },
    {
        name: "color",
        align: "center",
        label: "Color",
        field: "color",
        sortable: true,
    },
    {
        name: 'esActivo',
        align: 'center',
        label: 'Activo',
        field: 'esActivo',
        sortable: true
    },
    {
        name: "action",
        align: "center",
        label: "Acciones",
        field: "action",
        sortable: true,
    },
]);


export const dataColumnDescuento = Object.freeze([
    {
        name: 'nombre',
        align: 'center',
        label: 'Nombre',
        field: 'nombre',
        sortable: true
    },
    {
        name: 'porcentaje',
        align: 'center',
        label: 'Porcentaje',
        field: 'porcentaje',
        sortable: true
    },
    {
        name: "montoFijo",
        align: "center",
        label: "Monto Fijo",
        field: "montoFijo",
        sortable: true,
    },
    {
        name: "fechaInicio",
        align: "center",
        label: "Fecha de Inicio",
        field: "fechaInicio",
        sortable: true,
    },
    {
        name: "fechaFin",
        align: "center",
        label: "Fecha de Fin",
        field: "fechaFin",
        sortable: true,
    },
    /* {
         name: "productoCodigo",
         align: "center",
         label: "Producto asociado",
         field: "productoCodigo",
         sortable: true,
     },*/
    {
        name: 'esActivo',
        align: 'center',
        label: 'Activo',
        field: 'esActivo',
        sortable: true
    },
    {
        name: "action",
        align: "center",
        label: "Acciones",
        field: "action",
        sortable: true,
    },
]);


export const dataColumnPedido = Object.freeze([
    {
        name: 'usuario',
        align: 'center',
        label: 'Cliente',
        field: 'usuario',
        sortable: true
    },
    {
        name: 'estado',
        align: 'center',
        label: 'Estado',
        field: 'estado',
        sortable: true
    },
    {
        name: "total",
        align: "center",
        label: "Total",
        field: "total",
        sortable: true,
    },
    {
        name: "monedaCodigo",
        align: "center",
        label: "Moneda",
        field: "monedaCodigo",
        sortable: true,
    },
    {
        name: "cuponCodigo",
        align: "center",
        label: "Cupon",
        field: "cuponCodigo",
        sortable: true,
    },

    {
        name: "action",
        align: "center",
        label: "Acciones",
        field: "action",
        sortable: true,
    },
]);


export const dataColumnCupon = Object.freeze([
    {
        name: 'codigo',
        align: 'center',
        label: 'Codigo',
        field: 'codigo',
        sortable: true
    },
    {
        name: 'descripcion',
        align: 'center',
        label: 'Descripción',
        field: 'descripcion',
        sortable: true
    },
    {
        name: 'porcentaje',
        align: 'center',
        label: 'Porcentaje',
        field: 'porcentaje',
        sortable: true
    },
    {
        name: "montoFijo",
        align: "center",
        label: "Monto Fijo",
        field: "montoFijo",
        sortable: true,
    },
    {
        name: "fechaInicio",
        align: "center",
        label: "Fecha de Inicio",
        field: "fechaInicio",
        sortable: true,
    },
    {
        name: "fechaFin",
        align: "center",
        label: "Fecha de Fin",
        field: "fechaFin",
        sortable: true,
    },
    {
        name: "maximoUsos",
        align: "center",
        label: "Máximo de Usos",
        field: "maximoUsos",
        sortable: true,
    },
    {
        name: "usosActuales",
        align: "center",
        label: "Usos Actuales",
        field: "usosActuales",
        sortable: true,
    },
    {
        name: "montoMinimoPedido",
        align: "center",
        label: "Monto Mínimo de Pedido",
        field: "montoMinimoPedido",
        sortable: true,
    },
    {
        name: 'esActivo',
        align: 'center',
        label: 'Activo',
        field: 'esActivo',
        sortable: true
    },
    {
        name: "action",
        align: "center",
        label: "Acciones",
        field: "action",
        sortable: true,
    },
]);

export const dataColumnInventario = Object.freeze([
    {
        name: 'productoCodigo',
        align: 'center',
        label: 'Producto Código',
        field: 'productoCodigo',
        sortable: true
    },
    {
        name: "productoDescripcion",
        align: "center",
        label: "Producto Descripción",
        field: "productoDescripcion",
        sortable: true,
    },
    {
        name: 'productoSku',
        align: 'center',
        label: 'SKU',
        field: 'productoSku',
        sortable: true
    },
    {
        name: "cantidadDisponible",
        align: "center",
        label: "Cantidad Disponible",
        field: "cantidadDisponible",
        sortable: true,
    },
    {
        name: "cantidadReservada",
        align: "center",
        label: "Cantidad Reservada",
        field: "cantidadReservada",
        sortable: true,
    },
    /*{
        name: 'activo',
        align: 'center',
        label: 'Activo',
        field: 'activo',
        sortable: true
    },*/
    {
        name: "action",
        align: "center",
        label: "Acciones",
        field: "action",
        sortable: true,
    },
]);


export const dataColumnUsuario = Object.freeze([
    {
        name: 'nombreCompleto',
        align: 'center',
        label: 'Nombre Completo',
        field: 'nombreCompleto',
        sortable: true
    },
    {
        name: "username",
        align: "center",
        label: "Username",
        field: "username",
        sortable: true,
    },

    {
        name: 'esActivo',
        align: 'center',
        label: 'Activo',
        field: 'esActivo',
        sortable: true
    },
    {
        name: 'rol',
        align: 'center',
        label: 'Rol',
        field: 'rol',
        sortable: true
    },

    {
        name: "action",
        align: "center",
        label: "Acciones",
        field: "action",
        sortable: true,
    },
]);
