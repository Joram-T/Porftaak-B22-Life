using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.Exception
{
    class SluitBestellingException : System.Exception
    {
        public SluitBestellingException()
        {
        }

        public SluitBestellingException(string message) : base(message)
        {
        }

        public SluitBestellingException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
