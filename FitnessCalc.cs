using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWF
{
    public class FitnessCalc
    {
        static byte[] solution = new byte[64];

        private static Func<int, double> func;

        public static void setSolution(Func<int, double> func)
        {
            FitnessCalc.func = func;
        }

        /* Публичные методы */
        // Представление возможного решения в виде байтового массива
        /*public static void setSolution(byte[] newSolution)
        {
            solution = newSolution;
        }*/
        // Представление возможного решения в виде строки,
        // состоящей из 1 и 0

        /*public static void setSolution(string newSolution)
        {
            solution = new byte[newSolution.Length];
            // Цикл по каждому символу строки с сохранением в
            // байтовый массив (парсинг)
            for (int i = 0; i < newSolution.Length; i++)
            {
                string character = newSolution.Substring(i, 1);
                if (character.Contains('0') || character.Contains('1'))
                {
                    solution[i] = byte.Parse(character);
                }
                else
                {
                    solution[i] = 0;
                }
            }
        }*/
        // Сравнение значений фитнес-функции особей с возможным решением
        /*public static int getFitness(Individual individual)
        {
            int fitness = 0;
            // Цикл по популяции
            for (int i = 0; i < individual.size() && i < solution.Length; i++)
            {
                if (individual.getGene(i) == solution[i])
                {
                    fitness++;
                }
            }
            return fitness;
        }*/

        public static double getFitness(Individual individual)
        {
            if(func == null)
            {
                throw new ArgumentNullException("func is null");
            }
            else
            {
                return func(individual.Phenotype);
            }
            
        }

        // Возвращает максимум значений фитнес-функции
        /*public static int getMaxFitness()
        {
            int maxFitness = solution.Length;
            return maxFitness;
        }*/

        public static ushort grayencode(ushort g)
        {
            return (ushort)(g ^ (g >> 1));
        }

        public static ushort graydecode(ushort gray)
        {
            ushort bin;
            for (bin = 0; gray != 0; gray >>= 1)
            {
                bin ^= gray;
            }
            return bin;
        }
    }
}
