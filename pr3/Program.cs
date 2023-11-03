using System.Collections;

namespace pr3
{
    public class Chapters
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

            stack.Append('?');
            stack.Prepend('!');

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
            Chapters chapters = new Chapters();

        }


        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }

    }
}