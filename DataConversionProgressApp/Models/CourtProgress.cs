namespace DataConversionProgressApp.Models
{
    public class CourtProgress
    {
        public DateTime DateReceived { get; set; }
        public bool Court1Disposed { get; set; }
        public bool Court1Warrant { get; set; }
        public bool Court2Disposed { get; set; }
        public bool Court2Warrant { get; set; }

        public int Progress
        {
            get
            {
                int count = 0;
                if (Court1Disposed) count++;
                if (Court1Warrant) count++;
                if (Court2Disposed) count++;
                if (Court2Warrant) count++;
                return count * 25;
            }
        }

        public string Status
        {
            get
            {
                if (Progress == 0) return ""; // Show blank if nothing ticked
                if (Progress == 100) return "Completed";
                return "In Progress";
            }
        }
    }

}
