using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mongodb
{
    public class MongoDBConnection
    {
        private readonly string _strConn;
        private readonly string _database;
        private readonly string _collection;
        public IMongoClient Client { get; private set; }
        public IMongoDatabase Database { get; private set; }

        public MongoDBConnection(string strConn, string database, string collection)
        {
            _strConn = strConn;
            _database = database;
            _collection = collection;
        }

        public IMongoCollection<T> ConnectAndReturnCollection<T>()
        {
            Client = new MongoClient(_strConn);
            Database = Client.GetDatabase(_database);
            return Database.GetCollection<T>(_collection);
        }
    }
}
