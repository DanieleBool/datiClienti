using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace datiClienti
{
    class Cliente
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Citta { get; set; }
        public string Sesso { get; set; }
        public DateTime DataDiNascita { get; set; }

        public Cliente(string id, string nome, string cognome, string citta, string sesso, DateTime dataDiNascita)
        {
            ID = id;
            Nome = nome;
            Cognome = cognome;
            Citta = citta;
            Sesso = sesso;
            DataDiNascita = dataDiNascita;
        }
        public string ToRead()
        {
            return $"ID: {ID}\nNome: {Nome}\nCognome: {Cognome}\nCittà: {Citta}\nSesso: {Sesso}\nData di Nascita: {DataDiNascita:dd/MM/yyyy}";
            // "\n" serve per andare a capo.
        }

        public string ToWrite()
        {
            return $"{ID};{Nome};{Cognome};{Citta};{Sesso};{DataDiNascita:dd/MM/yyyy}";
        }
    }
}
