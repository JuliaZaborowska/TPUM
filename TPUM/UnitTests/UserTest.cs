using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void AreUsersDifferent()
        {
            DataLayer.Model.User first = new DataLayer.Model.User();
            DataLayer.Model.User second = new DataLayer.Model.User();

            first.FirstName = "james";
            first.Email = "james@test.com";
            first.LastName = "classified";

            second.FirstName = "chuck";
            second.Email = "chuck@test.com";
            second.LastName = "unknown";

            Assert.AreNotEqual(first, second);
        }

        [TestMethod]
        public void IsNotNull()
        {
            DataLayer.Model.User first = new DataLayer.Model.User();

            first.FirstName = "james";
            first.Email = "james@test.com";
            first.LastName = "classified";

            Assert.IsNotNull(first);
        }
    }
}
