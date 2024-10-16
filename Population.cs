using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWF
{
    internal class Population
    {
        public readonly List<Individual> Individuals;
        public int Size { get {  return Individuals.Count; } }
        public readonly bool isMax;

        /* конструкторы */
        // создание популяции
        public Population(int populationSize, bool initialize, bool isMax)
        {
            Individuals = new List<Individual>();
            // Инициализация популяции
            if (initialize)
            {
                // цикл по созданию особей
                for (int i = 0; i < populationSize; i++)
                {
                    Individual newIndividual = new Individual();
                    newIndividual.generateIndividual();
                    Individuals.Add(newIndividual);
                }
            }

            this.isMax = isMax;
        }

        public Individual getFittest()
        {
            Individual fittest = Individuals[0];
            // цикл для вычисления значений фитнес-функции у особей популяции
            for (int i = 0; i < Size; i++)
            {
                if (fittest.getFitness() <= Individuals[i].getFitness())
                {
                    fittest = Individuals[i];
                }
            }
            return fittest;
        }
    }
}
