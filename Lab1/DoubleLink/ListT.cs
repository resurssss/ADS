namespace Lab1.DoubleLink;

public class Position<T>
{
    internal ListT<T>.Node? Pos { get; init; } 

    internal Position(ListT<T>.Node? pos)
    {
        Pos = pos;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Position<T> other)
            return Pos == other.Pos;
        return false;
    }
}

public class ListT<T>
{
    private Node? head;  // Голова списка (первый элемент)
    private Node? tail;   // Хвост списка (последний элемент)
 
    internal class Node
    {
        internal T Data { get; set; }
        internal Node? Previous { get; set; }
        internal Node? Next { get; set; }

        internal Node(T data, Node? prev = null, Node? next = null)
        {
            Data = data;
            Previous = prev;
            Next = next;
        }
    }

    public Position<T> End()
    {
        return new Position<T>(null);   // позиция конца списка
    }

    private bool Check_pos(Position<T> p)   // проверка существует ли эта позиция в списке
    {
        Node? cur = head;
        while (cur != null)      // проход циклом, если нашли, возвращаем True
        {
            if (cur == p.Pos)
                return true;
            cur = cur.Next;
        }

        return false;
    }

    public void Insert(T x, Position<T> p)   // вставка объекта x в позицию p
    {
        Node newNode;

        if (p.Pos == null)    // позиция после последнего
        {
            newNode = new Node(x);
            if (head == null)    // если список пустой
                    head = tail = newNode;     // вставляем новый элемент — и голова, и хвост
            else
            {
                // не пустой
                tail!.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
            return;
        }

        if (p.Pos == head)
        {
            newNode = new Node(x);
            newNode.Next = head;
            if (head != null)
                head.Previous = newNode;
            head = newNode;
            return;
        }

        if (p.Pos == tail)
        {
            newNode = new Node(x);
            tail.Next = newNode;
            newNode.Previous = tail;
            tail = newNode;
            return;
        }

        if (!Check_pos(p))   // проверка есть ли эта позиция в списке
            throw new Exception("Данная позиция не существует в списке");

        Node cur = p.Pos;   // текущий узел
        newNode = new Node(cur.Data);    // создаём новый узел с данными текущего

        //  Вставляем его после cur
        newNode.Next = cur.Next;
        newNode.Previous = cur;

        if (cur.Next != null)        
            cur.Next.Previous = newNode;

        cur.Next = newNode;
        cur.Data = x;        // в текущий узел записываем новые данные
    }

    public void Delete(Position<T> p)  // удалить элемент списка в позиции p
    {
        if (p.Pos == null)      // позиция после последнего
            throw new Exception("Данная позиция не существует в списке");

        
        if (p.Pos == head)      // проверка на удаление из головы
        {
            if (head == tail)  // если список состоит из 1 элемента, очищаем его
            {
                head = tail = null;
                return;
            }

            head = head!.Next;
            head!.Previous = null;

            return;
        }

        if (p.Pos == tail)          // проверка на удаление из хвоста
        {
            tail = tail!.Previous;
            tail!.Next = null;
            return;
        }

        if (!Check_pos(p))   // проверка есть ли эта позиция в списке
            throw new Exception("Данная позиция не существует в списке");

        // удаление из середины 
        Node nodeToDelete = p.Pos!;
        nodeToDelete.Previous!.Next = nodeToDelete.Next;
        nodeToDelete.Next!.Previous = nodeToDelete.Previous;
    }
    
    public Position<T> Locate(T x)    // возвращает позицию в списке объекта x
    {
        Node? cur = head;
        while (cur != null)
        {
            if (Equals(x, cur.Data))            // если объекты совпадают, то возвращает текущую позицию 
                return new Position<T>(cur);        
            cur = cur.Next;
        }

        return new Position<T>(null);    // если объекта в списке нет, то возвращается конец
    }

    public T Retrieve(Position<T> p)    // возвращается объект списка в позиции p
    {
        if (p.Pos == null)          // позиция после последнего
            throw new Exception("Нельзя получить данные из несуществующей позиции");

        if (!Check_pos(p))          // проверка позиции
            throw new Exception("Нельзя получить данные из несуществующей позиции");

        return p.Pos.Data;
    }

    public void Makenull()       // делает список пустым
    {
        head = tail = null;
    }

    public Position<T> Next(Position<T> p)    // возвращает следующую за p позицию в списке 
    {
        if (p.Pos == null)          // позиция после последнего
            throw new Exception("Данная позиция не существует в списке");

        if (!Check_pos(p))          // проверка позиции
            throw new Exception("Данная позиция не существует в списке");

        return new Position<T>(p.Pos.Next); 
    }

    public Position<T> Previous(Position<T> p)      // возвращает предыдущую перед p позицию в списке
    {
        if (p.Pos == null || p.Pos == head)          // позиция после последнего или у головы нет предыдущего
            throw new Exception("Данная позиция не существует в списке");

        if (!Check_pos(p))          // проверка позиции
            throw new Exception("Данная позиция не существует в списке");
        
        return new Position<T>(p.Pos.Previous);
    }

    public Position<T> First()   // получение позиции первого элемента списка
    {
        return new Position<T>(head); // выводим голову
    }

    public void PrintList()  // вывод списка
    {
        Console.Write("[");
        Node? cur = head;
        while (cur != null)
        {
            Console.Write($",\n {cur.Data}");
            cur = cur.Next;
        }
        Console.WriteLine("]"); 
    }
}
