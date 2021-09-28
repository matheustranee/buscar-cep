using BuscarCep.Domain.Interfaces;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace BuscarCep.Domain.Bases
{
    public class Result<T> : IResult<T>
    {
        public Result()
        {
            Notifications = new List<Notification>();
        }

        public Result(T data, string message) : this()
        {
            Data = data;
            Message = message;
        }

        public Result(IReadOnlyCollection<Notification> notifications, string message) : this()
        {
            AddNotifications(notifications);
            Message = message;
        }

        [JsonIgnore]
        public T Data { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        [JsonIgnore]
        public List<Notification> Notifications { get; set; }

        #region Métodos

        public T GetData() => Data;

        public string GetMessage() => Message;

        public IEnumerable<Notification> GetNotifications() => Notifications;

        public void AddNotifications(IReadOnlyCollection<Notification> notifications) => Notifications.AddRange(notifications);

        public bool IsValid()
        {
            return !Notifications.Any();
        }

        #endregion
    }
}
