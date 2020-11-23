using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] A;
        int[,] B;


        public MainWindow()
        {
            InitializeComponent();

            //MatrixConsoleRead();
            MatrixFileRead("0.txt");
            Multiplication(A, B);
        }

        /// <summary>
        /// Чтение матрицы с экрана
        /// </summary>
        private void MatrixConsoleRead()
        {
            Console.WriteLine("Введите размерность первой матрицы: ");
            A = new int[Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine())];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write("A[{0},{1}] = ", i, j);
                    A[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            Console.WriteLine("Введите размерность второй матрицы: ");
            B = new int[Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine())];
            for (int i = 0; i < B.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    Console.Write("B[{0},{1}] = ", i, j);
                    B[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            Console.WriteLine("\nМатрица A:");
            Print(A);
            Console.WriteLine("\nМатрица B:");
            Print(B);
            
            Console.ReadLine();
        }

        /// <summary>
        /// Чтение матрицы из файла
        /// </summary>
        private void MatrixFileRead(string fileName)
        {
            StreamReader file = new StreamReader(fileName);
            string s = file.ReadToEnd();
            file.Close();            
            string strA = s.Split('*')[0].Trim();
            string strB = s.Split('*')[1].Trim();
            string[] strMasA = strA.Trim('\r').Split('\n');
            string[] strMasB = strB.Trim('\r').Split('\n');
            string[] rowA = strMasA[0].Split(' ');
            string[] rowB = strMasB[0].Split(' ');

            int[,] a = new int[strMasA.Length, rowA.Length];
            int t = 0;           
            for (int i = 0; i < strMasA.Length; i++)
            {
                rowA = strMasA[i].Split(' ');
                for (int j = 0; j < rowA.Length; j++)
                {
                    t = Convert.ToInt32(rowA[j]);
                    a[i, j] = t;                    
                }                
            }

            A = a;
            int[,] b = new int[strMasB.Length, rowB.Length];            
            for (int i = 0; i < strMasB.Length; i++)
            {
                rowB = strMasB[i].Split(' ');
                for (int j = 0; j < rowB.Length; j++)
                {
                    t = Convert.ToInt32(rowB[j]);
                    b[i, j] = t;
                }
            }
            B = b;
            
            Console.WriteLine("\nМатрица A:");
            Print(A);
            Console.WriteLine("\nМатрица B:");
            Print(B);

            Console.ReadLine();
        }
       

        /// <summary>
        /// Перемножение
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int[,] Multiplication(int[,] a, int[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            int[,] r = new int[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            Console.WriteLine("\nМатрица C = A * B:");
            int[,] C = Multiplication(A, B);
            Print(C);
            
            return r;
        }

        /// <summary>
        /// Вывод матрицы в консоль
        /// </summary>
        /// <param name="a"></param>
        static void Print(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write("{0} ", a[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
