namespace Lab5;

public class Course : BaseN
{   
    public char[] Name = new char[NameSize + 1];
    public const int NameSize = 23;
    public Register? Students { get; set; }    // ссылка на запись

    public Course(char[] name)  // конструктор запись
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