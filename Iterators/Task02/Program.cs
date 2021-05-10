﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
В основной программе объявите и инициализируйте одномерный строковый массив 
и выполните циклический перебор его элементов с разных «начальных точек», 
разделяя элементы одним пробелом.

Тестирование приложения выполняется путем запуска разных наборов тестов.
На вход в первой строке поступает число - номер элемента, начиная с которого 
пойдет циклический перебор.
В следующей строке указаны элементы последовательности, разделенные одним или 
несколькими пробелами.
3
1 2 3 4 5
Программа должна вывести на экран:
3 4 5 1 2

В случае ввода некорректных данных выбрасывайте ArgumentException.

Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.

 */
namespace Task02
{
    class IteratorSample : IEnumerable<string> // НЕ МЕНЯТЬ
    {
        string[] values;
        int start;

        public IteratorSample(string[] values, int start)
        {
            this.values = values;
            this.start = start - 1;
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            for (int i = 0; i < values.Length; ++i)
            {
                yield return values[(start + i) % values.Length];
            }
        }

        // Не представляю, что сюда правильно писать.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int startingIndex;
                if (!int.TryParse(Console.ReadLine(), out startingIndex))
                    throw new ArgumentException("Нет");
                string[] values = Console.ReadLine().Split();

                foreach (string ob in new IteratorSample(values, startingIndex))
                    Console.Write(ob + " ");
                Console.WriteLine();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
            catch (Exception)
            {
                Console.WriteLine("problem");
            }

            Console.ReadLine();
        }
    }
}
