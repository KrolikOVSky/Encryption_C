using System;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace encryption
{
    class Program
    {
        private static char[] textToUnicode(string text)
        {
            string codeString = "";
            char[] arr = text.ToCharArray();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                codeString += string.Format("{0:x4}", (int)arr[i]);
            }
            char[] code = codeString.ToCharArray();
            Console.WriteLine("Length = {0}\n", code.GetLength(0));
            return code;
        }

        private static void arrayList(char[,] array)
        {
            int length = array.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine("");
            }
            //Console.WriteLine();
        }

        private static void arrayList2(char[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                Console.Write(array[i]);
            }
            Console.WriteLine();
        }

        private static void arrayList3(int[] array)
        {
            int length = array.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                Console.Write(array[i]);
            }
            Console.WriteLine();
        }

        private static int arrayLength(char[] codeString)
        {
            int textLength = codeString.GetLength(0);
            double arrayLengthDouble = Math.Sqrt(textLength);
            double arrayLengthDoubleRest = Math.Sqrt(textLength) % 1;
            if (arrayLengthDoubleRest > 0)
            {
                arrayLengthDouble = Math.Sqrt(textLength) + 1;
            }
            int arrayLength = (int)Math.Round(arrayLengthDouble) + 1;
            return arrayLength;
        }

        private static char[,] fillingOfArray(char[,] array, char[] code)
        {
            int textLength = code.Length;
            int arrayLength = array.GetLength(0);
            //Console.WriteLine("textLength = {0} \t arrayLength = {1}", textLength, arrayLength);
            int q = 0;
            for (int i = 1; i < arrayLength; i++)
            {
                if (q == textLength)
                {
                    continue;
                }
                for (int j = 1; j < arrayLength; j++)
                {
                    if (q == textLength)
                    {
                        array[i, j] = '0';
                        continue;
                    }
                    array[i, j] = code[q];
                    q++;
                }
            }

            nameOfLinesColumns(array);
            return array;
        }

        private static char[,] nameOfLinesColumns(char[,] a)
        {
            char letters = (char)65;
            int length = a.GetLength(0);
            int i = 1;
            while (letters < 91 && i < length)
            {
                a[0, i] = letters;
                i++;
                letters++;
            }

            letters = (char)97;
            while (letters < 123 && i < length)
            {
                a[0, i] = letters;
                i++;
                letters++;
            }

            letters = (char)65;
            i = 1;
            while (letters < 91 && i < length && a[i, 1] != 0x0000)
            {
                a[i, 0] = letters;
                i++;
                letters++;
            }

            letters = (char)97;
            while (letters < 123 && i < length && a[i, 1] != 0x0000)
            {
                a[i, 0] = letters;
                i++;
                letters++;

            }
            return a;
        }

        private static char[,] changeLines(char[,] array, int n, int m)
        {
            --n;
            --m;
            char t;
            int length = array.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                t = array[m, i];
                array[m, i] = array[n, i];
                array[n, i] = t;
            }
            return array;
        }

        private static char[,] changeColumns(char[,] array, int n, int m)
        {
            --n;
            --m;
            char t;
            int length = array.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                t = array[i, m];
                array[i, m] = array[i, n];
                array[i, n] = t;
            }
            return array;
        }

        private static char[,] changeColumnsByKey(char[,] mainArray)
        {
            int length = mainArray.GetLength(0);
            char[,] array = new char[length, length];
            int[] keyEnc = keyForEncrypting(mainArray);
            arrayList3(keyEnc);
            for (int i = 0, k = 1; i < keyEnc.GetLength(0); i++, k++)
            {
                for (int j = 0; j < length; j++)
                {
                    array[j, k] = mainArray[j, keyEnc[i]];
                }
            }
            for (int i = 0; i < length; i++)
            {
                array[i, 0] = mainArray[i, 0];
            }
            return array;
        }

        private static char[,] changeLinesByKey(char[,] mainArray)
        {
            int length = mainArray.GetLength(0);
            char[,] array = new char[length, length];
            int[] keyEnc = keyForEncrypting(mainArray);
            arrayList3(keyEnc);
            for (int i = 0, k = 1; i < keyEnc.GetLength(0); i++, k++)
            {
                for (int j = 0; j < length; j++)
                {
                    array[k, j] = mainArray[keyEnc[i], j];
                }
            }
            for (int i = 0; i < length; i++)
            {
                array[0, i] = mainArray[0, i];
            }
            return array;
        }

        private static int random(int min, int max)
        {
            int diff = max - min;
            Random random = new Random();
            int i = random.Next(diff + 1);
            i += min;
            return i;
        }

        private static int[] keyForEncrypting(char[,] mainArray)
        {
            //Random random = new Random();
            int length = mainArray.GetLength(0) - 1;
            //Console.WriteLine("Length in method = {0}", length);
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                int r = random(1, length);
                int k = 0;
                while (k < i)
                {
                    if (array[i] == 0)
                    {
                        if (array[k] != r)
                        {
                            k++;
                            continue;
                        }
                        r = random(1, length);
                        k = 0;
                    }
                }
                array[i] = r;
            }
            Console.WriteLine("Length in method = {0}", array.GetLength(0));
            Console.Beep(500, 100);
            return array;
        }

        static void Main(string[] args)
        {
            Console.Write("Enter word = ");
            string text = Console.ReadLine();
            char[] code = textToUnicode(text);
            arrayList2(code);
            char[,] myArray = new char[arrayLength(code), arrayLength(code)];
            fillingOfArray(myArray, code);
            arrayList(myArray);
            myArray = changeColumnsByKey(myArray);
            arrayList(myArray);
            arrayList(changeLinesByKey(myArray));
            //Console.WriteLine("GetLength(0) = {0}___GetLength(1) = {1}",myArray.GetLength(0), myArray.GetLength(1));
            //arrayList3(keyForEncrypting(myArray));
            //keyForEncrypting(myArray);
            Console.ReadKey();
        }
    }
}
