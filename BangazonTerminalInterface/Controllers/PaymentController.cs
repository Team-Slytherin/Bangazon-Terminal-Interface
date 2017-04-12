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
using System.Threading;
using System.Diagnostics;

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
            PaymentTypeValid repo = new PaymentTypeValid();
                     
            ENTERTYPE:
            _consoleHelper.WriteHeaderToConsole("Payment Type");
            _consoleHelper.WriteExitCommand();
            string[] paymentOptions = new string[] { "Visa", "MasterCard", "Discover", "American Express" };
            int counter = 1;
            foreach (var option in paymentOptions)
            {
                _consoleHelper.WriteLine($"{counter}. {option} ");
                counter++;
            }
            string input = _consoleHelper.WriteAndReadFromConsole("Enter Payment Type > ");

            bool userContinue = _consoleHelper.CheckForUserExit(input);

            if (userContinue)
            {
                UserContinue = false;
                return;
            }

            string selectedPaymentType = "";

            try

            {
                if (!(input.Equals("")) && Convert.ToInt32(input) <= paymentOptions.Count())
                {
                    selectedPaymentType = paymentOptions[Convert.ToInt32(input) - 1];
                }

                else
                {
                    _consoleHelper.WriteLine("Invalid input, please select an option from the menu above.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto ENTERTYPE;
                }

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                _consoleHelper.WriteLine("Invalid payment type");
                Thread.Sleep(1000);
                Console.Clear();
                goto ENTERTYPE;
            }

            if (!repo.ValidatePaymentType(input))
            {

            }
            payment.PaymentType = selectedPaymentType;
        }

        private void requestPaymentActNumber()
        {
            Console.Clear();
            _consoleHelper.WriteHeaderToConsole("Account Number");

            AccountNumberValid repo = new AccountNumberValid();
            _consoleHelper.WriteExitCommand();

            EnterAccount:
            string input = _consoleHelper.WriteAndReadFromConsole("Enter Account Number > ");

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
            payment.PaymentAccountNumber = Convert.ToInt64(input.Replace("-",""));
        }

        public void addPaymentToDb()
        {
            PaymentRepository newPayment = new PaymentRepository();
            newPayment.AddPayment(payment.CustomerId, payment.PaymentType, payment.PaymentAccountNumber);
            IsComplete = true;

        }
    }
}
