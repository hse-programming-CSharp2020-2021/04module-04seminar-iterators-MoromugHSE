using System;
using System.Collections;

/* На вход подается число N.
 * Нужно создать коллекцию из N квадратов последовательного ряда натуральных чисел 
 * и вывести ее на экран дважды. Элементы коллекции разделять пробелом. 
 * Выводы всей коллекции разделять переходом на новую строку.
 * Не хранить всю коллекцию в памяти.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * (И это, к слову, отвратительно, потому как я уже не помню, не удалял ли чего...)
 * Не использовать yield и foreach. Не вызывать метод Reset() в классе Program.
 * 
 * Пример входных данных:
 * 3
 * 
 * Пример выходных данных:
 * 1 4 9
 * 1 4 9
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
*/
namespace Task04
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int value = 0;
                if (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
                    throw new ArgumentException("Нельзя");
                MyInts myInts = new MyInts();
                IEnumerator enumerator = myInts.MyEnumerator(value);

                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }

        }

        static void IterateThroughEnumeratorWithoutUsingForeach(IEnumerator enumerator)
        {
            if (enumerator.MoveNext())
            {
                Console.Write(enumerator.Current);
            }
            while (enumerator.MoveNext())
            {
                Console.Write($" {enumerator.Current}");
            }
        }
    }

    class MyInts : IEnumerator // НЕ МЕНЯТЬ ЭТУ СТРОКУ
    {
        private int currentInt = 0;
        private int limit = -1;

        public bool MoveNext()
        {
            if (currentInt >= limit)
            {
                Reset();
                return false;
            }
            ++currentInt;
            return true;
        }

        public void Reset()
        {
            currentInt = 0;
        }

        public IEnumerator MyEnumerator(int limit)
        {
            this.limit = limit;
            return this;
        }

        public object Current
        {
            get => currentInt*currentInt;
        }
    }
}
