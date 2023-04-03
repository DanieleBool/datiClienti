namespace datiClienti
{
    public interface IGestoreC
    {
        //Cliente ClienteTrovato { get; set; }

        void AggiungiCliente( Cliente nuovoCliente);
        Cliente CercaCliente(string parametroRicerca);
    }
}
