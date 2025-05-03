using MVC.Aids.Attributes;

namespace MVC.Soft.Data
{
    public static class EmailGenerator
    {
        private static readonly Random _random = new();

        public static string Generate(string? firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return $"user{_random.Next(1000, 9999)}@{GetRandomDomain()}";

            return $"{firstName.ToLower()}.{lastName.ToLower()}@{GetRandomDomain()}";
        }

        private static string GetRandomDomain()
        {
            var index = _random.Next(0, EmailDomainProvider.EmailDomains.Count);
            return EmailDomainProvider.EmailDomains[index];
        }
    }
}