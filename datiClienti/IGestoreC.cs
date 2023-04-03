namespace datiClienti
{
    public interface IGestoreC
    {
        void AggiungiCliente( Cliente nuovoCliente, string filePercorso);
        void CercaCliente(string filePercorso, string parametroRicerca);
    }
}
