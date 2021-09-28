using Flunt.Notifications;
using System.Collections.Generic;

namespace BuscarCep.Domain.Interfaces
{
    public interface IResult<TData>
    {
        IEnumerable<Notification> GetNotifications();

        void AddNotifications(IReadOnlyCollection<Notification> notifications);

        TData GetData();

        string GetMessage();

        public bool IsValid();
    }
}
