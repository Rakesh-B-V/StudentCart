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
            //For Server Database
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            client = new MongoClient(settings);
            databaseName = "StudentsCart";

            ////For Local data base
            //client = new MongoClient(connectionString);
            //databaseName = connectionString.Split('/').ToList().Last();
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
