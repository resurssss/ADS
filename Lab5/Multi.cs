namespace Lab5;

public class Multi
{
    private const int size = 20; 

    private readonly ListStudent studentList;
    private readonly ListCourse courseList;

    public Multi()
    {
        studentList = new ListStudent();
        courseList = new ListCourse();
    }

    // Инициализация 
    public void Init()
    {
        // добавление студентов
        studentList.Insert("Ivanov".ToCharArray());
        studentList.Insert("Petrov".ToCharArray());
        studentList.Insert("Sidorov".ToCharArray());
        studentList.Insert("Molotov".ToCharArray());

        // добавление курсов
        courseList.Insert("CS1".ToCharArray());
        courseList.Insert("CS2".ToCharArray());
        courseList.Insert("CS3".ToCharArray());
    }


    public void AddStudentToCourse(char[] s, char[] c)
    {
        // получить студента
        int studentIndex = studentList.Member(s);
        Students? student = studentIndex == -1 ? null : studentList.GetStudent_ar()[studentIndex];

        // получить курс
        int courseIndex = courseList.Member(c);
        Course? course = courseIndex == -1 ? null : courseList.GetCourse_ar()[courseIndex];

        if (student == null || course == null)    // если студент или курс не существуют
            return;

        Register? link;  // текущая регистрационная запись

        if (student.Course == null)  // у студента нет курсов
        {
            link = new Register();  // создаем первую рег запись у студента
            student.Course = link;
            link.Students = student;  // замыкаем на узел студента

            if (course.Students == null)   // на курсе еще нет студентов
            {
                link.Course = course;    // замыкаем на сам курс
                course.Students = link;
                return;
            }

            // у студента нет курсов, но на курсе студенты уже есть 
            link.Course = course.Students;    // новая запись указывает на старую первую запись курса
            course.Students = link;  // вставляем в головуы
            return;
        }

        // у студента уже есть курсы

        if (GetPrevNodeStudentAtCourse(student, course) != null)    // проверяем, записан ли уже студент на этот курс
            return;
        
        link = new Register();    // создаём новую регистрационную запись
        link.Students = student.Course;   // вставка в голову, новая запись указывает на прошлую первую запись курса
        student.Course = link;

        // на этом курсе ещё нет студентов
        if (course.Students == null)
        {
            link.Course = course;   // вставка в голову
            course.Students = link;
            return;
        }

        // у курса уже есть студенты
        link.Course = course.Students;
        course.Students = link;
    }


    // Удаляет студента с курса
    public void RemoveStudentFromCourse(char[] s, char[] c)
    {
        // получить студента
        int studentIndex = studentList.Member(s);
        Students? student = studentIndex == -1 ? null : studentList.GetStudent_ar()[studentIndex];
        if (student == null) 
            throw new InvalidOperationException("Студента не существует в коллекции.");

        // получить курс
        int courseIndex = courseList.Member(c);
        Course? course = courseIndex == -1 ? null : courseList.GetCourse_ar()[courseIndex];
        if (course == null) 
            throw new InvalidOperationException("Курс не существует в коллекции.");

        // ищем предыдущий узел перед записью, связывающей студента с курсом
        BaseN? prevStudent = GetPrevNodeStudentAtCourse(student, course);

        if (prevStudent == null) return;  // связь не найдена
        // удаление записи у студента
        if (prevStudent.Not_register())  // prevStudent это Students (удаляемый первый в цепочке)
        {
            Students prevNode = (Students)prevStudent;
            BaseN newNode = prevNode.Course!.Students;  // следующий за удаляемым
            if (!newNode.Not_register())          // если после удаляемого идёт след запись
                prevNode.Course = (Register)newNode;  // newNode становится головой 
            else                               // если замыкание на студента (то есть была одна запись)
                prevNode.Course = null;  // обнуляем, теперь курсов нет
        }
        else  // prevStudent это Register
        {
            Register prevLink = (Register)prevStudent;
            prevLink.Students = ((Register)prevLink.Students).Students;  // перепригиваем через удаляемый
        }
        
        // ищем предыдущий узел перед записью, связывающей курс со студентом
        BaseN prevCourse = GetPrevNodeCourseAtStudent(student, course)!;
        // удаление записи у курса
        if (prevCourse.Not_register())  // prevCourse это Course (удаляемый первый в цепочке)
        {
            Course prevNode = (Course)prevCourse;
            BaseN newNode = prevNode.Students!.Course;  // следующий за удаляемым
            if (!newNode.Not_register())          // если после удаляемого идёт след запись
                prevNode.Students = (Register)newNode;   // newNode становится головой
            else                               // если замыкание на курс (то есть была одна запись)
                prevNode.Students = null;   // обнуляем, теперь студентов нет
        }
        else   // prevCourse это Register
        {
            Register prevLink = (Register)prevCourse;
            prevLink.Course = ((Register)prevLink.Course).Course;   // перепригиваем через удаляемый
        }
    }


    // Удаляет студента со всех курсов
    public void RemoveStudent(char[] s)
    {
        // получить студента
        int studentIndex = studentList.Member(s);
        Students? student = studentIndex == -1 ? null : studentList.GetStudent_ar()[studentIndex];
        if (student == null) 
            throw new InvalidOperationException("Студента не существует в коллекции.");

        BaseN? cur = student.Course;    // голова записей
        if (cur == null) return;

        while (cur != student)  // пока не обойдем все кольцо
        {
            // сохранить следующую запись до удаления
            BaseN nextCur = ((Register)cur).Students;

            // спуститься по цепочке Course до самого курса
            Course curNode = GoDownToCourse(cur);

            // найти предыдущий узел в цепочке курса
            BaseN prevCourse = GetPrevNodeFromCourse_reg(curNode, (Register)cur)!; 

            // удаление этого студента с курса
            if (prevCourse.Not_register())  // prevCourse это Course (удаляемый первый в цепочке)
            {
                Course prevNode = (Course)prevCourse;
                BaseN newNode = prevNode.Students!.Course;  // следующий за удаляемым
                if (!newNode.Not_register())          // если после удаляемого идёт след запись
                    prevNode.Students = (Register)newNode;   // newNode становится головой
                else                               // если замыкание на курс (то есть была одна запись)
                    prevNode.Students = null;   // обнуляем, теперь студентов нет
            }
            else   // prevCourse это Register
            {
                Register prevLink = (Register)prevCourse;
                prevLink.Course = ((Register)prevLink.Course).Course;   // перепригиваем через удаляемый
            }

            // перейти к следующей записи студента (сохранённой ранее)
            cur = nextCur;
        }

        student.Course = null;
    }


    // Удаляет всех студентов с курса, сам курс остаётся в хеш-таблице
    public void RemoveCourse(char[] c)
    {
        // получить курс
        int courseIndex = courseList.Member(c);
        Course? course = courseIndex == -1 ? null : courseList.GetCourse_ar()[courseIndex];
        if (course == null) 
            throw new InvalidOperationException("Курс не существует в коллекции.");

        BaseN? cur = course.Students;
        if (cur == null) return;

        while (cur != course)
        {
            // сохранить следующую запись до удаления
            BaseN nextCur = ((Register)cur).Course;

            // спуститься по цепочке Students до самого студента
            Students curNode = GoDownToStudent(cur);

            // найти предыдущий узел в цепочке студента и удалить
            BaseN prevStudent = GetPrevNodeFromStudent_reg(curNode, (Register)cur)!;   
            // удаление этого курса у студента
            if (prevStudent.Not_register())  // prevStudent это Students (удаляемый первый в цепочке)
            {
                Students prevNode = (Students)prevStudent;
                BaseN newNode = prevNode.Course!.Students;  // следующий за удаляемым
                if (!newNode.Not_register())          // если после удаляемого идёт след запись
                    prevNode.Course = (Register)newNode;  // newNode становится головой 
                else                               // если замыкание на студента (то есть была одна запись)
                    prevNode.Course = null;  // обнуляем, теперь курсов нет
            }
            else  // prevStudent это Register
            {
                Register prevLink = (Register)prevStudent;
                prevLink.Students = ((Register)prevLink.Students).Students;  // перепригиваем через удаляемый
            }

            // перейти к следующей записи курса (сохранённой ранее)
            cur = nextCur;
        }

        course.Students = null;
    }


    // выводит списков всех студентов, которые посещают этот курс
    public void PrintStudentsOfCourse(char[] c)
    {
        // получить курс
        int courseIndex = courseList.Member(c);
        Course? course = courseIndex == -1 ? null : courseList.GetCourse_ar()[courseIndex];

        if (course == null)
        {
            Console.WriteLine("Курс не найден");
            return;
        }

        Console.Write($"{new string(c)}: ");

        BaseN? cur = course.Students;   // первый узел в списке студентов, записанных на этот курс
        if (cur == null)    // нет ни одной записи
        {
            Console.WriteLine();
            return;
        }

        while (!cur.Not_register())   // пока текущий узел является Register
        {
            Students st = GoDownToStudent(cur);
            Console.Write($"{new string(st.Name)} ");
            cur = ((Register)cur).Course;
        }

        Console.WriteLine();
    }


    // Выводит список всех курсов, которые посещает студент
    public void PrintCoursesOfStudent(char[] s)
    {
        // получить студента
        int studentIndex = studentList.Member(s);
        Students? student = studentIndex == -1 ? null : studentList.GetStudent_ar()[studentIndex];

        if (student == null)
        {
            Console.WriteLine("Студент не найден");
            return;
        }

        Console.Write($"{new string(s)}: ");

        BaseN? cur = student.Course;  // первый узел в списке курсов, на которые записан студент
        if (cur == null)    // нет ни одной записи
        {
            Console.WriteLine();
            return;
        }

        while (!cur.Not_register())
        {
            // спускаемся до курса
            Course course = GoDownToCourse(cur);
            Console.Write($"{new string(course.Name)} ");
            cur = ((Register)cur).Students;
        }

        Console.WriteLine();
    }


    // Спускается по цепочке Students от заданного узла до конечного студента
    private static Students GoDownToStudent(BaseN startNode)
    {
        BaseN node = startNode;
        while (!node.Not_register())              // пока не конечный узел
            node = ((Register)node).Students!;
        return (Students)node;
    }

    // Спускается по цепочке Course от заданного узла до конечного курса
    private static Course GoDownToCourse(BaseN startNode)
    {
        BaseN node = startNode;
        while (!node.Not_register())              // пока не конечный узел
            node = ((Register)node).Course!;
        return (Course)node;
    }


    // Ищет предыдущий узел перед записью, связывающей студента с курсом в цепочке студента
    private static BaseN? GetPrevNodeStudentAtCourse(Students student, Course course)
    {
        BaseN prev = student;
        BaseN? cur = student.Course; // голова

        if (cur == null) return null;   // у студента нет курсов

        while (cur != student)   // пока не обойдем все кольцо
        {
            // спуститься по цепочке Course до конечного узла
            Course curNode = GoDownToCourse(cur);

            if (curNode == course)  // если ссылка на этот курс совпадает с искомым
                return prev;  // возвращаем узел перед ней

            // переход к следующей записи
            prev = cur;
            cur = ((Register)cur).Students;
        }

        return null;
    }

    // Ищет предыдущий узел перед записью, связывающей курс со студентом в цепочке курса
    private static BaseN? GetPrevNodeCourseAtStudent(Students student, Course course)
    {
        BaseN prev = course;
        BaseN? cur = course.Students;  

        if (cur == null) return null;  // на курсе нет студентов

        while (cur != course)  // пока не обойдем все кольцо
        {
            // спуститься по цепочке Students до конечного узла
            Students curNode = GoDownToStudent(cur);

            if (curNode == student)  // если ссылка на этого студента совпадает с искомым
                return prev;

            prev = cur;
            cur = ((Register)cur).Course;
        }

        return null;
    }


    // Ищет предыдущий узел перед заданной регистрационной записью в цепочке студента// Ищет предыдущий узел перед заданной регистрационной записью в цепочке студента
    private static BaseN? GetPrevNodeFromStudent_reg(Students student, Register targetNode)
    {
        BaseN prev = student;
        BaseN? cur = student.Course;

        if (cur == null) return null;

        while (cur != student)   // пока не обошли все кольцо
        {
            if (cur == targetNode)   // если текущий узел совпадает
                return prev; // возвращаем узел перед ним

            prev = cur;
            cur = ((Register)cur).Students;  // переходим на следующий узел
        }

        return null;
    }
    
    // Ищет предыдущий узел перед заданной регистрационной записью в цепочке курса
    private static BaseN? GetPrevNodeFromCourse_reg(Course course, Register targetNode)
    {
        BaseN prev = course;
        BaseN? cur = course.Students;

        if (cur == null) return null;

        while (cur != course)  // пока не обошли все кольцо
        {
            if (cur == targetNode)  // если текущий узел совпадает
                return prev;

            prev = cur;
            cur = ((Register)cur).Course;  // переходим на следующий узел
        }

        return null;
    }
}