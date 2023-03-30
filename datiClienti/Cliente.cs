using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace datiClienti
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Citta { get; set; }
        public string Sesso { get; set; }
        public DateTime DataDiNascita { get; set; }

        public override string ToString()
        {
            return $"{ID};{Nome};{Cognome};{Citta};{Sesso};{DataDiNascita:dd/MM/yyyy}";
        }
    }
}
