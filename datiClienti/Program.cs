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
            Console.WriteLine("3. Modifica cliente");
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

                    // Legge la scelta dell'utente dall'input della console (scelta che poi passerò al metodo)
                    string scelta = (Console.ReadLine());

                    //imposta il tipo di ricerca(riscrivendo sulla variabile "scelta") in base alla scelta dell'utente
                    switch (scelta)
                    {
                        case "1":
                            scelta = "ID";
                            break;
                        case "2":
                            scelta = "Nome";
                            break;
                        case "3":
                            scelta = "Cognome";
                            break;
                        case "4":
                            scelta = "Citta";
                            break;
                        case "5":
                            scelta = "Sesso";
                            break;
                        case "6":
                            scelta = "DataDiNascita";
                            break;
                    }

                    // Legge il parametro di ricerca dall'input della console
                    Console.WriteLine("Scrivi l'informazione da cercare:");
                    string parametroRicerca = Console.ReadLine();

                    // Chiama il metodo CercaCliente ed inserisce il risultato nella lista "clientiTrovati"
                    List<Cliente> clientiOut = gestore.CercaCliente(parametroRicerca, scelta);

                    // Controlla se la lista dei clienti trovati è vuota
                    if (clientiOut.Count > 0)
                    {
                        Console.WriteLine("Clienti trovati:");
                        // Stampa le informazioni di ogni cliente trovato nella lista
                        foreach (Cliente cliente in clientiOut)
                        {
                            Console.WriteLine(cliente.ToRead());
                        }
                    }
                    else
                    {
                        // Se la lista dei clienti trovati è vuota, stampa un messaggio per informare l'utente
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
                    ///////////////
                case 3:
                    Console.Write("Inserisci l'ID del cliente da modificare: ");
                    string idCliente = Console.ReadLine();

                    Cliente clienteDaModificare = gestore.CercaCliente(idCliente, "ID").FirstOrDefault();

                    if (clienteDaModificare == null)
                    {
                        Console.WriteLine("Cliente non trovato.");
                        break;
                    }

                    Console.WriteLine("Inserisci le nuove informazioni del cliente o premi Invio per mantenere le informazioni attuali:");

                    Console.Write($"Inserisci il nuovo nome del cliente ({clienteDaModificare.Nome}): ");
                    string nuovoNome = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuovoNome))
                    {
                        nuovoNome = clienteDaModificare.Nome;
                    }

                    Console.Write($"Inserisci il nuovo cognome del cliente ({clienteDaModificare.Cognome}): ");
                    string nuovoCognome = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuovoCognome))
                    {
                        nuovoCognome = clienteDaModificare.Cognome;
                    }

                    Console.Write($"Inserisci la nuova città del cliente ({clienteDaModificare.Citta}): ");
                    string nuovaCitta = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuovaCitta))
                    {
                        nuovaCitta = clienteDaModificare.Citta;
                    }

                    Console.Write($"Inserisci il nuovo sesso del cliente ({clienteDaModificare.Sesso}): ");
                    string nuovoSesso = Console.ReadLine().ToUpper();
                    if (string.IsNullOrEmpty(nuovoSesso))
                    {
                        nuovoSesso = clienteDaModificare.Sesso;
                    }
                    else if (nuovoSesso != "M" && nuovoSesso != "F")
                    {
                        Console.WriteLine("Sesso non valido. Inserisci 'M' o 'F'.");
                        return;
                    }

                    Console.Write($"Inserisci la nuova data di nascita del cliente ({clienteDaModificare.DataDiNascita:dd/MM/yyyy}): ");
                    string nuovaDataInserita = Console.ReadLine();

                    DateTime nuovaDataDiNascita = clienteDaModificare.DataDiNascita;
                    if (!string.IsNullOrEmpty(nuovaDataInserita))
                    {
                        if (!DateTime.TryParseExact(nuovaDataInserita, new[] { "ddMMyyyy", "dd/MM/yyyy" },
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out nuovaDataDiNascita))
                        {
                            Console.WriteLine("Formato data non valido. Riprova.");
                            return;
                        }
                    }

                    Cliente clienteModificato = new Cliente(idCliente, nuovoNome, nuovoCognome, nuovaCitta, nuovoSesso, nuovaDataDiNascita);
                    gestore.ModificaCliente(idCliente, clienteModificato);
                    Console.WriteLine("Cliente modificato con successo.");

                    break;
                    ////////////////
                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    break;
            }
        }
    }
}