using System;

namespace Rozumatic_ASD_Lab6
{
    // ВУЗОЛ ДВОЗВ'ЯЗНОГО СПИСКУ
    public class DoublyNode<T>
    {
        public T Data { get; set; }
        public DoublyNode<T>? Next { get; set; }
        public DoublyNode<T>? Prev { get; set; }

        public DoublyNode(T data)
        {
            Data = data;
        }
    }

    // ДВОЗВ'ЯЗНИЙ СПИСОК
    public class DoublyLinkedList<T>
    {
        private DoublyNode<T>? _head;
        private DoublyNode<T>? _tail;
        private int _count;

        public int Count => _count;

        // Вставка в кінець - O(1)
        public DoublyNode<T> AddLast(T data)
        {
            DoublyNode<T> newNode = new DoublyNode<T>(data);
            if (_tail == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                newNode.Prev = _tail;
                _tail.Next = newNode;
                _tail = newNode;
            }
            _count++;
            return newNode;
        }

        // Прямий обхід (від голови до хвоста)
        public void PrintForward()
        {
            Console.Write($"Прямий обхід [Count={_count}]: null ⇄ ");
            DoublyNode<T>? curr = _head;
            while (curr != null)
            {
                Console.Write($"[{curr.Data}] ⇄ ");
                curr = curr.Next;
            }
            Console.WriteLine("null");
        }

        // ==========================================
        // ІНДИВІДУАЛЬНЕ ЗАВДАННЯ: ВАРІАНТ 2
        // Обмін місцями двох сусідніх вузлів (лише посилання)
        // ==========================================
        public void SwapAdjacent(DoublyNode<T> node1, DoublyNode<T> node2)
        {
            if (node1 == null || node2 == null) return;

            // Перевіряємо та коригуємо порядок, щоб node1 завжди йшов перед node2
            if (node1.Next != node2)
            {
                if (node2.Next == node1)
                {
                    var temp = node1;
                    node1 = node2;
                    node2 = temp;
                }
                else
                {
                    throw new InvalidOperationException("Вузли не є сусідніми!");
                }
            }

            // Запам'ятовуємо зовнішні вузли, які стоять ДО та ПІСЛЯ нашої пари
            DoublyNode<T>? before = node1.Prev;
            DoublyNode<T>? after = node2.Next;

            // 1. Оновлюємо зв'язки зовнішніх вузлів з нашою парою
            if (before != null) before.Next = node2;
            else _head = node2; // якщо node1 був першим, тепер першим стає node2

            if (after != null) after.Prev = node1;
            else _tail = node1; // якщо node2 був останнім, тепер останнім стає node1

            // 2. Переставляємо посилання всередині самої пари
            node2.Prev = before;
            node2.Next = node1;
            node1.Prev = node2;
            node1.Next = after;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  РОЗУМАТИК // ЛАБОРАТОРНА РОБОТА №6: ВАРІАНТ 2 (ОБМІН СУСІДНІХ ВУЗЛІВ)");
            Console.WriteLine("==========================================================================");

            DoublyLinkedList<int> dList = new DoublyLinkedList<int>();
            
            // Створюємо список: 10 ⇄ 20 ⇄ 30 ⇄ 40
            dList.AddLast(10);
            var node20 = dList.AddLast(20);
            var node30 = dList.AddLast(30);
            dList.AddLast(40);

            Console.WriteLine("\n--- Список ДО обміну місцями ---");
            dList.PrintForward();

            Console.WriteLine("\nМіняємо місцями сусідні вузли [20] та [30]...");
            dList.SwapAdjacent(node20, node30);

            Console.WriteLine("\n--- Список ПІСЛЯ обміну місцями ---");
            dList.PrintForward();

            Console.WriteLine("\n✅ Тестування варіанту 2 завершено успішно!");
        }
    }
}
