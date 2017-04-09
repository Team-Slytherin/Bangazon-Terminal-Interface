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
using BangazonTerminalInterface.Helpers;

namespace BangazonTerminalInterface.Controllers
{
    class PaymentController
    {
        //To add new payment option
        Payment payment = new Payment();
        public  PaymentController(Customer customer)
        {
            payment.CustomerId = customer.CustomerId;
            Console.Clear();
            requestPaymentType();
            Console.Clear();
            requestPaymentActNumber();
        }

        private void requestPaymentType() // Ask for payment Type
        {
            PaymentTypeValid repo = new PaymentTypeValid();
            var paymentType = Helper.WriteToConsole("Enter Payment Type" + "\n" + "> ");

            while (!repo.ValidatePaymentType(paymentType))  // Send Reponse to Validator
            {
                paymentType = Helper.WriteToConsole("payment type invalid.  we only accept visa or mastercard." + "\n" + "> ");
            }
            payment.PaymentType = paymentType;
        }

        private void requestPaymentActNumber() // Ask for payment account number
        {
            AccountNumberValid repo = new AccountNumberValid();
            var paymentAccountNumber = Helper.WriteToConsole("Enter Payment Account Number" + "\n" + "> ");

            while (!repo.ValidatePaymentAccountNumber(paymentAccountNumber))  // Send Reponse to Validator
            {
                paymentAccountNumber = Helper.WriteToConsole("Account number invalid.  Please input 16 digits." + "\n" + "> ");
            }
            payment.PaymentAccountNumber = Convert.ToInt64(paymentAccountNumber);
        }

        public void addNewPayment()
        {
            PaymentRepository newPayment = new PaymentRepository();
            newPayment.AddPayment(payment.CustomerId, payment.PaymentType, payment.PaymentAccountNumber);
        }
    }
}
