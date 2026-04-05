// using Lab2.Map.Linked;
// using Lab2.Queue.Linked;
// using Lab2.Stack.Linked;

namespace Lab2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Lab2.Map.Linked.Map map = new Lab2.Map.Linked.Map();
            Lab2.Queue.ATD.Queue<char> queue = new();
            Lab2.Stack.ATD.Stack<char> stack = new();

            char[] mass = ToCharArray("Hello World");

            for (int i = 0; i < mass.Length; i++)
            {
                if (!queue.Full())
                    queue.Enqueue(mass[i]);
                
                if (!stack.Full())
                {
                    stack.Push(mass[i]);
                }
            }

            Console.WriteLine("\nРезультат работы очереди:");
            while (!queue.Empty())
            {
                Console.Write(queue.Dequeue());
            }
            Console.WriteLine("\n"); 
            
            Console.WriteLine("Результат работы стека:");
            while (!stack.Empty())
            {
                Console.Write(stack.Pop());
            }

            Console.WriteLine("\n1. Добавляем три значения с различными ключами:");
            map.Assign(ToCharArray("key1"), ToCharArray("value1"));
            map.Assign(ToCharArray("key2"), ToCharArray("value2"));
            map.Assign(ToCharArray("key3"), ToCharArray("value3"));

            Console.WriteLine("\n2. Выводим Отображение на экран:");
            map.Print();

            Console.WriteLine("\n3. Задаем новое значение по существующему ключу 'key2': new_value2");
            map.Assign(ToCharArray("key2"), ToCharArray("new_value2"));

            Console.WriteLine("\n4. Вновь выводим Отображение на экран:");
            map.Print();

            Console.WriteLine("\n5. Запрашиваем значение по существующему ключу 'key1':");
            char[] result = [];
            if (map.Compute(ToCharArray("key1"), ref result))
            {
                Console.WriteLine($"true, полученное значение: {new string(result)}");
            }

            Console.WriteLine("\n6. Запрашиваем значение по не существующему ключу 'key999':");
            char[] result2 = [];
            if (!map.Compute(ToCharArray("key88"), ref result2))
            {
                Console.WriteLine("false");
            }
        }

        static char[] ToCharArray(string str)
        {
            char[] mass = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
                mass[i] = str[i];
            return mass;
        }
    }
}
