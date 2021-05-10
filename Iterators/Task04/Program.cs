using System;
using System.Collections;

/* На вход подается число N.
 * Нужно создать коллекцию из N квадратов последовательного ряда натуральных чисел 
 * и вывести ее на экран дважды. Элементы коллекции разделять пробелом. 
 * Выводы всей коллекции разделять переходом на новую строку.
 * Не хранить всю коллекцию в памяти.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
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
                int value = int.Parse(Console.ReadLine());
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
        private int limit = 0;
        
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
            if (limit < 0)
                throw new ArgumentException("Нельзя");
            this.limit = limit;
            Reset();
            return (IEnumerator)this;
        }

        public object Current
        {
            get => currentInt*currentInt;
        }
    }
}
