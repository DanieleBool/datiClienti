//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
        //using System.Globalization;
        //using System.Text;

//namespace datiClienti
//{
//    internal class GestoreClienti
//    {
//        private string _filePercorso;
//        private List<Cliente> _clienti;

//        public GestoreClienti(string filePercorso)
//        {
//            _filePercorso = filePercorso;
//            _clienti = new List<Cliente>();
//        }

//LA FUNZIONE CARICACLIENTI CARICA I DATI FAL FILE.TXT E LI MEMORIZZA NELLA NUOVA LISTA CLIENTI CHE SARA' POI UTILIZZATA PER AGGIUNGERE, CERCARE E VISUALIZZARE I CLIENTI

            //static List<Cliente> CaricaClienti(string filePercorso)
            //{
            //    List<Cliente> clienti = new List<Cliente>();
            //    using (StreamReader sr = new StreamReader(filePercorso))
            //    {
            //        string line;
            //        while ((line = sr.ReadLine()) != null)
            //        {
            //            string[] parti = line.Split(';');
            //            clienti.Add(new Cliente(parti[0], parti[1], parti[2], parti[3], parti[4],
            //                DateTime.ParseExact(parti[5], "dd/MM/yyyy", null)));
            //        }
            //    }

            //    return clienti;
            //}

            //static void AggiungiCliente(List<Cliente> clienti, string filePercorso)
            //{
            //    Console.Write("Inserisci l'ID del cliente: ");
            //    string id = Console.ReadLine();

            //    Console.Write("Inserisci il nome del cliente: ");
            //    string nome = Console.ReadLine();

            //    Console.Write("Inserisci il cognome del cliente: ");
            //    string cognome = Console.ReadLine();

            //    Console.Write("Inserisci la città del cliente: ");
            //    string citta = Console.ReadLine();

            //    Console.Write("Inserisci il sesso del cliente (M/F): ");
            //    string sesso = Console.ReadLine();

            //    Console.Write("Inserisci la data di nascita del cliente (formato: dd/MM/yyyy): ");
            //    string dataInserita = Console.ReadLine();

            //    DateTime dataDiNascita;
            //    //GRAZIE ALL'IF SEGUENTE POSSO SCRIVERE LA DATA ANCHE IN FORMATO GGMMNNN, LASCIO COMMENTATO IL METODO COMUNE
            //    if (DateTime.TryParseExact(dataInserita, new[] { "ddMMyyyy", "dd/MM/yyyy" },
            //        CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDiNascita))
            //    //DateTime dataDiNascita = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
            //    {
            //        Cliente nuovoCliente = new Cliente(id, nome, cognome, citta, sesso, dataDiNascita)
            //        {
            //            ID = id,
            //            Nome = nome,
            //            Cognome = cognome,
            //            Citta = citta,
            //            Sesso = sesso,
            //            DataDiNascita = dataDiNascita
            //        };

            //        clienti.Add(nuovoCliente);

            //        // SALVA IL CLIENTE
            //        using (StreamWriter sw = new StreamWriter(filePercorso, true, Encoding.UTF8))
            //        {
            //            sw.WriteLine(nuovoCliente.ToWrite());
            //        }
            //        Console.WriteLine("Cliente aggiunto con successo.");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Formato data non valido. Riprova.");
            //    }
            //}

            //static List<Cliente> CercaCliente(List<Cliente> clienti, string parametroRicerca)
            //{
            //    List<Cliente> clientiTrovati = new List<Cliente>();

            //    foreach (Cliente cliente in clienti)
            //    {
            //        if (cliente.ID.ToString() == parametroRicerca ||
            //            //StringComparison.OrdinalIgnoreCase E' UTILIZZATO PER CONFRONTARE STRINGHE SENZA TENER CONTO DELLE MAIUSCIOLE E DELLE MINUSCOLE
            //            //CON EQUALS CONTROLLO CHE I VARI PARAMENTRI SIANO UNUALI AL PARAMETRO IN INPUT
            //            cliente.Nome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
            //            cliente.Cognome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
            //            cliente.Citta.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
            //            cliente.Sesso.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
            //            //
            //            DateTime.Compare(cliente.DataDiNascita.Date, DateTime.Parse(parametroRicerca).Date) == 0)
            //        //cliente.DataDiNascita.Date == DateTime.ParseExact(parametroRicerca, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date)
            //        {
            //            clientiTrovati.Add(cliente);
            //        }
            //    }

            //    return clientiTrovati;
            //}
//        }

//    }
//}