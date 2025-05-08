using BookStore.Base.Domain;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Domain.Entities
{
    public class Book: BaseEntity
    {
        [Required]
        public long GenreId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 2, ErrorMessage = "Author must be between 2 and 70 characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "ISBN must be 10 or 13 digits.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Page count is required.")]
        [Range(1, 2500, ErrorMessage = "Page count must be between 1 and 2500.")]
        public int PageCount { get; set; }

        [Required(ErrorMessage = "Published date is required.")]
        public DateTime PublishedDate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Publisher name cannot exceed 100 characters.")]
        public string Publisher { get; set; }


        [StringLength(1500, ErrorMessage = "Description cannot exceed 1500 characters.")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string CoverImageUrl { get; set; }

        public Genre Genre { get; set; }
    }
}
