using Lab1.DoubleLink;

namespace Lab2.Queue.List_;

public class Queue<T>
{
    private const int size = 100;        // Максимальный размер очереди
    private readonly T[] array = new T[size];  // Массив для хранения элементов
    private int front = 0;             // Указатель на начало очереди
    private int rear = size - 1;             // Указатель на конец очереди, при первом добавлении элемента сдвинется на следующую позицию и окажется на индексе 0

    private int Next(int pos)    // возвращает следующую позицию в массиве
    {
        return (pos + 1) % size;
    }

    public void Enqueue(T x)    // вставляет элемент x в конец очереди Q
    {
        rear = Next(rear);   // передвигаем rear
        array[rear] = x;   // элемент x в rear
    }

    public T? Front()  // возвращает первый элемент (копия) очереди Q
    {
        return array[front];   // первый элемент очереди (front)
    }

    public T Dequeue()   // удаляет первый элемент очереди Q, и возвращает его в качестве результата
    {
        T cur = array[front];    // записываем текущий элемент
        front = Next(front);   // передвигаем front
        return cur;
    }

    public void MakeNull()   // очищает очередь Q
    {
        front = Next(rear);
    }

    // оставляем одну пустую ячейку, чтобы отличать пустую очередь от полной
    public bool Full()     // возвращает значение true, если очередь Q полная, и значение false в противном случае
    {
        // следующий за следующим за rear будет front
        return Next(Next(rear)) == front;
    }

    public bool Empty()   // возвращает значение true, если очередь Q пустая, и значение false в противном случае
    {
        // следующий за rear будет front
        return Next(rear) == front;
    }
}