using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BangazonTerminalInterface.Controllers;
using BangazonTerminalInterface.Models;
using Moq;
using BangazonTerminalInterface.Interfaces;
using System.Collections.Generic;

namespace BangazonTerminalInterfaceTests.ControllerTest
{
    [TestClass]
    public class SelectActiveCustomerTest
    {
        [TestMethod]
        public void TestValidChoiceGetRightCustomer()
        {
            var mockICustomer = new Mock<ICustomer>();
            var mockIConsoleHelper = new Mock<IConsoleHelper>();
            var selectCustomerController = new SelectCustomerController(mockICustomer.Object, mockIConsoleHelper.Object);
            var customer1 = new Customer() { CustomerId = 1, CustomerName = "aa ab", CustomerStreetAddress = "1232 fsdf 33" };
            var customer2 = new Customer() { CustomerId = 2, CustomerName = "bb bc", CustomerStreetAddress = "1555 yyyf 33" };
            var list = new List<Customer>();
            list.Add(customer1);
            list.Add(customer2);
            mockICustomer.Setup(x => x.GetAllCustomers()).Returns(list);
            mockIConsoleHelper.Setup(x => x.WriteAndReadFromConsole("> ")).Returns("1");

            var selectedCustomer = selectCustomerController.SelectActiveCustomer();
            var expected = 1;
            var actual = selectedCustomer.CustomerId;

            Assert.AreEqual(expected, actual);
        }
    }
}
