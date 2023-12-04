namespace AIcounting
{
    public enum ACC_CLASS
    {
        ILLEGAL = -1,
        ASSETS = 1, LIABILITIES = 2, INCOME = 3, EXPENSES = 4,
    };
    public enum CURRENCY
    {
        USD, EUR, CHF,
    };

    public static class AccDefined
    {
        public const int ROUNDING_DIGITS = 2;

    }
}