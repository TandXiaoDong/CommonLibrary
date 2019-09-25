﻿using System;
using System.Diagnostics;
using fastCSharp.reflection;
using Int = System.UInt32;

namespace fastCSharp.test.radixSort
{
    class uint32
    {
        /// <summary>
        /// 测试数据类型
        /// </summary>
        private static readonly string type = typeof(Int).fullName();
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        private static void random(Int[] number1, Int[] number2)
        {
            for (int index = number1.Length; index != 0;)
            {
                --index;
                number2[index] = number1[index] = (uint)fastCSharp.random.Default.Next();
            }
        }
        /// <summary>
        /// 排序测试
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        private static void sort(Int[] number1, Int[] number2)
        {
            random(number1, number2);
            Stopwatch time = new Stopwatch();
            time.Start();
            Array.Sort(number2);
            time.Stop();
            if (number1.Length < 1 << 10) Console.WriteLine("Array.Sort " + type + "[" + number1.Length.toString() + "] " + time.ElapsedTicks.ToString() + "t");
            else Console.WriteLine("Array.Sort " + type + "[" + number1.Length.toString() + "] " + time.ElapsedMilliseconds.ToString() + "ms");
            time.Reset();
            time.Start();
            number1.sort();
            time.Stop();
            if (number1.Length < 1 << 10) Console.WriteLine("fastCSharp.sort " + type + "[" + number1.Length.toString() + "] " + time.ElapsedTicks.ToString() + "t");
            else Console.WriteLine("fastCSharp.sort " + type + "[" + number1.Length.toString() + "] " + time.ElapsedMilliseconds.ToString() + "ms");
            for (int index = number1.Length; index != 0;)
            {
                --index;
                if (number1[index] != number2[index])
                {
                    Console.WriteLine(type + " sort Error");
                    break;
                }
            }
        }
        /// <summary>
        /// 排序测试
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        private static void sortDesc(Int[] number1, Int[] number2)
        {
            random(number1, number2);
            Stopwatch time = new Stopwatch();
            time.Start();
            Array.Sort(number2);
            Array.Reverse(number2);
            time.Stop();
            if (number1.Length < 1 << 10) Console.WriteLine("Array.Sort+Array.Reverse " + type + "[" + number1.Length.toString() + "] " + time.ElapsedTicks.ToString() + "t");
            else Console.WriteLine("Array.Sort+Array.Reverse " + type + "[" + number1.Length.toString() + "] " + time.ElapsedMilliseconds.ToString() + "ms");
            time.Reset();
            time.Start();
            number1.sortDesc();
            time.Stop();
            if (number1.Length < 1 << 10) Console.WriteLine("fastCSharp.sortDesc " + type + "[" + number1.Length.toString() + "] " + time.ElapsedTicks.ToString() + "t");
            else Console.WriteLine("fastCSharp.sortDesc " + type + "[" + number1.Length.toString() + "] " + time.ElapsedMilliseconds.ToString() + "ms");
            for (int index = number1.Length; index != 0;)
            {
                --index;
                if (number1[index] != number2[index])
                {
                    Console.WriteLine(type + " sort Error");
                    break;
                }
            }
        }
        /// <summary>
        /// 排序测试
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        private static void sort(Int[] number1, Int[] number2, int startIndex, int length)
        {
            random(number1, number2);
            Stopwatch time = new Stopwatch();
            time.Start();
            Array.Sort(number2, startIndex, length);
            time.Stop();
            if (number1.Length < 1 << 10) Console.WriteLine("Array.Sort " + type + "[" + length.toString() + "] " + time.ElapsedTicks.ToString() + "t");
            else Console.WriteLine("Array.Sort " + type + "[" + length.toString() + "] " + time.ElapsedMilliseconds.ToString() + "ms");
            time.Reset();
            time.Start();
            number1.sort(startIndex, length);
            time.Stop();
            if (number1.Length < 1 << 10) Console.WriteLine("fastCSharp.sort " + type + "[" + length.toString() + "] " + time.ElapsedTicks.ToString() + "t");
            else Console.WriteLine("fastCSharp.sort " + type + "[" + length.toString() + "] " + time.ElapsedMilliseconds.ToString() + "ms");
            for (int index = startIndex, endIndex = startIndex + length; index != endIndex; ++index)
            {
                if (number1[index] != number2[index])
                {
                    Console.WriteLine(type + " sort Error");
                    break;
                }
            }
        }
        /// <summary>
        /// 排序测试
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        private static void sortDesc(Int[] number1, Int[] number2, int startIndex, int length)
        {
            random(number1, number2);
            Stopwatch time = new Stopwatch();
            time.Start();
            Array.Sort(number2, startIndex, length);
            Array.Reverse(number2, startIndex, length);
            time.Stop();
            if (number1.Length < 1 << 10) Console.WriteLine("Array.Sort+Array.Reverse " + type + "[" + length.toString() + "] " + time.ElapsedTicks.ToString() + "t");
            else Console.WriteLine("Array.Sort+Array.Reverse " + type + "[" + length.toString() + "] " + time.ElapsedMilliseconds.ToString() + "ms");
            time.Reset();
            time.Start();
            number1.sortDesc(startIndex, length);
            time.Stop();
            if (number1.Length < 1 << 10) Console.WriteLine("fastCSharp.sortDesc " + type + "[" + length.toString() + "] " + time.ElapsedTicks.ToString() + "t");
            else Console.WriteLine("fastCSharp.sortDesc " + type + "[" + length.toString() + "] " + time.ElapsedMilliseconds.ToString() + "ms");
            for (int index = startIndex, endIndex = startIndex + length; index != endIndex; ++index)
            {
                if (number1[index] != number2[index])
                {
                    Console.WriteLine(type + " sort Error");
                    break;
                }
            }
        }
        /// <summary>
        /// 排序测试
        /// </summary>
        /// <param name="count"></param>
        internal static void Test(int count)
        {
            Int[] number1 = new Int[count], number2 = new Int[count];
            sort(number1, number2);
            Console.WriteLine();
            sortDesc(number1, number2);
            Console.WriteLine();
            sort(number1, number2, (count >> 2) - 1, (count >> 1) - 5);
            Console.WriteLine();
            sortDesc(number1, number2, (count >> 2) - 1, (count >> 1) - 5);
            Console.WriteLine();
        }
    }
}
