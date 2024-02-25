﻿using Sudoku.PSO;
using System;
using System.Collections.Generic;

namespace Sudoku.PSO
{
    public class PSO2OptBest4Sudoku : IMetaheuristic, ITunableMetaheuristic
    {
        protected double timePenalty = 100;
        protected double particlesCount = 6;
        protected double prevConf = 0.6;
        protected double neighConf = 0.6;

        public void Start(string fileInput, string fileOutput, int timeLimit)
        {
            SudokuInstance instance = new SudokuInstance(fileInput);

            // Setting the parameters of the PSO for this instance of the problem.
            int[] lowerBounds = new int[instance.NumberCities];
            int[] upperBounds = new int[instance.NumberCities];
            for (int i = 0; i < instance.NumberCities; i++)
            {
                lowerBounds[i] = 0;
                upperBounds[i] = instance.NumberCities - 1;
            }
            DiscretePSO pso = new DiscretePSO2OptBest4Sudoku(instance, (int)particlesCount, prevConf, neighConf, lowerBounds, upperBounds);

            // Solving the problem and writing the best solution found.
            pso.Run(timeLimit - (int)timePenalty);
            SudokuSolution solution = new SudokuSolution(instance, pso.BestPosition);
            solution.Write(fileOutput);
        }

        public string Name
        {
            get
            {
                return "PSO with 2-opt (best improvement) local search for TSP";
            }
        }

        public MetaheuristicType Type
        {
            get
            {
                return MetaheuristicType.PSO;
            }
        }

        public ProblemType Problem
        {
            get
            {
                return ProblemType.TSP;
            }
        }

        public string[] Team
        {
            get
            {
                return About.Team;
            }
        }


        public void UpdateParameters(double[] parameters)
        {
            timePenalty = parameters[0];
            particlesCount = parameters[1];
            prevConf = parameters[2];
            neighConf = parameters[3];
        }
    }
}

