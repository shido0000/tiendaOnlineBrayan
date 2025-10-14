const dicc_notify = Object.freeze({

    // **************************************************** ERRORES ********************************************************************

    //                                ************ LOAD DATA EDIT *************
    Error_Notify_Edit: 'Tuvimos problemas al cargar los datos para Editar',
    Error_Notify_EditElement: 'Ocurrio un error al editar el elemento',

    Error_Notify_LoadDataElement: 'Tuvimos problemas al cargar los datos del elemento',
    Error_Notify_LoadData: 'Ocurrio un error al cargar los datos',

    //                                ************ STATUS 500 *************
    Error_Notify_ErrorServer: 'Ocurrio un error en el servidor',

    //                                ************ EXTRAS  *************
    Error_Notify_ErrorConexionServer: 'Error al conectarse al servidor',
    Error_Notify_ErrorConexionServerAcc: 'Error al conectarse al servidor ZUNacc',
    Error_Notify_ErrorSesionClose: 'Estamos teniendo problemas para cerrar la sesión',
    Error_Notify_NotFound404: 'No hay elementos para mostrar',
    Error_Notify_ErrorImprimir: 'Error al imprimir',
    Error_Notify_FechaSolapada: 'Las fechas se solapan con un rango existente.',


    //                                ************ NOT FOUND BY IDs  *************


    //                                ************  NOT FOUND   *************


    //                                ************ DELETE *************
    Error_Notify_DelecteObject: 'Ocurrio un error al eliminar',

    //                                ************ DELETE RELATION *************
    Error_Notify_DeletingElementIsRelated: 'Este registro no puede ser eliminado porque existen relaciones con él.',


    //                             ************ CREATE *************
    Error_Notify_CreateElement: 'Ocurrio un error al crear el elemento',



    //                             ************ USER *************


    // **************************************************** SUCCESS ********************************************************************

    //                                ************ DELETE *************
    Success_Notify_ConectionSuccess: 'Conexión exitosa',

    //                                ************ DELETE *************


    //                                ************ EDIT *************


    //                                ************ CREATE *************
    Success_Notify_CreateElement: 'El elemento ha sido creado correctamente',


    //                                ************ USER *************
    Success_Notify_ChangesPassword: 'Contraseña actualizada exitosamente.',

})

export const {

    // *** ERROR ***
    Error_Notify_Edit,
    Error_Notify_EditElement,
    Error_Notify_ErrorServer,
    Error_Notify_ErrorSesionClose,
    Error_Notify_DeletingElementIsRelated,
    Error_Notify_ErrorConexionServerAcc,
    Error_Notify_ErrorConexionServer,
    Error_Notify_NotFound404,
    Error_Notify_DelecteObject,
    Error_Notify_ErrorImprimir,
    Error_Notify_FechaSolapada,
    Error_Notify_LoadDataElement,
    Error_Notify_LoadData,
    Error_Notify_CreateElement,
    // *** SUCCESS ***
    Success_Notify_ConectionSuccess,
    Success_Notify_ChangesPassword,
    Success_Notify_CreateElement,
} = dicc_notify
