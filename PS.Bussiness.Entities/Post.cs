namespace PS.Bussiness.Entities
{
    public class Post
    {
        //public ObjectId Id { get; set; }

        public string Text { get; set; }

        public int WordsThatEndWithN { get; set; }

        public int SentencesWithMoreThan15Words { get; set; }

        public int Paragraphs { get; set; }

        public int CharactersDifferensToN { get; set; }
    }
}
