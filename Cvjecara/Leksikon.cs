using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    public interface ILeksikon
    {
        public bool ValidnoLatinskoIme(string ime);
    }

    public class Leksikon : ILeksikon
    {
        public bool ValidnoLatinskoIme(string ime)
        {
            if (ime.Equals("Rosa rubiginosa") || ime.Equals("Calendula officinalis") || 
                ime.Equals("Leucanthemum vulgare") || ime.Equals("Orchidaceae") || ime.Equals("Lilium bosniacum"))
                return true;
            return false;
        }
    }
}
