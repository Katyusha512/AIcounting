namespace AIcounting
{
    public record class Account(ACC_CLASS c, string n, int no)
    {

        public ACC_CLASS AccountType = c;
        public string AccountName = n;
        public int AccountNo = no;
        public List<BookingEntry> BookingEntries = [];
    }
}
