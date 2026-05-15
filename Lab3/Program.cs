//using Dict = Lab3.Open.Dict;
using Dict = Lab3.Closed.Dict;

/* общество защиты тунцов (ОЗТ) имеет базу данных с записями результатов самого последнего голосования законодателей по законопроектам об охране тунцов. 
База данных состоит из двух списков (множеств) имен законодателей, которые названы goodguys (хорошие парни) и badguys (плохие парни). 
Например, после голосования все законодатели, проголосовавшие за законопроект, заносятся в список goodguys и удаляются из списка badguys, т
огда как над оппонентами этого законопроекта совершается обратная процедура. 
Законодатели, не принимавшие участие в голосовании, остаются в тех списках, в которых они были ранее. Для управления описываемой базой данных 
при вводе имен законодателей применяются односимвольные команды, за символом команды через пробел следуют до 10 символов – имя законодателя. 
Каждая команда располагается в отдельной строке. Используются следующие односимвольные команды:
F – (законодатель голосовал «правильно»);
U –  (законодатель голосовал «неправильно»);
? –  (надо определить статус законодателя).
Использовать символы 'P' без имени для вывода обоих списков на экран и 'Е' без имени для 
обозначения окончания работы (ввода списков законодателей и их вывода на экран).
*/

class Program
{
    private static int size = 11;
    private static char[] mass = new char[size];

    // Переводит строку в массив без выделения новой памяти
    static char[] ToCharArray(string name)
    {
        int len = name.Length;
        
        // Копируем имя в массив (не более 10 символов)
        for (int i = 0; i < name.Length && i < size; i++)
        {
            mass[i] = name[i];
        }

        mass[len] = '\0';
        return mass;
    }

    static void WhereIs(char[] name, Dict good, Dict bad)
    {
        Console.WriteLine(good.Member(name) ? "good" : bad.Member(name) ? "bad" : "unknown");
    }

    static void Print(Dict goodguys, Dict badguys)
    {
        Console.Write("good: ");
        goodguys.Print();
        Console.Write("bad: ");
        badguys.Print();
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        Dict goodguys = new Dict();
        Dict badguys = new Dict();
        
        Console.WriteLine("Введите команды (F имя, U имя, ? имя, P, E):");
        
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            
            if (string.IsNullOrEmpty(input))
                continue;
            
            string[] parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            char command = parts[0][0];
            
            if (command == 'E' || command == 'e')
            {
                Console.WriteLine("Программа завершена.");
                break;
            }
            
            if (command == 'P' || command == 'p')
            {
                Print(goodguys, badguys);
                continue;
            }
            
            if (parts.Length < 2)
            {
                Console.WriteLine("Ошибка: для команды требуется имя законодателя");
                continue;
            }
            
            string nameStr = parts[1];
            if (nameStr.Length > 10)
                nameStr = nameStr.Substring(0, 10);
            
            char[] name = ToCharArray(nameStr);
            
            switch (command)
            {
                case 'F':
                case 'f':
                    goodguys.Insert(name);
                    badguys.Delete(name);
                    break;
                    
                case 'U':
                case 'u':
                    badguys.Insert(name);
                    goodguys.Delete(name);
                    break;
                    
                case '?':
                    WhereIs(name, goodguys, badguys);
                    break;
                    
                default:
                    Console.WriteLine($"Неизвестная команда: {command}");
                    break;
            }
        }
    }
}