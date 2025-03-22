using Amazon.Runtime.Internal.Util;
using Library.Types;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library.Services
{
    public class Mongo
    {
        private MongoClient client;

        public Mongo(string host, string port)
        {
            client = new MongoClient($"mongodb://{host}:{port}");
        }

        public Mongo(string? connectionString)
        {
            if (connectionString == null)
            {
                Console.WriteLine("Fatal error must be throwed"); // TODO(kra53n): use logger
            }
            client = new MongoClient(connectionString);
        }

        public Database Database(string name)
        {
            return new Database(client, name);
        }
    }

    public class Database
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase db;

        public Database(MongoClient client, string name)
        {
            this.client = client;
            this.db = client.GetDatabase(name);
        }

        public Collection Collection(string name)
        {
            return new Collection(client, db, name);
        }
    }

    public class Collection
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<BsonDocument> collection;

        public Collection(MongoClient client, IMongoDatabase db, string name)
        {
            this.client = client;
            this.db = db;
            this.collection = db.GetCollection<BsonDocument>(name);
        }

        public async Task Insert(Guid id, IFormFile formFile)
        {
            if (collection == null)
            {
                Console.WriteLine(""); // TODO(kra53n): use logger
                return;
            }
            var memStream = new MemoryStream();
            await formFile.CopyToAsync(memStream);
            await collection.InsertOneAsync(
                new Img
                {
                    Id = id,
                    Bytes = memStream.ToArray(),
                    ContentType = formFile.ContentType,
                }.AsBsonDocument()
            );
        }

        public async Task<Img> GetImgById(Guid id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("id", id.ToString());
            var document = await collection.Find(filter).FirstOrDefaultAsync();
            if (document == null)
            {
                Console.WriteLine("Image was not found"); // TODO(kra53n): use here some logger kek:)
                return new Img { };
            }
            return new Img
            {
                Id = new Guid(document["id"].AsString),
                Bytes = document["data"].AsBsonBinaryData.Bytes,
                ContentType = document["contentType"].AsString,
            };
        }

        public async Task Insert(int id, IFormFile formFile)
        {
            if (collection == null)
            {
                Console.WriteLine(""); // TODO(kra53n): use logger
                return;
            }
            var memStream = new MemoryStream();
            await formFile.CopyToAsync(memStream);
            await collection.InsertOneAsync(
                new Img
                {
                    Id = new Guid(),
                    Bytes = memStream.ToArray(),
                    ContentType = formFile.ContentType,
                }.AsBsonDocument()
            );
        }

        public async Task<Img> GetImgById(int id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("id", id.ToString());
            var document = await collection.Find(filter).FirstOrDefaultAsync();
            if (document == null)
            {
                Console.WriteLine("Image was not found"); // TODO(kra53n): use here some logger kek:)
                return new Img { };
            }
            return new Img
            {
                Id = new Guid(document["id"].AsString),
                Bytes = document["data"].AsBsonBinaryData.Bytes,
                ContentType = document["contentType"].AsString,
            };
        }

        public async Task Insert(Guid id, string content)
        {
            if (collection == null)
            {
                Console.WriteLine(""); // TODO(kra53n): use logger
                return;
            }
            await collection.InsertOneAsync(
                new Content { Id = id, Value = content }.AsBsonDocument()
            );
        }

        public async Task<Content> GetContentById(Guid id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("id", id.ToString());
            var document = await collection.Find(filter).FirstOrDefaultAsync();
            if (document == null)
            {
                Console.WriteLine("Content was not found"); // TODO(kra53n): use here some logger kek:)
            }
            return new Content
            {
                Id = new Guid(document["id"].AsString),
                Value = document["value"].AsString,
            };
        }

        public async Task Insert(int id, string content)
        {
            if (collection == null)
            {
                Console.WriteLine(""); // TODO(kra53n): use logger
                return;
            }
            await collection.InsertOneAsync(
                new Content { Id = new Guid(), Value = content }.AsBsonDocument()
            );
        }

        public async Task<Content> GetContentById(int id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("id", id.ToString());
            var document = await collection.Find(filter).FirstOrDefaultAsync();
            if (document == null)
            {
                Console.WriteLine("Content was not found"); // TODO(kra53n): use here some logger kek:)
            }
            return new Content
            {
                Id = new Guid(document["id"].AsString),
                Value = document["value"].AsString,
            };
        }

        public async Task Delete(Guid id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("id", id.ToString());
            await collection.DeleteOneAsync(filter);
        }

        public async Task Delete(int id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("id", id.ToString());
            await collection.DeleteOneAsync(filter);
        }
    }
}
