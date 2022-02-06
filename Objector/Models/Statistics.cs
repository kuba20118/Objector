namespace Objector.Models
{
    public class Statistics
    {
        public Guid Id { get; set; }
        public Guid ImageId { get; set; }
        public IList<string> FoundObjects { get; set; }
        public Feedback FeedbackFromUser { get; set; }
        public int NumberOfObjectsFound { get; set; }
        public long Time { get; set; }
        public int CritMistakes { get; set; }
        public int AllMistakes { get; set; }

        public Statistics()
        {

        }

        public Statistics(Guid imageId, IList<string> description, long time, Feedback feedback)
        {
            FoundObjects = new List<string>();
            Id = Guid.NewGuid();
            Time = time;
            SetImageId(imageId);
            SetFoundObjects(description);
            SetFeedback(feedback);
        }

        private void SetImageId(Guid imageId)
        {
            ImageId = imageId;
        }

        private void SetFeedback(Feedback feedback)
        {
            FeedbackFromUser = feedback;
            CritMistakes += (feedback.Incorrect + feedback.NotFound);
            AllMistakes += (CritMistakes + feedback.IncorrectBox + feedback.MultipleFound);
        }

        private void SetFoundObjects(IList<string> description)
        {
            if (description.Count == 0 || description == null)
            {
                NumberOfObjectsFound = 0;
                FoundObjects = null;
                return;
            }

            foreach (var obj in description)
            {
                var key = obj.Split('(').First();
                FoundObjects.Add(key);
            }

            NumberOfObjectsFound = description.Count;
        }
    }
}
