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

        public Mock<IConsoleHelper> mockedConsoleHelper { get; set; }
        public Mock<ICustomerNameValidation> mockedNameValidator { get; set; }
        public Mock<ICustomerAddressValidation> mockedAddressValidator { get; set; }
        public Mock<ICustomerCityValidation> mockedCityValidator { get; set; }
        public Mock<ICustomerStateValidation> mockedStateValidator { get; set; }
        public Mock<ICustomerZipValidation> mockedZipValidator { get; set; }
        public Mock<ICustomerPhoneValidation> mockedPhoneValidator { get; set; }
        public Mock<CustomerController> mockedCustomerController { get; set; }
        public CustomerController controller { get; set; }


        [TestInitialize]
        public void Setup()
        {
            mockedConsoleHelper = new Mock<IConsoleHelper>();
            mockedNameValidator = new Mock<ICustomerNameValidation>();
            mockedAddressValidator = new Mock<ICustomerAddressValidation>();
            mockedCityValidator = new Mock<ICustomerCityValidation>();
            mockedStateValidator = new Mock<ICustomerStateValidation>();
            mockedZipValidator = new Mock<ICustomerZipValidation>();
            mockedPhoneValidator = new Mock<ICustomerPhoneValidation>();
            mockedCustomerController = new Mock<CustomerController>();
            controller = new CustomerController(mockedNameValidator.Object,
                                                    mockedConsoleHelper.Object,
                                                    mockedAddressValidator.Object,
                                                    mockedCityValidator.Object,
                                                    mockedStateValidator.Object,
                                                    mockedZipValidator.Object,
                                                    mockedPhoneValidator.Object);
        }


        [TestMethod]
        public void WhenAStringIsEnteredANameIsReturned()
        {
            //arrange
            //mockedConsoleHelper.Setup(x => x.WriteAndReadFromConsole(It.IsAny<string>())).Returns("Justin Leggett");
            mockedCustomerController.Setup(x => x.EnterName()).Returns("Justin Leggett");
            //act
            var expectedResult = "Justin Leggett";
            var result = controller.EnterName();

            //assert
            Assert.AreEqual(result, expectedResult);
            //mockedCustomerController.Verify(x => x.EnterName(), Times.Once);
        }

        [TestMethod]
        public void WhenAStringIsEnteredAStreetIsReturned()
        {
            //arrange
            mockedConsoleHelper.Setup(x => x.WriteAndReadFromConsole(It.IsAny<string>())).Returns("123 Main St.");
            //act
            var expectedResult = "123 Main St.";
            var result = controller.EnterName();

            //assert
            Assert.AreEqual(result, expectedResult);
            mockedConsoleHelper.Verify(x => x.WriteAndReadFromConsole(It.IsAny<string>()), Times.Once);
        }
    }
}
