using DbKata.Entities;
using DbKata.Sql;
using NUnit.Framework;
using System.Linq;

namespace DbKata.Tests
{
    [TestFixture]
    public class SqlRequestBuilderTest
    {
        private ISqlRequestBuilder<Customer, string> _sqlRequestBuilder;

        [SetUp]
        public void Setup()
        {
            _sqlRequestBuilder = new SqlRequestBuilder<Customer, string>();
        }

        [Test]
        public void SqlRequestBuilder_BuildGetAllRequest_Test()
        {
            var request = _sqlRequestBuilder.BuildGetAllRequest();
            Assert.IsNull(request.Parameters);
            Assert.AreEqual("select * from tcustomer", request.Sql.ToLower());
        }

        [Test]
        public void SqlRequestBuilder_BuildGetOneRequest_Test()
        {
            var request = _sqlRequestBuilder.BuildGetOneRequest("c1");
            Assert.NotNull(request.Parameters);
            var names = request.Parameters.ParameterNames;
            Assert.AreEqual(1, names.Count());
            Assert.AreEqual("id", names.Single().ToLower());
            Assert.AreEqual("c1", request.Parameters.Get<string>(names.Single()));
            Assert.AreEqual("select * from tcustomer where id=@id", request.Sql.ToLower());
        }

        [Test]
        public void SqlRequestBuilder_BuildAddRequest_Test()
        {
            var request = _sqlRequestBuilder.BuildAddRequest(new Customer
            {
                Id = "c1",
                Firstname = "Lance",
                Lastname = "Marat"
            });

            Assert.NotNull(request.Parameters);
            var names = request.Parameters.ParameterNames.ToList();
            Assert.AreEqual(3, names.Count);
            Assert.AreEqual("Id", names[0]);
            Assert.AreEqual("FNAME", names[1]);
            Assert.AreEqual("LNAME", names[2]);

            Assert.AreEqual("c1", request.Parameters.Get<string>("Id"));
            Assert.AreEqual("Lance", request.Parameters.Get<string>("FNAME"));
            Assert.AreEqual("Marat", request.Parameters.Get<string>("LNAME"));
            Assert.AreEqual("insert into tcustomer(id, fname, lname) output inserted.id values(@id, @fname, @lname)", request.Sql.ToLower());
        }
    }
}
