﻿// See https://aka.ms/new-console-template for more information
//using datiClienti;
//using System.Globalization;
//using System.Runtime.ConstrainedExecution;
//using System.Text;


//class Program
//{
//    static void Main(string[] args)
//    {
//        //INDICO DA QUALE FILE PRENDERE I DATI
//        string filePercorso = "C:\\Users\\d.dieleuterio\\source\\repos\\datiClienti\\datiClienti\\clienti.txt";
//        List<Cliente> clienti = CaricaClienti(filePercorso);

//        //CHIEDO ALL'UTENTE SE VUOLE CERCARE O AGGIUNGERE UN CLIENTE, GESTISCO IL TUTTO CON UN SWICH-CASE E DELLE FUNZIONI CHE CREERO' AL DIFUORI DEL MAIN
//        while (true)
//        {
//            Console.WriteLine("Scegli un'opzione:");
//            Console.WriteLine("1. Cerca cliente");
//            Console.WriteLine("2. Aggiungi cliente");
//            Console.Write("Inserisci il numero dell'opzione: ");

//            int opzione = int.Parse(Console.ReadLine());

//            switch (opzione)
//            {
//                case 1:
//                    Console.WriteLine("Inserisci un parametro di ricerca (ID, nome, cognome, città o data di nascita): ");
//                    string parametroRicerca = Console.ReadLine();
//                    List<Cliente> clientiTrovati = CercaCliente(clienti, parametroRicerca);

//                    if (clientiTrovati.Count > 0)
//                    {
//                        Console.WriteLine("Clienti trovati:");
//                        foreach (Cliente cliente in clientiTrovati)
//                        {
//                            Console.WriteLine(cliente.ToRead());
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("Nessun cliente trovato con il parametro di ricerca fornito.");
//                    }
//                    break;

//                case 2:
//                    AggiungiCliente(clienti, filePercorso);
//                    break;

//                default:
//                    Console.WriteLine("Opzione non valida. Riprova.");
//                    break;
//            }
//        }
//    }

//    //LA FUNZIONE CARICACLIENTI CARICA I DATI FAL FILE.TXT E LI MEMORIZZA NELLA NUOVA LISTA CLIENTI CHE SARA' POI UTILIZZATA PER AGGIUNGERE, CERCARE E VISUALIZZARE I CLIENTI
//    static List<Cliente> CaricaClienti(string filePercorso)
//    {
//        List<Cliente> clienti = new List<Cliente>();
//        using (StreamReader sr = new StreamReader(filePercorso))
//        {
//            string line;
//            while ((line = sr.ReadLine()) != null)
//            {
//                string[] parti = line.Split(';');
//                clienti.Add(new Cliente(parti[0], parti[1], parti[2], parti[3], parti[4],
//                    DateTime.ParseExact(parti[5], "dd/MM/yyyy", null)));
//            }
//        }
//        return clienti;
//    }

//    static void AggiungiCliente(List<Cliente> clienti, string filePercorso)
//    {
//        Console.Write("Inserisci l'ID del cliente: ");
//        string id = Console.ReadLine();

//        Console.Write("Inserisci il nome del cliente: ");
//        string nome = Console.ReadLine();

//        Console.Write("Inserisci il cognome del cliente: ");
//        string cognome = Console.ReadLine();

//        Console.Write("Inserisci la città del cliente: ");
//        string citta = Console.ReadLine();

//        Console.Write("Inserisci il sesso del cliente (M/F): ");
//        string sesso = Console.ReadLine();

//        Console.Write("Inserisci la data di nascita del cliente (formato: dd/MM/yyyy): ");
//        string dataInserita = Console.ReadLine();

//        DateTime dataDiNascita;
//        //GRAZIE ALL'IF SEGUENTE POSSO SCRIVERE LA DATA ANCHE IN FORMATO GGMMNNN, LASCIO COMMENTATO IL METODO COMUNE
//        if (DateTime.TryParseExact(dataInserita, new[] { "ddMMyyyy", "dd/MM/yyyy" },
//            CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDiNascita))
//        //DateTime dataDiNascita = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
//        {
//            Cliente nuovoCliente = new Cliente(id, nome, cognome, citta, sesso, dataDiNascita)
//            {
//                ID = id,
//                Nome = nome,
//                Cognome = cognome,
//                Citta = citta,
//                Sesso = sesso,
//                DataDiNascita = dataDiNascita
//            };

//            clienti.Add(nuovoCliente);

//            // SALVA IL CLIENTE
//            using (StreamWriter sw = new StreamWriter(filePercorso, true, Encoding.UTF8))
//            {
//                sw.WriteLine(nuovoCliente.ToWrite());
//            }
//            Console.WriteLine("Cliente aggiunto con successo.");
//        }
//        else
//        {
//            Console.WriteLine("Formato data non valido. Riprova.");
//        }
//    }

//    //   DateTime.Compare(cliente.DataDiNascita.Date, DateTime.Parse(parametroRicerca).Date) == 0)
//    //   cliente.DataDiNascita.Date == DateTime.ParseExact(parametroRicerca, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date)

//    static List<Cliente> CercaCliente(List<Cliente> clienti, string parametroRicerca)
//    {
//        List<Cliente> clientiTrovati = new List<Cliente>();
//        // Dichiarazione della variabile "dataDiNascita" che sarà utilizzata per contenere la data di nascita cercata.
//        DateTime dataDiNascita;
//        // La funzione "DateTime.TryParseExact" viene utilizzata per verificare se il parametro di ricerca inserito dall'utente può essere convertito in un oggetto DateTime. Restituisce un valore booleano che indica se la conversione è stata effettuata con successo e assegna il valore convertito alla variabile "dataDiNascita".
//        bool isDate = DateTime.TryParseExact(parametroRicerca, new[] { "dd/MM/yyyy", "ddMMyyyy" },
//                CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDiNascita);

//        foreach (Cliente cliente in clienti)
//        {
//            //StringComparison.OrdinalIgnoreCase E' UTILIZZATO PER CONFRONTARE STRINGHE SENZA TENER CONTO DELLE MAIUSCIOLE E DELLE MINUSCOLE
//            //CON EQUALS CONTROLLO CHE I VARI PARAMENTRI SIANO UGUALI AL PARAMETRO IN INPUT
//            if (cliente.ID.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
//                cliente.Nome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
//                cliente.Cognome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
//                cliente.Citta.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
//                cliente.Sesso.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
//                (isDate && DateTime.Compare(cliente.DataDiNascita.Date, dataDiNascita.Date) == 0))
//            {
//                clientiTrovati.Add(cliente);
//            }
//        }

//        return clientiTrovati;
//    }
//}






using datiClienti;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string filePercorso = "C:\\Users\\d.dieleuterio\\source\\repos\\datiClienti\\datiClienti\\clienti.txt";
        GestoreClienti gestoreClienti = new GestoreClienti(filePercorso);

        while (true)
        {
            Console.WriteLine("Scegli un'opzione:");
            Console.WriteLine("1. Cerca cliente");
            Console.WriteLine("2. Aggiungi cliente");
            Console.Write("Inserisci il numero dell'opzione: ");

            int opzione = int.Parse(Console.ReadLine());

            switch (opzione)
            {
                case 1:
                    Console.WriteLine("Inserisci un parametro di ricerca (ID, nome, cognome, città o data di nascita): ");
                    string parametroRicerca = Console.ReadLine();
                    gestoreClienti.CercaCliente(filePercorso, parametroRicerca);
                    break;

                case 2:
                    gestoreClienti.AggiungiCliente(filePercorso);
                    break;

                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    break;
            }
        }
    }
}