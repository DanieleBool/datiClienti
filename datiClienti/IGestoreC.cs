namespace DatiClienti
{
    public interface IGestoreC
    {
        void Esegui();
        void AggiungiCliente(string filePercorso);
        void CercaCliente(string filePercorso, string parametroRicerca);
    }
}
