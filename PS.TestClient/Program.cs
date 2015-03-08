using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PS.TestClient
{
    class Program
    {        
        static void Main()
        {            
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            SimulateClient();

            stopwatch.Stop();

            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
         
            Console.ReadKey();
        }
        
        private static string CreateNewPost(int size)
        {   
            /*Pattern for creat str. [ A-Za-z0-9,._] where _ represents a end period.*/    
            const string characters = " ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789,._";

            var random = new Random((int)DateTime.Now.Ticks);

            var result = new string( Enumerable.Range(0, size).Select(x => characters[random.Next(0, characters.Length)]).ToArray());
            
            return result;
        }

        static async Task<HttpResponseMessage> SendPost()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://localhost:60001/");
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

        static async void SimulateClient()
        { 
            for (var j = 0; j < 1000; j++)
            {                
               await SendPost();
            }
        }      
    }
}
