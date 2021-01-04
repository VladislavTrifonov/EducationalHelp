namespace EducationalHelp.Core.Entities
{
    public enum Mark
    {
        None, 
        Excellent,
        Good,
        Satisfactory,
        Unsatisfactory,
        Poor
    }

    public static class MarkExtension
    {
        public static uint GetDigitOfMark(this Mark mark)
        {
            if (mark == Mark.None)
                return 0;

            return 6 - (uint)mark;
        }
    }
}