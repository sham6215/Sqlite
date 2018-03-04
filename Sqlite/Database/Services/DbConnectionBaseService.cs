using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Services
{
    public class DbConnectionBaseService
    {
        protected string GetDbSource(string path)
        {
            return $"Data Source={path};Version=3;foreign keys=true";
        }
    }
}
