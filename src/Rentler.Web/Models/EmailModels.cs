using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Models
{
    public class EmailForgotPasswordModel
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }
}