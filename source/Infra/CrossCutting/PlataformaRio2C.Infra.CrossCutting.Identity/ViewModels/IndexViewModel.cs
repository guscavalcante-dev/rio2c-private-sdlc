﻿using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }
}
