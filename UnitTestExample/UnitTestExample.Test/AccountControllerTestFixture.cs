using Moq;
using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Abstractions;
using UnitTestExample.Controllers;
using UnitTestExample.Entities;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            var result = accountController.ValidateEmail(email);

            //Assert
            Assert.AreEqual(result, expectedResult);
        }

        [
            Test,
            TestCase("abcd1234", false),
            TestCase("ABCD1234", false),
            TestCase("abcdeFgh", false),
            TestCase("abDC123", false),
            TestCase("abcd1234", false),
            TestCase("aBCd1234", true),
            TestCase("abcD12345", true)
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            
            var accountController = new AccountController();

            
            var result = accountController.ValidatePassword(password);

            
            Assert.AreEqual(result, expectedResult);
        }

        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234567"),
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            var accountManagerMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountManagerMock
                .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                .Returns<Account>(a => a);
            var accountController = new AccountController();
            accountController.AccountManager = accountManagerMock.Object;
            
            var result = accountController.Register(email, password);

            Assert.AreEqual(email, result.Email);
            Assert.AreEqual(password, result.Password);
            Assert.AreNotEqual(Guid.Empty, result.ID);
            accountManagerMock.Verify(m => m.CreateAccount(result), Times.Once);
        }

        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234"),
            TestCase("irf.uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "abcd1234"),
            TestCase("irf@uni-corvinus.hu", "ABCD1234"),
            TestCase("irf@uni-corvinus.hu", "abcdABCD"),
            TestCase("irf@uni-corvinus.hu", "Ab1234"),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            var accountController = new AccountController();


            

            try
            {
                accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }
        }
    }
}
