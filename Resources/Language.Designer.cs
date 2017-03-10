﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AppResources {
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    public class Language {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Language() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager temp = new ResourceManager("AppResources.Language", typeof(Language).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ha ocurrido un error al respaldar la configuración actual del equipo: {0}.
        /// </summary>
        public static string BackupError_Text {
            get {
                return ResourceManager.GetString("BackupError_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Configuración respaldada con exito..
        /// </summary>
        public static string BackupSuccess_Text {
            get {
                return ResourceManager.GetString("BackupSuccess_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Está a punto de eliminar un archivo de configuración, esta acción no se puede deshacer. ¿Desea continuar?.
        /// </summary>
        public static string DeleteConfirmation_Text {
            get {
                return ResourceManager.GetString("DeleteConfirmation_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Los cambios realizados se perderán ¿Desea continuar?.
        /// </summary>
        public static string EditCancel_Confirmation {
            get {
                return ResourceManager.GetString("EditCancel_Confirmation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Datos Inválidos.
        /// </summary>
        public static string Error_Data_Tittle {
            get {
                return ResourceManager.GetString("Error_Data_Tittle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El contenido de la configuración no puede estar vacio..
        /// </summary>
        public static string Error_EmptyContent_Text {
            get {
                return ResourceManager.GetString("Error_EmptyContent_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El nombre de la configuración es requerido..
        /// </summary>
        public static string Error_EmptyName_Text {
            get {
                return ResourceManager.GetString("Error_EmptyName_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La ruta del archivo es requerida..
        /// </summary>
        public static string Error_EmptyPath_Text {
            get {
                return ResourceManager.GetString("Error_EmptyPath_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El nombre contiene caracteres inválidos, el nombre solo puede contener carácteres alfanumericos, guión medio y gión bajo..
        /// </summary>
        public static string Error_InvalidNameFormat_Text {
            get {
                return ResourceManager.GetString("Error_InvalidNameFormat_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El nombre no puede tener mas de {1} caracteres..
        /// </summary>
        public static string Error_NameTooLong_Text {
            get {
                return ResourceManager.GetString("Error_NameTooLong_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La información introducida es incorrecta..
        /// </summary>
        public static string FileImportInvalidData_Text {
            get {
                return ResourceManager.GetString("FileImportInvalidData_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Error.
        /// </summary>
        public static string FileImportInvalidData_Tittle {
            get {
                return ResourceManager.GetString("FileImportInvalidData_Tittle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Acerca de....
        /// </summary>
        public static string Interface_AboutMenuItem {
            get {
                return ResourceManager.GetString("Interface_AboutMenuItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Guardar Configuración Actual.
        /// </summary>
        public static string Interface_BackupCommand {
            get {
                return ResourceManager.GetString("Interface_BackupCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Eliminar.
        /// </summary>
        public static string Interface_DeleteCommand {
            get {
                return ResourceManager.GetString("Interface_DeleteCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Cancelar.
        /// </summary>
        public static string Interface_Edit_CancelBtn {
            get {
                return ResourceManager.GetString("Interface_Edit_CancelBtn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Contenido:.
        /// </summary>
        public static string Interface_Edit_ContentLabel {
            get {
                return ResourceManager.GetString("Interface_Edit_ContentLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Editar.
        /// </summary>
        public static string Interface_Edit_FormTittle {
            get {
                return ResourceManager.GetString("Interface_Edit_FormTittle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Nombre:.
        /// </summary>
        public static string Interface_Edit_NameLabel {
            get {
                return ResourceManager.GetString("Interface_Edit_NameLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Guardar.
        /// </summary>
        public static string Interface_Edit_SaveBtn {
            get {
                return ResourceManager.GetString("Interface_Edit_SaveBtn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Editar....
        /// </summary>
        public static string Interface_EditCommand {
            get {
                return ResourceManager.GetString("Interface_EditCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Editar.
        /// </summary>
        public static string Interface_EditMenuItem {
            get {
                return ResourceManager.GetString("Interface_EditMenuItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Salir.
        /// </summary>
        public static string Interface_ExitMenuItem {
            get {
                return ResourceManager.GetString("Interface_ExitMenuItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Archivo.
        /// </summary>
        public static string Interface_FileMenuItem {
            get {
                return ResourceManager.GetString("Interface_FileMenuItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ayuda.
        /// </summary>
        public static string Interface_HelpMenuItem {
            get {
                return ResourceManager.GetString("Interface_HelpMenuItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Nombre configuración: .
        /// </summary>
        public static string Interface_Import_NameLabel {
            get {
                return ResourceManager.GetString("Interface_Import_NameLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ruta: .
        /// </summary>
        public static string Interface_Import_PathLabel {
            get {
                return ResourceManager.GetString("Interface_Import_PathLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a .:: Importar Archivo ::..
        /// </summary>
        public static string Interface_Import_WindowTittle {
            get {
                return ResourceManager.GetString("Interface_Import_WindowTittle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Importar....
        /// </summary>
        public static string Interface_ImportCommand {
            get {
                return ResourceManager.GetString("Interface_ImportCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Explorar....
        /// </summary>
        public static string Interface_Impot_BrowseCommand {
            get {
                return ResourceManager.GetString("Interface_Impot_BrowseCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Cancelar.
        /// </summary>
        public static string Interface_Impot_CancelCommand {
            get {
                return ResourceManager.GetString("Interface_Impot_CancelCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Aceptar.
        /// </summary>
        public static string Interface_Impot_OkCommand {
            get {
                return ResourceManager.GetString("Interface_Impot_OkCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Nuevo....
        /// </summary>
        public static string Interface_NewCommand {
            get {
                return ResourceManager.GetString("Interface_NewCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Cargar Configuración.
        /// </summary>
        public static string Interface_SetCommand {
            get {
                return ResourceManager.GetString("Interface_SetCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Debe de seleccionar una configuración..
        /// </summary>
        public static string InvalidConfigurationError_Text {
            get {
                return ResourceManager.GetString("InvalidConfigurationError_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Valor inválido.
        /// </summary>
        public static string InvalidConfigurationError_Tittle {
            get {
                return ResourceManager.GetString("InvalidConfigurationError_Tittle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No hay configuraciones para cargar, por favor cree o importe configuraciones de Arhivo Host..
        /// </summary>
        public static string NoConfigurationFoundError_Text {
            get {
                return ResourceManager.GetString("NoConfigurationFoundError_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Sin Resultados.
        /// </summary>
        public static string NoConfigurationFoundError_Tittle {
            get {
                return ResourceManager.GetString("NoConfigurationFoundError_Tittle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ya existe una configuración con ese nombre ¿Desea sobreescribirla?.
        /// </summary>
        public static string OverWriteMessage_Text {
            get {
                return ResourceManager.GetString("OverWriteMessage_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Confirmación.
        /// </summary>
        public static string OverwriteMessage_Tittle {
            get {
                return ResourceManager.GetString("OverwriteMessage_Tittle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Exito.
        /// </summary>
        public static string Success_Tittle {
            get {
                return ResourceManager.GetString("Success_Tittle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Se ha guardado la configuración satisfactoriamente..
        /// </summary>
        public static string SuccessEdit_Text {
            get {
                return ResourceManager.GetString("SuccessEdit_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Se ha importado la configuración satisfactoriamente..
        /// </summary>
        public static string SuccessImport_Text {
            get {
                return ResourceManager.GetString("SuccessImport_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Se ha cargado la configuración {0} en el sistema operativo..
        /// </summary>
        public static string SuccessSetConfiguration_Text {
            get {
                return ResourceManager.GetString("SuccessSetConfiguration_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ocurrio un error inesperado: {0}.
        /// </summary>
        public static string UnexpectedError_Text {
            get {
                return ResourceManager.GetString("UnexpectedError_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Error Inesperado.
        /// </summary>
        public static string UnexpectedError_Tittle {
            get {
                return ResourceManager.GetString("UnexpectedError_Tittle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Advertencia.
        /// </summary>
        public static string Warning_Tittle {
            get {
                return ResourceManager.GetString("Warning_Tittle", resourceCulture);
            }
        }
    }
}
