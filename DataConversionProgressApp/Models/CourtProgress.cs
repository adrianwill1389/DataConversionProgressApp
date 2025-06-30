namespace DataConversionProgressApp.Models
{
    public class CourtProgress
    {
        public DateTime DateReceived { get; set; }
        public bool Court1Disposed { get; set; }
        public string Court1DisposedBy { get; set; }
        public bool Court1Warrant { get; set; }
        public string Court1WarrantBy { get; set; }
        public bool Court2Disposed { get; set; }
        public string Court2DisposedBy { get; set; }
        public bool Court2Warrant { get; set; }
        public string Court2WarrantBy { get; set; }
        public bool Court1Night { get; set; }
        public string Court1NightBy { get; set; }
        public bool Court2Night { get; set; }
        public string Court2NightBy { get; set; }
        public bool Court3Night { get; set; }
        public string Court3NightBy { get; set; }
    

public int Progress
{
    get
    {
        int count = 0;

        if (Court1Disposed) count++;
        if (Court1Warrant) count++;
        if (Court2Disposed) count++;
        if (Court2Warrant) count++;
        if (Court1Night) count++;
        if (Court2Night) count++;
        if (Court3Night) count++;

        // If any night court checkbox is ticked, calculate using 3-night-court fields
        if (Court1Night || Court2Night || Court3Night)
        {
            return (int)Math.Round((double)count / 3 * 100);
        }

        // Otherwise, calculate using 4 Day court fields
        return count * 25;
    }
}



public string Status
        {
            get
            {
                if (Progress == 0) return "";
                if (Progress == 100) return "Completed";
                return "In Progress";
            }
        }


    }

}
