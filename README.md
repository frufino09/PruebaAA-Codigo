# PruebaAA<Código>
Adición y Eliminación masiva grandes volúmenes de datos .Net Core

  - Se lee un fichero CSV alojado en una URL de Azure, para la lectura se utiliza la clase HttpWebRequest perteneciente al dominio System.Net dado que el fichero se encuentra público, en caso de haber tenido el fichero con seguridad bajo algún tipo de autenticación se deberia utilizar la bliblioteca Azure.Storage.Blobs con su respectiva autenticación definiendo la ruta de almacenamiento del fichero.
  - Un vez obtenido los datos se transforman en un listado de objetos de tipo Inventory que posteriormente se almacenan en una base de datos SQL Server. Antes de guardar la data obtenida se eliminan todos los registros existentes en la base de datos de alguna posible previa importación .
  - El objetivo de este trabajo es lograr realizar este proceso con grandes volúmenes de datos de la manera más eficiente posible y de la misma forma controlar los recusos utilizados.
  
# Antecedentes
 - Entity Framework (EF) puede ser muy lento en las operaciones de inserción / actualización / eliminación en masa. Incluso los ajustes a menudo sugeridos para desactivar AutoDetectChanges y / o ValidateOnSaveEnabled no siempre ayudan.
  -  Cuando se desea realizar trabajos con grandes volúmenes de datos para operaciones de transformación, inserción, actualización o eliminación masiva utilizando EF, se vuelve un proceso bastante complejo en términos de demora computacional, esto sucede porque por cada llamada del SaveChanges()  Entity Framework realiza un viaje de ida y vuelta a la base de datos por cada elemento, con el objetivo de revisar en cada paso la integridad de la estructura de la base de datos. 
  - Ante este problema los desarrolladores en muchas ocasiones realizamos particiones en bloques de los datos para no cargar tanto la tarea de EF en su proceso de almacenamiento, logrando cierta mejora en el rendimiento del proceso, pero aún así es muy lento. Este proceso de particionar los datos también aporta una disminución de la memoria utilizada, de esta forma se lograria un balance entre rendimiento y capacidad de memoria cuando sea necesario.
 
# Propuesta de Solución
 - Existen varias bibliotecas que mejoran drásticamente el rendimiento de EF mediante el uso de operaciones masivas y por lotes, logrando realizar tareas que superan la velocidad de EF hasta 50 veces más rápido. 
 - Para esta solución se decide utilizar la extención [Entity Framework Extensions](https://entityframework-extensions.net/bulk-savechanges). Instalada mediante su paquete nuget [Z.EntityFramework.Extensions](https://www.nuget.org/packages/Z.EntityFramework.Extensions/4.0.106)
 - Existen otras alternativas para este propósito como [EntityFramework.Utilities](https://github.com/MikaelEliasson/EntityFramework.Utilities) pero no son del todo seguras y por lo general aún tienen temas pendientes en desarrollo.

# Ejecutar Proyecto
 - Una vez descargado el proyecto debe montar la base de datos a través del script que se encuentra dentro de la carpeta AppData. 
 - Actualizar la cadena de conexión que está dentro del fichero Models/DBContext en el método OnConfiguring. (Para proyectos reales la cadena de conexión debe estar dentro de un fichero de configuración)

# Conclusiones
 - Luego de realizar las pruebas comparativas de rendimiento entre Entity Framework Core Extensions vs Entity Framework Core (3.1.9) para un volumen de datos de 17,175,295 de registros, los resultados fueron los siguientes:
 
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
