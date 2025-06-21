namespace DataConversionProgressApp.Models
{
    public class CourtProgress
    {
        internal bool Court2CaseManagement;

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
                return (int)(count / 4.0 * 100);
            }
        }

        public string Status => Progress == 100 ? "Completed" : "In Progress";
        public string User { get; set; }  // Who checked it
    }

}
