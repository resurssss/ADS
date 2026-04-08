using Lab1.DoubleLink;

namespace Lab2.Queue.ATD;

public class Queue<T>
{
    private ListT<T> list = new();  // Основа очереди

    public void Enqueue(T x)    // вставляет элемент x в конец очереди Q
    {
        list.Insert(x, list.End());   // Insert End()
    }

    public T Front()  // возвращает первый элемент (копия) очереди Q
    {
        T obj = list.Retrieve(list.First());
        return obj;
    }

    public T Dequeue()   // удаляет первый элемент очереди Q, и возвращает его в качестве результата
    {
        T obj = list.Retrieve(list.First());     // записываем объект через Retrieve
        list.Delete(list.First());   // удаление Delete First()
        return obj;
    }

    public void MakeNull()   // очищает очередь Q
    {
        list.Makenull();
    }

    public bool Full()     // возвращает значение true, если очередь Q полная, и значение false в противном случае
    {
        return false;
    }

    public bool Empty()   // возвращает значение true, если очередь Q пустая, и значение false в противном случае
    {
        return list.First().Equals(list.End());
    }
}