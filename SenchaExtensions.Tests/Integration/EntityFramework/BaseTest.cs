using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions.Tests.Integration.EntityFramework
{
    public abstract class BaseTest
    {
        public void Init(out ApplicationDbContext db)

        {
            // Set the |DataDirectory| path used in connection strings to point to the correct directory for console app and migrations
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", baseDirectory);

            db = new ApplicationDbContext();
            db.Database.EnsureCreated();
        }
    }
}
