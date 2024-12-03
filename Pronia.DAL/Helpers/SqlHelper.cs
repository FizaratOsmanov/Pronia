using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronia.DAL.Helpers
{
    public static class SqlHelper
    {
        private static readonly string _connectionStr = @"Server=FIZARET;Database=ProniaDB;Trusted_Connection=True;TrustServerCertificate=True;";


        public static string GetConnectionString()
        {
            return _connectionStr;
        }
    }
}
