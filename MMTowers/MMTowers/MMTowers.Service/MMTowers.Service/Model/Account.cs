﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMTowers.Service.Model
{
    public class Account
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
