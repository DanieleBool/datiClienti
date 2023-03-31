using datiClienti;
using System.Globalization;
using System.Text;

public class GestoreC
{
    // Campo privato che memorizza il percorso del file dei clienti
    private string _filePercorso;

    // Costruttore che accetta il percorso come argomento
    public GestoreC(string filePercorso)
    {
        _filePercorso = filePercorso;
    }

    public void Esegui()
    {
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
                    CercaCliente(_filePercorso, parametroRicerca);
                    break;

                case 2:
                    AggiungiCliente(_filePercorso);
                    break;

                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    break;
            }
        }
    }
    public void AggiungiCliente(string filePercorso)
    {
        Console.Write("Inserisci l'ID del cliente: ");
        string id = Console.ReadLine();

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
        // Tenta di convertire la data inserita in un oggetto DateTime
        if (DateTime.TryParseExact(dataInserita, new[] { "ddMMyyyy", "dd/MM/yyyy" },
            CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDiNascita))
        {
            // Crea un nuovo oggetto Cliente con i dettagli forniti
            Cliente nuovoCliente = new Cliente(id, nome, cognome, citta, sesso, dataDiNascita);

            // Aggiunge il nuovo cliente al file dei clienti
            using (StreamWriter sw = new StreamWriter(_filePercorso, true, Encoding.UTF8))
            {
                sw.WriteLine(nuovoCliente.ToWrite());
            }
            Console.WriteLine("Cliente aggiunto con successo.");
        }
        else
        {
            Console.WriteLine("Formato data non valido. Riprova.");
        }
    }

    public void CercaCliente(string filePercorso, string parametroRicerca)
    {
        bool clienteTrovato = false;
        // Apre il file dei clienti per la lettura
        using (StreamReader sr = new StreamReader(_filePercorso))
        {
            string line;
            // Legge il file riga per riga
            while ((line = sr.ReadLine()) != null)
            {
                // Divide la riga letta in parti separate dai caratteri ';'
                string[] parti = line.Split(';');
                // Crea un nuovo oggetto Cliente a partire dalle parti lette
                Cliente cliente = new Cliente(parti[0], parti[1], parti[2], parti[3], parti[4],
                    DateTime.ParseExact(parti[5], "dd/MM/yyyy", null));

                // Tenta di convertire il parametro di ricerca in un oggetto DateTime se il parametro di ricerca è una data valida, isDataDiNascita sarà true e parametroDataDiNascita conterrà la data
                bool isDataDiNascita = DateTime.TryParse(parametroRicerca, out DateTime parametroDataDiNascita);
                // StringComparison.OrdinalIgnoreCase per un confronto case-insensitive
                if (cliente.ID.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    cliente.Nome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    cliente.Cognome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    cliente.Citta.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    cliente.Sesso.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    //se le due date sono uguali restituisce 0
                    (isDataDiNascita && DateTime.Compare(cliente.DataDiNascita, parametroDataDiNascita) == 0))
                {
                    Console.WriteLine(cliente.ToRead());
                    clienteTrovato = true;
                }
            }
        }

        if (!clienteTrovato)
        {
            Console.WriteLine("Nessun cliente trovato con il parametro di ricerca fornito.");
        }
    }

}
