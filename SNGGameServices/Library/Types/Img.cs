using MongoDB.Bson;

namespace Library.Types
{
    public class Img
    {
        public Guid Id { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }

        public BsonDocument AsBsonDocument()
        {
            return new BsonDocument
            {
                { "id", Id.ToString() },
                { "data", new BsonBinaryData(Bytes) },
                { "contentType", ContentType },
            };
        }
    }
}
