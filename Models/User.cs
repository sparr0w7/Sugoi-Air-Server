using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SugoiAirServer.Models
{
    public class User
    {
        public int uNo { get; set; }
        public string uId { get; set; }
        public string uName { get; set; }
        public string uPassword { get; set; }

        internal AppDb Db { get; set; }

        public User() { }

        internal User(AppDb db)
        {
            Db = db;
        }

    }
}
