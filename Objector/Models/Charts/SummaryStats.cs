namespace Objector.Models.Charts
{
    public class SummaryStats
    {
        public List<ChartData> ChartsData { get; set; }
        public double AverageTime { get; set; }
        public double Effectiveness { get; set; }
    }

    public class ChartData
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public string ChartType { get; set; }

        public Tuple<List<string>, List<int>> Data { get; set; }
    }
}
