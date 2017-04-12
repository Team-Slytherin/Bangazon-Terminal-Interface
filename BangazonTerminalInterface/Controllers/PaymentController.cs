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
        ConsoleHelper _consoleHelper;
        public PaymentController ()
        {
            _consoleHelper = new ConsoleHelper();
        }

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
            _consoleHelper.WriteHeaderToConsole("Payment Type");

            PaymentTypeValid repo = new PaymentTypeValid();
            _consoleHelper.WriteExitCommand();

        EnterType:
            string input = _consoleHelper.WriteAndReadFromConsole("Enter Payment Type > ");

            bool userContinue = _consoleHelper.CheckForUserExit(input);

            if (userContinue)
            {
                UserContinue = false;
                return;
            }

            if (!repo.ValidatePaymentType(input))
            {
                _consoleHelper.WriteAndReadFromConsole("Invalid input." + "/n" + "We only accept visa / mastercard / discover / american express." + "\n" + "> ");
                goto EnterType;
            }
            payment.PaymentType = input;
        }

        private void requestPaymentActNumber()
        {
            _consoleHelper.WriteHeaderToConsole("Account Number");

            AccountNumberValid repo = new AccountNumberValid();
            _consoleHelper.WriteExitCommand();

            EnterAccount:
            string input = _consoleHelper.WriteAndReadFromConsole("Enter Payment Type > ");

            bool userContinue = _consoleHelper.CheckForUserExit(input);

            if (userContinue)
            {
                UserContinue = false;
                return;
            }

            if (!repo.ValidatePaymentAccountNumber(input))
            {
                _consoleHelper.WriteAndReadFromConsole("Invalid input." + "\n" + "Please input 16 digits in this format" + "\n" + "0000-0000-0000-0000." + "\n" + "> ");
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
