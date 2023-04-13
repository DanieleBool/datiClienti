﻿// See https://aka.ms/new-console-template for more information
using ClientiLibrary;
using AssemblyGestore;
using AssemblyGestoreFile;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        // Carica gli assembly dinamicamente.
        Assembly assemblyGestore = Assembly.Load("AssemblyGestore");
        Assembly assemblyGestoreFile = Assembly.Load("AssemblyGestoreFile");
        Assembly clientiLibrary = Assembly.Load("ClientiLibrary");

        // Ottengo informazioni dalle classi
        Type gestoreClientiType = assemblyGestore.GetType("AssemblyGestore.GestoreClienti");
        Type gestoreFileClientiType = assemblyGestoreFile.GetType("AssemblyGestoreFile.GestoreFileClienti");

        // Creo le istanze delle classi //
        // CONNESSIONE DB
        string connectionDB = ConfigurationManager.AppSettings["DatabaseConnection"];
        // Invoca il costruttore gestoreClientiType(GestoreClienti)(che ha tutte le info della classe GS) tramite la stringa di connessione per creare l'istanza GestoreClienti
        object gestoreDatabaseInstance = Activator.CreateInstance(gestoreClientiType, connectionDB);

        // CONNESSIONE FILE
        string filePercorso = ConfigurationManager.AppSettings["FileConnection"];
        object gestoreFileInstance = Activator.CreateInstance(gestoreFileClientiType, filePercorso);

        IGestoreC gestore;
        int sceltaArchiviazione;
        do
        {
            Console.WriteLine("Scegli un metodo di archiviazione: \n");
            Console.WriteLine("1. Database");
            Console.WriteLine("2. File di testo");
            int.TryParse(Console.ReadLine(), out sceltaArchiviazione);
        } while (sceltaArchiviazione != 1 && sceltaArchiviazione != 2); // In base alla scelta dell'utente, usa l'istanza di "Gestore

        if (sceltaArchiviazione == 1)
        {
            // Converte le istanze(cast) create dinamicamente in oggetti che implementano l'interfaccia "IGestoreC" per poi assegnare l'oggetto alla scelta()
            gestore = (IGestoreC)Activator.CreateInstance(gestoreClientiType, connectionDB);
        }
        else
        {
            gestore = (IGestoreC)Activator.CreateInstance(gestoreFileClientiType, filePercorso);
        }

        while (true)
        {
            Console.WriteLine("Scegli un'opzione:\n");
            Console.WriteLine("1. Cerca cliente");
            Console.WriteLine("2. Aggiungi cliente");
            Console.WriteLine("3. Modifica cliente");
            Console.WriteLine("4. Elimina cliente");
            Console.Write("Inserisci il numero dell'opzione: ");
            //controllo input dell'opzione, legge l'input e lo converte in intero (la conversone in intero mi serve per dei controlli sulla scelta dell'opzione)
            int.TryParse(Console.ReadLine(), out int opzione);

            switch (opzione)
            {

                // CERCA CLIENTE //
                case 1:
                    try
                    {
                        Console.WriteLine("Scegli l'informazione da cercare:");
                        Console.WriteLine("1. ID");
                        Console.WriteLine("2. Nome");
                        Console.WriteLine("3. Cognome");
                        Console.WriteLine("4. Città");
                        Console.WriteLine("5. Sesso");
                        Console.WriteLine("6. Data di Nascita");

                        string scelta;
                        int sceltaInt;
                        do
                        {
                            scelta = Console.ReadLine();
                            int.TryParse(scelta, out sceltaInt);
                            if (sceltaInt < 1 || sceltaInt > 6)
                            {
                                Console.WriteLine("Inserisci un numero tra 1 e 6:");
                            }
                        } while (sceltaInt < 1 || sceltaInt > 6);

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

                        Console.WriteLine("Scrivi l'informazione da cercare:");
                        string parametroRicerca = Console.ReadLine();

                        List<Cliente> clientiOut = gestore.CercaCliente(parametroRicerca, scelta);

                        if (clientiOut.Count > 0)
                        {
                            Console.WriteLine("Clienti trovati:");
                            foreach (Cliente cliente in clientiOut)
                            {
                                Console.WriteLine(cliente.ToRead());
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Errore: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Errore generico: {ex.Message}");
                    }
                    break;

                // AGGIUNGI CLIENTE //
                case 2:
                    bool continuaAdAggiungereClienti = true;
                    while (continuaAdAggiungereClienti)
                    {
                        try
                        {
                            Console.Write("Inserisci l'ID del cliente: ");
                            string id = Console.ReadLine();
                            //if ((id.Length < 1) || (id.Length > 5) || (id == null))
                            //{
                            //    Console.WriteLine("L'ID del cliente deve essere di 5 caratteri.");
                            //    break;
                            //}

                            string nome;
                            do
                            {
                                Console.Write("Inserisci il nome del cliente: ");
                                nome = Console.ReadLine();
                            } while (string.IsNullOrEmpty(nome));

                            string cognome;
                            do
                            {
                                Console.Write("Inserisci il cognome del cliente: ");
                                cognome = Console.ReadLine();
                            } while (string.IsNullOrEmpty(cognome));

                            string citta;
                            do
                            {
                                Console.Write("Inserisci la città del cliente: ");
                                citta = Console.ReadLine();
                            } while (string.IsNullOrEmpty(citta));

                            string sesso = string.Empty;
                            while (string.IsNullOrEmpty(sesso))
                            {
                                Console.Write("Inserisci il sesso del cliente (M/F): ");
                                string sessoInput = Console.ReadLine().ToUpper();
                                if (sessoInput == "M" || sessoInput == "F")
                                {
                                    sesso = sessoInput;
                                }
                                else
                                {
                                    Console.WriteLine("Inserimento non valido. Inserisci 'M' o 'F'.");
                                }
                            }

                            DateTime dataDiNascita;
                            while (true)
                            {
                                Console.Write("Inserisci la data di nascita del cliente (formato: dd/MM/yyyy): ");
                                string dataInserita = Console.ReadLine();

                                string[] formatiData = { "dd/MM/yyyy", "dd-MM-yyyy", "ddMMyyyy" };
                                //L'array formatiData contiene i formati che verranno provati uno alla volta per verificare se la stringa inserita dall'utente corrisponde a uno di essi, metterdo dopo la irgola fa parte di DateTime.TryParseExact
                                if (!DateTime.TryParseExact(dataInserita, formatiData, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDiNascita))
                                {
                                    Console.WriteLine("Formato data non valido. Riprova.");
                                }
                                else
                                {
                                    break;
                                }
                            }

                            Cliente nuovoCliente = new Cliente(id, nome, cognome, citta, sesso, dataDiNascita);

                            gestore.AggiungiCliente(nuovoCliente);
                            Console.WriteLine("Cliente aggiunto con successo.");

                            //string dataDiNascita;
                            //do
                            //{
                            //    Console.Write("Inserisci la data di nascita del cliente (formato: dd/MM/yyyy): ");
                            //    dataDiNascita = Console.ReadLine();
                            //} while (!gestore.ValidaData(dataDiNascita));

                            //Cliente nuovoCliente = new Cliente(id, nome, cognome, citta, sesso, DateTime.ParseExact(dataDiNascita, "dd/MM/yyyy", CultureInfo.InvariantCulture));

                            //gestore.AggiungiCliente(nuovoCliente);
                            //Console.WriteLine("Cliente aggiunto con successo.");

                            // Chiedi all'utente se vuole continuare ad aggiungere clienti
                            Console.WriteLine("Premi \"Invio\" per aggiungere un'altro cliente o \"N\" per uscire ");
                            string continua = Console.ReadLine().ToUpper();
                            if (continua == "N")
                            {
                                continuaAdAggiungereClienti = false;
                            }
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Errore file: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Errore database: " + ex.Message);
                        }
                    }
                    break;

                // MODIFICA CLIENTE //
                case 3: 
                    Console.Write("Inserisci l'ID del cliente da modificare: ");
                    string idCliente = Console.ReadLine();
                    try
                    {
                        // Tramite Find  estraggo da CercaCliente il cliente con l'ID corrispondente alla ricerca(quindi lo estraggo ma non lo leggo in console come nel case1)
                        // crca nel file tramite il metodo CercaCliente con il paramentro di scelta inpostato su "ID" e usa FirstOrDefault() per estrarre il primo oggetto Cliente trovato nel file o null se la lista è vuota.
                        Cliente clienteDaModificare = gestore.CercaCliente(idCliente, "ID").Find(cliente => cliente.ID == idCliente); //.FirstOrDefault();

                        Console.WriteLine("Inserisci le nuove informazioni del cliente o premi Invio per mantenere le informazioni attuali:");

                        //Console.Write($"Inserisci il nuovo nome del cliente ({clienteDaModificare.Nome}): ");
                        //string nuovoNome = Console.ReadLine();
                        //if (string.IsNullOrEmpty(nuovoNome))
                        //{
                        //    nuovoNome = clienteDaModificare.Nome;
                        //}

                        //se l'utente non inserisce alcun valore per la nuova città, il codice mantiene il valore della città attuale del cliente. Se l'utente inserisce un nuovo valore, il codice aggiorna la città del cliente con il nuovo valore inserito.
                        Console.Write($"Inserisci il nuovo nome del cliente ({clienteDaModificare.Nome}): ");
                        string nuovoNome = Console.ReadLine();
                        nuovoNome = string.IsNullOrEmpty(nuovoNome) ? clienteDaModificare.Nome : nuovoNome;

                        Console.Write($"Inserisci il nuovo cognome del cliente ({clienteDaModificare.Cognome}): ");
                        string nuovoCognome = Console.ReadLine();
                        nuovoCognome = string.IsNullOrEmpty(nuovoCognome) ? clienteDaModificare.Cognome : nuovoCognome;

                        Console.Write($"Inserisci la nuova città del cliente ({clienteDaModificare.Citta}): ");
                        string nuovaCitta = Console.ReadLine();
                        nuovaCitta = string.IsNullOrEmpty(nuovaCitta) ? clienteDaModificare.Citta : nuovaCitta;

                        //SESSO
                        string nuovoSesso = clienteDaModificare.Sesso; // nuovoSesso sarà uguale al sesso originale, in questo modo non cambia se non nell'"else if"
                        while (true)
                        {
                            Console.Write($"Inserisci il nuovo sesso del cliente ({clienteDaModificare.Sesso}): ");
                            string inputSesso = Console.ReadLine().ToUpper();

                            if (string.IsNullOrEmpty(inputSesso)) // se l'input è vuoto mantiene l'info esistente
                            {
                                break;
                            }
                            else if (inputSesso == "M" || inputSesso == "F")
                            {
                                nuovoSesso = inputSesso; // Aggiorna il valore di nuovoSesso solo se l'input è valido
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Sesso non valido. Inserisci 'M' o 'F'.");
                            }
                        }

                        //DATA
                        Console.Write($"Inserisci la nuova data di nascita del cliente ({clienteDaModificare.DataDiNascita:dd/MM/yyyy}): ");
                        DateTime nuovaDataDiNascita = clienteDaModificare.DataDiNascita;

                        do
                        {
                            string nuovaDataInserita = Console.ReadLine();
                            if (string.IsNullOrEmpty(nuovaDataInserita))
                            {
                                break;
                            }

                            if (DateTime.TryParseExact(nuovaDataInserita, new[] { "ddMMyyyy", "dd/MM/yyyy" },
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out nuovaDataDiNascita))
                            {
                                break;
                            }
                            Console.WriteLine("Formato data non valido. Riprova.");

                        } while (true);

                        // Creo il nuovo oggetto
                        Cliente clienteModificato = new Cliente(idCliente, nuovoNome, nuovoCognome, nuovaCitta, nuovoSesso, nuovaDataDiNascita);

                        gestore.ModificaCliente(idCliente, clienteModificato); // Si avvia il metodo
                        Console.WriteLine("Cliente modificato con successo.");
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Database" + ex.Message);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Errore: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);  
                    }
                    
                    break;
                    

               // ELIMINA CLIENTE //
                case 4:
                    Console.WriteLine("Inserisci l'ID del cliente da eliminare");
                    string outID = Console.ReadLine();
                    try
                    {
                        // Se il Metodo restituisce true, "eliminato" sarà true e quindi non si passerà per il catch
                        bool eliminato = gestore.EliminaCliente(outID);
                        Console.WriteLine("Cliente eliminato con successo.");
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    break;
            }
        }
    }
}