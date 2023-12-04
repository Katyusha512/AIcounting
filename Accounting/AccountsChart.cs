namespace AIcounting.Accounting
{
    public class AccountsChart(int StartBookingID = 1)
    {
        public int CurrentBookingID = StartBookingID;

        public DateTime StartDate;
        public DateTime EndDate;
        public List<Account> Accounts = [];

        public int RequestBookingID()
        {
            CurrentBookingID++;
            return((CurrentBookingID - 1));
        }
    }
}
