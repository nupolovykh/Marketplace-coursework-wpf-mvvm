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
        private MarketPlaceContext _marketPlaceContext;

        public LoginCommand(MarketPlaceContext db)
        {
            _marketPlaceContext = db;
        }

        public override void Execute(object parameter)
        {
            _marketPlaceContext.SaveChanges();
        }
    }
}
