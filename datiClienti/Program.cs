﻿// See https://aka.ms/new-console-template for more information
using ClientiLibrary;
using AssemblyGestore;
using System;
using System.Globalization;
using System.IO;


class Program
{
    static void Main(string[] args)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string connectionDB;

        connectionDB = "server=127.0.0.1;uid=root;" +
            "pwd=Kondor99$;port=3306;database=clienti_db";

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = connectionDB;
            conn.Open();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.WriteLine("Connessione non riuscita.");
        }



        //string connectionDB = "Server=localhost;Database=clienti_db;port=3306;Uid=root;Pwd=Kondor99$;";
        IGestoreC gestore = new GestoreClienti(connectionDB);


        while (true)
        {
            Console.WriteLine("Scegli un'opzione:");
            Console.WriteLine("1. Cerca cliente");
            Console.WriteLine("2. Aggiungi cliente");
            Console.WriteLine("3. Modifica cliente");
            Console.WriteLine("4. Elimina cliente");
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
                        using (StreamReader sr = new StreamReader(connectionDB))
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

                    //crca nel file tramite il metodo CercaCliente con il paramentro di scelta inpostato su "ID" e usa FirstOrDefault() per estrarre il primo oggetto Cliente trovato nel file o null se la lista è vuota.
                    Cliente clienteDaModificare = gestore.CercaCliente(idCliente, "ID").FirstOrDefault();

                    if (clienteDaModificare == null)
                    {
                        Console.WriteLine("Cliente non trovato.");
                        break;
                    }
                    else
                    {
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
                        // trasforma la stringa in data e la aggiunge al nuovo oggetto
                        DateTime nuovaDataDiNascita = clienteDaModificare.DataDiNascita;
                        // se la data è vuota
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
                    }
                case 4:
                    Console.WriteLine("Inserisci l'ID del cliente da eliminare");
                    string outID = Console.ReadLine();
                    bool eliminato = gestore.EliminaCliente(outID);
                    if (eliminato)
                    {
                        Console.WriteLine("Cliente eliminato con successo.");
                    }
                    else
                    {
                        Console.WriteLine("Cliente non trovato o impossibile eliminarlo.");
                    }
                    break;   
                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    break;


                    //case 3:
                    //    static string RichiediInformazione(string messaggio, string valoreDefault)
                    //    {
                    //        Console.Write($"{messaggio} ({valoreDefault}): ");
                    //        string valoreInserito = Console.ReadLine();
                    //        return string.IsNullOrEmpty(valoreInserito) ? valoreDefault : valoreInserito;
                    //    }


                    //    Console.Write("Inserisci l'ID del cliente da modificare: ");
                    //    string idCliente = Console.ReadLine();

                    //    Cliente clienteDaModificare = gestore.CercaCliente(idCliente, "ID").FirstOrDefault();

                    //    if (clienteDaModificare == null)
                    //    {
                    //        Console.WriteLine("Cliente non trovato.");
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Inserisci le nuove informazioni del cliente o premi Invio per mantenere le informazioni attuali:");

                    //        string nuovoNome = RichiediInformazione("Inserisci il nuovo nome del cliente", clienteDaModificare.Nome);
                    //        string nuovoCognome = RichiediInformazione("Inserisci il nuovo cognome del cliente", clienteDaModificare.Cognome);
                    //        string nuovaCitta = RichiediInformazione("Inserisci la nuova città del cliente", clienteDaModificare.Citta);

                    //        string nuovoSesso = RichiediInformazione("Inserisci il nuovo sesso del cliente", clienteDaModificare.Sesso).ToUpper();
                    //        while (nuovoSesso != "M" && nuovoSesso != "F")
                    //        {
                    //            Console.WriteLine("Sesso non valido. Inserisci 'M' o 'F'.");
                    //            nuovoSesso = RichiediInformazione("Inserisci il nuovo sesso del cliente", clienteDaModificare.Sesso).ToUpper();
                    //        }

                    //        string nuovaDataInserita = RichiediInformazione("Inserisci la nuova data di nascita del cliente", clienteDaModificare.DataDiNascita.ToString("dd/MM/yyyy"));
                    //        DateTime nuovaDataDiNascita = clienteDaModificare.DataDiNascita;
                    //        while (!string.IsNullOrEmpty(nuovaDataInserita) && !DateTime.TryParseExact(nuovaDataInserita, new[] { "ddMMyyyy", "dd/MM/yyyy" },
                    //                    CultureInfo.InvariantCulture, DateTimeStyles.None, out nuovaDataDiNascita))
                    //        {
                    //            Console.WriteLine("Formato data non valido. Riprova.");
                    //            nuovaDataInserita = RichiediInformazione("Inserisci la nuova data di nascita del cliente", clienteDaModificare.DataDiNascita.ToString("dd/MM/yyyy"));
                    //        }

                    //        Cliente clienteModificato = new Cliente(idCliente, nuovoNome, nuovoCognome, nuovaCitta, nuovoSesso, nuovaDataDiNascita);
                    //        gestore.ModificaCliente(idCliente, clienteModificato);
                    //        Console.WriteLine("Cliente modificato con successo.");

                    //        break;
                    //    }
                    //default:
                    //    Console.WriteLine("Opzione non valida. Riprova.");
                    //    break;

            }
        }
    }
}