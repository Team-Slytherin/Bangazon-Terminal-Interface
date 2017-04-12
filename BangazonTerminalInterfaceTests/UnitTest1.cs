using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BangazonTerminalInterface.Helpers;
using Moq;
using BangazonTerminalInterface.DataValidation.CustomerValidation;
using BangazonTerminalInterface.Controllers;

namespace BangazonTerminalInterfaceTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WhenAnInvalidNameIsEnteredAnErrorShouldBeDisplayed()
        {
            //arrange
            var mockedConsoleHelper = new Mock<IConsoleHelper>();
            var mockedNameValidator = new Mock<ICustomerNameValid>();
            var controller = new CustomerController(mockedNameValidator.Object, mockedConsoleHelper.Object);

            mockedConsoleHelper.Setup(x => x.CheckForUserExit(It.IsAny<string>())).Returns(false);
            mockedNameValidator.Setup(x => x.ValidateName(It.IsAny<string>())).Returns(false);

            //act
            var result = controller.EnterName();

            //assert
            Assert.IsTrue(result);
            mockedConsoleHelper.Verify(x => x.WriteLine("Invalid input please enter in the format John Smith."), Times.Once);

        }
    }
}
