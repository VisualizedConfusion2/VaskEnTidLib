using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLib.Repositories
{
    public class ErrorTypeRepo
    {
        private readonly string _connectionString;

        public ErrorTypeRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

    }
}
