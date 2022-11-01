using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCart.Repository.Data.Contracts
{
    public interface IDatabaseCollection
    {
        IMongoCollection<TEntity> GetCollection<TEntity>();
    }
}
