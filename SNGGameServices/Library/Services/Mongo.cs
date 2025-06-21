using Amazon.Runtime.Internal.Util;
using Library.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net.Mime;

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
                Bytes = document["data"].AsString,
                ContentType = document["contentType"].AsString,
            };
        }

        public async Task<List<Img>> GetImgsByIds(List<Guid> ids)
        {
            var idStrings = ids.Select(id => id.ToString()).ToList();
            var filter = Builders<BsonDocument>.Filter.In("id", idStrings);
            var documents = await collection.Find(filter).ToListAsync();
            return documents.Select(document => new Img
            {
                Id = new Guid(document["id"].AsString),
                Bytes = document["data"].AsString,
                ContentType = document["contentType"].AsString,
            }).ToList();
        }

        public async Task Insert(Guid id, IFormFile formFile)
        {
            // TODO(kra53n): delete this method
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
                    Bytes = "asd",
                    //Bytes = memStream.ToArray(),
                    ContentType = formFile.ContentType,
                }.AsBsonDocument()
            );
        }

        public async Task InsertImg(Guid id, string bytes, string contentType)
        {
            if (collection == null)
            {
                Console.WriteLine(""); // TODO(kra53n): use logger
                return;
            }
            await collection.InsertOneAsync(
                new Img
                {
                    Id = id,
                    Bytes = bytes,
                    ContentType = contentType,
                }.AsBsonDocument()
            );
        }

        public async Task UpdateImg(Guid id, string bytes, string contentType)
        {
            if (collection == null) return;
            var filter = Builders<BsonDocument>.Filter.Eq("id", id.ToString());
            var update = Builders<BsonDocument>.Update
                .Set("data", bytes)
                .Set("contentType", contentType);
            await collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateStrContent(Guid id, string content)
        {
            if (collection == null) return;
            var filter = Builders<BsonDocument>.Filter.Eq("id", id.ToString());
            var update = Builders<BsonDocument>.Update
                .Set("value", content);
            await collection.UpdateOneAsync(filter, update);
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
                Bytes = document["data"].AsString,
                ContentType = document["contentType"].AsString,
            };
        }

        public async Task InsertStrContent(Guid id, string content)
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

        public async Task<List<Content>> GetContentsByIds(List<Guid> ids)
        {
            var idStrings = ids.Select(id => id.ToString()).ToList();
            var filter = Builders<BsonDocument>.Filter.In("id", idStrings);
            var documents = await collection.Find(filter).ToListAsync();
            return documents.Select(document => new Content
            {
                Id = new Guid(document["id"].AsString),
                Value = document["value"].AsString,
            }).ToList();
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
    }
}
