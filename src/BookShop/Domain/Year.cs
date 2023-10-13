namespace BookShop.Domain;
public class Year
{
    public int Value { get; }

    public Year(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);
        Value = value;
    }
}
