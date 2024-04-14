using System;
using System.Collections.Generic;

namespace AppWPF.Database
{
    public partial class User
    {
        public int UserId { get; set; }
        public string? Password { get; set; }
        public string? Login { get; set; }
        public string? Role { get; set; }
    }
}
