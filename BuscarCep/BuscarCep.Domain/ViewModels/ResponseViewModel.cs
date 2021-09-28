using System.Collections.Generic;

namespace BuscarCep.Domain.ViewModels
{
    public class ResponseViewModel<T>
    {
        public ResponseViewModel()
        {
            Notifications = new List<NotificationViewModel>();
        }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public IEnumerable<NotificationViewModel> Notifications { get; set; }

        public T Data { get; set; }
    }
}
