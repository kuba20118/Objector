namespace Objector.Models
{
    public class Result
    {
        public long ElapsedTime { get; set; }
        public byte[] ImageStringProcessed { get; set; }
        public byte[] ImageStringOriginal { get; set; }
        public IList<string> Description { get; set; }
    }
}
