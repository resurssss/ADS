namespace Lab1;

public class People
{
    public readonly char[] Name = new char[20];      // имя адресата (максимум 20 символов)   
    public readonly char[] Address = new char[50];         // адрес (максимум 50 символов)

    // копия объекта
    public People(string name, string address)
    {
        // копируем имя
        int nameSize = Math.Min(name.Length, 20);
        for (int i = 0; i < nameSize; i++)
        {
            Name[i] = name[i];
        }
        Name[nameSize] = '\0';

        // копируем адрес
        int addressSize = Math.Min(address.Length, 50);
        for (int i = 0; i < addressSize; i++)
        {
            Address[i] = address[i];
        }
        Address[addressSize] = '\0';
    }


    public bool Equals(People? obj)     // посимвольное сравнение
    {
        if (obj == null)       // проверка на null
            return false;

        if (ReferenceEquals(this, obj))
            return true;
        
        
        for (int i = 0; i < Name.Length; i++)          // посимвольное сравнение циклом имени
        {
            if (Name[i] != obj.Name[i]) 
                return false;
            if (Name[i] == '\0') 
                break; // Достигли конца строки
        }

        for (int i = 0; i < Address.Length; i++)       // посимвольное сравнение циклом адреса
        {
            if (Address[i] != obj.Address[i]) 
                return false;
            if (Address[i] == '\0') 
                break; // Достигли конца строки
        }
        
        return true;      // если всё совпало, возвращаем true
    }

    public override bool Equals(object? obj)    // вызывается по умолчанию
    {
        return Equals(obj as People);
    }

    public override string ToString()
    {
        string nameStr = new string(Name).TrimEnd('\0');
        string addressStr = new string(Address).TrimEnd('\0');
        return $"{nameStr}: {addressStr}";
    }
}