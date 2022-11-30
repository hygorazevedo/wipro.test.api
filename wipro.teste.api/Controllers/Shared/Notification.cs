namespace wipro.teste.api.Controllers.Shared
{
    public class Notification: INotification
    {
        public IList<string> Notifications { get; internal set; }
        public bool HasNotifications { get; internal set; }

        public Notification()
        {
            Notifications = new List<string>();
        }

        public void AddNotification(string message)
        {
            Notifications.Add(message);
            HasNotifications = true;
        }
    }
}
