namespace Objector.Models
{
    public class ImageX
    {
        public Guid Id { get; set; }
        public byte[] ImageOriginal { get; set; }
        public byte[] ImageProcessed { get; set; }
        public IList<string> Description { get; set; }
        public long ElapsedTime { get; set; }
        public DateTime Added { get; set; }

        public ImageX()
        {
        }

        public ImageX(Guid guid, byte[] imageOriginal, byte[] imageProcessed, IList<string> description, long time)
        {
            Id = guid;
            SetOriginalImage(imageOriginal);
            SetProcessedImage(imageProcessed);
            SetDescriptionList(description);
            SetElapsedTime(time);
            Added = DateTime.UtcNow;
        }

        private void SetElapsedTime(long time)
        {
            if (time > 0)
                ElapsedTime = time;
            else
                ElapsedTime = 0;
        }

        private void SetDescriptionList(IList<string> description)
        {
            Description = description;
        }

        private void SetProcessedImage(byte[] image)
        {
            ImageProcessed = image;
        }

        void SetOriginalImage(byte[] image)
        {
            ImageOriginal = image;
        }
    }
}
