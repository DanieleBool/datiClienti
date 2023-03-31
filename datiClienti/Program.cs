// See https://aka.ms/new-console-template for more information
using DatiClienti;
using System.Globalization;
using System.Text;


class Program
{
    static void Main(string[] args)
    {
        string filePercorso = "C:\\Users\\d.dieleuterio\\source\\repos\\datiClienti\\datiClienti\\clienti.txt";
        IGestoreC gestoreClienti = new GestoreC(filePercorso);
        gestoreClienti.Esegui();
    }
}































//class Program
//{
//    static void Main(string[] args)
//    {
//        string filePercorso = "C:\\Users\\d.dieleuterio\\source\\repos\\datiClienti\\datiClienti\\clienti.txt";
//        //creo un istanza della classe GestoreClienti con il colegamento al file .txt
//        GestoreClienti gestoreClienti = new GestoreClienti(filePercorso);

//        while (true)
//        {
//            Console.WriteLine("Scegli un'opzione:");
//            Console.WriteLine("1. Cerca cliente");
//            Console.WriteLine("2. Aggiungi cliente");
//            Console.Write("Inserisci il numero dell'opzione: ");

//            // Legge l'input dell'utente e lo converte in un intero
//            int opzione = int.Parse(Console.ReadLine());

//            switch (opzione)
//            {
//                case 1:
//                    Console.WriteLine("Inserisci un parametro di ricerca (ID, nome, cognome, città o data di nascita): ");
//                    string parametroRicerca = Console.ReadLine();
//                    gestoreClienti.CercaCliente(parametroRicerca);
//                    break;

//                case 2:
//                    gestoreClienti.AggiungiCliente();
//                    break;

//                default:
//                    Console.WriteLine("Opzione non valida. Riprova.");
//                    break;
//            }
//        }
//    }
//}