namespace Lab4;

public class PartialOrderSet
{
    private Pair[] _pairs;

    // создаем частично упорядоченное множество
    public PartialOrderSet(Pair[] pairs)
    {
        _pairs = pairs;
    }

    // возвращает массив пар
    public Pair[] GetPairs()
    {
        return _pairs;
    }
}


// класс хранения одной пары отношения порядка
public class Pair
{
    public int First { get; }
    public int Second { get; }

    public Pair(int first, int second)
    {
        First = first;
        Second = second;
    }
}
