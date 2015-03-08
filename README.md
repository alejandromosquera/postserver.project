<b>Descripción</b>

El proyecto postserver.project es una solución construida en VisualStudio.NET que tiene dos principales salidas. PS.WebApi y Ps.TestClient. 

<b>Ps.WebApi:</b> Soporta y ejecuta peticiones simultaneas desde diferentes clientes para el tratamiento de posts.
<b>Ps.TestClient:</b> Crear y envia 1000 solictudes simultaneas con un peso de 2kb al WebAPI en menos de 1 segundo.

<b>Arquitectura:</b>
El proyecto esta desarrollado en 3 capas: Presentación, Negocio y datos.

<b>Consideraciones:</b>
<br />
1. En el algoritmo de generación de cadenas se considera _ como punto aparte.<br />
2. El método de escritura en disco es SQL Server, por encima de Archivos y de NOSQL, este último por no tener soporte estable en la ejecuión de métodos asíncronos.
 
<b>Base de datos</b>
La base de datos esta en SQL Server, 2012. En el proyecto Ps.WebApi se encuentra el script.

<b>Rendimiento</b><br />
1. Las pruebas de rendimiento en TestClient se hicieron utilizando la clase Stopwatch().<br />
2. Las pruebas de rendimiento del WebApi se hicieron utilizando el Performance Monitor de Windows, que da como resultado el procesamiento concurrente de mas 10000 solicitudes simultáneas en 1 segundo.<br />
3. Aunque el servidor soporte y procese multiples solicitudes en 1 segundo. Sql Server encola las peticiones por lo que el proceso de escritura es rápido pero tarda mas de 1 segundo.<br />
4. La cadena de conexión de la base de datos se debe especificar en el Web.Config del proyecto PS.WebApi<br />
