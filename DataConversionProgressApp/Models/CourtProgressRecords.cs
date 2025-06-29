namespace DataConversionProgressApp.Models
{
    public class CourtProgressRecord
    {
        public int Id { get; set; }
        public DateTime DateReceived { get; set; }
        public string CourtType { get; set; }

        public bool Court1Disposed { get; set; }
        public string Court1DisposedBy { get; set; }

        public bool Court1Warrant { get; set; }
        public string Court1WarrantBy { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
    }


}
