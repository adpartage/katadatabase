using DbKata.Entities;
using DbKata.Repositories;
using DbKata.Sql;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace DbKata.Tests.Integration
{
    [TestFixture]
    public class RepositoryIntegrationTest
    {
        private string _connectionString;
        private ISqlRequestBuilder<Customer, int> _requestBuilder;

        [SetUp]
        public void Setup()
        {
            _connectionString = @"Data Source=.\SQLExpress;Initial Catalog=katadb;Integrated Security=True;Pooling=False";
            _requestBuilder = new SqlRequestBuilder<Customer, int>();
        }

        [Test]
        public void GivenACustomer_WhenAdd_ThenShouldBeSavedToDatabase()
        {
            using (new TransactionScope())
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                var repository = new Repository<int, Customer>(dbConnection, _requestBuilder);
                var customer = new Customer { Firstname = "John", Lastname = "Shaft" };
                var id = repository.Add(customer);

                var savedCustomer = repository.GetOne(id);

                Assert.NotNull(savedCustomer);
                Assert.AreEqual(savedCustomer.Firstname, customer.Firstname);
                Assert.AreEqual(savedCustomer.Lastname, customer.Lastname);
            }
        }

        [Test]
        public void GivenACustomer_WhenDelete_ThenShouldBeRemovedFromDatabase()
        {
            using (new TransactionScope())
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                var repository = new Repository<int, Customer>(dbConnection, _requestBuilder);
                var customer = new Customer { Firstname = "John", Lastname = "Shaft" };
                customer.Id = repository.Add(customer);

                var succeded = repository.Delete(customer);

                var savedCustomer = repository.GetOne(customer.Id);
                Assert.IsTrue(succeded);
                Assert.IsNull(savedCustomer);
            }
        }

        [Test]
        public void GivenACustomer_WhenGetAll_ThenShouldSelectAllFromDatabase()
        {
            using (new TransactionScope())
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                var repository = new Repository<int, Customer>(dbConnection, _requestBuilder);
                var customer1 = new Customer { Firstname = "John", Lastname = "Shaft" };
                var customer2 = new Customer { Firstname = "John Jr.", Lastname = "Shafty" };
                customer1.Id = repository.Add(customer1);
                customer2.Id = repository.Add(customer2);

                var customers = repository.GetAll()?.ToList();

                Assert.NotNull(customers);
                Assert.AreEqual(2, customers.Count);
                Assert.AreEqual(customer1.Firstname, customers[0].Firstname);
                Assert.AreEqual(customer1.Lastname, customers[0].Lastname);
                Assert.AreEqual(customer2.Firstname, customers[1].Firstname);
                Assert.AreEqual(customer2.Lastname, customers[1].Lastname);
            }
        }

        public void GivenACustomer_WhenUpdated_ThenShouldBeUpdatedInDatabase()
        {
            using (new TransactionScope())
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                var repository = new Repository<int, Customer>(dbConnection, _requestBuilder);
                var customer = new Customer { Firstname = "John", Lastname = "Shaft" };
                customer.Id = repository.Add(customer);
                customer.Firstname = "John Jr";

                repository.Update(customer);

                var savedCustomer = repository.GetOne(customer.Id);
                Assert.NotNull(savedCustomer);
                Assert.AreEqual("John Jr.", savedCustomer.Firstname);
                Assert.AreEqual("Shaft", savedCustomer.Lastname);
            }
        }
    }
}
