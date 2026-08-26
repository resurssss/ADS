using static Lab5.Opt;   // ComputeHash, Equal, CopyInto
namespace Lab5;

// класс таблицы курсов
public class ListCourse
{
    private const int size = 20;          // размер
    private readonly Course?[] array;      // массив курсов (хэш-таблица курсов)

    // заполняем выделяем память
    public ListCourse()
    {
        array = new Course[size];
    }

    
    public Course?[] GetCourse_ar()
    {
        return array;   // возвращаем местопложение по индексу
    }

    private int Get_hash(char[] x)
    {
        int hash = ComputeHash(x);
        return hash % size;
    }

    private int Linear_hash(int i, int h)
    {
        return (h + i) % size;
    }

    // Поиск
    private int Contain(char[] x, int hash)
    {
        int cur = hash;
        int i = 0;
        do
        {
            if (array[cur] != null &&
                array[cur]!.Name[0] != '\0' &&
                Equal(array[cur]!.Name, x))
                return cur;
            i++;
            cur = Linear_hash(i, hash);
        } while (cur != hash);
        return -1;
    }

    // Теперь Member возвращает int
    public int Member(char[] x)
    {
        int hash = Get_hash(x);
        int ind = Contain(x, hash);
        return ind;
    }

    // Insert
   public void Insert(char[] name)
    {
        int hash = Get_hash(name);
        int cur = hash;
        int firstDeleted = -1;
        int i = 0;

        do
        {
            // случай пустой ячейки
            if (array[cur] == null)
            {
                if (firstDeleted != -1)
                {
                    // переиспользуем удалённую ячейку — копируем имя в существующий объект
                    CopyInto(array[firstDeleted]!.Name, name);
                }
                else
                {
                    // выделяем память: создаём копию имени, затем новый объект
                    char[] copy = new char[name.Length];
                    CopyInto(copy, name);
                    array[cur] = new Course(copy);
                }
                return;
            }

            // случай удалённой ячейки
            if (array[cur]!.Name[0] == '\0' && firstDeleted == -1)
            {
                firstDeleted = cur;
            }

            // дубликат (только среди активных)
            if (array[cur]!.Name[0] != '\0' && Equal(array[cur]!.Name, name))
            {
                return;
            }

            i++;
            cur = Linear_hash(i, hash);
        } while (cur != hash);

        if (firstDeleted != -1)
        {
            CopyInto(array[firstDeleted]!.Name, name);
            return;
        }

        throw new Exception("Хэш-таблица курсов переполнена");
    }

    // Delete – полностью как старый
    public void Delete(char[] x)
    {
        int hash = Get_hash(x);
        int ind = Contain(x, hash);
        if (ind == -1) return;
        array[ind]!.Name[0] = '\0';
    }

    public void MakeNull()
    {
        for (int i = 0; i < size; i++)
            array[i] = null;
    }

    public void Print()
    {
        for (int i = 0; i < size; i++)
        {
            if (array[i] != null && array[i]!.Name[0] != '\0')
                Console.Write($"{new string(array[i]!.Name)} ");
        }
        Console.WriteLine();
    }
}