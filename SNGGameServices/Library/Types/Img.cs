using MongoDB.Bson;

namespace Library.Types
{
    public class Img
    {
        public Guid Id { get; set; }
        public string Bytes { get; set; }
        public string ContentType { get; set; }

        public BsonDocument AsBsonDocument()
        {
            return new BsonDocument
            {
                { "id", Id.ToString() },
                { "data", Bytes },
                { "contentType", ContentType },
            };
        }
    }
}
