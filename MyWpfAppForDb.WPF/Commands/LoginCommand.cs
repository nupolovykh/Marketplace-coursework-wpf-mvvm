using MyWpfAppForDb.EntityFramework;
using MyWpfAppForDb.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyWpfAppForDb.WPF.Commands
{
    public class LoginCommand : CommandBase
    {
        private AppDbContext _marketPlaceContext;

        public LoginCommand(AppDbContext db)
        {
            _marketPlaceContext = db;
        }

        public override void Execute(object parameter)
        {
            _marketPlaceContext.SaveChanges();
        }
    }
}
