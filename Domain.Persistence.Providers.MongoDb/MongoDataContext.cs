﻿using System;
using System.Collections.Generic;
using System.Text;
using Domain.Infrastructure;
using Domain.Infrastructure.Persistence;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.Persistence.Providers.MongoDb
{
    public class MongoDataContext : IDataContext
    {
        private readonly string _mongoDbName;
        private readonly MongoClient _client;

        public MongoDataContext(string connectionString, string mongoDbName)
        {
            _mongoDbName = mongoDbName;
            _client = new MongoClient(connectionString);
        }

        public IMongoDatabase GetDatabase()
        {
            return _client.GetDatabase(_mongoDbName);
        }

        public void DropDatabase(string dbName)
        {
            _client.DropDatabase(dbName);
        }

        public void DropCollection(string collectionName)
        {
            var database = GetDatabase();

            database.DropCollection(collectionName);
        }
    }
}
