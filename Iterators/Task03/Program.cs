using System;
using System.Text;
using System.Collections;
using System.Threading;
using System.Globalization;

/* На вход подается число N.
 * На каждой из следующих N строках записаны ФИО человека, 
 * разделенные одним пробелом. Отчество может отсутствовать.
 * Используя собственноручно написанный итератор, выведите имена людей,
 * отсортированные в лексико-графическом порядке в формате 
 *      <Фамилия_с_большой_буквы> <Заглавная_первая_буква_имени>.
 * Затем выведите имена людей в исходном порядке.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield.
 * 
 * Пример входных данных:
 * 3
 * Banana Bill Bananovich
 * Apple Alex Applovich
 * Carrot Clark Carrotovich
 * 
 * Пример выходных данных:
 * Apple A.
 * Banana B.
 * Carrot C.
 * 
 * Banana B.
 * Apple A.
 * Carrot C.
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
*/
namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            var cult = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cult;
            Thread.CurrentThread.CurrentUICulture = cult;
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                int N = 0;
                if (!int.TryParse(Console.ReadLine(), out N) || N <= 0)
                    throw new ArgumentException("Нельзя");
                Person[] people = new Person[N];
                for (int i = 0; i < N; ++i)
                {
                    var name = Console.ReadLine().Split(new char[] { },
                        StringSplitOptions.RemoveEmptyEntries);
                    if (name.Length < 2)
                        throw new ArgumentException("Это подло...");
                    people[i] = new Person(name[1], name[0]);
                }

                People peopleList = new People(people);

                foreach (Person p in peopleList)
                    Console.WriteLine(p);

                Console.WriteLine();

                foreach (Person p in peopleList.GetPeople)
                    Console.WriteLine(p);
            }
            catch (ArgumentException)
            {
                Console.Write("error");
            }
        }
    }

    public class Person
    {
        public string firstName;
        public string lastName;

        public Person(string firstName, string lastName)
        {
            this.firstName = CultureInfo.CurrentCulture.
                TextInfo.ToTitleCase(firstName.ToLower());
            this.lastName = CultureInfo.CurrentCulture.
                TextInfo.ToTitleCase(lastName.ToLower());
        }

        public override string ToString()
        {
            return $"{lastName} {firstName[0]}.";
        }
    }


    public class People : IEnumerable
    {
        private Person[] _people;
        public Person[] GetPeople
        {
            get
            {
                return _people;
            }
        }

        public People(Person[] people)
        {
            _people = (Person[])people.Clone();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }

    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;
        private readonly IEnumerator enumerator;

        public PeopleEnum(Person[] people)
        {
            _people = (Person[])people.Clone();
            Array.Sort(_people,
                (a, b) => a.ToString().CompareTo(b.ToString()));
            enumerator = _people.GetEnumerator();
        }

        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }

        public void Reset()
        {
            enumerator.Reset();
        }

        object IEnumerator.Current
        {
            get => enumerator.Current;
        }

        public Person Current
        {
            get => (Person)enumerator.Current;
        }
    }
}
