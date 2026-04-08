using Lab1.DoubleLink;

namespace Lab2.Map.Linked;

public class Map
{
    private Node? head;

    // посимвольное сравнение ключей
    private bool CompareKeys(char[] key1, char[] key2)
    {
        if (key1 == null || key2 == null)   // защита от null
            return key1 == key2;

        for (int i = 0; i < key1.Length; i++)
        {
            if (key1[i] != key2[i])
                return false;
            
            if (key1[i] == '\0')
                return true;
        }
        return true;
    }

    // поиск узла по ключу. если не находит, возвращает null
    private Node? Find_Node(char[] key)
    {
        Node? cur = head;    // начинаем с головы
        
        while (cur != null)    // проходимя циклом по узлам
        {
            if (CompareKeys(key, cur.Key))   // если узел совпадает, возвращаем cur 
                return cur;
            cur = cur.Next;
        }
        return null;     // если не нашли, возвращаем null   
    }


    public void Assign(char[] d, char[] r)    // делает M(d) равным r независимо от того, как M(d) было определено ранее
    {
        Node? foundNode = Find_Node(d);   // поиск узла по ключу
        
        if (foundNode != null)
        {
            foundNode.Value = r;    // если нашли, обновляем ссылку
        }
        else
        {
            Node newNode = new Node(d, r, head);
            head = newNode;
        }
    }

    public bool Compute(char[] d, ref char[] r)     // возвращает значение true и присваивает переменной r значение M(d), если последнее определено, и возвращает false в противном случае
    {
        Node? foundNode = Find_Node(d);  // поиск узла по ключу d
        if (foundNode != null)
        {
            r = foundNode.Value;    // если нашли, присваиваем r значение узла и возвращаем true
            return true;
        }
        return false;    // если не нашли узла по ключу, возвращаем false
    }

    public void MakeNull()
    {
        head = null;
    }

    public void Print()
    {
        Console.Write("[");
        Node? cur = head;
        while (cur != null)
        {
            Console.Write($"\n {cur}");
            cur = cur.Next;
        }
        Console.WriteLine("]"); 
    }

    private class Node(char[] key, char[] value, Node? next)
    {
        public char[] Key { get; set; } = key;      // Ключ
        public char[] Value { get; set; } = value; // Значение
        public Node? Next { get; set; } = next;   // Следующий узел

        public override string ToString()
        {
            return $"{new string(Key).TrimEnd('\0')}: {new string(Value).TrimEnd('\0')}";
        }
    }
}