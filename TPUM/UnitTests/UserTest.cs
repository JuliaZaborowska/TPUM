using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class UserTest
    {
<<<<<<< HEAD
        [TestMethod]
        public void AreUsersDifferent()
        {
            DataLayer.Model.User first = new DataLayer.Model.User();
            DataLayer.Model.User second = new DataLayer.Model.User();

=======
        private DataLayer.Model.User first = new DataLayer.Model.User();
        private DataLayer.Model.User second = new DataLayer.Model.User();
        private DataLayer.Model.User third = null;

        public UserTest()
        {
>>>>>>> develop
            first.FirstName = "james";
            first.Email = "james@test.com";
            first.LastName = "classified";

            second.FirstName = "chuck";
            second.Email = "chuck@test.com";
            second.LastName = "unknown";
<<<<<<< HEAD

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
=======
        }        

        [TestMethod]
        public void AreFirstNamesDifferent()
        {
            Assert.AreNotEqual(first.FirstName, second.FirstName);
        }

        [TestMethod]
        public void AreEmailDifferent()
        {
            Assert.AreNotEqual(first.Email, second.Email);
        }

        [TestMethod]
        public void AreLastNameDifferent()
        {
            Assert.AreNotEqual(first.LastName, second.LastName);
        }

        [TestMethod]
        public void IsNotNull()
        {
            Assert.IsNotNull(first);
        }

        [TestMethod]
        public void IsNull()
        {
            Assert.IsNull(third);
        }
>>>>>>> develop
    }
}
