using BankRayo.Controllers;
using BankRayo.Models;
using BankRayo.Repository;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRayoTests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public async Task DeleteAccountSucesfull()
        {
            //Arrange
            var account = new Account
            {
                Number = 478758,
                Type = "Ahorros",
                InitialBalance = 2000,
                State = true,
                IdClient = 433443
            };

            var mockRepo = new Mock<IAccountRepository>();
            var mockLogger = new Mock<ILogger<AccountController>>();
            ILogger<AccountController> logger = (ILogger<AccountController>)mockRepo.Object;

            mockRepo.Setup(repo => repo.DeleteAccountAsync(account))
                    .Returns(Task.FromResult(true));

            var controller = new AccountController(mockRepo.Object, (ILogger<ClientController>)logger);

            //Act
            var result = await controller.Delete(account.Number);

            //Assert
            

        }

        private Account GetAccount()
        {
            return new Account
            {
                Number = 478758,
                Type = "Ahorros",
                InitialBalance = 2000,
                State = false,
                IdClient = 433443
            };
        }
    }
}
