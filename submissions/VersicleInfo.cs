namespace myMLApp.DataModels
{
    public class VersicleInfo
    {
        public VersicleInfo(string abbrev, int chapter, int startvers, int? endvers)
        {
            BookAbbreviation = abbrev;
            BookChapter = chapter;
            StartVersicle = startvers;
            EndVersicle = endvers;
        }

        public string BookAbbreviation { get; set;}

        public int BookChapter { get; set; }

        public int StartVersicle { get; set; }

        public int? EndVersicle { get; set; }
    }
}