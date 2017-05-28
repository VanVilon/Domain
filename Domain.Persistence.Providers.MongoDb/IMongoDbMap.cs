using System;
using System.Collections.Generic;
using System.Text;
using Domain.Infrastructure;
using MongoDB.Bson.Serialization;

namespace Domain.Persistence.Providers.MongoDb
{
    public class MongoDbMap<TEntity> : BsonClassMap<TEntity>
    {
    }
}
