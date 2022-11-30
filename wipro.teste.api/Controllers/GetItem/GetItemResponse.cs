using wipro.teste.api.Controllers.Shared;

namespace wipro.teste.api.Controllers.GetItem
{
    public class GetItemResponse : Notification
    {
        public string Moeda { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
