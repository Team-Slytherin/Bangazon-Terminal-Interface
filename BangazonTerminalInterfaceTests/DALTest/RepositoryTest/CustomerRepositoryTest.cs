using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Models;

namespace BangazonTerminalInterfaceTests.DALTest.RepositoryTest
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        [TestMethod]
        public void EnsureICanCreateAnInstance()
        {
            CustomerRepository repo = new CustomerRepository();

            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureICanGetAllCustomers()
        {
            
        }


        [TestMethod]
        public void EnsureICanGetCustomerById()
        {
            
        }
    }
}

