using System.ComponentModel;

namespace GatherUp.Models
{
    public class Event
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Lokacja")]
        public string Location { get; set; }
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        [DisplayName("Obrazek")]
        public string Image {  get; set; }

    }
}
