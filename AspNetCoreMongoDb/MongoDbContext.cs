using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreMongoDb.Entities;
using MongoDB.Driver;

namespace AspNetCoreMongoDb
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext()
        {
            _db = new MongoClient("mongodb://localhost:27017").GetDatabase("AspNetCoreMongoDb");
        }

        public IMongoCollection<Customer> Customers
        {
            get { return _db.GetCollection<Customer>("customers"); }
        }

        public IMongoCollection<City> Cities
        {
            get { return _db.GetCollection<City>("cities"); }
        }
    }
}
