using DbKata.Entities;
using DbKata.Sql;
using NUnit.Framework;
using System.Linq;

namespace DbKata.Tests
{
    [TestFixture]
    public class SqlRequestBuilderTest
    {
        private ISqlRequestBuilder<Customer, int> _sqlRequestBuilder;

        [SetUp]
        public void Setup()
        {
            _sqlRequestBuilder = new SqlRequestBuilder<Customer, int>();
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
            var request = _sqlRequestBuilder.BuildGetOneRequest(13);
            Assert.NotNull(request.Parameters);
            var names = request.Parameters.ParameterNames;
            Assert.AreEqual(1, names.Count());
            Assert.AreEqual("cid", names.Single().ToLower());
            Assert.AreEqual(13, request.Parameters.Get<int>(names.Single()));
            Assert.AreEqual("select * from tcustomer where cid=@cid", request.Sql.ToLower());
        }

        [Test]
        public void SqlRequestBuilder_BuildAddRequest_Test()
        {
            var request = _sqlRequestBuilder.BuildAddRequest(new Customer
            {
                Firstname = "John",
                Lastname = "Shaft"
            });

            Assert.NotNull(request.Parameters);
            var names = request.Parameters.ParameterNames.ToList();
            Assert.AreEqual(2, names.Count);
            Assert.AreEqual("FNAME", names[0]);
            Assert.AreEqual("LNAME", names[1]);
            Assert.AreEqual("John", request.Parameters.Get<string>("FNAME"));
            Assert.AreEqual("Shaft", request.Parameters.Get<string>("LNAME"));
            Assert.AreEqual("insert into tcustomer(fname, lname) output inserted.cid values(@fname, @lname)", request.Sql.ToLower());
        }

        [Test]
        public void SqlRequestBuilder_BuildUpdateRequest_Test()
        {
            var request = _sqlRequestBuilder.BuildUpdateRequest(new Customer
            {
                Id = 19,
                Firstname = "John",
                Lastname = "Shaft"
            });

            Assert.NotNull(request.Parameters);
            var names = request.Parameters.ParameterNames;
            Assert.AreEqual(3, names.Count());
            Assert.AreEqual("cid", names.ElementAt(0).ToLower());
            Assert.AreEqual("fname", names.ElementAt(1).ToLower());
            Assert.AreEqual("lname", names.ElementAt(2).ToLower());
            Assert.AreEqual(19, request.Parameters.Get<int>(names.ElementAt(0)));
            Assert.AreEqual("John", request.Parameters.Get<string>(names.ElementAt(1)));
            Assert.AreEqual("Shaft", request.Parameters.Get<string>(names.ElementAt(2)));
            Assert.AreEqual("update tcustomer set fname=@fname, set lname=@lname where cid=@cid", request.Sql.ToLower());
        }
    }
}
