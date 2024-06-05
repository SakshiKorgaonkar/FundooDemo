using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.CustomException
{
    public class CustomException1: Exception
    {
        public CustomException1(string message):base(message) { }
    }
}
