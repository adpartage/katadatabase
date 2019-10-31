using DbKata.Entities;
using DbKata.Utils;
using NUnit.Framework;

namespace DbKata.Tests
{
    [TestFixture]
    public class EntityExtensionTest
    {        
        [Test]
        public void GetDbColumn()
        {
            var customer = new Customer();
            Assert.AreEqual("CId", customer.GetDbName(c => c.Id));
            Assert.AreEqual("FNAME", customer.GetDbName(c => c.Firstname));
            Assert.AreEqual("LNAME", customer.GetDbName(c => c.Lastname));
        }
    }
}
