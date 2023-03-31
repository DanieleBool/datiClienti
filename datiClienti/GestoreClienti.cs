using datiClienti;
using System.Globalization;
using System.Text;

public class GestoreClienti
{
    private string _filePercorso;

    public GestoreClienti(string filePercorso)
    {
        _filePercorso = filePercorso;
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
        if (DateTime.TryParseExact(dataInserita, new[] { "ddMMyyyy", "dd/MM/yyyy" },
            CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDiNascita))
        {
            Cliente nuovoCliente = new Cliente(id, nome, cognome, citta, sesso, dataDiNascita)
            {
                ID = id,
                Nome = nome,
                Cognome = cognome,
                Citta = citta,
                Sesso = sesso,
                DataDiNascita = dataDiNascita
            };

            using (StreamWriter sw = new StreamWriter(filePercorso, true, Encoding.UTF8))
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
        using (StreamReader sr = new StreamReader(filePercorso))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parti = line.Split(';');
                Cliente cliente = new Cliente(parti[0], parti[1], parti[2], parti[3], parti[4],
                    DateTime.ParseExact(parti[5], "dd/MM/yyyy", null));

                bool isDataDiNascita = DateTime.TryParse(parametroRicerca, out DateTime parametroDataDiNascita);

                if (cliente.ID.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    cliente.Nome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    cliente.Cognome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    cliente.Citta.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    cliente.Sesso.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase) ||
                    (isDataDiNascita && DateTime.Compare(cliente.DataDiNascita.Date, parametroDataDiNascita.Date) == 0))
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
