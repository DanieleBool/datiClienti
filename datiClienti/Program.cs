// See https://aka.ms/new-console-template for more information
using datiClienti;
using System;
using System.Globalization;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string filePercorso = "C:\\Users\\d.dieleuterio\\source\\repos\\datiClienti\\datiClienti\\clienti.txt";

        //creo un istanza della classe GestoreClienti con il colegamento al file .txt
        IGestoreC gestore = new GestoreClienti(filePercorso);

        while (true)
        {
            Console.WriteLine("Scegli un'opzione:");
            Console.WriteLine("1. Cerca cliente");
            Console.WriteLine("2. Aggiungi cliente");
            Console.Write("Inserisci il numero dell'opzione: ");
            //controllo input dell'opzione, legge l'input e lo converte in intero
            bool invalidInput = int.TryParse(Console.ReadLine(), out int opzione);
            if (!invalidInput)
            {
                Console.WriteLine("Inserimento non valido. Inserisci un numero.");
                continue;
            }

            switch (opzione)
            {
                case 1:
                    Console.WriteLine("Scegli l'informazione da cercare:");
                    Console.WriteLine("1. ID");
                    Console.WriteLine("2. Nome");
                    Console.WriteLine("3. Cognome");
                    Console.WriteLine("4. Città");
                    Console.WriteLine("5. Sesso");
                    Console.WriteLine("6. Data di Nascita");

                    string scelta = (Console.ReadLine());

                    Console.WriteLine("Scrivi l'informazione da cercare:");
                    string parametroRicerca = Console.ReadLine();

                    List<Cliente> clientiTrovati = gestore.CercaCliente(parametroRicerca, scelta);

                    if (clientiTrovati.Count > 0)
                    {
                        Console.WriteLine("Clienti trovati:");
                        foreach (Cliente cliente in clientiTrovati)
                        {
                            Console.WriteLine(cliente.ToRead());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nessun cliente trovato.");
                    }
                    break;

                case 2:
                    Console.Write("Inserisci l'ID del cliente: ");
                    string id = Console.ReadLine();
                    //controllo NOT NULL
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        Console.WriteLine("ID cliente non valido o già esistente. Inserisci un ID valido e univoco.");
                        return;
                    }
                    //controllo unique con reader WORK IN PROGRESS
                    if (IdEsistente(id))
                    {
                        Console.WriteLine("ID cliente già esistente. Inserisci un ID univoco.");
                        break;
                    }
                    bool IdEsistente(string id)
                    {
                        using (StreamReader sr = new StreamReader(filePercorso))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] parti = line.Split(';');
                                if (parti[0].Equals(id, StringComparison.OrdinalIgnoreCase))
                                {
                                    return true;
                                }
                            }
                        }
                        return false;
                    }
                    ////

                    Console.Write("Inserisci il nome del cliente: ");
                    string nome = Console.ReadLine();

                    Console.Write("Inserisci il cognome del cliente: ");
                    string cognome = Console.ReadLine();

                    Console.Write("Inserisci la città del cliente: ");
                    string citta = Console.ReadLine();

                    Console.Write("Inserisci il sesso del cliente (M/F): ");
                    string sesso = Console.ReadLine().ToUpper();
                    if (sesso != "M" && sesso != "F")
                    {
                        Console.WriteLine("Sesso non valido. Inserisci 'M' o 'F'.");
                        return;
                    }

                    Console.Write("Inserisci la data di nascita del cliente (formato: dd/MM/yyyy): ");
                    string dataInserita = Console.ReadLine();

                    DateTime dataDiNascita;
                    // Tenta di convertire la data inserita in un oggetto DateTime
                    if (DateTime.TryParseExact(dataInserita, new[] { "ddMMyyyy", "dd/MM/yyyy" },
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDiNascita))
                    {
                        // Crea un nuovo oggetto Cliente con i dettagli forniti
                        Cliente nuovoCliente = new Cliente(id, nome, cognome, citta, sesso, dataDiNascita);
                        //FUNZIONE
                        gestore.AggiungiCliente(nuovoCliente);
                        Console.WriteLine("Cliente aggiunto con successo.");
                    }
                    else
                    {
                        Console.WriteLine("Formato data non valido. Riprova.");
                    }
                    
                    break;

                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    break;
            }
        }
    }
}