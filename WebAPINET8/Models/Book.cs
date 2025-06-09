using System.ComponentModel.DataAnnotations;

namespace WebAPINET8.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Title { get; set; }

        public string Author { get; set; }

        [Range(0, 999999.99, ErrorMessage = "El precio debe ser un valor entre 0 y 999999.99.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }
    }
}
