namespace datiClienti
{
    public interface IGestoreC
    {
        void AggiungiCliente( Cliente nuovoCliente);
        List<Cliente> CercaCliente(string parametroRicerca, string scelta);
        void ModificaCliente(string id, Cliente clienteModificato);

    }
}
