using Lab1.DoubleLink;

namespace Lab2.Stack.ATD;

public class Stack<T>
{
    private ListT<T> list = new();  // Основа стека

    public void Push(T x)   // вставляет элемент x в вершину стека S
    {
        list.Insert(x, list.First());   // вставка Insert в First()
    }

    public T Pop()   // удаляет элемент из вершины стека S, и возвращает его в качестве результатата
    {
        T obj = list.Retrieve(list.First());     // записываем объект через Retrieve
        list.Delete(list.First());   // удаление Delete First()
        return obj;
    }

    public T Top()    // возвращает элемент (копия) из вершины стека S
    {
        T obj = list.Retrieve(list.First());
        return obj;
    }

    public void MakeNull()   // делает стек пустым
    {
        list.Makenull();
    }

    public bool Full()   // возвращает значение true, если стек S полный, и значение false в противном случае
    {
        return false;
    }

    public bool Empty()   // возвращает значение true, если стек S пустой, и значение false в противном случае.
    {
        return list.First() == list.End();
    }
}