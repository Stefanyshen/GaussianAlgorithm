using System.Collections.Generic;
using System.Linq;

namespace GaussAlgorithm
{
    public class Solver
    {
        public double[] Solve(double[][] matrix, double[] freeMembers)
        {
            var checkRow = new List<int>();
            bool isZeros = false;
            for (int i = 0; i < matrix[0].Length && i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length && j < matrix[i].Length; j++)
                {
                    if (matrix[j][i] != 0 && !checkRow.Contains(j))
                    {
                        checkRow.Add(j);
                        isZeros = false;
                        break;
                    }
                    isZeros = true;
                }

                if (isZeros) continue;

                for (int j = 0; j < matrix.Length; j++)
                {
                    if (j == checkRow.Last()) continue;
                    var multiplier = GetMultiplier(matrix[checkRow.Last()][i], matrix[j][i]);
                    for (int k = 0; k < matrix[i].Length && k < matrix.Length; k++)
                    {
                        matrix[j][k] += matrix[checkRow.Last()][k] * multiplier;
                    }
                    freeMembers[j] += freeMembers[checkRow.Last()] * multiplier;
                }
            }
            if (!IsThereSolution(matrix, freeMembers)) throw new NoSolutionException("");
            var result = new double[matrix[0].Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                var index = GetNotZeroIndex(matrix[i]);
                if (index == -1) continue;
                result[index] = freeMembers[i] / matrix[i][index];
            }
            return result;
        }

        private int GetNotZeroIndex(double[] line)
        {
            for (int i = 0; i < line.Length; i++)
                if (line[i] >= 0.00000001 || line[i] <= -0.00000001) return i;
            return -1;
        }

        private double GetMultiplier(double startNum, double needsNum) => needsNum / startNum * -1;

        private bool IsThereSolution(double[][] matrix, double[] freeMembers)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i].Sum() == 0 && freeMembers[i] != 0) return false;
            }
            return true;
        }
    }
}
