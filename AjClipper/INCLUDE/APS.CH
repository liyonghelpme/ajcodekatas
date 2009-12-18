//.........................................................................
//
//   Program Name: APS.CH            Copyright: APSystems SRL                                            
//   Date Created: 01/28/93           Language: Clipper 5.0                                         
//   Time Created: 22:47:13             Author: APSystems                                 
//   /brief/library.src
//.........................................................................
// parametros especiales para tbrowse (APbrowse)
//
#include "rddsys.ch"
#include "windows.ch"

#define MAX_PARAM      20   // cantidad maxima de parametros

#define REGLA_SN        1   // pone regla si o no
#define REGLA_TEX       2   // texto para columna regla
#define REGLA_COL       3   // columna hasta la cual congela regla
#define REGLA_BLK       4   // bloque o funcion a ejecutar para regla
#define REGLA_INSCOL    5   // lugar donde se desea insertar la regla
#define ALTA_SN         6   // si permite o no altas
#define BAJA_SN         7   // si permite o no bajas
#define TIPO_BOX        8   // tipo de box, mirar \clipper5\include\box.ch
#define ARRIBA_COL      9   // divisor entre cabezera y columnas
#define ENTRE_COL      10   // divisor entre columnas
#define REG_COMP       11   // function de registro completo
#define GRAB_EXT       12   // function de grabacion externa
#define TECLA_EXT      13   // fucntion de activacion de teclas externas
#define DB_TITULO      14   // titulo del dbedit margen superior izq.
#define DB_PIE         15   // titulo pie del dbedit margen inferior izq.
#define DEL_EXT        16   // funcion de borrado externo
#define MODIF_SN       17   // si permite o no modificar
#define DEL_PAS        18   // funcion despues de borrado
#define PRUEBA_COLOR   19   // prueba cambio de color
#define INI_FUNC       20   // funcion de inicializacion

// -------------------------------------------------------------------------
// definiciones de diferentes sonidos para funcion sonidos en func.prg
#define S_ERROR         1   // sonido de error alarma tipo f_error
#define S_ALARMA        2   // 
#define S_OJO           3   //

// -------------------------------------------------------------------------
// definiciones para archivos sistema de diccionario de datos
#define DB_NOMBRE       1   // nombre del archivo 
#define DB_DIRECTORIO   2   // directorio 
#define DB_ALIAS        3   // alias de busqueda
#define DB_RECNO        4   // recno
// -------------------------------------------------------------------------
// 
