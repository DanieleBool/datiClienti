// See https://aka.ms/new-console-template for more information
using datiClienti;
using System.Globalization;
using System.Text;


class Program
{
    static void Main(string[] args)
    {
        string filePercorso = "C:\\Users\\d.dieleuterio\\source\\repos\\datiClienti\\datiClienti\\clienti.txt";

        List<Cliente> clienti = CaricaClienti(filePercorso);

        while (true)
        {
            Console.WriteLine("Scegli un'opzione:");
            Console.WriteLine("1. Cerca cliente");
            Console.WriteLine("2. Aggiungi cliente");
            Console.WriteLine("3. Salva e esci");
            Console.Write("Inserisci il numero dell'opzione: ");

            int opzione = int.Parse(Console.ReadLine());

            switch (opzione)
            {
                case 1:
                    Console.WriteLine("Inserisci un parametro di ricerca (ID, nome, cognome, città o data di nascita): ");
                    string parametroRicerca = Console.ReadLine();
                    List<Cliente> clientiTrovati = CercaCliente(clienti, parametroRicerca);

                    if (clientiTrovati.Count > 0)
                    {
                        Console.WriteLine("Clienti trovati:");
                        foreach (Cliente cliente in clientiTrovati)
                        {
                            Console.WriteLine(cliente.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nessun cliente trovato con il parametro di ricerca fornito.");
                    }
                    break;

                case 2:
                    AggiungiCliente(clienti, filePercorso);
                    break;

                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    break;
            }
        }
    }

    
    static List<Cliente> CaricaClienti(string filePercorso)
    {
        List<Cliente> clienti = new List<Cliente>();
        using (StreamReader sr = new StreamReader(filePercorso))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parti = line.Split(';');
                clienti.Add(new Cliente
                {
                    ID = int.Parse(parti[0]),
                    Nome = parti[1],
                    Cognome = parti[2],
                    Citta = parti[3],
                    Sesso = parti[4],
                    DataDiNascita = DateTime.ParseExact(parti[5], "dd/MM/yyyy", null)
                });
            }
        }

        return clienti;
    }

    static void AggiungiCliente(List<Cliente> clienti, string filePercorso)
    {
        Console.Write("Inserisci l'ID del cliente: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Inserisci il nome del cliente: ");
        string nome = Console.ReadLine();

        Console.Write("Inserisci il cognome del cliente: ");
        string cognome = Console.ReadLine();

        Console.Write("Inserisci la città del cliente: ");
        string citta = Console.ReadLine();

        Console.Write("Inserisci il sesso del cliente (M/F): ");
        string sesso = Console.ReadLine();

        Console.Write("Inserisci la data di nascita del cliente (formato: dd/MM/yyyy): ");
        string dataInserita = Console.ReadLine();

        DateTime dataDiNascita;
        if (DateTime.TryParseExact(dataInserita, new[] { "ddMMyyyy", "dd/MM/yyyy" },
            CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDiNascita))

        //DateTime dataDiNascita = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
        { 
        Cliente nuovoCliente = new Cliente
        {
            ID = id,
            Nome = nome,
            Cognome = cognome,
            Citta = citta,
            Sesso = sesso,
            DataDiNascita = dataDiNascita
        };

        clienti.Add(nuovoCliente);


        // Salva il cliente nel file direttamente nella funzione AggiungiCliente
        using (StreamWriter sw = new StreamWriter(filePercorso, true, Encoding.UTF8))
        {
            sw.WriteLine(nuovoCliente.ToString());
        }

        Console.WriteLine("Cliente aggiunto con successo.");
        }
        else
        {
            Console.WriteLine("Formato data non valido. Riprova.");
        }
    }


    static List<Cliente> CercaCliente(List<Cliente> clienti, string parametroRicerca)
    {
        List<Cliente> clientiTrovati = new List<Cliente>();

        foreach (Cliente cliente in clienti)
        {
            if (cliente.ID.ToString() == parametroRicerca ||
                cliente.Nome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                cliente.Cognome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                cliente.Citta.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                cliente.Sesso.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                cliente.DataDiNascita.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase))
            {
                clientiTrovati.Add(cliente);
            }
        }

        return clientiTrovati;
    }



}