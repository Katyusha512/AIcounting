namespace AIcounting.AI
{
    public record class RobotQueries
    {
        public record class BookingData(string a, string b)
        {
            public readonly string Company = a;
            public readonly string Topic = b;
        }

        public record class BookingQueries(string a, string b, string c, string d)
        {
            public readonly string CompanyClue_Name = a;
            public readonly string CompanyClue_Address = b;
            public readonly string CompanyClue_BusinessField = c;
            public readonly string CompanyClue_Contact = d;
        }
    }
}
