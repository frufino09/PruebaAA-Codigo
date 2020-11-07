# PruebaAA<Código>
Adición y Eliminación masiva grandes volúmenes de datos .Net Core

  - Se lee un fichero CSV alojado en una URL de Azure, se tranforma en un listado de Objetos que posteriormente se almacenan en una base de datos SQL Server. Antes de guardar la data obtenida se eliminan todos los registros existentes en la base de datos de alguna importación previa.
  - El objetivo de este trabajo es lograr realizar este proceso con grandes volumenes de datos de la manera más eficiente posible y de la misma forma controlar los recusos utilizados.
  
# Antecedente
 - Entity Framework (EF) puede ser muy lento en las operaciones de inserción / actualización / eliminación en masa. Incluso los ajustes a menudo sugeridos para desactivar AutoDetectChanges y / o ValidateOnSaveEnabled no siempre ayudan.
  -  Cuando se desea realizar trabajos con grandes volumenes de datos para operaciones de transformacion, inserción, actualización o eliminación masiva utilizando EF, se vuelve un proceso bastante complejo en términos de demora computacional, esto sucede porque por cada llamada del SaveChanges()  Entity Framework realiza un viaje de ida y vuelta a la base de datos por cada elemento, con el objetivo de revisar en cada paso la integridad de la estructura de la base de datos. 
  - Ante este problema los desarrolladores en muchas ocaciones realizamos particiones en bloques de los datos para no cargar tanto la tarea de EF en su proceso de almacenamiento, logrando cierta mejora en el rendimiento del proceso, pero aun asi es muy lento. Este proceso de particionar los datos también aporta una disminución de la memoria utilizada, de esta forma se lograria un balance entre rendimiento y capacidad de memoria cuando sea necesario.
 
# Propuesta de Solución
 - Existen varias bibliotecas que mejoran drásticamente el rendimiento de EF mediante el uso de operaciones masivas y por lotes, logrando realizar tareas en velocidades superior hasta 50 veces más rápido. Para esta solución se decide utilizar la extencion [Entity Framework Extensions](https://entityframework-extensions.net/bulk-savechanges). Instalada mediante su paquete nuget [Z.EntityFramework.Extensions](https://www.nuget.org/packages/Z.EntityFramework.Extensions/4.0.106)
 - Existen otras alternativas para este proposito como [EntityFramework.Utilities](https://github.com/MikaelEliasson/EntityFramework.Utilities) pero no son del todo seguras y por lo general aún tienen temas pendientes en desarrollo.

# Ejecutar Proyecto
 - Una ves descargado el proyecto debe montar la base de datos a través del script que se encuentra dentro de la carperta AppData. 
 - Actualizar la cadena de conexión que esta dentro del fichero Models/DBContext en el metodo OnConfiguring. (Para proyectos reales el lugar de la cadena de conexión debe estar dentro de un fichero de configuración)

# Conclusiones
 - Luego de realizar las pruebas para un volumen de datos de 17,175,295 los resultados fueron los siguientes:
 ```sh
Eliminar  7m 54s 874ms
Insertar  4m 23s 770ms
```
 - Ambiente de prueba CPU: core-i7 2.60GHz RAM: 16GB
