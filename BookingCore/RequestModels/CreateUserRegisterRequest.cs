using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingCore.RequestModels
{
    public class CreateUserRegisterRequest
    {
        //[Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Gender { get; set; }
        public string Role { get; set; }
        public bool WannabeManager { get; set; }
    }
}
