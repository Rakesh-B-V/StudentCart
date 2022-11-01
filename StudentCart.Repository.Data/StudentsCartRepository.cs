using MongoDB.Driver;
using StudentCart.Repository.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCart.Repository.Data
{
    public class StudentsCartRepository : IStudentsCartRepository
    {
        public readonly IDatabaseCollection _DataOperations;

        public StudentsCartRepository(IDatabaseCollection dataOperations)
        {
            this._DataOperations = dataOperations;
        }

        public IMongoCollection<TEntity> GetCollectionIMongo<TEntity>() where TEntity : class
        {
            var mongoCollection = this._DataOperations.GetCollection<TEntity>();
            return mongoCollection;
        }
    }
}
