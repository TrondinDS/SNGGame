using MongoDB.Bson;

namespace Library.Types
{
    public class Content
    {
        public string Id { get; set; }
        public string Value { get; set; }

        public BsonDocument Document
        {
            get
            {
                return new BsonDocument
                {
                    { "contentId", Id },
                    { "value", Value }
                };
            }
        }
    }
}
