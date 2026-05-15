namespace Lab3.Opt;

public class Opt
{   
    public static int ComputeHash(char[] x)
    {
        // если массив пустой, возвращаем 0
        if (x == null) 
            return 0;

        // Переменная для накопления суммы кодов символов
        int count = 0;
        // циклом проходим по символам в имени, пока не дойдем до конца
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] == '\0')   // если встретили '\0', прекращаем цикл
                break;
            count += x[i];  // Добавляем код символа к общей сумме
        }   
        return count;       // Возвращаем полученную сумму
    }

    public static bool Equal(char[] a, char[] b)
    {
        // если массивы пустые, возвращаем a == b
        if (a == null || b == null) 
            return a == b;
        // циклом проходим по символам в имени, пока не встретим '\0' или не дойдем до конца
        for (int i = 0; i < a.Length; i++)   // посимвольно сравниваем, если что возвращаем false
        {
            if (a[i] != b[i])
                return false;

            if (a[i] == '\0')     // дошли до конца - возвращаем true
                return true;
        }
        return true;   // Возвращаем true, если все символы совпадают
    }

    // метод для копирования без выделения памяти
    public static void CopyInto(char[] dest, char[] src)
    {
        // Проверка на null
        if (dest == null || src == null) return;
        
        // Копируем до минимальной длины
        int length = (dest.Length < src.Length) ? dest.Length : src.Length;
        
        for (int i = 0; i < length; i++)
        {
            dest[i] = src[i];
        }
        
        // Если dest длиннее src, ставим '\0' в конце
        if (src.Length < dest.Length)
        {
            dest[src.Length] = '\0';
        }
    }
}