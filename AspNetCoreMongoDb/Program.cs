using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMongoDb.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AspNetCoreMongoDb
{
    class Program
    {
        private static MongoDbContext _context;
        private static string _customerId;
        private static string _cityId;
        static void Main(string[] args)
        {
            _context = new MongoDbContext();
            
            RemoveAllCustomers();
            RemoveAllCities();
            AddCity();
            AddCustomer();
            UpdateCustomers();
            GetAllCustomers();
        }

        static void AddCustomer()
        {
            var customer = new Customer()
            {
                Name = "Rodrigo",
                CityId = _cityId
            };

             _context.Customers.InsertOne(customer);
             _customerId = customer.Id;
        }

        static void AddCity()
        {
            var city = new City()
            {
                Name = "São Paulo"
            };

            _context.Cities.InsertOne(city);
            _cityId = city.Id;
        }

        static void GetAllCustomers()
        {
            var customersCollection = _context.Customers;
            var citiesCollection = _context.Cities;
            var customersConnected = customersCollection.AsQueryable().Where(x => x.Id == _customerId);


            var query = from a in customersConnected
                        join b in citiesCollection.AsQueryable() on a.CityId equals b.Id into cities
                select new Customer()
                {
                    Name = a.Name,
                    City = cities.First(),
                    CityId = a.CityId
                };

            var customers = query.ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine($"Register: {customer.Id} - {customer.Name} ({customer.City.Name})");
            }

            if(customers.Count == 0)
                Console.WriteLine($"Register: ");

        }

        static void UpdateCustomers()
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, _customerId);
            var update = Builders<Customer>.Update
                .Set(x => x.Name, "Rodrigo Test");
            _context.Customers.UpdateOne(filter, update);
        }

        static void RemoveById()
        {
            _context.Customers.DeleteOne(Builders<Customer>.Filter.Eq(x => x.Id, _customerId));
        }

        static void RemoveAllCustomers()
        {
            _context.Customers.DeleteMany(x => true);
        }
        static void RemoveAllCities()
        {
            _context.Cities.DeleteMany(x => true);
        }
    }
}
