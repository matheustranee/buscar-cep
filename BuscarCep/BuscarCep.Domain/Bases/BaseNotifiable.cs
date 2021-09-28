using BuscarCep.Domain.Enum;
using BuscarCep.Domain.Interfaces;
using Flunt.Notifications;
using System.Collections.Generic;

namespace BuscarCep.Domain.Bases
{
    public abstract class BaseNotifiable : Notifiable<Notification>
    {
        public static IResult<TData> Success<TData>(TData data, string message = null)
        {
            message ??= "Operação realizada com sucesso.";

            return new Result<TData>(data, message);
        }

        public static IResult<TData> Error<TData>(IReadOnlyCollection<Notification> notifications, string message = null)
        {
            message ??= "Erro ao realizar a operação.";

            return new Result<TData>(notifications, message);
        }

        public IResult<TData> StopWithNotification<TData>(ENotificationMessage notification, string key = "")
        {
            AddNotification(key, notification.ToString());

            return Error<TData>(Notifications);
        }

        /// <summary>
        /// Une as notificações de um IResult com as notificações do Notifiable atual.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns>true quando está válido (sem notificações).</returns>
        public bool MergeNotificationsAndValidate<T>(IResult<T> result)
        {
            IEnumerable<Notification> notifications = result.GetNotifications();

            foreach (Notification notification in notifications)
                AddNotification(notification);

            return IsValid;
        }

    }
}
