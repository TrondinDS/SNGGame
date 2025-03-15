using MongoDB.Bson;
using System.Net.Mime;

namespace Library.Types
{
    public class Content
    {
        public Guid Id { get; set; }
        public string Value { get; set; }

        public BsonDocument AsBsonDocument()
        {
            return new BsonDocument
            {
                { "id", Id.ToString() },
                { "value", Value }
            };
        }
    }
}
