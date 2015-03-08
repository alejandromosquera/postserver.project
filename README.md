# postserver.project

Posts API Rest/json

Es un proyecto desarrollado en Visual Studio .Net, que tiene como principales salidas dos proyectos: PS.WebApi y PS.TestClient

<b>PS.WebApi</b>

Es un web api que soporta y permite la ejecución de multiples solicitudes simultaneamnete gracias al método asíncrono que recibe las peticiones de los clientes.

```
[SessionState(SessionStateBehavior.Disabled)]
public class PostsController : ApiController
{
  public async void Post([FromBody]string value)
  {
    var manager = new PostsManager();
    await manager.ProcessPostAndSaveInSql(value);
    //await manager.ProcessPostAndSaveInMongDb(value);
  }<br />
}<br />
```

La antoción [SessionState(SessionStateBehavior.Disabled)] en el encabezado de la función deshabilita la restricción de peticiones simultáneas que se ejecutan desde un mismo cliente.

<b>PS.TestClient</b>

Envia 1000 solicitudes al Web API simultanemanete a través del método SendPost()

```
static async Task<HttpResponseMessage> SendPost()
{
    try
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri("http://localhost:34757/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await httpClient.PostAsJsonAsync("api/Posts/", CreateNewPost(1024));                   

            return result;
        }
    }
    catch (Exception)
    {               
        return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
    }
}
```

Este método asíncrono envia las peticiones al servidor basado en el patrón: Fire-and-Forget, dado que una vez se haga la solicitud de envío no se trata la respuesta.

<b>Arquitectura</b>

Este proyecto esta diseñado basícamente en 3 capas: Presentación, Negocio y Datos.

<b>Consideraciones generales</b >
</br >
1. Para generar las cadena aleatorias se ha considerado el siguiente algoritmo

```
private static string CreateNewPost(int size)
{   
    /*Pattern for creat str. [ A-Za-z0-9,._] where _ represents a end period.*/    
    const string characters = " ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789,._";

    var random = new Random((int)DateTime.Now.Ticks);

    var result = new string( Enumerable.Range(0, size).Select(x => characters[random.Next(0, characters.Length)]).ToArray());
    
    return result;
}
```

Este código crea cadenas de texto aleatorio que contienen las letras del alfabeto en español (mayúsculas y minúsculas), dígitos y caracteres especiales como espacio, coma, punto y guión bajo (El guión bajo representa en el texto .\n (punto aparte)). No se usa \n directamente dado que el punto aparte es realmente un punto y despues un salto de linea. adicional a esto la compresión de datos a Json hace el salto de linea automáticamente. Sin embargo, el algoritmo de procesamiento es capaz de indentificar _ y .\n como punto aparte

2. Uso de tecnología para escribir en disco. </br >

Mientras se pensaba esta solución, se consideró 3 formas de escritura en disco: Archivos, Bases de datos relacionales y bases de datos no relacionales.

Los archivos se descartaron dado que en comparación a las otras dos tecnologías el proceso de escritura es mucho mas lento.

Las bases de datos relacionales pese a su rendimiento excepcional escribir 20000 o 30000 registros en unos pocos segundos se descartaron dado que el API de C# no tienen soporte estable aun para métodos asincronos, en ocasiones presenta fallos en la inserción. Sin embargo en la capa de datos existe lógica para guarda en estas bases de datos.

La opción elegida para almacenar los datos fue SQL Server que proporciona un método asincrono para la conexión y escritura de datos.




