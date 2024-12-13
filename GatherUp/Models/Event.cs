using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GatherUp.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa wydarzenia jest wymagana.")]
        [StringLength(100, MinimumLength = 3)]
        [DisplayName("Nazwa wydarzenia")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [StringLength(500, MinimumLength = 1)]
        [DisplayName("Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Lokacja jest wymagana.")]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("Lokacja")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Data jest wymagana.")]
        [DateNotInPast(ErrorMessage = "Data nie może być wcześniejsza niż dziś")]
        [DisplayName("Data")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Obrazek jest wymagany.")]
        [DisplayName("Obrazek")]
        public string Image {  get; set; }

        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

    }
}
