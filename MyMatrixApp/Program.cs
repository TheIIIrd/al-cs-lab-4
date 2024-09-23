/*
Задание 1
Создайте класс MyMatrix, представляющий матрицу m на n.

Создайте конструктор, принимающий число строк и столбцов, заполняющий
матрицу случайными числами в диапазоне, который пользователь вводит
при запуске программы.

Определите операторы сложения, вычитания и умножения матриц, а также
умножения и деления матрицы на число.

Создайте пользовательский индексатор матрицы для доступа к элементам
матрицы по номеру строки и столбца.
*/

using System;

public class MyMatrix
{
    private int[,] _matrix;
    private int _rows;
    private int _cols;

    public MyMatrix(int rows, int cols, int minValue, int maxValue)
    {
        _rows = rows;
        _cols = cols;
        _matrix = new int[_rows, _cols];
        Random rand = new Random();

        // Заполнение матрицы случайными числами
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                _matrix[i, j] = rand.Next(minValue, maxValue + 1);
            }
        }
    }

    public int this[int row, int col]
    {
        get => _matrix[row, col];
        set => _matrix[row, col] = value;
    }

    public static MyMatrix operator +(MyMatrix a, MyMatrix b)
    {
        if (a._rows != b._rows || a._cols != b._cols)
            throw new InvalidOperationException("Matrices must have the same dimensions for addition.");

        MyMatrix result = new MyMatrix(a._rows, a._cols, 0, 0);
        for (int i = 0; i < a._rows; i++)
        {
            for (int j = 0; j < a._cols; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }
        return result;
    }

    public static MyMatrix operator -(MyMatrix a, MyMatrix b)
    {
        if (a._rows != b._rows || a._cols != b._cols)
            throw new InvalidOperationException("Matrices must have the same dimensions for subtraction.");

        MyMatrix result = new MyMatrix(a._rows, a._cols, 0, 0);
        for (int i = 0; i < a._rows; i++)
        {
            for (int j = 0; j < a._cols; j++)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }
        return result;
    }

    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        if (a._cols != b._rows)
            throw new InvalidOperationException("Matrix A's columns must match Matrix B's rows for multiplication.");

        MyMatrix result = new MyMatrix(a._rows, b._cols, 0, 0);
        for (int i = 0; i < a._rows; i++)
        {
            for (int j = 0; j < b._cols; j++)
            {
                for (int k = 0; k < a._cols; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return result;
    }

    public static MyMatrix operator *(MyMatrix matrix, int scalar)
    {
        MyMatrix result = new MyMatrix(matrix._rows, matrix._cols, 0, 0);
        for (int i = 0; i < matrix._rows; i++)
        {
            for (int j = 0; j < matrix._cols; j++)
            {
                result[i, j] = matrix[i, j] * scalar;
            }
        }
        return result;
    }

    public static MyMatrix operator /(MyMatrix matrix, int scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Cannot divide by zero.");

        MyMatrix result = new MyMatrix(matrix._rows, matrix._cols, 0, 0);
        for (int i = 0; i < matrix._rows; i++)
        {
            for (int j = 0; j < matrix._cols; j++)
            {
                result[i, j] = matrix[i, j] / scalar;
            }
        }
        return result;
    }

    public void PrintMatrix()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                Console.Write(_matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите количество строк матрицы:");
        int rows = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество столбцов матрицы:");
        int cols = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите минимальное значение случайного числа:");
        int minValue = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите максимальное значение случайного числа:");
        int maxValue = int.Parse(Console.ReadLine());

        MyMatrix matrix = new MyMatrix(rows, cols, minValue, maxValue);
        Console.WriteLine("Сгенерированная матрица:");
        matrix.PrintMatrix();
    }
}
