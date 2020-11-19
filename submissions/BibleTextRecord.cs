namespace myMLApp.DataModels
{
    public class BibleTextRecord
    {
        public int VerseID { get; set; }

        public int BookID { get; set; }

        public int Chapter { get; set; }

        public int Versicle { get; set; }

        public string Text { get; set; }
    }
}