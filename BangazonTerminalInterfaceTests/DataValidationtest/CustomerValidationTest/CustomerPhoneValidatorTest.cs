using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BangazonTerminalInterface.Interfaces;
using Moq;
using BangazonTerminalInterface.Interfaces.CustomerValidationInterfaces;
using BangazonTerminalInterface.Controllers;
using BangazonTerminalInterface.Helpers;

namespace BangazonTerminalInterfaceTests.DataValidationtest.CustomerValidationTest
{
    [TestClass]
    public class CustomerPhoneValidatorTest
    {
        [TestMethod]
        public void EnsureIAmAbleToWriteToConsole()
        {
            //arrange
            //This probably needs to move to a test helper method
            var mockedConsoleHelper = new Mock<IConsoleHelper>();
            //var mockedNameValidator = new Mock<ICustomerNameValidation>();
            //var mockedAddressValidator = new Mock<ICustomerAddressValidation>();
            //var mockedCityValidator = new Mock<ICustomerCityValidation>();
            //var mockedStateValidator = new Mock<ICustomerStateValidation>();
            //var mockedZipValidator = new Mock<ICustomerZipValidation>();
            //var mockedPhoneValidator = new Mock<ICustomerPhoneValidation>();
            ConsoleHelper consoleHelper = new ConsoleHelper();
            //var mockCustomerController = new Mock<CustomerController>();
            //var controller = new CustomerController(mockedNameValidator.Object,
            //                                        mockedConsoleHelper.Object,
            //                                        mockedAddressValidator.Object,
            //                                        mockedCityValidator.Object,
            //                                        mockedStateValidator.Object,
            //                                        mockedZipValidator.Object,
            //                                        mockedPhoneValidator.Object);

            //mockedConsoleHelper.Setup(x => x.WriteAndReadFromConsole(It.IsAny<string>())).Returns("Justin Leggett");
            //mockedConsoleHelper.Setup(x => x.Write("Justin Leggett"));
            mockedConsoleHelper.Setup(x => x.ReadLine()).Returns("Justin Leggett");


            //act
            var expectedResult = "Justin Leggett";
            var result = consoleHelper.ReadLine();

            //assert
            Assert.AreEqual(result, expectedResult);
            //mockCustomerController.Verify(x => x.EnterName());
            //mockedConsoleHelper.Verify(x => x.WriteAndReadFromConsole(It.IsAny<string>()), Times.Once);
        }
    }
}
