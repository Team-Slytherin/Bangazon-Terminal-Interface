﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.CustomerValidation
{
    class ZipValid
    {
        public bool ValidateZip(string zip)
        {

            String s = "123456";
            String regex = "\\d{5}";

            // bug in regex you can enter 5555e
            bool isNumeric = Regex.IsMatch(zip, @"[0-9]");
            if (zip.Length == 5 && isNumeric)
                return true;
            else
                return false;
        }
    }
}
