using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PS.WebApi.Controllers;

namespace PS.WebApi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new PostsController();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < 20000; i++)
            {
                controller.Post("hola");
            }

            stopwatch.Stop();

            Debug.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
          
        }

        private static string CreateNewPost(int size)
        {
            const string characters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789 ,.*";

            var random = new Random();

            var result = new string(Enumerable.Repeat(characters, size).Select(s => s[random.Next(0, s.Length)]).ToArray());

            return result;
        }

       
    }
}
