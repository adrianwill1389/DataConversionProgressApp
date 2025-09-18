using System.ComponentModel.DataAnnotations;

namespace DataConversionProgressApp.Models
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime? LastSeenAnnouncementTime { get; set; }


    }


}
