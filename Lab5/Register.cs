namespace Lab5;

public class Register : BaseN
{   
    public BaseN? Students { get; set; }  // студент
    public BaseN? Course { get; set; }   // курс

    public override bool Not_register()   // является записью
    {
        return false;
    }
}