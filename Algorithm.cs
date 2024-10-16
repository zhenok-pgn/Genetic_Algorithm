using System;
using System.Collections.Generic;

namespace GeneticAlgorithmWF
{
    internal class Algorithm
    {
        /* параметры (вероятности и др.) */
       /* private static readonly double uniformRate = 0.5;
        private static readonly double mutationRate = 0.015;
        private static readonly int tournamentSize = 3;
        private static readonly bool elitism = false;*/
        private static readonly Random random = new Random();
        /* публичные методы */


        public static void evolvePopulation(Population pop)
        {
            var newGeneration = new List<Individual>();

            for (int i = 0; i < pop.Size / 2; i++)
            {
                // create pairs of parents
                int parentNum1 = 0, parentNum2 = 0;
                while(parentNum1 == parentNum2)
                {
                    parentNum1 = random.Next(pop.Size);
                    parentNum2 = random.Next(pop.Size);
                }

                newGeneration.AddRange(Crossover(pop.Individuals[parentNum1], pop.Individuals[parentNum2]));
            }

            Mutate(newGeneration, 1);
            pop.Individuals.AddRange(newGeneration);
            Reduction(pop, newGeneration.Count);
        }

        // Кроссовер особей
        private static List<Individual> Crossover(Individual parent1, Individual parent2)
        {
            // get random crossover point
            var crossoverPoint = random.Next(parent1.GeneLength - 1);

            //add 2 child
            var generation = new List<Individual>() { new Individual(), new Individual()};

            for (int i = 0; i < parent1.GeneLength; i++)
            {
                // swap genes by crossover point
                if (i <= crossoverPoint)
                {
                    generation[0].setGene(i, parent1.getGene(i));
                    generation[1].setGene(i, parent2.getGene(i));
                }
                else
                {
                    generation[0].setGene(i, parent2.getGene(i));
                    generation[1].setGene(i, parent1.getGene(i));
                }
            }

            return generation;
        }

        // Мутация особей в потомстве в количестве mutationCount
        private static void Mutate(List<Individual> generation, int mutationCount)
        {
            int i = 0;
            var prevIndivNums = new List<int>();
            while(i < mutationCount)
            {
                // check if individual unique
                var individualNum = random.Next(generation.Count);
                if (prevIndivNums.Contains(individualNum))
                    continue;

                prevIndivNums.Add(individualNum);
                var genePosition = random.Next(generation[individualNum].GeneLength);
                generation[individualNum].setGene(
                    genePosition,
                    (byte)(1 - generation[individualNum].getGene(genePosition))
                );
                i++;
            }
        }

        private static void Reduction(Population population, int reductionCount)
        {
            population.Individuals.Sort(new IndividualComparer());
            if (population.isMax)
            {
                population.Individuals.RemoveRange(0, reductionCount);
            }
            else
            {
                population.Individuals.RemoveRange(population.Individuals.Count - reductionCount, reductionCount);
            }
           
        }

        // Эволюция популяции
        /*public static Population evolvePopulation(Population pop)
        {
            Population newPopulation = new Population(pop.size(), false);
            // Сохранение лучшей особи
            if (elitism)
            {
                newPopulation.saveIndividual(0, pop.getFittest());
            }
            // Кроссовер
            int elitismOffset;
            if (elitism)
            {
                elitismOffset = 1;
            }
            else
            {
                elitismOffset = 0;
            }
            // Цикл по популяции и создание новых особей путём
            // кроссовера
            for (int i = elitismOffset; i < pop.size(); i++)
            {
                Individual indiv1 = tournamentSelection(pop);
                Individual indiv2 = tournamentSelection(pop);
                Individual newIndiv = crossover(indiv1, indiv2);
                newPopulation.saveIndividual(i, newIndiv);
            }
            // Мутация популяции
            for (int i = elitismOffset; i < newPopulation.size(); i++)
            {
                mutate(newPopulation.getIndividual(i));
            }
            return newPopulation;
        }

        // Кроссовер особей
        private static Individual crossover(Individual indiv1, Individual indiv2)
        {
            Individual newSol = new Individual();
            // Цикл по геному
            for (int i = 0; i < indiv1.Size(); i++)
            {
                // Кроссовер
                if (random.NextDouble() <= uniformRate)
                {
                    newSol.setGene(i, indiv1.getGene(i));
                }
                else
                {
                    newSol.setGene(i, indiv2.getGene(i));
                }
            }
            return newSol;
        }

        // Мутация особи
        private static void mutate(Individual indiv)
        {
            // Цикл по геному
            for (int i = 0; i < indiv.Size(); i++)
            {
                if (random.NextDouble() <= mutationRate)
                {
                    // Создание случайного генома
                    byte gene = (byte)Math.Round(random.NextDouble());
                    indiv.setGene(i, gene);
                }
            }
        }
        // Выбор особи для кроссовера
        private static Individual tournamentSelection(Population pop)
        {
            // Создание новой популяции размером tournamentSize
            Population tournament = new Population(tournamentSize, false);
            // Для каждого места в популяции создаётся случайно особь
            for (int i = 0; i < tournamentSize; i++)
            {
                int randomId = (int)(random.NextDouble() * pop.size());
                tournament.saveIndividual(i, pop.getIndividual(randomId));
            }
            // Вычислить значение фитнес-функции
            Individual fittest = tournament.getFittest();
            return fittest;
        }*/


    }
}
