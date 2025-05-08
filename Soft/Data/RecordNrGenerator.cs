namespace MVC.Soft.Data
{
    public static class RecordNrGenerator
    {
        private static readonly HashSet<string> _generatedNumbers = new HashSet<string>();
        private static readonly Random _random = new Random();

        public static string GenerateRecordNr()
        {
            string recordNumber;
            do
            {
                var randomNumber = _random.Next(100, 1000);
                recordNumber = $"#{randomNumber}";
            }
            while (!_generatedNumbers.Add(recordNumber));

            return recordNumber;
        }
    }
}
