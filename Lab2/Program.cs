
namespace Lab2
{
    public class Program
    {
        const int size_n = 20;  // размер ключа (имя)
        const int size_a = 50;  // размер значения (адрес)

        static char[] ToCharArray(string str, int size)
        {
            char[] mass = new char[size];  // элементы по умолчанию '\0'

            // копируем строку
            for (int i = 0; i < str.Length && i < size; i++)
                mass[i] = str[i];
            
            return mass;
        }

        static void Main(string[] args)
        {
            Lab2.Map.Linked.Map map = new Lab2.Map.Linked.Map();
            Lab2.Queue.Linked.Queue<char> queue = new();
            Lab2.Stack.Linked.Stack<char> stack = new();

            char[] mass = ToCharArray("Hello World", size_n);

            for (int i = 0; i < mass.Length; i++)
            {
                if (mass[i] == '\0')
                    break;

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
            Console.WriteLine("\n"); 
            
            Console.WriteLine("1. Добавляем три значения с различными ключами и выводим их на экран:");
            map.Assign(ToCharArray("key1", size_n), ToCharArray("value1", size_a));
            map.Assign(ToCharArray("key2", size_n), ToCharArray("value2", size_a));
            map.Assign(ToCharArray("key3", size_n), ToCharArray("value3", size_a));
            map.Print();

            Console.WriteLine("\n2. Задаем новое значение по существующему ключу 'key2': new_value2");
            map.Assign(ToCharArray("key2", size_n), ToCharArray("new_value2", size_a));

            Console.WriteLine("\n3. Вновь выводим Отображение на экран:");
            map.Print();

            Console.WriteLine("\n4. Запрашиваем значение по существующему ключу 'key1':");
            char[] result = new char[size_a];
            if (map.Compute(ToCharArray("key1", size_n), ref result))
            {
                // Вывод до терминального нуля
                string value = new string(result).TrimEnd('\0');
                Console.WriteLine($"true, полученное значение: {value}");
            }

            Console.WriteLine("\n5. Запрашиваем значение по несуществующему ключу 'key999':");
            char[] result2 = new char[size_a];
            if (!map.Compute(ToCharArray("key999", size_n), ref result2))
            {
                Console.WriteLine("false");
            }
        }
    }
}
