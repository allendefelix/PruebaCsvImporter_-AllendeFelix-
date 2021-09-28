# CsvImporter
Es un proyecto de consola que permite leer un fichero .csv almacenado en una cuenta de almacenamiento de Azure e inserte su contenido en una BD SQL Server local. 



## Tabla de contenidos
1. [Informacion General](#general-info)
2. [Tecnologias](#technologies)
3. [Instalacion](#installation)
4. [Test Unitario](#collaboration)
5. [FAQs](#faqs)

### Informacion General
***
Para la lectura del archivo en Azure opte por 2 alternativas, por defecto esta la alternativa de leer el fichero desde la URL pero los tiempos de lectura no son los optimos por distintas variables ( conectividad, servidor, etc.).
La segunda alternativa es descargar el fichero y leerlo en forma local ( la lectura es en segundos) a fines practicos no puedo subir el archivo .csv al repositorio por exceder la capacidad maxima permitida, en caso de querer probar el sistema de esta forma se debe hacer lo siguiente:
1) Descargar el fichero y copiarlo dentro de la carpeta "CsvImporter".
2) Modificar el archivo "appsettings" que se encuentra en la raiz del proyecto "CsvImporter" y comentar la Key "URL_CSV_FILE" que contiene el fichero en la web y descomentar la key que se encuentra debajo tambien llamada "URL_CSV_FILE" que solo hace referencia al nombre del archivo.

## Instalacion
*** 
```
1) Abrir Microsoft SQL Server Management Studio
2) Correr los scripts 01-Create DataBase CsvImporter y 02 - Create Table Stocks
que se encuentran en la raiz del repositorio.
3) $ git clone https://github.com/allendefelix/PruebaCsvImporter_-AllendeFelix-.git
4) Si no desea clonarlo puede descargar el proyecto como zip, luego abrir visual studio 2019 y abrir la solucion. 
```
### Test Unitario
***
Para la realizacion de las pruebas unitarias se utilizo el marco de pruebas xUnit.NET ya que es una herramienta gratuita y de código abierto y muy simple de implementar, se implementaron 3 pruebas unitarias :
1) ReadAllRowsFromCSVFile() : el cual nos permite probar si el fichero ha sido leido, a fines practicos se lee un fichero de mucho menor tamaño.
2) DeleteAllFilesTableStocks() : el cual nos permite probar si se han eliminado todos los registros de la bd, retornando null cuando la bd no contiene ningun dato.
3) InsertRowsToSQLAsync() : el cual nos permite probar el insert de una cantidad de registros a fines practicos se prueba insertar 10.0000 registros.
## FAQs
***
1. **Que escenario eligio para leer el archivo .csv alojado en Azure. ?**
Para leer un archivo .csv en un contenedor blob en Azure se opto por la herramienta "CsvHelper" ya que nos permite manejar grandes volumenes de datos mediante una pequena configuracion, manejando con gran facilidad cuando el archivo tiene o no cabecera y muy simple tambien a la hora de manejar el/los delimitadores del archivo.
2. __Que escenario eligio para insertar el archivo .csv en una base de datos SQL Server. ?__ 
Para esta tarea opte por utilizar SqlBulkCopy una clase, que pertenece al espacio de nombres System.Data.SqlCliente, que permite hacer una carga masiva de datos en una tabla de SQL Server, utilizando insercion por lotes (4000 registros) siendo este un valor que no afecta la performance ni el consumo de recursos.
3. **Que utilizo para eliminar todos los registros de la base de datos, previo al insert masivo. ?**
Utilice Un objeto SqlCommand con el metodo "ExecuteScalar" enviando la consulta "Truncate table" la cual borra fisicamente todos los registros e inicializa el contador de los registros a 0 .
4. **Que alternativas descarto para leer el archivo .csv. ?**
Descarte la utilizacion de librerias tales como "Microsoft.WindowsAzure.Storage" y "Microsoft.WindowsAzure.Storage.Blob" ya que a efectos del ejercicio no vi necesario agregarle toda esa logica siendo que el fichero era publico y podia darle un tratamiento como a cualquier fichero normal en la web.
Tambien descarte el uso del objeto "StreamReader" en mi opinion le agrega mas logica a la lectura del fichero y por ende mas lineas de codigos siendo esto mas dificil de mantener.
5. **Que alternativas descarto para eliminar los registros en la base de datos. ?**
Descarte la utilizacion de la  libreria Entity Framework Core ya que para los fines de este practico no considere necesario agregarle la complejidad propia de aplicar la misma en un proyecto de consola como la creacion de un dbContext siendo que este proyecto contaba con una sola tabla por lo tanto una sola entidas en el proyecto.
6. **Que alternativas descarto para insertar los registros .csv a SQL Server. ?**
Descarte la utilizacion de la  libreria Entity Framework Core por una cuestion de performance siendo que EFC es muy agil con su metodo SaveChanges() pero no tiene comparacion con el SQLBulkCopy para dar un ej. SaveChanges() inserta 5000 registros en 6000 ms siendo que BulkCopy lo hace en 75ms.# CsvImporter
Es un proyecto de consola que permite leer un fichero .csv almacenado en una cuenta de almacenamiento de Azure e inserte su contenido en una BD SQL Server local. 
