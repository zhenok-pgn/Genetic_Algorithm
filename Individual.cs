using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWF
{
    public class Individual
    {
        static int defaultGeneLength = 6;
        static int delta = -10;
        private byte[] genes = new byte[defaultGeneLength]; // in gray code

        private static readonly Random random = new Random();
        private ushort Value { get {  return convertToInt(); } }
        public int Phenotype { get { return Value + delta; } }

        public int GeneLength { get { return genes.Length; } }

        // Создание особи случайным образом (random)
        public void generateIndividual()
        {
            ushort randNum = (ushort)random.Next(65);
            var grayEncodedNum = Convert.ToString(FitnessCalc.grayencode(randNum), 2);
            for (int i = 0; i < GeneLength; i++)
            {
                //byte gene = (byte)Math.Round(random.NextDouble());
                if(i < grayEncodedNum.Length)
                {
                    genes[i] = (byte)(grayEncodedNum[grayEncodedNum.Length - i - 1] - '0');
                }
                else
                {
                    genes[i] = 0;
                }
            }
        }
        /* геттеры и сеттеры */
        // для создания генома особи с указанной длиной
        public static void setDefaultGeneLength(int length)
        {
            defaultGeneLength = length;
        }

        public byte getGene(int index)
        {
            return genes[index];
        }
        public void setGene(int index, byte value)
        {
            genes[index] = value;
        }
        /* Публичные методы */

        public double getFitness()
        {
            return FitnessCalc.getFitness(this);
        }
        public override string ToString()
        {
            string geneString = "";
            for (int i = 0; i < GeneLength; i++)
            {
                geneString += getGene(i);
            }
            return geneString;
        }

        private ushort convertToInt()
        {
            ushort result = 0;

            for (int i = 0; i < GeneLength; i++)
            {
                result += (ushort)(genes[i] * Math.Pow(2, i));
            }

            return FitnessCalc.graydecode(result);
        }
    }

    public class IndividualComparer : IComparer<Individual>
    {
        public int Compare(Individual x, Individual y)
        {
            return x.getFitness().CompareTo(y.getFitness());
        }
    }
}
