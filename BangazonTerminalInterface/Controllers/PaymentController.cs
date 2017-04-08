using BangazonTerminalInterface.DataValidation.CustomerValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Controllers
{
    class PaymentController
    {
        string paymentType; //To Capture the payment type
        int paymentAccountNumber; //To capture the account number
        string newPayment; //To build the new payment that will be added to the DB
        
        private void requestPaymentType() // Ask for payment Type
        {
            PaymentTypeValid repo = new PaymentTypeValid();
            Console.WriteLine("Enter Payment Type" + "\n"
                    + "> ");
            paymentType = Console.ReadLine();
        }
        
        ValidatePaymentType(string paymentType); // Send Reponse to Validator

       
        private void requestPaymentAccountNumber() // Ask for account number
        {
            PaymentTypeValid repo = new PaymentTypeValid();
            Console.WriteLine("Enter Payment Account Number" + "\n"
                    + "> ");
            paymentAccountNumber = Console.ReadLine();
        }

        
        ValidatePaymentAccountNumber(int paymentAccountNumber); // Send Reponse to Validator

    }
}
