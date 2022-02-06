using Objector.Models;
using Objector.Models.Charts;
using Objector.Repositories;
using Objector.Repositories.Interfaces;
using Objector.Services.Interfaces;
using System;

namespace Objector.Services
{
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _statsRepository;
        private readonly IImagesRepository _imageRepository;
        private readonly IGeneralStatsRepository _generalStatsRepository;

        public StatsService(IStatsRepository statsRepository, IImagesRepository imageRepository, IGeneralStatsRepository generalStatsRepository)
        {
            _statsRepository = statsRepository;
            _imageRepository = imageRepository;
            _generalStatsRepository = generalStatsRepository;
        }

        public async Task AddStatsToImage(Guid id, Feedback stats)
        {
            var image = await _imageRepository.GetImageAsync(id);

            var statistics = new Statistics(id, image.Description, image.ElapsedTime, stats);
            await _statsRepository.AddAsync(statistics);
        }

        public Task<IEnumerable<Statistics>> GetAllAsync() => _statsRepository.GetAllAsync();

        public Task<Statistics> GetImageStatsAsync(Guid id) => _statsRepository.GetAsync(id);

        public async Task<SummaryStats> GetSummaryStats()
        {
            var generalStats = await _generalStatsRepository.GetAsync();
            var mistakes = new List<Tuple<string, int>>
            {
                new Tuple<string, int>("Niepoprawnie wykryty obiekt", generalStats.IncorrectObjectsDetections),
                new Tuple<string, int>("Nieznaleziony obiekt", generalStats.NotFoundObjects ),
                new Tuple<string, int>("Wielokrotnie znaleziony obiekt", generalStats.MultipleObjectsDetections ),
                new Tuple<string, int>("Niepoprawne zaznaczenie", generalStats.IncorrectBoxDetections)
            };
            mistakes.Sort((x, y) => y.Item2.CompareTo(x.Item2));

            var labelList = new List<string>();
            var valueList = new List<int>();
            foreach (var item in generalStats.ObjectsFound.Take(10))
            {
                labelList.Add(item.Key);
                valueList.Add(item.Value);
            }


            var correctAndAllMistakesChart = new ChartData
            {
                Title = "Bezbłędne wykrycia",
                Key = "correctAndAllMistakesChart",
                ChartType = "doughnut",
                Data = new Tuple<List<string>, List<int>>
                (
                    new List<string> { "Poprawne", "Niepoprawne" },
                    new List<int> { generalStats.CorrectObjectsDetections, generalStats.AllMistakes }
                )
            };

            var correctAndSmallMistakesChart = new ChartData
            {
                Title = "Wykrycia z małoistotnymi błędami",
                Key = "correctAndSmallMistakesChart",
                ChartType = "doughnut",
                Data = new Tuple<List<string>, List<int>>
                (
                    new List<string> { "Poprawne", "Niepoprawne" },
                    new List<int> { generalStats.CorrectObjectsDetections, generalStats.SmallMistakes }
                )
            };

            var topMistakes = new ChartData
            {
                Title = "Najczęsciej występujące błędy",
                Key = "topMistakes",
                ChartType = "Bar",
                Data = Unpack(mistakes)
            };

            var topFoundObjects = new ChartData
            {
                Title = "Najczęsciej wykrywane obiekty",
                Key = "topFoundObjects",
                ChartType = "Bar",
                Data = new Tuple<List<string>, List<int>>
                (
                    labelList,
                    valueList
                )
            };

            var chartsList = new List<ChartData> { correctAndAllMistakesChart, correctAndSmallMistakesChart, topMistakes, topFoundObjects };

            var summaryStats = new SummaryStats
            {
                AverageTime = generalStats.AverageTime,
                ChartsData = chartsList,
                Effectiveness = (double)generalStats.CorrectObjectsDetections / (generalStats.ObjectsFoundByML + generalStats.NotFoundObjects)
            };

            return summaryStats;
        }

        public async Task UpdateGeneralStats(Guid id, Feedback stats)
        {
            var generalStats = await _generalStatsRepository.GetAsync();
            if (generalStats == null || generalStats.Time == 0)
                await _generalStatsRepository.CreateAsync();

            var imageStats = await _statsRepository.GetAsync(id);

            await _generalStatsRepository.UpdateAsync(stats, imageStats.NumberOfObjectsFound, imageStats.FoundObjects, imageStats.Time);
        }

        static Tuple<List<A>, List<B>> Unpack<A, B>(List<Tuple<A, B>> list)
            => list.Aggregate(Tuple.Create(new List<A>(list.Count), new List<B>(list.Count)),
                (unpacked, tuple) =>
                {
                    unpacked.Item1.Add(tuple.Item1);
                    unpacked.Item2.Add(tuple.Item2);

                    return unpacked;
                });
    }
}
