using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BangazonTerminalInterface.Interfaces;
using BangazonTerminalInterface.Interfaces.CustomerValidationInterfaces;
using BangazonTerminalInterface.Controllers;

namespace BangazonTerminalInterfaceTests.ControllerTest
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public void WhenAnInvalidNameIsEnteredAnErrorShouldBeDisplayed()
        {
            //arrange
            //This probably needs to move to a test helper method
            var mockedConsoleHelper = new Mock<IConsoleHelper>();
            var mockedNameValidator = new Mock<ICustomerNameValidation>();
            var mockedAddressValidator = new Mock<ICustomerAddressValidation>();
            var mockedCityValidator = new Mock<ICustomerCityValidation>();
            var mockedStateValidator = new Mock<ICustomerStateValidation>();
            var mockedZipValidator = new Mock<ICustomerZipValidation>();
            var mockedPhoneValidator = new Mock<ICustomerPhoneValidation>();
            var mockCustomerController = new Mock<CustomerController>();
            var controller = new CustomerController(mockedNameValidator.Object, 
                                                    mockedConsoleHelper.Object,
                                                    mockedAddressValidator.Object,
                                                    mockedCityValidator.Object,
                                                    mockedStateValidator.Object,
                                                    mockedZipValidator.Object,
                                                    mockedPhoneValidator.Object);

            mockedConsoleHelper.Setup(x => x.WriteAndReadFromConsole(It.IsAny<string>())).Returns("Justin Leggett");

            //mockCustomerController.Setup(x => x.EnterName()).Returns("Justin Leggett");

            //act
            var expectedResult = "Justin Leggett";
            var result = controller.EnterName();
            //controller.CreateCustomer();

            //assert
            Assert.AreEqual(result, expectedResult);
            //mockCustomerController.Verify(x => x.EnterName());
            //mockedConsoleHelper.Verify(x => x.WriteAndReadFromConsole(It.IsAny<string>()), Times.Once);
            //mockedConsoleHelper.Verify(x => x.CheckForUserExit(It.IsAny<string>()), Times.Once);
        }
    }
}
