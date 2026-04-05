using Lab1.Cursor;   // выбор текущей реализации

namespace Lab1;

public class Program
{
    public static void Main()
    {
        ListT<People> list = new ListT<People>(); // создаем пустой лист
        Start_list(list);
        list.PrintList(); // вывод

        Delete_dublicates(list);
        Console.WriteLine("\n Список после удаления дубликатов:");
        list.PrintList(); // вывод

        Console.WriteLine("=== ПРОГРАММА ЗАВЕРШЕНА ===");
    }
    
    public static void Start_list(ListT<People> L)   // Добавляем адресатов с повторениями
    {
        L.Insert(new People("Андрей Соколов", "Казань, ул. Чистопольская 42"), L.End());
        L.Insert(new People("Дмитрий Васильев", "Москва, ул. Тверская 15"), L.End());
        L.Insert(new People("Екатерина Беляева", "Екатеринбург, ул. Малышева 5"), L.End());
        L.Insert(new People("Максим Павлов", "Челябинск, ул. Кирова 12"), L.End());
        L.Insert(new People("Юлия Федорова", "Нижний Новгород, ул. Рождественская 8"), L.End());
        L.Insert(new People("Андрей Соколов", "Казань, ул. Чистопольская 42"), L.End()); // Повтор
        L.Insert(new People("Кирилл Михайлов", "Омск, ул. Ленина 20"), L.End());
        L.Insert(new People("Татьяна Никитина", "Ростов-на-Дону, ул. Пушкинская 3"), L.End());
        L.Insert(new People("Илья Степанов", "Красноярск, пр. Мира 55"), L.End());
        L.Insert(new People("Екатерина Беляева", "Екатеринбург, ул. Малышева 5"), L.End()); // Повтор
        L.Insert(new People("Алина Семенова", "Воронеж, ул. Плехановская 17"), L.End());
        L.Insert(new People("Виктор Кузьмин", "Пермь, ул. Сибирская 9"), L.End());
        L.Insert(new People("Дмитрий Васильев", "Москва, ул. Тверская 15"), L.End()); // Повтор
        L.Insert(new People("Полина Григорьева", "Саратов, ул. Вольская 33"), L.End());
        L.Insert(new People("Максим Павлов", "Челябинск, ул. Кирова 12"), L.End()); // Повтор
        L.Insert(new People("Анастасия Титова", "Тюмень, ул. Республики 27"), L.End());
        L.Insert(new People("Роман Макаров", "Ульяновск, ул. Гончарова 11"), L.End());
        L.Insert(new People("Юлия Федорова", "Нижний Новгород, ул. Рождественская 8"), L.End()); // Повтор
        L.Insert(new People("Глеб Егоров", "Иркутск, ул. Карла Маркса 6"), L.End());
        L.Insert(new People("Глеб Егоров", "Иркутск, ул. Карла Маркса 6"), L.End());   // Повтор
    }


    public static void Delete_dublicates(ListT<People> L)
    {
        Position<People> p = L.First();
        Position<People> end = L.End();

        while (p.Equals(end) == false)
        {
            Position<People> q = L.Next(p);
            while (q.Equals(end) == false)
            {
                if (L.Retrieve(p).Equals(L.Retrieve(q)))
                {
                    Position<People> toDelete = q;
                    q = L.Next(q);
                    L.Delete(toDelete);
                }
                else
                {
                    q = L.Next(q);
                }
            }
            p = L.Next(p);
        }
    }
}

