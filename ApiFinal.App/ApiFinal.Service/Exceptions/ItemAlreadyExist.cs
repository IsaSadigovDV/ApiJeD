using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFinal.Service.Exceptions
{
    public class ItemAlreadyExist:Exception
    {
        public ItemAlreadyExist(string msg):base(msg)
        {

        }
    }
}
