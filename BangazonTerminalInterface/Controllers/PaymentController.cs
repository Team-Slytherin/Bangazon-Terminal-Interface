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
using BangazonTerminalInterface.Interfaces;
using BangazonTerminalInterface.Interfaces.PaymentValidationInterfaces;

namespace BangazonTerminalInterface.Controllers
{
    public class PaymentController
    {
        Payment payment = new Payment();
        private bool UserContinue = true;
        private bool IsComplete = false;
        private IConsoleHelper _consoleHelper;
        private IPaymentAccountValidation _paymentAccount;
        private IPaymentTypeValidation _paymentType;
        public PaymentController()
        {
            _consoleHelper = new ConsoleHelper();
            _paymentType = new PaymentTypeValidator();
            _paymentAccount = new PaymentAccountValidator();
        }

        public PaymentController(IPaymentTypeValidation typeValidator, IPaymentAccountValidation accountValidator, IConsoleHelper consoleHelper)
        {
            _consoleHelper = consoleHelper;
            _paymentType = typeValidator;
            _paymentAccount = accountValidator;
        }

        public Payment RequestPayment(Customer customer)
        {
            payment.CustomerId = customer.CustomerId;
            while (!IsComplete)
            {
                requestPaymentType();
                if (!UserContinue) break;
                requestPaymentActNumber();
                if (!UserContinue) break;
                addPaymentToDb();
                return payment;
            }
            return null;
        }

        private void requestPaymentType() // Ask for payment Type
        {
            PaymentTypeValidator repo = new PaymentTypeValidator();

            ENTERTYPE:
            Console.Clear();
            _consoleHelper.WriteHeaderToConsole("Payment Type");
            string[] paymentOptions = new string[] { "Visa", "MasterCard", "Discover", "American Express" };
            int counter = 1;
            foreach (var option in paymentOptions)
            {
                _consoleHelper.WriteLine($"{counter}. {option} ");
                counter++;
            }
            
            _consoleHelper.WriteExitCommand();

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
                    _consoleHelper.ErrorMessage("Invalid input, please select an option from the menu above.");
                    goto ENTERTYPE;
                }

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                _consoleHelper.ErrorMessage("Invalid payment type");
                goto ENTERTYPE;
            }

            if (!repo.ValidatePaymentType(input))
            {

            }
            payment.PaymentType = selectedPaymentType;
        }


        private void requestPaymentActNumber()
        {
            ENTERACCOUNT:
            Console.Clear();
            _consoleHelper.WriteHeaderToConsole("Account Number");

            PaymentAccountValidator repo = new PaymentAccountValidator();
            _consoleHelper.WriteExitCommand();

            string input = _consoleHelper.WriteAndReadFromConsole("Enter Account Number > ");

            bool userContinue = _consoleHelper.CheckForUserExit(input);

            if (userContinue)
            {
                UserContinue = false;
                return;
            }

            if (!repo.ValidatePaymentAccountNumber(input))
            {
                _consoleHelper.ErrorMessage("Invalid input. Enter 16 digits in this format" + "\n" + "0000-0000-0000-0000.");
                goto ENTERACCOUNT;
            }
            payment.PaymentAccountNumber = Convert.ToInt64(input.Replace("-", ""));
        }

        public void addPaymentToDb()
        {
            PaymentRepository newPayment = new PaymentRepository();
            newPayment.AddPayment(payment.CustomerId, payment.PaymentType, payment.PaymentAccountNumber);
            IsComplete = true;

        }
    }
}
