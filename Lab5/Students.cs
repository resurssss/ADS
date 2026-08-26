namespace Lab5;

public class Students : BaseN
{
    public char[] Name = new char[NameSize + 1];
    public const int NameSize = 15;
    public Register? Course { get; set; }   // ссылка на запись

    public Students(char[] name)  // конструктор
    {
        int length = name.Length < NameSize ? name.Length : NameSize;
        
        for (int i = 0; i < length; i++)
            Name[i] = name[i];
        
        Name[length] = '\0';
    }
    
    public override string ToString()
    {
        return string.Join("", Name);
    }
}