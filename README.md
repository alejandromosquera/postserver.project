<b>Descripción</b>

El proyecto postserver.project es una solución construida en VisualStudio.NET que tiene dos principales salidas. PS.WebApi y Ps.TestClient. 

<b>Ps.WebApi:</b> Soporta y ejecuta peticiones simultaneas desde diferentes clientes para el tratamiento de posts.
<b>Ps.TestClient:</b> Crear y envia 1000 solictudes simultaneas con un peso de 2kb al WebAPI en menos de 1 segundo.

<b>Consideraciones:</b>
1. En el algoritmo de generación de cadenas se considera _ como punto aparte.
2. El método de escritura en disco es SQL Server, por encima de Archivos y de NOSQL, este último por no tener soporte estable en la ejecuión de métodos asíncronos.
 
<b>Base de datos</b>
La base de datos esta en SQL Server, 2012. En el proyecto Ps.WebApi se encuentra el script.
