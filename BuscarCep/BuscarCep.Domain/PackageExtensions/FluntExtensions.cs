using BuscarCep.Domain.Enum;
using Flunt.Notifications;

namespace BuscarCep.Domain.PackageExtensions
{
    public static class FluntExtensions
    {
        public static void AddNotification<T>(this Notifiable<T> notifiable, ENotificationMessage notification, string key = "") where T : Notification
        {
            notifiable.AddNotification(key, notification.ToString());
        }
    }
}
