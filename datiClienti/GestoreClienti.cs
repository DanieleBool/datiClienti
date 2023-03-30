//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;


//namespace datiClienti
//{
//    internal class GestoreClienti
//    {
//        private string _filePercorso;
//        private List<Cliente> _clienti;

//        public GestoreClienti(string filePercorso)
//        {
//            _filePercorso = filePercorso;
//            _clienti = new List<Cliente>();
//        }

//        public void CaricaClienti()
//        {
//            _clienti.Clear();
//            using (StreamReader sr = new StreamReader(_filePercorso)) 
//            {
//                string riga;
//                while ((riga = sr.ReadLine()) != null)
//                {
//                    string[] dati = riga.Split(';');
//                    int id = int.Parse(dati[0]);
//                    string nome = dati[1];
//                    string cognome = dati[2];
//                    string città = dati[3];
//                    char sesso = char.Parse(dati[4]);
//                    DateTime dataDiNascita = DateTime.Parse(dati[5]);
//                    Cliente cliente = new Cliente(id, nome, cognome, città, sesso, dataDiNascita);
//                    _clienti.Add(cliente);
//                }
//            }
//        }

//        public void SalvaClienti()
//        {
//            using (StreamWriter sw = new StreamWriter(_filePercorso))
//            {
//                foreach (Cliente cliente in _clienti)
//                {
//                    sw.WriteLine(cliente.ToFileString());
//                }
//            }
//        }

//        public Cliente CercaCliente(int id)
//        {
//            return _clienti.Find(c => c.ID == id);
//        }

//        public List<Cliente> CercaClienti(int tipoRicerca, string valoreRicerca)
//        {
//            List<Cliente> risultati = new List<Cliente>();

//            switch (tipoRicerca)
//            {
//                case 1: // ID
//                    int id = int.Parse(valoreRicerca);
//                    Cliente cliente = CercaCliente(id);
//                    if (cliente != null)
//                    {
//                        risultati.Add(cliente);
//                    }
//                    break;
//                case 2: // Nome
//                    risultati = _clienti.FindAll(c => c.Nome.Equals(valoreRicerca, StringComparison.OrdinalIgnoreCase));
//                    break;
//                case 3: // Cognome
//                    risultati = _clienti.FindAll(c => c.Cognome.Equals(valoreRicerca, StringComparison.OrdinalIgnoreCase));
//                    break;
//                case 4: // Data di nascita
//                    DateTime dataDiNascita = DateTime.Parse(valoreRicerca);
//                    risultati = _clienti.FindAll(c => c.DataDiNascita.Date == dataDiNascita.Date);
//                    break;
//                case 5: // Luogo
//                    risultati = _clienti.FindAll(c => c.Città.Equals(valoreRicerca, StringComparison.OrdinalIgnoreCase));
//                    break;
//                default:
//                    Console.WriteLine("Tipo di ricerca non valido");
//                    break;
//            }

//            return risultati;
//        }

//        public void AggiungiCliente(Cliente nuovoCliente)
//        {
//            _clienti.Add(nuovoCliente);
//        }

//    }
//}