namespace datiClienti
{
    public interface IGestoreC
    {
        void AggiungiCliente( Cliente nuovoCliente);
        List<Cliente> CercaCliente(string parametroRicerca, string tipoRicerca);
    }
}
