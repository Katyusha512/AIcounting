namespace AIcounting
{
    public record class BookingEntry(
             long b, DateTime d, string src,
             Account D, Account C, string txt,
             double Amount, CURRENCY curr, double conversion)
    {
        public readonly long BookingID = b;
        public readonly DateTime BookingDate = d;
        public readonly string SrcID = src;
        public readonly Account DebitAcc = D;
        public readonly Account CreditAcc = C;
        public readonly string BookingTxt = txt;
        public readonly double AmountLocal = Math.Round(Amount, AccDefined.ROUNDING_DIGITS);
        public readonly CURRENCY Currency = curr;
        public readonly double AmountForeign = (Math.Round((Amount * conversion), AccDefined.ROUNDING_DIGITS));
    }
}
