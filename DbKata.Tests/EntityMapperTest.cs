using DbKata.Entities;
using DbKata.Sql;
using NUnit.Framework;

namespace DbKata.Tests
{
    [TestFixture]
    public class EntityMapperTest
    {
        [Test]
        public void PropertyMaps_WhenMapperInitialized_ThenShouldContainMapping()
        {
            var mapper = new EntityMapper<Customer>();
            Assert.NotNull(mapper.PropertyMaps);
            Assert.AreEqual(3, mapper.PropertyMaps.Count);
            Assert.AreEqual("CId", mapper.PropertyMaps[0].ColumnName);
            Assert.AreEqual(nameof(Customer.Id), mapper.PropertyMaps[0].PropertyInfo.Name);
            Assert.AreEqual("FNAME", mapper.PropertyMaps[1].ColumnName);
            Assert.AreEqual(nameof(Customer.Firstname), mapper.PropertyMaps[1].PropertyInfo.Name);
            Assert.AreEqual("LNAME", mapper.PropertyMaps[2].ColumnName);
            Assert.AreEqual(nameof(Customer.Lastname), mapper.PropertyMaps[2].PropertyInfo.Name);
        }
    }
    
}
