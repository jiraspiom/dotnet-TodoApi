using System.ComponentModel.DataAnnotations;

namespace TodoApi.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
