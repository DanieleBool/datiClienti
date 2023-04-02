// See https://aka.ms/new-console-template for more information
using datiClienti;
using System;
using System.Globalization;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //string filePercorso = "C:\\Users\\d.dieleuterio\\source\\repos\\datiClienti\\datiClienti\\clienti.txt";
        string filePercorso = "C:\\Users\\danie\\Source\\Repos\\datiClienti\\datiClienti\\clienti.txt";

        //creo un istanza della classe GestoreClienti con il colegamento al file .txt
        IGestoreC gestore = new GestoreClienti(filePercorso);
        //Cliente cliente = new Cliente(filePercorso);

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
            //int opzione = int.Parse(Console.ReadLine());

            switch (opzione)
            {
                case 1:
                    Console.WriteLine("Inserisci un parametro di ricerca (ID, nome, cognome, città o data di nascita): ");
                    string parametroRicerca = Console.ReadLine();
                    gestore.CercaCliente(filePercorso, parametroRicerca);
                    break;

                case 2:
                    gestore.AggiungiCliente(filePercorso);
                    break;

                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    break;
            }
        }
    }
}