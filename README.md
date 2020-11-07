# PruebaAA<Código>
Adición y Eliminación masiva grandes volúmenes de datos .Net Core

  - Se lee un fichero CSV alojado en una URL de Azure, se tranforma en un listado de Objetos que posteriormente se almacenan en una base de datos SQL Server. Antes de guardar la data obtenida se eliminan todos los registros existentes en la base de datos de alguna importacion previa.
  - El objetivo de este trabajo es lograr realizar este proceso con grandes volumenes de datos de la manera mas eficiente posible y de la misma forma controlar los recusos utilizados.
  
# Antecendete
 - Entity Framework (EF) puede ser muy lento en las operaciones de inserción / actualización / eliminación en masa. Incluso los ajustes a menudo sugeridos para desactivar AutoDetectChanges y / o ValidateOnSaveEnabled no siempre ayudan.
  -  Cuando se desea realizar trabajos con grandes volumenes de datos para operaciones de transformacion de los datos, almacenamiento o eliminacion masiva de los mismos, utilizando EF, se vuelve un proceso bastante complejo en terminos de demora computacinal, debido a que por cada llamada del SaveChanges()  Entity Framework realiza un viaje de ida y vuelta a la base de datos por cada elemento que va a adicionar, esto con el objetivo de revisar en cada paso la integridad de la estructura de la base de datos. 
  - Ante este problema los desarrolladores en muchas ocaciones realizamos particiones en bloques de los datos para no cargar tanto la tarea de EF en su proceso de almacenamiento, logrando cierta mejora en el rendimiento del proceso, pero aun asi es demaciado lento. Este proceso de particionar los datos tambien aporta una disminución de la memoria utilizada, de esta forma se lograria un balance entre rendimiento y capacidad de memoria cuando sea necesario.
 
# Propuesta de Solución
 - Existen varias biblioteca que mejora drásticamente el rendimiento de EF mediante el uso de operaciones masivas y por lotes, logrando realizar tareas en velocidades superir hasta 50 veces mas rapido. Para esta solución se deside utilizar la extencion [Entity Framework Extensions](https://entityframework-extensions.net/bulk-savechanges). Instalada mediante su paquete nuget [Z.EntityFramework.Extensions](https://www.nuget.org/packages/Z.EntityFramework.Extensions/4.0.106)
 - Existen otras alternativas para este proposito como [EntityFramework.Utilities](https://github.com/MikaelEliasson/EntityFramework.Utilities) pero no son del todo seguras y por lo general aun tienen temas pendientes en desarrollo.

# Ejecutar Proyecto
 - Una ves descargado el proyecto debe montar la base de datos a travez del script que se encuentra dentro de la carperta AppData. 
 - Actualizar la cadena de conexion que esta dentro del fichero Models/DBContext en el metodo OnConfiguring. (Para proyectos reales el lugar de la cadena de conexion debe ser dentro de un fichero de configuración json)

# Conclusiones
 - Luego de realizar las pruebas para un volumen de datos de 17,175,295 los resultados fueron los siguientes:
 ```sh
Eliminar  7m 54s 874ms
Insertar  4m 23s 770ms
```
 - Ambiente de prueba CPU: core-i7 2.60GHz RAM: 16GB
