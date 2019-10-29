using DbKata.Entities;
using DbKata.Sql;
using NUnit.Framework;
using System.Data.SqlClient;

namespace DbKata.Tests
{
    [TestFixture]
    public class RepositoryTest
    {
        [Test]
        public void GivenACustomer_WhenAdd_ThenShouldBeSavedToDb()
        {
            using (var connection = new SqlConnection(""))
            using (var transaction = connection.BeginTransaction())
            {
                var customer = new Customer { Firstname = "Lance", Lastname = "Marat" };
                var repo = new Repository<string, Customer>(connection);
                var id = repo.Add(customer);
                customer = repo.GetOne(id);
                Assert.NotNull(customer);
                transaction.Rollback();
            }
        }
    }
}
