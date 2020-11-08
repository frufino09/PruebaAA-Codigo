# PruebaAA<Código>
Adición y Eliminación masiva de grandes volúmenes de datos ASP.Net Core 3.1

# Problemática
  - Se tiene un fichero .CSV alojado en una URL de Azure con 17,175,295 lineas de texto.
  - Un vez obtenido los datos, se deben transformar en un listado de objetos que posteriormente se almacenarán en una base de datos SQL Server.
  - Antes de guardar los datos obtenidos se eliminarán todos los registros existentes en la base de datos de alguna posible previa importación.
  - Este proceso se debe realizar con el mejor rendimiento posible y el minimo consumo de recursos.
  
 # Objetivo
  - El objetivo de este desarrollo es lograr dar solución al problema planteado de la manera más eficiente posible y de la misma forma controlar los recusos utilizados.
  
# Antecedentes
 - Cualquiera pensaria en tratar de utilizar Entity Framework Core para dar solución al problema e intentar manejarlo de alguna forma pero; Entity Framework tiene fama de ser muy lento al guardar múltiples entidades. El problema de rendimiento se debe principalmente al método DetectChanges y al número de viajes de ida y vuelta de la base de datos. Por ejemplo, para SQL Server, para cada entidad que guarde, se debe realizar un viaje de ida y vuelta a la base de datos. Por lo tanto, si necesita insertar 100 entidades, se realizarán 100 viajes de ida y vuelta a la base de datos, lo que hace que el proceso sea muy lento. 
  - Ante este problema los desarrolladores en muchas ocasiones realizamos particiones en bloques de los datos para no cargar tanto la tarea de EF en su proceso de almacenamiento, logrando cierta mejora en el rendimiento del proceso, pero aún así es muy lento. Este proceso de particionar los datos también aporta una disminución de la memoria utilizada, de esta forma se lograria un balance entre rendimiento y la cantidad de memoria utilizada.
 
# Propuesta de Solución
 - Para la lectura del fichero se utilizará la clase HttpWebRequest perteneciente al dominio System.Net dado que el fichero se encuentra público, en caso de haber tenido el fichero con seguridad bajo algún tipo de autenticación se deberia utilizar la bliblioteca Azure.Storage.Blobs con su respectiva autenticación definiendo la ruta de almacenamiento del fichero.
 - Para manejar el tema de la eliminación y adición de los datos existen varias bibliotecas que mejoran drásticamente el rendimiento de EF mediante el uso de operaciones masivas y por lotes, logrando realizar tareas que superan la velocidad de EF hasta 50 veces más rápido. 
 - Para esta solución se decide utilizar la extención [Entity Framework Extensions](https://entityframework-extensions.net/bulk-savechanges). Instalada mediante su paquete nuget [Z.EntityFramework.Extensions](https://www.nuget.org/packages/Z.EntityFramework.Extensions/4.0.106)
 - Existen otras alternativas para este propósito como [EntityFramework.Utilities](https://github.com/MikaelEliasson/EntityFramework.Utilities) pero no todas son seguras o por lo general aún tienen temas pendientes en desarrollo.

# Ejecutar Proyecto
 - Una vez descargado el proyecto debe montar la base de datos a través del script que se encuentra dentro de la carpeta AppData. 
 - Actualizar la cadena de conexión que está dentro del fichero Models/DBContext en el método OnConfiguring. (Para proyectos reales la cadena de conexión debe estar dentro de un fichero de configuración)

# Conclusiones
 - Luego de realizar las pruebas comparativas de rendimiento entre Entity Framework Extensions vs Entity Framework Core (3.1.9) para un volumen de datos de 17,175,295 de registros, los resultados fueron los siguientes:
 
 ```sh
Entity Framework Extensions
Eliminar 0h 06m 49s 417ms
Insertar 0h 03m 54s 920ms

Entity Framework Core (3.1.9)
Eliminar 1h 37m 47s 387ms
Insertar 
```
# Ambiente de prueba 
 ```sh
 CPU: core-i7 2.60GHz 
 RAM: 16GB
```
