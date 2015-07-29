using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarterKit.ViewModels
{
    public class IndexUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public bool EmailConfirmed { get; set; }
    }

    public class DetailUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}