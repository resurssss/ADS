using static Lab3.Opt.Opt;
namespace Lab3.Open;

public class Dict
{
    private const int size = 100;        // размер
    private readonly Node?[] nodes = new Node[size];  // Массив для хранения элементов

    // Словарь – массив, состоящий из элементов. Элемент массива – имя (символьный массив из 10 символов) 
    // и ссылка на следующий такой же элемент (будет задействована при разрешении коллизий). 
    // Функция хеширования – остаток от деления суммы кодов символов имени на размер массива
    
    
    // Поиск элемента в цепочке по хешу
    // функция возвращает предыдущий узел узла, который совпадает. возвращает null, если узел не найден или когда элемент является головой
    private Node? FindPrev(char[] x, int hash)
    {
        Node? prev = null;
        Node? current = nodes[hash];
        while (current != null)
        {
            if (Equal(x, current.Data))
                return prev;
            prev = current;
            current = current.Next;
        }
        return null;
    }

    // Хеш-функция
    private int Get_hash(char[] x)
    {
        // получаем хеш-код от Hash класса
        int hash = ComputeHash(x);
        // возвращаем хеш % размер
        return hash % size;
    }


    // проверка принадлежности элемента множеству
    public bool Member(char[] x)
    {
        int hash = Get_hash(x);
        Node? prev = FindPrev(x, hash);
        
        if (prev == null)
        {
            // Проверяем голову
            return nodes[hash] != null && Equal(x, nodes[hash]!.Data);    // если голова не пуста и совпадает с x 
        }
        
        return true;   // FindPrev нашёл элемент
    }

    // делаем x элементом словаря
    public void Insert(char[] x)
    {
        int hash = Get_hash(x);
        Node? prev = FindPrev(x, hash);

        // случай, если элемент уже есть
        if (prev == null)
        {
            // Проверяем голову
            if (nodes[hash] != null && Equal(x, nodes[hash]!.Data))           // если голова не пуста и совпадает с x 
                return;
        }
        else
        {
            // FindPrev нашёл такой элемент, значит вставлять не нужно
            return;
        }

        char[] copy = new char[x.Length];
        CopyInto(copy, x);
        nodes[hash] = new Node(copy, nodes[hash]);
    }

    // удаляет элемент х из словаря
    public void Delete(char[] x)
    {
        int hash = Get_hash(x);
        Node? prev = FindPrev(x, hash);

        if (prev == null)
        {
            // Проверяем голову
            if (nodes[hash] != null && Equal(x, nodes[hash]!.Data))        // если голова не пуста и совпадает с x 
                nodes[hash] = nodes[hash]!.Next;
            return;   // если не голова, значит, элемента нет, ничего не делаем
        }
        
        // FindPrev нашёл элемент в prev.Next, удаляем его
        prev.Next = prev.Next!.Next;
    }

    // делает словарь пустым
    public void MakeNull()
    {
        // проход по всем элементам массива
        for (int i = 0; i < size; i++)
            nodes[i] = null;   // обнуляем все головы списков
    }

    // выводит словарь на экран
    public void Print()
    {
        // проход по всем элементам массива
        for (int i = 0; i < size; i++)
        {
            Node? cur = nodes[i];   
            while (cur != null)   // проход по всем узлам цепочки
            {
                Console.Write($"{new string(cur.Data)} ");
                cur = cur.Next;
            }
        }
        Console.WriteLine();
    }
    

    // узел
    private class Node(char[] data, Node? next = null)
    {
        public char[] Data { get; set; } = data;
        public Node? Next { get; set; } = next;
    }
}