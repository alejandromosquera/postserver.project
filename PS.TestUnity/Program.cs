using System;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver;
using PS.Bussiness.Entities;
using PS.Bussiness.Managers;

namespace PS.TestUnity
{
    class Program
    {
        private static void Main()
        {
            var postManager = new PostsManager();

            var postManager.ProcessPostAndSaveInSql(CreateNewPost(512));
        }


        private static string CreateNewPost(int size)
        {
            /*Pattern for creat str. [ A-Za-z0-9,._] where _ represents a end dot.*/
            const string characters = " ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789,._";

            var random = new Random((int)DateTime.Now.Ticks);

            var result = new string(Enumerable.Range(0, size).Select(x => characters[random.Next(0, characters.Length)]).ToArray());

            return result;
        }
    }
}
