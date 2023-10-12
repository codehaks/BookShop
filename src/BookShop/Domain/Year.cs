namespace BookShop.Domain;
public class Year
{
    public int Value { get; }

    public Year(int value)
    {
        if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Year can not be less than 0");
        Value = value;
    }
}
