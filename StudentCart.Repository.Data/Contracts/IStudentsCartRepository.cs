using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCart.Repository.Data.Contracts
{
    public interface IStudentsCartRepository
    {
        IMongoCollection<TEntity> GetCollectionIMongo<TEntity>() where TEntity : class;
    }
}
