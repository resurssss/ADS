using static Lab3.Opt.Opt;
namespace Lab3.Closed;

public class Dict
{
    private const int size = 20;                           // Фиксированный размер хеш-таблицы
    private readonly char[]?[] array = new char[size][];  // Основной массив для хранения данных

    
    // Хеш-функция
    private int Get_hash(char[] x)
    {
        // получаем хеш-код массива
        int hash = ComputeHash(x);
        // возвращаем хеш % размер
        return hash % size;
    }

    // линейное хеширование (hi = (h + i) % размер массива, где h – исходное хеш-значение, i – номер повторного хеширования: 1, 2. 3 и т. д.).
    private int Linear_hash(int i, int h)
    {
        return (h + i) % size;
    }

    // !ищет x в хеш-таблице и возвращает его индекс, если находит
    private int Contain(char[] x, int hash)
    {
        // линейное хэширование
        for (int i = 0; i < size; i++)
        {
            int index = Linear_hash(i, hash);  // вычисляем индекс с учетом хэширования
            
            // если ячейка пустая — элемент не найден
            if (array[index] == null)     // удалённые ячейки имеют array[index][0] == '\0', они не null
                return -1;
            
            // если ячейка не пустая и имена совпадают — нашли
            if (Equal(array[index]!, x))
                return index;
        }                                       // Элемент не найден
        return -1;   // не нашли, возвращаем false
    }

    // Проверка принадлежности элемента множеству
    public bool Member(char[] x)
    {
        // смотрим хэш
        int hash = Get_hash(x);
        // проверяем, есть ли такой узел в цепочке с помощью Contain
        if (Contain(x, hash) == -1)    // не нашли узел
            return false;
        return true;
    }

    // делает x элементом словаря
    public void Insert(char[] x)
    {
        int hash = Get_hash(x);    // вычисляем начальный хеш-индекс
        int cur = hash;    // текущий индекс для проверки

        int firstDeleted = -1;     // индекс первой встреченной удалённой ячейки (-1 = нет)
        int i = 0;                // номер попытки

        do
        {
            // случай пустой ячейки
            if (array[cur] == null)
            {
                // если по пути встречали удалённую ячейку - вставляем в нее
                if (firstDeleted != -1)
                    // переиспользуем удалённую ячейку
                    CopyInto(array[firstDeleted]!, x);
                else
                {
                    // вставляем в пустую ячейку
                    array[cur] = new char[x.Length];
                    CopyInto(array[cur]!, x);
                }
                return;
            }

            // случай удалённой ячейки
            if (array[cur]![0] == '\0' && firstDeleted == -1)   // ячейка не пуста, первый символ = '\0' И мы ещё не запомнили удалённую
            {
                firstDeleted = cur;      // запоминаем индекс первой удалённой ячейки
            }

            // дубликат
            if (Equal(array[cur]!, x))
                return;

            i++;         // увеличиваем номер попытки
            cur = Linear_hash(i, hash);       // вычисляем следующий индекс
            
        } while (cur != hash);   // продолжаем, пока не вернулись к начальному индексу (не обошли всю таблицу)

        // обошли всю таблицу, пустых ячеек нет

        // если есть удалённые ячейки
        if (firstDeleted != -1)
        {
            // вставляем в первую удалённую
            CopyInto(array[firstDeleted]!, x);
            return;
        }

        throw new Exception("Хэш-таблица переполнена");
    }

    public void Delete(char[] x)
    {
        int hash = Get_hash(x);     // получаем хэш
        int ind = Contain(x, hash);    // вызываем Contain 
        if (ind == -1)    // проверяем найденный индекс (если нет такого узла, то ничего не делаем)
            return;
        array[ind]![0] = '\0';          // помечаем как удаленный (первый символ = '\0')
    }


    public void MakeNull()
    {
        // проход по всем элементам массива
        for (int i = 0; i < size; ++i)
        {
            array[i] = null;
        }
    }
    
    // выводит словарь на экран
    public void Print()
    {
        for (int i = 0; i < size; ++i)
        {
            if (array[i] != null && array[i]![0] != '\0')  // ее null и не удаленный
            {
                Console.Write($"{new string(array[i])} ");
            }
        }
        Console.WriteLine();
    }
}