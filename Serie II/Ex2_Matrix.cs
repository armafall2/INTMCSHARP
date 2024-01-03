using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public static class Matrix
    {
        public static int[][] BuildingMatrix(int[] leftVector, int[] rightVector)
        {
        int[][] Matrice = new int[3][];
        for (int i = 0; i < 3; i++)
        {
            Matrice[i] = new int[3];
        }

        for (int i = 0; i < 3; i++)
        {
            Matrice[i][0] = leftVector[i];
            Matrice[i][1] = rightVector[i];
        }
        return Matrice;
        }


       public static int[][] Addition(int[][] leftMatrix, int[][] rightMatrix)
       {
       int[][] Matrice = new int[3][];
       for (int i = 0; i < 3; i++)
       {
           Matrice[i] = new int[3];
           for (int j = 0; j < 2; j++) 
           {
               Matrice[i][j] = leftMatrix[i][j] + rightMatrix[i][j];
           }
       }

       return Matrice;
       }

        public static int[][] Substraction(int[][] leftMatrix, int[][] rightMatrix)
        {
            int[][] Matrice = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                Matrice[i] = new int[3];
                for (int j = 0; j < 2; j++)
                {
                    Matrice[i][j] = leftMatrix[i][j] - rightMatrix[i][j];
                }
            }
            return Matrice;
        }

        public static int[][] Multiplication(int[][] leftMatrix, int[][] rightMatrix)
        {
            int leftRows = leftMatrix.Length;
            int leftCols = leftMatrix[0].Length;
            int rightCols = rightMatrix[0].Length;

           
               int[][] resultMatrix = new int[leftRows][];

               for (int i = 0; i < leftRows; i++)
                {
                    resultMatrix[i] = new int[rightCols];
                    for (int j = 0; j < rightCols; j++)
                        for (int k = 0; k < leftCols; k++)
                            resultMatrix[i][j] += leftMatrix[i][k] * rightMatrix[k][j];
                }

    return resultMatrix;
}

        public static void DisplayMatrix(int[][] matrix)
        {
            string s = string.Empty;
            for (int i = 0; i < matrix.Length; ++i)
            {
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    s += matrix[i][j].ToString().PadLeft(5) + " ";
                }
                s += Environment.NewLine;
            }
            Console.WriteLine(s);
      }
        
    }
}
