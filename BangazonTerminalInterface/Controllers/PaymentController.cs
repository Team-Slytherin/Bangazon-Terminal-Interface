using BangazonTerminalInterface.DataValidation.CustomerValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BangazonTerminalInterface.DataValidation.PaymentValidation;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Components;

namespace BangazonTerminalInterface.Controllers
{
    class PaymentController
    {
        string paymentType; //To Capture the payment type
        int paymentAccountNumber; //To capture the account number
        
        private void requestPaymentType() // Ask for payment Type
        {
            PaymentTypeValid repo = new PaymentTypeValid();
            Console.WriteLine("Enter Payment Type" + "\n"
                    + "> ");
            paymentType = Console.ReadLine();

            while (!repo.ValidatePaymentType(paymentType))  // Send Reponse to Validator
            {
                Console.WriteLine("payment type invalid.  we only accept visa or mastercard.");
                Console.ReadLine();
            }  
        }

        private void requestPaymentActNumber() // Ask for payment account number
        {
            AccountNumberValid repo = new AccountNumberValid();
            Console.WriteLine("Enter Payment Account Number" + "\n"
                    + "> ");
            paymentType = Console.ReadLine();

            while (!repo.ValidatePaymentAccountNumber(paymentAccountNumber))  // Send Reponse to Validator
            {
                Console.WriteLine("Account number invalid.  Please input ####-####-####-####.");
                Console.ReadLine();
            }
        }

        public void addNewPayment(Customer activeCustomer, string paymentType, int paymentAccountNumber)
        {
            PaymentRepository newPayment = new PaymentRepository();
            newPayment.AddPayment(activeCustomer.CustomerId, paymentType, paymentAccountNumber);
        }

    }
}
