//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using BangazonTerminalInterface.Interfaces;
//using Moq;
//using BangazonTerminalInterface.Interfaces.CustomerValidationInterfaces;
//using BangazonTerminalInterface.Controllers;
//using BangazonTerminalInterface.Helpers;

//namespace BangazonTerminalInterfaceTests.DataValidationtest.CustomerValidationTest
//{
//    [TestClass]
//    public class CustomerPhoneValidatorTest
//    {
//        public CustomerPhoneValidator phoneValidation { get; set; }
//        public Mock<IConsoleHelper> mockedConsoleHelper { get; set; }
//        public Mock<ICustomerNameValidation> mockedNameValidator { get; set; }
//        public Mock<ICustomerAddressValidation> mockedAddressValidator { get; set; }
//        public Mock<ICustomerCityValidation> mockedCityValidator { get; set; }
//        public Mock<ICustomerStateValidation> mockedStateValidator { get; set; }
//        public Mock<ICustomerZipValidation> mockedZipValidator { get; set; }
//        public Mock<ICustomerPhoneValidation> mockedPhoneValidator { get; set; }
//        public Mock<CustomerController> mockedCustomerController { get; set; }
//        public CustomerController controller { get; set; }


//        [TestInitialize]
//        public void Setup()
//        {
//            phoneValidation = new CustomerPhoneValidator();
//            //mockedConsoleHelper = new Mock<IConsoleHelper>();
//            //mockedNameValidator = new Mock<ICustomerNameValidation>();
//            //mockedAddressValidator = new Mock<ICustomerAddressValidation>();
//            //mockedCityValidator = new Mock<ICustomerCityValidation>();
//            //mockedStateValidator = new Mock<ICustomerStateValidation>();
//            //mockedZipValidator = new Mock<ICustomerZipValidation>();
//            mockedPhoneValidator = new Mock<ICustomerPhoneValidation>();
//            //mockedCustomerController = new Mock<CustomerController>();
//            //controller = new CustomerController(mockedNameValidator.Object,
//            //                                        mockedConsoleHelper.Object,
//            //                                        mockedAddressValidator.Object,
//            //                                        mockedCityValidator.Object,
//            //                                        mockedStateValidator.Object,
//            //                                        mockedZipValidator.Object,
//            //                                        mockedPhoneValidator.Object);
//        }


//        [TestMethod]
//        public void WhenAStringIsEnteredANameIsReturned()
//        {
//            //arrange
//            //mockedConsoleHelper.Setup(x => x.WriteAndReadFromConsole(It.IsAny<string>())).Returns("Justin Leggett");
//            mockedPhoneValidator.Setup(x => x.ValidatePhone(It.IsAny<string>())).Returns(true);
//            //act
//            var expectedResult = true;
//            var result = controller.EnterName();

//            //assert
//            Assert.AreEqual(result, expectedResult);
//            //mockedCustomerController.Verify(x => x.EnterName(), Times.Once);
//        }

//        [TestMethod]
//        public void WhenAStringIsEnteredAStreetIsReturned()
//        {
//            //arrange
//            mockedConsoleHelper.Setup(x => x.WriteAndReadFromConsole(It.IsAny<string>())).Returns("123 Main St.");
//            //act
//            var expectedResult = "123 Main St.";
//            var result = controller.EnterName();

//            //assert
//            Assert.AreEqual(result, expectedResult);
//            mockedConsoleHelper.Verify(x => x.WriteAndReadFromConsole(It.IsAny<string>()), Times.Once);
//        }
//    }
//}
