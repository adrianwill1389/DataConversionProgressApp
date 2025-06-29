using System.ComponentModel.DataAnnotations;

namespace DataConversionProgressApp.Models
{
    public class CourtProgressRecord
    {
        public DateTime DateReceived { get; set; }
        public string CourtType { get; set; }

        // Day court
        public bool Court1Disposed { get; set; }
        public string Court1DisposedBy { get; set; }

        public bool Court1Warrant { get; set; }
        public string Court1WarrantBy { get; set; }

        public bool Court2Disposed { get; set; }
        public string Court2DisposedBy { get; set; }

        public bool Court2Warrant { get; set; }
        public string Court2WarrantBy { get; set; }

        // Night court
        public bool Court1Night { get; set; }
        public string Court1NightBy { get; set; }

        public bool Court2Night { get; set; }
        public string Court2NightBy { get; set; }

        public bool Court3Night { get; set; }
        public string Court3NightBy { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }

        [Key]
        public int Id { get; set; }


    }




}
