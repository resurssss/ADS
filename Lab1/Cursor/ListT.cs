namespace Lab1.Cursor;

public class Position<T>
{
    internal int Pos { get; init; }   // индекс в массиве узлов

    internal Position(int index)
    {
        Pos = index;
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
    private static readonly Node[] Elem;      // массив объектов списка и указателей

    private int start = -1;        // индекс первого элемента списка, -1 означает пустой список
 
    private static int space;       // указатель на первый пустой индекс в массиве (голова "пустого списка")      

    private const int MaxSize = 100;      // максимальный размер списка

    // статический конструктор — инициализация массивов и списка пустых ячеек
    static ListT()
    {
        Elem = new Node[MaxSize];
        const int size = MaxSize - 1;
        // каждая ячейка указывает на следующую пустую
        for (int i = 0; i < size; i++)
        {
            Elem[i] = new Node
            {
                Next = i + 1
            };
        }

        // последняя свободная указывает на конец (-1)

        space = 0;
    }

    internal class Node
    {
        internal T Data { get; set; }
        internal int Next { get; set; }

        internal Node()
        {
            Data = default!;
            Next = -1;
        }

        internal Node(T data, int next)
        {
            Data = data;
            Next = next;
        }
    }


    public Position<T> End()    // позиция конца списка
    {
        return new Position<T>(-1);    
    }

    // возвращает позицию предыдущего значения перед P
    private int Get_prev(Position<T> p)   
    {
        int cur = start;
        while (cur != -1)
        {
            int next = Elem[cur].Next;     // следующий индекс
            if (next == p.Pos)        // если нашли, возвращаем позицию
                return cur;
            cur = next;
        }

        return -1;
    }
    
    // позволяет дойти до конца списка (местоположение последнего элемента в списке)
    private int Return_last()
    {
        int cur = start;
        while (cur != -1)
        {
            int next = Elem[cur].Next;     // следующий индекс
            if (next == -1)      // если следующий пустой, возвращаем текущую позицию
                return cur;
            cur = next;
        }

        return -1;
    }

    public void Insert(T x, Position<T> p)   // вставка объекта x в позицию p
    {
        if (p.Pos == -1)    // p - позиция после последнего
        {
            if (start == -1)    // случай пустого списка: вставляем новый элемент (головой)  
            {
                start = space;      // берем первый свободный узел (по индексу space)
                Elem[start].Data = x;    // помещаем объект в эту позицию
                space = Elem[space].Next;     // передвигаем указатель на следующий свободный узел
                Elem[start].Next = -1;    // у последнего элемента next = -1
            }
            else
            {
                int newIndex = space;      // сохраняем индекс этого узла
                Elem[newIndex].Data = x;   // помещаем объект в эту позицию
                space = Elem[space].Next;     // передвигаем указатель на следующий свободный узел
                Elem[newIndex].Next = -1;    // у нового элемента next = -1

                int last = Return_last(); // находим последний
                Elem[last].Next = newIndex;     // бывший последний узел next указывает на новый
            }
            return;
        }

        if (p.Pos == start)     // позиция p - голова 
        {
            int newIndex = space;           // берем свободный узел
            Elem[newIndex].Data = x;        // записываем в него новые данные
            space = Elem[space].Next;       // обновляем space
            
            Elem[newIndex].Next = start;    // новый узел указывает на старую голову
            start = newIndex;                // новый узел становится головой

            return;
        }

        if (Get_prev(p) == -1)      // проверка get_prev
            throw new Exception("Данная позиция не существует в списке");

        // ВСТАВКА В СЕРЕДИНУ
        Elem[space].Data = Elem[p.Pos].Data;    // текущее значение в позиции p вставляем в позицию, равную space (первый пустой)
        Elem[p.Pos].Data = x;                 // новое значение x вставляем в позицию p

        int ind = space;
        space = Elem[space].Next;    // обновляем space: оно равно следующему пустому
        
        Elem[ind].Next = Elem[p.Pos].Next;
        Elem[p.Pos].Next = ind;    // следующее при x равно позиции переставленного элемента
    }

    public void Delete(Position<T> p)  // удалить элемент списка в позиции p
    {
        if (p.Pos == -1)    // p - позиция после последнего
            throw new Exception("Данная позиция не существует в списке");

        if (p.Pos == start)   // p - голова
        {
            int oldHead = start;

            if (Elem[oldHead].Next == -1)   // если список состоит из 1 элемента, очищаем его
                start = -1;
            else
                start = Elem[oldHead].Next;   // сдвинуть позицию головы, переместив к следующему элементу

            Elem[oldHead].Next = space;    // oldHead указывает на первый свободный
            space = oldHead;

            return;
        }

        if (Get_prev(p) == -1)      // проверка get_prev
            throw new Exception("Данная позиция не существует в списке");
        
        // УДАЛЕНИЕ ИЗ СЕРЕДИНЫ
        int prev = Get_prev(p);   // получаем позицию предыдущего элемента Get_prev
        int old = p.Pos;              // записываем индекс удаляемого узла

        Elem[prev].Next = Elem[old].Next;       // перекидываем ссылку: предыдущий удаляемого.Next = следующий удаляемого
        int ind = space;             // сохраняем старое значение space
        space = old;                 // обновляем space: оно равно индексу удаляемого узла
        
        Elem[space].Next = ind;          // включаем удаленный узел в список свободных мест
    }

    public Position<T> Locate(T x)    // возвращает позицию в списке объекта x
    {
        int cur = start;
        while (cur != -1)
        {
            if (Equals(x, Elem[cur].Data))     // сравнивает текущий объект с объектом x
                return new Position<T>(cur);
            cur = Elem[cur].Next;
        }

        return new Position<T>(-1);    // если объекта в списке нет, то возвращается конец
    }

    public T Retrieve(Position<T> p)    // возвращается объект списка в позиции p
    {
        if (p.Pos == -1)    // p - позиция после последнего
            throw new Exception("Данная позиция не существует в списке");
        
        if (p.Pos != start && Get_prev(p) == -1)      // проверка get_prev
            throw new Exception("Данная позиция не существует в списке");

        return Elem[p.Pos].Data;
    }

    public void Makenull()       // делает список пустым
    {
        if (start == -1)    // проверка на пустой список
            return;
        Elem[Return_last()].Next = space;     // цепляем освободившиеся ячейки к списку свободных
        space = start;
        start = -1;
    }


    public Position<T> Next(Position<T> p)    // возвращает следующую за p позицию в списке 
    {
        if (p.Pos == -1)      // p - позиция после последнего
            throw new Exception("Данная позиция не существует в списке");

        if (p.Pos != start && Get_prev(p) == -1)      // проверка get_prev
            throw new Exception("Данная позиция не существует в списке");

        int next = Elem[p.Pos].Next;
        if (next == -1)           // если это хвост, возвращается end
            return new Position<T>(-1);    

        return new Position<T>(next);
    }

    public Position<T> Previous(Position<T> p)      // возвращает предыдущую перед p позицию в списке
    {
        if (p.Pos == -1)      // p - позиция после последнего
            throw new Exception("Данная позиция не существует в списке");

        if (p.Pos == start)    // это голова (нет предыдущего)
            throw new Exception("Данная позиция не существует в списке");

        int prev = Get_prev(p);
        if (prev == -1)      // проверка get_prev
            throw new Exception("Данная позиция не существует в списке");

        return new Position<T>(prev);
    }

    public Position<T> First()   // получение позиции первого элемента списка
    {
        return new Position<T>(start);          
    }

    public void PrintList()  // вывод списка
    {
        Console.Write("[");
        int cur = start;
        while (cur != -1) 
        {
            Console.Write($",\n{Elem[cur].Data}");
            cur = Elem[cur].Next;
        }
        Console.WriteLine("]");
    }
}



