using System.Diagnostics;
using System.Text;

class Program
{
    class NameComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, true);
        }
    }

    class Person
    {
        public Person(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
    private static Random randomSeed = new Random();
    static void Main()
    {
        static void Sort(List<Person> list)
        {
            list.Sort((p1, p2) => string.Compare(p1.Name, p2.Name, true));
        }

        static void OrderBy(List<Person> list)
        {
            var result = list.OrderBy(n => n.Name, new NameComparer()).ToArray();
        }

        //Senario 1
        /*
        List<Person> persons = new List<Person>();
        persons.Add(new Person("P005", "Janson"));
        persons.Add(new Person("P002", "Aravind"));
        persons.Add(new Person("P007", "Kazhal"));

        Sort(persons);
        OrderBy(persons);

        const int COUNT = 1000000;
        Stopwatch watch = Stopwatch.StartNew();
        for (int i = 0; i < COUNT; i++)
        {
            Sort(persons);
        }
        watch.Stop();
        Console.WriteLine("Sort: {0}ms", watch.ElapsedMilliseconds);

        watch = Stopwatch.StartNew();
        for (int i = 0; i < COUNT; i++)
        {
            OrderBy(persons);
        }
        watch.Stop();
        Console.WriteLine("OrderBy: {0}ms", watch.ElapsedMilliseconds);
    // In this case: Sort Speed is more than orderby
 */

        //----------------------------------------------------------
        //Senario 2
        /*
        List<Person> persons = new List<Person>();
        for (int i = 0; i < 100000; i++)
        {
            persons.Add(new Person("P" + i.ToString(), "Janson" + i.ToString()));
        }

        Sort(persons);
        OrderBy(persons);

        const int COUNT = 30;
        Stopwatch watch = Stopwatch.StartNew();
        for (int i = 0; i < COUNT; i++)
        {
            Sort(persons);
        }
        watch.Stop();
        Console.WriteLine("Sort: {0}ms", watch.ElapsedMilliseconds);//5289

        watch = Stopwatch.StartNew();
        for (int i = 0; i < COUNT; i++)
        {
            OrderBy(persons);
        }
        watch.Stop();
        Console.WriteLine("OrderBy: {0}ms", watch.ElapsedMilliseconds);//6444

        // In this case: Sort Speed is more than orderby
        */

        //--------------------------------------------------------------------------------
        //Senario3

        List<Person> persons = new List<Person>();
        for (int i = 0; i < 100000; i++)
        {
            persons.Add(new Person("P" + i.ToString(), RandomString(5, true)));
        }
        Stopwatch watch = Stopwatch.StartNew();
        Sort(persons);//

        watch.Stop();
        Console.WriteLine("Sort: {0}ms", watch.ElapsedMilliseconds);//570ms

        watch = Stopwatch.StartNew();
        OrderBy(persons);
        watch.Stop();
        Console.WriteLine("Orderby: {0}ms", watch.ElapsedMilliseconds);//416ms
    }

    private static string RandomString(int size, bool lowerCase)
    {
        var sb = new StringBuilder(size);
        int start = (lowerCase) ? 97 : 65;
        for (int i = 0; i < size; i++)
        {
            sb.Append((char)(26 * randomSeed.NextDouble() + start));
        }
        return sb.ToString();
    }
    // In this case: Orderby is faster bthan Sort
}

//Resource: https://stackoverflow.com/questions/1832684/c-sharp-sort-and-orderby-comparison