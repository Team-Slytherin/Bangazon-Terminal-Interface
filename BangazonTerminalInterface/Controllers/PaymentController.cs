using BangazonTerminalInterface.DataValidation.CustomerValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BangazonTerminalInterface.DataValidation.PaymentValidation;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Components;
using BangazonTerminalInterface.Models;

namespace BangazonTerminalInterface.Controllers
{
    class PaymentController
    {
        string paymentType; //To Capture the payment type
        string paymentAccountNumber; //To capture the account number
        Payment payment = new Payment();
        PaymentRepository newPayment = new PaymentRepository();

        public PaymentController()
        {
            requestPaymentType();
            Console.Clear();
            requestPaymentActNumber();
            Console.Clear();
            newPayment.AddPayment(payment.CustomerId, payment.Type, payment.Account);
        }

        public void requestPaymentType() // Ask for payment Type
        {
            PaymentTypeValid repo = new PaymentTypeValid();
            Console.WriteLine("Enter Payment Type" + "\n"
                    + "> ");
            paymentType = Console.ReadLine();

            while (!repo.ValidatePaymentType(paymentType))  // Send Reponse to Validator
            {
                Console.WriteLine("payment type invalid.  we only accept visa or mastercard.");
                paymentType = Console.ReadLine();
            }
            payment.Type = paymentType;
        }

        private void requestPaymentActNumber() // Ask for payment account number
        {
            AccountNumberValid repo = new AccountNumberValid();
            Console.WriteLine("Enter Payment Account Number" + "\n"
                    + "> ");
            paymentAccountNumber = Console.ReadLine();

            while (!repo.ValidatePaymentAccountNumber(paymentAccountNumber))  // Send Reponse to Validator
            {
                Console.WriteLine("Account number invalid.  Please input ####-####-####-####.");
                paymentAccountNumber = Console.ReadLine();
            }
            payment.Account = paymentAccountNumber;

        }

        // public void addNewPayment(Customer activeCustomer, string paymentType, string paymentAccountNumber)
        //{
          //  PaymentRepository newPayment = new PaymentRepository();
            //newPayment.AddPayment(activeCustomer.CustomerId, paymentType, paymentAccountNumber);
        //}

    }
}
