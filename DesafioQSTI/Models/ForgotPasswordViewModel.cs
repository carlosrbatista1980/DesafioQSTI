using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioQSTI.Data.Repositories;

namespace DesafioQSTI.Models
{
    public class ForgotPasswordViewModel
    {
        public string PasswordForgottenEmail { get; set; }
        public DateTime PasswordForgottenRequesTime { get; set; }
        public string PasswordForgottenResponse { get; set; }


        public ForgotPasswordViewModel()
        {
            PasswordForgottenRequesTime = DateTime.Now;
        }
    }

    
}
