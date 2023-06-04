using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;
using MiniTodo.Models;

namespace MiniTodo.ViewModel
{
    public class CreateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; } = null!;

        public Todo MapTo()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o título da tarefa")
                .IsGreaterThan(Title, 5, "O título deve conter mais de 5 caracteres.");
            
            AddNotifications(contract);

            //return new Todo(Guid.NewGuid(), Title, false);
            return new Todo();
        }
    }
}