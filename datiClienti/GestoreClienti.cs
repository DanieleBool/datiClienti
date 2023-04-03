using datiClienti;
using System.Globalization;
using System.Text;
using System.Collections.Generic;

public class GestoreClienti : IGestoreC
{
    // Campo privato che memorizza il percorso del file dei clienti
    private string _filePercorso;

    // Costruttore che accetta il percorso come argomento
    public GestoreClienti(string filePercorso)
    {
        _filePercorso = filePercorso;
    }

    public void AggiungiCliente( Cliente nuovoCliente)
    {
        // Aggiunge il nuovo cliente al file dei clienti
        using (StreamWriter sw = new StreamWriter(_filePercorso, true, Encoding.UTF8))
        {
            sw.WriteLine(nuovoCliente.ToWrite());
        }
    }

    public List<Cliente> CercaCliente(string parametroRicerca, string scelta)
    {
        // Crea una nuova lista vuota per memorizzare i clienti trovati
        List<Cliente> clientiTrovati = new List<Cliente>();

        // Imposta il tipo di ricerca in base alla scelta dell'utente
        string tipoRicerca = "";
        switch (scelta)
        {
            case "1":
                tipoRicerca = "ID";
                break;
            case "2":
                tipoRicerca = "Nome";
                break;
            case "3":
                tipoRicerca = "Cognome";
                break;
            case "4":
                tipoRicerca = "Citta";
                break;
            case "5":
                tipoRicerca = "Sesso";
                break;
            case "6":
                tipoRicerca = "DataDiNascita";
                break;
        }

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

                // TryParse tenta di convertire il parametro di ricerca in un oggetto DateTime se il parametro di ricerca è una data valida, isDataDiNascita sarà true e parametroDataDiNascita conterrà la data
                bool isDataDiNascita = DateTime.TryParse(parametroRicerca, out DateTime parametroDataDiNascita);
                // Confronta il parametro di ricerca con le informazioni del cliente
                if ((tipoRicerca == "ID" && cliente.ID.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase)) ||
                (tipoRicerca == "Nome" && cliente.Nome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase)) ||
                (tipoRicerca == "Cognome" && cliente.Cognome.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase)) ||
                (tipoRicerca == "Citta" && cliente.Citta.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase)) ||
                (tipoRicerca == "Sesso" && cliente.Sesso.Equals(parametroRicerca, StringComparison.OrdinalIgnoreCase)) ||
                (tipoRicerca == "DataDiNascita" && isDataDiNascita && DateTime.Compare(cliente.DataDiNascita, parametroDataDiNascita) == 0))

                {
                    clientiTrovati.Add(cliente); // Aggiunge il cliente trovato alla lista dei clienti trovati
                }
            }
        }
        // Restituisce la lista dei clienti trovati
        return clientiTrovati;
    }

}
