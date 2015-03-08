using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MongoDB.Driver;
using PS.Bussiness.Entities;

namespace PS.DataAccess
{
    public class PostsDao
    {
        public async Task<int> Create(Post post)
        {
            var connectionString =
                System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();

                    const string query =
                        @"INSERT INTO [Posts] ([Text], [WordsThatEndWithN], [SentencesWithMoreThan15Words], [Paragraphs], [CharactersDifferensToN]) VALUES (@Text, @WordsThatEndWithN, @SentencesWithMoreThan15Words, @Paragraphs, @CharactersDifferensToN)";

                    var sqlCommand = new SqlCommand(query, sqlConnection);

                    sqlCommand.Parameters.Add(new SqlParameter("Text", post.Text));
                    sqlCommand.Parameters.Add(new SqlParameter("WordsThatEndWithN", post.WordsThatEndWithN));
                    sqlCommand.Parameters.Add(new SqlParameter("SentencesWithMoreThan15Words",
                        post.SentencesWithMoreThan15Words));
                    sqlCommand.Parameters.Add(new SqlParameter("Paragraphs", post.Paragraphs));
                    sqlCommand.Parameters.Add(new SqlParameter("CharactersDifferensToN", post.CharactersDifferensToN));

                    return await sqlCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception exception)
            {
                return new int();
            }
        }

        public async Task<int> CreateInMongoDb(Post post)
        {
            try
            {
                const string connectionString = "mongodb://localhost";
                
                var client = new MongoClient(connectionString);
                
                var database = client.GetDatabase("postsdatabase");
                
                var collection = database.GetCollection<Post>("posts");
                
                await collection.InsertOneAsync(post);

                return new int();
            }
            catch (Exception exception)
            {
                return new int();
            }
        }

        public int Count()
        {
            var connectionString =
                System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    const string query =
                        @"SELECT COUNT(*) FROM Posts";

                    var sqlCommand = new SqlCommand(query, sqlConnection);

                    return (int) sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception exception)
            {
                return new int();
            }
        }
    }
}
