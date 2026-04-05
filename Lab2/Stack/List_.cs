namespace Lab2.Stack.List_;

public class Stack<T>
{
    private const int size = 100;        // Максимальный размер стека
    private readonly T[] array = new T[size];  // Массив для хранения элементов
    private int up = -1;              // Указатель на вершину стека
    

    public void Push(T x)   // вставляет элемент x в вершину стека S
    {
        // вставка в up х, увеличиваем указатель
        array[++up] = x;
    }

    public T Pop()   // удаляет элемент из вершины стека S, и возвращает его в качестве результатата
    {
        // возвращаем элемент индекса up и уменьшаем указатель
        return array[up--];
    }

    public T Top()    // возвращает элемент (копия) из вершины стека S
    {
        return array[up];
    }

    public void MakeNull()   // делает стек пустым
    {
        up = -1;
    }

    public bool Full()   // возвращает значение true, если стек S полный, и значение false в противном случае
    {
        return up == size - 1;    // указатель равен концу массива
    }

    public bool Empty()   // возвращает значение true, если стек S пустой, и значение false в противном случае.
    {
        return up < 0;   // указатель ниже начала массива
    }
}