namespace datiClienti
{
    public interface IGestoreC
    {
        void MenuC();
        void AggiungiCliente(string filePercorso);
        void CercaCliente(string filePercorso, string parametroRicerca);
    }
}
