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
    public class PaymentController
    {
        Payment payment = new Payment();

        private bool UserContinue = true;
        private bool IsComplete = false;

        public void RequestPayment(Customer customer)
        {
            payment.CustomerId = customer.CustomerId;
            while (!IsComplete)
            {
                requestPaymentType();
                if (!UserContinue) break;
                requestPaymentActNumber();
                if (!UserContinue) break;
                addPaymentToDb();
                if (!UserContinue) break;
            }
        }

        private void requestPaymentType() // Ask for payment Type
        {
            Helper.WriteHeaderToConsole("Payment Type");

            PaymentTypeValid repo = new PaymentTypeValid();
            Helper.WriteExitCommand();

        EnterType:
            string input = Helper.WriteToConsole("Enter Payment Type > ");

            bool userContinue = Helper.CheckForUserExit(input);

            if (userContinue)
            {
                UserContinue = false;
                return;
            }

            if (!repo.ValidatePaymentType(input))
            {
                Helper.WriteToConsole("Invalid input." + "/n" + "We only accept visa / mastercard / discover / american express." + "\n" + "> ");
                goto EnterType;
            }
            payment.PaymentType = input;
        }

        private void requestPaymentActNumber()
        {
            Helper.WriteHeaderToConsole("Account Number");

            AccountNumberValid repo = new AccountNumberValid();
            Helper.WriteExitCommand();

            EnterAccount:
            string input = Helper.WriteToConsole("Enter Payment Type > ");

            bool userContinue = Helper.CheckForUserExit(input);

            if (userContinue)
            {
                UserContinue = false;
                return;
            }

            if (!repo.ValidatePaymentAccountNumber(input))
            {
                Helper.WriteToConsole("Invalid input." + "\n" + "Please input 16 digits in this format" + "\n" + "0000-0000-0000-0000." + "\n" + "> ");
                goto EnterAccount;
            }
            payment.PaymentAccountNumber = Convert.ToInt64(input);
        }

        public void addPaymentToDb()
        {
            PaymentRepository newPayment = new PaymentRepository();
            newPayment.AddPayment(payment.CustomerId, payment.PaymentType, payment.PaymentAccountNumber);
            IsComplete = true;

        }
    }
}
