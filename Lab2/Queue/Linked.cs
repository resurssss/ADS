using Lab1.DoubleLink;

namespace Lab2.Queue.Linked;

public class Queue<T>
{
    private Node? tail;

    public void Enqueue(T x)    // вставляет элемент x в конец очереди Q
    {
        if (tail == null)
        {
            tail = new Node(x, null);
            tail.Next = tail;
            return;
        }
        
        Node newNode = new Node(x, tail!.Next); // новый узел указывает на текущую голову
        tail.Next = newNode; // старый хвост указывает на новый узел
        tail = newNode;      // хвостом становится новый узел
    }

    public T Front()  // возвращает первый элемент (копия) очереди Q
    {
        return tail!.Next!.Data;    // голова следующая после хвоста
    }

    public T Dequeue()   // удаляет первый элемент очереди Q, и возвращает его в качестве результата
    {
        T cur = tail!.Next!.Data;   // получаем Next tail - первый элемент очереди
        if (tail.Next == tail)   // один элемент очереди
            tail = null;
        else
            tail.Next = tail.Next.Next;   // next tail указывает на следующий
        return cur;
    }

    public void MakeNull()   // очищает очередь Q
    {
        tail = null;
    }

    public bool Full()     // возвращает значение true, если очередь Q полная, и значение false в противном случае
    {
        return false;
    }

    public bool Empty()   // возвращает значение true, если очередь Q пустая, и значение false в противном случае
    {
        return tail == null;
    }

    private class Node(T data, Node? next)
    {
        public T Data { get; set; } = data;
        public Node? Next { get; set; } = next;
    }
}