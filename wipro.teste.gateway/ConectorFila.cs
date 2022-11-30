using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace wipro.teste.gateway
{
    public class ConectorFila : IConectorFila
    {
        private readonly ItemFilaOutput _itemFilaOutput;

        public ConectorFila()
        {
            _itemFilaOutput = new ItemFilaOutput();
        }

        public Task<ItemFilaOutput> Obter()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqps://gczcjjgl:Xuwg2LsCKrNv-XiuiDrYs50UVB2agaWo@jackal.rmq.cloudamqp.com/gczcjjgl")
            };

            var itemLista = new ItemFila();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("ItemQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var result = channel.BasicGet(queue: "ItemQueue", autoAck: true);
                if (result == null)
                {
                    _itemFilaOutput.AddNotification("Sem itens na fila");
                    return Task.FromResult(_itemFilaOutput);
                }

                var message = Encoding.UTF8.GetString(result.Body.ToArray());

                itemLista = JsonConvert.DeserializeObject<ItemFila>(message);
                _itemFilaOutput.ItemFila = itemLista;

                return Task.FromResult(_itemFilaOutput);
            }
        }

        public Task Postar(IList<ItemFila> itens)
        {
            var factory = new ConnectionFactory ()
            {
                Uri = new Uri("amqps://gczcjjgl:Xuwg2LsCKrNv-XiuiDrYs50UVB2agaWo@jackal.rmq.cloudamqp.com/gczcjjgl")
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("ItemQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                itens.ToList().ForEach(item =>
                {
                    string itemSerializado = JsonConvert.SerializeObject(item);
                    var body  = Encoding.UTF8.GetBytes(itemSerializado);

                    var tasks = new List<Task>();
                    tasks.Add(Task.Run(() => channel.BasicPublish(exchange: "", routingKey: "ItemQueue", basicProperties: null, body: body)));
                    Task.WaitAll(tasks.ToArray());
                });
            }

            return Task.CompletedTask;
        }
    }
}