using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PS.Bussiness.Entities;
using PS.DataAccess;
using PS.Utilities;

namespace PS.Bussiness.Managers
{
    public class PostsManager
    {
        public async Task<int> ProcessPostAndSaveInSql(string text)
        {
            text = Regex.Replace(text, ".\n", "_");
            return await Create(ProcessPost(text));
        }

        public async Task<int> ProcessPostAndSaveInMongDb(string text)
        {
            text = Regex.Replace(text, ".\n", "_");
            return await CreateInMongoDb(ProcessPost(text));
        }

        public Post ProcessPost(string text)
        {
            text = Regex.Replace(text, ".\n", "_");
            return new Post
            {
                Text = text,
                WordsThatEndWithN = text.CountWordsThatEndWithChar('n'),
                SentencesWithMoreThan15Words = text.CountSentencesWithMaxWords(15, '_'),
                CharactersDifferensToN = text.CountCharsExcept('n'),
                Paragraphs = text.CountParagraphs('_')
            };
        }

        private async Task<int> Create(Post post)
        {
            var postsDao = new PostsDao();
            return await postsDao.Create(post);
        }

        private async Task<int> CreateInMongoDb(Post post)
        {
            var postsDao = new PostsDao();
            return await postsDao.CreateInMongoDb(post);
        }

        public int Count()
        {
            var postsDao = new PostsDao();
            return postsDao.Count();
        }
    }
}
