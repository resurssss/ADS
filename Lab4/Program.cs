namespace Lab4;

public class Program
{
    public static void Main()
    {
        Pair[] pairs =
        {
            new Pair(1, 2),
            new Pair(1, 3),
            new Pair(6, 3),
            new Pair(2, 4),
            new Pair(9, 4),
            new Pair(3, 5),
            new Pair(7, 5),
            new Pair(4, 6),
            new Pair(4, 8),
            new Pair(5, 8),
            new Pair(7, 9),
            new Pair(2, 10),
            new Pair(8, 9)
        };
        PartialOrderSet poset = new PartialOrderSet(pairs);

        PredecessorsList sorter = new PredecessorsList();

        if (!sorter.Initialize(poset))
        {
            Console.WriteLine("Ошибка: нарушена антирефлексивность");
            return;
        }

        if (!sorter.TopologySort())
        {
            Console.WriteLine("Невозможно выполнить топологическую сортировку (обнаружен цикл)");
            return;
        }

        Console.Write("Результат топологической сортировки: ");
        sorter.Print();
    }
}
