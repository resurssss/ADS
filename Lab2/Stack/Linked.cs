using Lab1.DoubleLink;

namespace Lab2.Stack.Linked;

public class Stack<T>
{
    private Node? head;  // Вершина стека

    public void Push(T x)   // вставляет элемент x в вершину стека S
    {
        Node? node = head;     // создаем новый узел 
        head = new Node(x, node);     // новый узел = голова 
    }

    public T Pop()   // удаляет элемент из вершины стека S, и возвращает его в качестве результатата
    {
        T obj = head.Data; // записываем текущую голову (Data)
        head = head.Next;    // передвигаем голову на следующий узел
        return obj;
    }

    public T Top()    // возвращает элемент (копия) из вершины стека S
    {
        return head.Data;   // смотрим Data
    }

    public void MakeNull()   // делает стек пустым
    {
        head = null;
    }

    public bool Full()   // возвращает значение true, если стек S полный, и значение false в противном случае
    {
        return false;
    }

    public bool Empty()   // возвращает значение true, если стек S пустой, и значение false в противном случае.
    {
        return head == null;
    }

    private class Node(T data, Node? next)
    {
        public T Data { get; set; } = data;
        public Node? Next { get; set; } = next;
    }
}