using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations.Schema;



namespace DataConversionProgressApp.Models
{
    [Table("TickedItems")]
    public class TickedItem
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string TaskName { get; set; }
        public DateTime Date { get; set; }
        public DateTime Timestamp { get; set; }

    }




}
