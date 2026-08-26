using System;

namespace Lab4;

public class PredecessorsList
{
    private Element head;

    // Базовый класс
    public class Predecessor
    {
        public Element nextElement;   // ссылка на следующий элемент в списке

        public Predecessor nextEdge;       // ссылка на следующее ребро в списке (следующий последователь)

        public Predecessor(Element nextElement, Predecessor nextEdge)  // конструктор
        {
            this.nextElement = nextElement;
            this.nextEdge = nextEdge;
        }
    }

    // Вершина
    public class Element : Predecessor
    {
        public int value;      // числовое значение вершины
        public int inDegree;   // количество входящих рёбер

        // Конструктор для создания вершины
        public Element(int value, Element nextElement) 
            : base(nextElement, null)
        {
            this.value = value;
            this.inDegree = 0;
        }
    }


    // Поиск вершины по значению. Если вершина уже существует, возвращается ссылка на неё
    private Element FindNode(int value)
    {
        // проходим по узлам, если значения узла совпадает с value, возвращаем этот узел
        Element cur = head;  

        while (cur != null)
        {
            if (cur.value == value)
                return cur;

            cur = cur.nextElement;
        }

        return null;
    }


    // Инициализация (строит граф из входных данных)
    public bool Initialize(PartialOrderSet set)
    {
        head = null;

        Pair[] pairs = set.GetPairs();
        int n = pairs.Length;

        if (n == 0)
            return true;

        // первая пара отдельно
        int from = pairs[0].First;
        int to = pairs[0].Second;

        if (from == to) // антирефлексивность
            return false;

        Element fromNode1 = new Element(from, null);
        head = fromNode1;

        Element toNode1 = new Element(to, head);
        head = toNode1;

        fromNode1.nextEdge = new Predecessor(toNode1, null);
        toNode1.inDegree++;

        // остальные пары
        for (int i = 1; i < n; i++)
        {
            from = pairs[i].First;
            to = pairs[i].Second;

            if (from == to)
                return false;

            Element fromNode = FindNode(from);
            Element toNode = FindNode(to);

            // fromNode отсутствует
            if (fromNode == null)
            {
                fromNode = new Element(from, head);
                head = fromNode;
            }

            // toNode отсутствует
            if (toNode == null)
            {
                toNode = new Element(to, head);
                head = toNode;
            }

            // создаём ребро от fromNode к toNode
            Predecessor edge = new Predecessor(toNode, fromNode.nextEdge);
            fromNode.nextEdge = edge;
            toNode.inDegree++;
        }

        return true;
    }

    // ищем элемент без предшественников
    private Element? FindZeroInDegreeElement()
    {
        Element? prev = null;
        Element? cur = head;
    
        while (cur != null)
        {
            if (cur.inDegree == 0)
                return prev;
            prev = cur;
            cur = cur.nextElement;
        }
    
        return null;
    }

    // удаление ребер у вершины
    private void RemoveEdges(Element cur)
    {
        Predecessor e = cur.nextEdge;
        while (e != null)
        {
            e.nextElement.inDegree--;   // уменьшаем количество предшественников у входящей вершин
            e = e.nextEdge;
        }
        cur.nextEdge = null;
    }

    // удаление вершины
    private void RemoveElement(Element prev, Element cur)
    {
        // переходим на следующий элемент
        if (prev == null)
            head = cur.nextElement;  
        else
            prev.nextElement = cur.nextElement;

        cur.nextElement = null;
    }


    // Топологическая сортировка
    public bool TopologySort()
    {
        Element resultHead;
        Element resultTail;

        Element? prev;
        Element? cur;

        // первый элемент результата
        prev = FindZeroInDegreeElement();  // ищем вершину, у которой нет предшественников
        if (prev == null)
        {
            if (head == null || head.inDegree != 0)  // отдельно обрабатываем голову и случай, если есть дальше цикл
                return false;
            cur = head;  // найденная вершина - голова
        }
        else
            cur = prev.nextElement;   // найденная вершина

        RemoveEdges(cur);  // убираем ребра (списки последователей)
        resultHead = cur;   // записываем вершину в отсортированный список
        resultTail = cur;
        RemoveElement(prev, cur);   // удаляем вершину из списка

        // остальные элементы
        while (head != null)
        {
            prev = FindZeroInDegreeElement();   // ищем вершину, у которой нет предшественников

            if (prev == null)
            {
                if (head == null || head.inDegree != 0)   // отдельно обрабатываем голову и случай, если есть дальше цикл
                    return false;

                cur = head;
            }
            else
                cur = prev.nextElement;

            RemoveEdges(cur);    // убираем ребра (списки последователей)
            resultTail.nextElement = cur;   // записываем вершину в отсортированный список
            resultTail = cur;
            RemoveElement(prev, cur);     // удаляем вершину из списка
        }

        head = resultHead;
        return true;
    }


    // Вывод
    public void Print()
    {
        Element cur = head;

        while (cur != null)
        {
            Console.Write(cur.value + " ");
            cur = cur.nextElement;
        }

        Console.WriteLine();
    }
}