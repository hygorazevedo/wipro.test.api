namespace wipro.teste.gateway
{
    public class ItemFilaOutput
    {
        public ItemFila ItemFila { get; set; }
        public IList<string> Notifications { get; }
        public bool HasNotifications { get; private set; }

        public ItemFilaOutput()
        {
            ItemFila = new ItemFila();
            Notifications = new List<string>();
        }

        public void AddNotification(string message)
        {
            Notifications.Add(message);
            HasNotifications = true;
        }
    }
}
