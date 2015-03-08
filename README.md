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






