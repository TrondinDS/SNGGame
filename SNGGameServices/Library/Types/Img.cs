using MongoDB.Bson;

namespace Library.Types
{
    public class Img
    {
        public string Id { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }

        public BsonDocument Document
        {
            get
            {
                return new BsonDocument
                {
                    { "imageId", Id },
                    { "imageData", new BsonBinaryData(Bytes) },
                    { "contentType", ContentType },
                };
            }
        }
    }
}
