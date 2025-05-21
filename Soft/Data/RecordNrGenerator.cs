namespace MVC.Soft.Data
{
    public static class RecordNrGenerator
    {
        private static readonly HashSet<string> _generatedNumbers = new HashSet<string>();
        private static readonly object _lock = new object();
        private static readonly Random _random = new Random();

        public static string GenerateRecordNr()
        {
            lock (_lock)
            {
                if (_generatedNumbers.Count >= 9900)
                    throw new InvalidOperationException("All possible record numbers have been generated.");

                string recordNumber;
                do
                {
                    var randomNumber = _random.Next(100, 10000);
                    recordNumber = $"#{randomNumber}";
                }
                while (!_generatedNumbers.Add(recordNumber));

                return recordNumber;
            }
        }
    }
}
