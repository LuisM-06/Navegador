using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navegador
{
    internal class Direccion
    {

        String url;
        int veces;
        DateTime fechaAcceseso;

        public string Url { get => url; set => url = value; }
        public int Veces { get => veces; set => veces = value; }
        public DateTime FechaAcceseso { get => fechaAcceseso; set => fechaAcceseso = value; }
    }
}
