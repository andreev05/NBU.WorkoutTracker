﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NBU.WorkoutTracker.Core.ViewModels
{
    public class CreateUserViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
