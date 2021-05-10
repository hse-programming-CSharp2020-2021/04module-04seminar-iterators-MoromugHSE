using System;
using System.Collections;
using System.Text;

/* На вход подается число N.
 * Нужно создать коллекцию из N элементов последовательного ряда натуральных чисел, возведенных в 10 степень, 
 * и вывести ее на экран ТРИЖДЫ. Инвертировать порядок элементов при каждом последующем выводе.
 * Элементы коллекции разделять пробелом. 
 * Очередной вывод коллекции разделять переходом на новую строку.
 * Не хранить всю коллекцию в памяти.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield и foreach. Не вызывать метод Reset() в классе Program.
 * 
 * Пример входных данных:
 * 2
 * 
 * Пример выходных данных:
 * 1 1024
 * 1024 1
 * 1 1024
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
 * В других ситуациях выбрасывайте ArithmeticException.
*/
namespace Task05
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MyDigits myDigits = new MyDigits();
                if (!int.TryParse(Console.ReadLine(), out int value) || value <= 0)
                {
                    throw new ArgumentException("Нельзя");
                }
                IEnumerator enumerator = myDigits.MyEnumerator(value);

                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
            catch (ArithmeticException)
            {
                Console.WriteLine("ooops");
            }
        }

        static void IterateThroughEnumeratorWithoutUsingForeach(IEnumerator enumerator)
        {
            var sb = new StringBuilder(); 
            if (enumerator.MoveNext())
            {
                sb.Append(enumerator.Current);
            }
            while (enumerator.MoveNext())
            {
                sb.Append($" {enumerator.Current}");
            }
            Console.Write(sb.ToString());
        }
    }

    class MyDigits : IEnumerator // НЕ МЕНЯТЬ ЭТУ СТРОКУ
    {
        private int step = 1;
        private int currentNumber = 0;
        private int limit = 0;

        public bool MoveNext()
        {
            var nextNumber = currentNumber + step;
            if (nextNumber <= 0 || nextNumber > limit)
            {
                Reset();
                return false;
            }
            currentNumber = nextNumber;
            return true;
        }

        public void Reset()
        {
            step = -step;
            currentNumber = step > 0 ? 0 : limit+1;
        }

        public IEnumerator MyEnumerator(int limit)
        {
            this.limit = limit;
            return this;
        }

        public object Current
        {
            get
            {
                var result = 1;
                for (int i = 0; i < 10; ++i)
                {
                    // OverflowException - это произвдное от ArithmeticException,
                    // так что в мэйне всё поймается правильно.
                    result = checked(result * currentNumber);
                }
                return result;
            }
        }

    }
}
