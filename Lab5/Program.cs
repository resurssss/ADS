namespace Lab5;

class Program
{
    static void Main(string[] args)
    {
        Multi map = new Multi();
        map.Init();

        // Добавление студентов на курсы
        map.AddStudentToCourse("Ivanov".ToCharArray(), "CS1".ToCharArray());
        map.AddStudentToCourse("Molotov".ToCharArray(), "CS1".ToCharArray());
        map.AddStudentToCourse("Molotov".ToCharArray(), "CS2".ToCharArray());
        map.AddStudentToCourse("Molotov".ToCharArray(), "CS3".ToCharArray());
        map.AddStudentToCourse("Petrov".ToCharArray(), "CS1".ToCharArray());
        map.AddStudentToCourse("Petrov".ToCharArray(), "CS3".ToCharArray());
        map.AddStudentToCourse("Sidorov".ToCharArray(), "CS2".ToCharArray());

        // Вывод начального состояния
        Console.WriteLine("\nНачальное состояние");
        map.PrintStudentsOfCourse("CS1".ToCharArray());
        map.PrintStudentsOfCourse("CS2".ToCharArray());
        map.PrintStudentsOfCourse("CS3".ToCharArray());
        map.PrintCoursesOfStudent("Ivanov".ToCharArray());
        map.PrintCoursesOfStudent("Molotov".ToCharArray());
        map.PrintCoursesOfStudent("Petrov".ToCharArray());
        map.PrintCoursesOfStudent("Sidorov".ToCharArray());

        // Удаление студента с курса
        Console.WriteLine("\n1. Удаление Molotov с CS2");
        map.RemoveStudentFromCourse("Molotov".ToCharArray(), "CS2".ToCharArray());
        map.PrintCoursesOfStudent("Molotov".ToCharArray());

        // Удаление студента со всех курсов
        Console.WriteLine("\n2. Удаление студента Petrov со всех курсов");
        map.RemoveStudent("Petrov".ToCharArray());
        map.PrintCoursesOfStudent("Petrov".ToCharArray());

        // Удаление всех студентов с курса
        Console.WriteLine("\n3. Удаление всех студентов с CS1");
        map.RemoveCourse("CS1".ToCharArray());
        map.PrintStudentsOfCourse("CS1".ToCharArray());

        // Итоговое состояние
        Console.WriteLine("\nИтоговое состояние");
        map.PrintStudentsOfCourse("CS1".ToCharArray());
        map.PrintStudentsOfCourse("CS2".ToCharArray());
        map.PrintStudentsOfCourse("CS3".ToCharArray());
        map.PrintCoursesOfStudent("Ivanov".ToCharArray());
        map.PrintCoursesOfStudent("Molotov".ToCharArray());
        map.PrintCoursesOfStudent("Petrov".ToCharArray());
        map.PrintCoursesOfStudent("Sidorov".ToCharArray());
    }
}