using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class FillInStateTest
    {
        [TestMethod]
        public void StateCreationIsNotNull()
        {
            DataLayer.State stateTest = new DataLayer.State(
                users: new List<DataLayer.Model.User>
                {
                    new DataLayer.Model.User
                    {
                        FirstName = "TestName", LastName = "TestSurname", Email = "test@test.com", Phone = "321654978"
                    }
                },
                books: new List<DataLayer.Model.Book>
                {
                    new DataLayer.Model.Book
                    {
                        Author = "Prince", Title = "Life of musician" , Price = 23, Publisher = "Uninversal Records"
                    }
                },
                discountCodes: new List<DataLayer.Model.DiscountCode>
                {
                    new DataLayer.Model.DiscountCode
                    {
                         Code = "Testing", Amount = 3
                    }
                });

            Assert.IsNotNull(stateTest);
        }
    }
}
