namespace datiClienti
{
    public interface IGestoreC
    {
        void AggiungiCliente( object nuovoCliente, string filePercorso);
        void CercaCliente(string filePercorso, string parametroRicerca);
    }
}
