namespace datiClienti
{
    public interface IGestoreC
    {
        void AggiungiCliente(string filePercorso);
        void CercaCliente(string filePercorso, string parametroRicerca);
    }
}
