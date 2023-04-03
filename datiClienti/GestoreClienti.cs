﻿using datiClienti;
using System.Globalization;
using System.Text;

public class GestoreClienti : IGestoreC
{
    // Campo privato che memorizza il percorso del file dei clienti
    private string _filePercorso;
    // Passa per l'interfaccia, la uso in CercaClienti per restituire l'oggetto trovato
    public Cliente ClienteTrovato { get; set; }

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

    public bool CercaCliente(string parametroRicerca)
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

                // TryParse tenta di convertire il parametro di ricerca in un oggetto DateTime se il parametro di ricerca è una data valida, isDataDiNascita sarà true e parametroDataDiNascita conterrà la data
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
                    clienteTrovato = true;
                    ClienteTrovato = cliente;
                    return true;
                }
            }
        }
        return false;

    }

}
