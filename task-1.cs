using System;
using System.IO;
using System.Linq;

//string? line;
string path = @"input.txt";
int dimension;
double[,] gMatrix;
double[] vector;

        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                dimension = int.Parse(sr.ReadLine());
                gMatrix = new double[dimension, dimension];
                vector = new double[dimension];

                // Считываем матрицу метрического тензора G
                for (int i = 0; i < dimension; i++)
                {
                    string[] line = sr.ReadLine().Split(',');
                    for (int j = 0; j < dimension; j++)
                    {
                        gMatrix[i, j] = double.Parse(line[j]);
                    }
                }

                // Считываем вектор
                string[] vectorLine = sr.ReadLine().Split(',');
                for (int i = 0; i < dimension; i++)
                {
                    vector[i] = double.Parse(vectorLine[i]);
                }
            }

            // Проверка симметричности матрицы
            if (!IsSymmetric(gMatrix, dimension))
            {
                throw new Exception("Матрица не является симметричной");
            }

            double length = CalculateVectorLength(vector, gMatrix, dimension);
            Console.WriteLine("Длина вектора: " + length);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }

 bool IsSymmetric(double[,] matrix, int dim)
    {
        for (int i = 0; i < dim; i++)
        {
            for (int j = i + 1; j < dim; j++)
            {
                if (matrix[i, j] != matrix[j, i])
                {
                    return false;
                }
            }
        }
        return true;
    }

 double CalculateVectorLength(double[] vector, double[,] gMatrix, int dim)
    {
        double[] temp = new double[dim];

        // Умножаем G на вектор
        for (int i = 0; i < dim; i++)
        {
            temp[i] = 0;
            for (int j = 0; j < dim; j++)
            {
                temp[i] += gMatrix[i, j] * vector[j];
            }
        }

        // Умножаем на вектор в обратном порядке (вектор * G * вектор^T)
        double length = 0;
        for (int i = 0; i < dim; i++)
        {
            length += temp[i] * vector[i];
        }

        return Math.Sqrt(length);
    }

