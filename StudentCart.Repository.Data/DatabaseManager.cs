using MongoDB.Driver;
using StudentCart.Repository.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentCart.Repository.Data
{
    public class DatabaseManager : IDatabaseCollection
    {
        protected IMongoDatabase db;
        protected IMongoClient client;
        String databaseName;

        public DatabaseManager(String connectionString)
        {
            client = new MongoClient(connectionString);
            databaseName = connectionString.Split('/').ToList().Last();
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            var db = client.GetDatabase(databaseName);
            if (db.GetCollection<TEntity>(typeof(TEntity).Name) != null)
                client.GetDatabase(databaseName).CreateCollectionAsync(typeof(TEntity).Name);

            return client.GetDatabase(databaseName).GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}
