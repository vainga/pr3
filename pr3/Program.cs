using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;

namespace pr3
{
    public interface IComparable
    {
        int CompareTo(object obj);
    }

    public interface IComparer
    {
        int Compare(object o1, object o2);
    }

    public interface ICloneable
    {
        object Clone();
    }

    public class Chapters : IComparable<Chapters>, ICloneable
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Название главы не может быть пустым!");
                name = value;
            }
        }
        private int pages;
        public int Pages
        {
            get { return pages; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("Количество страниц не может быть отрицательным!");
                pages = value;
            }
        }
        public Chapters()
        {
            name = "";
            pages = 0;
        }
        public Chapters(string name, int pages)
        {
            this.name = name;
            this.pages = pages;
        }

        public int CompareTo(Chapters otherChapter)
        {
            if (otherChapter == null) return 1;

            if (otherChapter != null)
            {
                // Сравнение на основе свойства Name
                return pages.CompareTo(otherChapter.pages);
            }
            else
            {
                throw new ArgumentException("Невозможно сравнить два объекта");
            }
        }

        public object Clone()
        {
            return new Chapters {Name = name, Pages = pages};
        }
    }

    public class ChapterCompare:IComparer<Chapters>
    {
        public int Compare(Chapters chapters1, Chapters chapters2)
        {
            if (chapters1.Pages > chapters2.Pages)
                return 1;
            else if (chapters1.Pages < chapters2.Pages)
                return -1;
            else
                return 0;
        }
    } 

    class Programm
    {
        static void Part1()
        {
            ArrayList arrayList = new ArrayList();
            Random rand = new Random();
            string str;

            for (int i = 0; i < 5; i++)
                arrayList.Add(rand.Next().ToString());

            Console.Write("Введите строку, которую котите добавить в коллекцию: ");
            str = Console.ReadLine();
            arrayList.Add(str);

            Console.WriteLine($"Колличество элементов в коллекции: {arrayList.Count}");

            for (int i = 0; i < arrayList.Count; i++)
                Console.WriteLine($"{i}) {arrayList[i]}");


            Console.Write("Введите значение, которое вы хотите найти: ");
            str = Console.ReadLine();

            Console.WriteLine($"Индекс искомого элемента: {arrayList.IndexOf(str)}");

        }

        static void Part2()
        {

            //1 коллекция

            Stack<char> stack = new Stack<char>();
            int n = 0;
            char ch;

            for(char c = 'a'; c <= 'z'; c++)
                stack.Push(c);

            Console.WriteLine("Stack:");
            foreach(char c in stack)
                Console.WriteLine(c);

            Console.Write("Введите кол-во элементов которые вы хотите удалить: ");
            n = int.Parse(Console.ReadLine());

            for(int i = 0; i < n; i++)
                stack.Pop();

            stack.Push('?');

            //2 коллекция

            List<char> list = new List<char>();
            foreach(char c in stack)
                list.Add(c);
            
            Console.WriteLine("List:");
            foreach (char c in list)
                Console.WriteLine(c);

            Console.Write("Введите символ, который вы хотите найти: ");
            ch = char.Parse(Console.ReadLine());

            Console.WriteLine($"Индекс искомого элемента: {list.IndexOf(ch)}");

        }

        static void Part3()
        {
            //1 коллекция

            Stack<Chapters> stack = new Stack<Chapters>();
            int n = 0;
            string searchName;
            string searchName1;
            string searchName2;

            Chapters chapters1 = new Chapters("Chapter 1", 500);
            Chapters chapters2 = new Chapters("Chapter 2", 300);
            Chapters chapters3 = new Chapters("Chapter 3", 2010);
            Chapters chapters4 = new Chapters("Chapter 4", 100);
            Chapters chapters5 = new Chapters("Chapter 5", 400);

            stack.Push(chapters1);
            stack.Push(chapters2);
            stack.Push(chapters3);
            stack.Push(chapters4);
            stack.Push(chapters5);

            Console.WriteLine("Stack:");
            foreach (Chapters c in stack)
                Console.WriteLine($"{c.Name}: {c.Pages}");

            Console.Write("Введите кол-во элементов которые вы хотите удалить: ");
            n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
                stack.Pop();

            Chapters chaptersStart = new Chapters("Start", 1);
            stack.Push(chaptersStart);

            //2 коллекция

            List<Chapters> list = new List<Chapters>();
            foreach (Chapters c in stack)
                list.Add(c);


            Console.WriteLine("List:");
            foreach (Chapters c in list)
                Console.WriteLine($"{c.Name}: {c.Pages}");

            Console.Write("Введите имя главы, которую вы хотите найти: ");
            searchName = Console.ReadLine();

            foreach (Chapters c in list)
                if (c.Name == searchName)
                    Console.WriteLine($"Индекс искомого элемента: {list.IndexOf(c)}");


            //Работа интерфейсов
            list.Sort();
            Console.WriteLine("IComparable");
            foreach (Chapters c in list)
                Console.WriteLine($"{c.Name}: {c.Pages}");

            Console.WriteLine("IComparer");
            list.Sort(new ChapterCompare());
            foreach (Chapters c in list)
                Console.WriteLine($"{c.Name}: {c.Pages}");

            Console.WriteLine("ICloneable");
            Chapters cloneChapter = (Chapters)chapters1.Clone();
            Console.WriteLine($"Клонированная 1 глава: {cloneChapter.Name} {cloneChapter.Pages}");

        }

        static ObservableCollection<Chapters> myCollection = new ObservableCollection<Chapters>();
        static void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine("Элементы были добавлены.");
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine("Элементы были удалены.");
            }
        }
        static void Part4()
        {
            myCollection.CollectionChanged += CollectionChangedHandler;

            // Добавление элементов
            myCollection.Add(new Chapters { Name = "Name 1", Pages = 1});
            myCollection.Add(new Chapters { Name = "Name 2", Pages = 2 });

            Console.WriteLine("После добавления: ");
            foreach(Chapters c in myCollection)
                Console.WriteLine($"{c.Name}: {c.Pages}");


            // Удаление элемента
            var itemToRemove = myCollection.FirstOrDefault(item => item.Name == "Name 1");
            if (itemToRemove != null)
            {
                myCollection.Remove(itemToRemove);
            }
            Console.WriteLine("После удаления: ");
            foreach (Chapters c in myCollection)
                Console.WriteLine($"{c.Name}: {c.Pages}");

        }

        static void Main(string[] args)
        {
            //Part1();
            //Part2();
            //Part3();
            //Part4();
        }

    }
}