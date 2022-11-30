namespace wipro.teste.gateway
{
    public interface IConectorFila
    {
        Task Postar(IList<ItemFila> itens);

        Task<ItemFilaOutput> Obter();
    }
}
