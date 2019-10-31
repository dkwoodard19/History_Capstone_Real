using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Mapper     //parent of all other mappers
    {
        public void Assert(bool condition, string message)
        {
            if (condition)
            {
                // expected; do nothing
            }
            else
            {
                throw new Exception(message);
            }
        }
    }
}
