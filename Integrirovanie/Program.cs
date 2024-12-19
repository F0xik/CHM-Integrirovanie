using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        CultureInfo culture = CultureInfo.InvariantCulture;

        Console.WriteLine("Функция: f(x) = sin(x) + ln(1 + x) + sqrt(x)");

        // Ввод начальных данных
        Console.Write("Введите a: ");
        double a = double.Parse(Console.ReadLine().Replace(",", "."), culture);

        Console.Write("Введите b: ");
        double b = double.Parse(Console.ReadLine().Replace(",", "."), culture);

        Console.Write("Введите точность e: ");
        string epsilonInput = Console.ReadLine().Replace(",", ".");
        double epsilon = double.Parse(epsilonInput, culture);

        int decimalPlaces = epsilonInput.Contains(".") ? epsilonInput.Split('.')[1].Length : 0;

        Console.WriteLine("Выберите метод:");
        Console.WriteLine("1. Метод трапеций");
        Console.WriteLine("2. Метод прямоугольников (левые)");
        Console.WriteLine("3. Метод прямоугольников (правые)");
        Console.WriteLine("4. Метод прямоугольников (средние)");

        Console.Write("Введите номер метода (1-4): ");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                // Метод трапеций
                double resultTrapezoidal = TrapezoidalRuleMethod(a, b, epsilon, decimalPlaces);
                Console.WriteLine(string.Format("Приближенное значение интеграла по методу трапеций: {0:F" + decimalPlaces + "}", resultTrapezoidal));
                break;

            case 2:
                // Левые прямоугольники
                double resultLeftRectangles = RectanglesMethod(a, b, epsilon, decimalPlaces, "left");
                Console.WriteLine(string.Format("Приближенное значение интеграла (левые прямоугольники): {0:F" + decimalPlaces + "}", resultLeftRectangles));
                break;

            case 3:
                // Правые прямоугольники
                double resultRightRectangles = RectanglesMethod(a, b, epsilon, decimalPlaces, "right");
                Console.WriteLine(string.Format("Приближенное значение интеграла (правые прямоугольники): {0:F" + decimalPlaces + "}", resultRightRectangles));
                break;

            case 4:
                // Средние прямоугольники
                double resultMiddleRectangles = RectanglesMethod(a, b, epsilon, decimalPlaces, "middle");
                Console.WriteLine(string.Format("Приближенное значение интеграла (средние прямоугольники): {0:F" + decimalPlaces + "}", resultMiddleRectangles));
                break;

            default:
                Console.WriteLine("Неверный выбор метода.");
                break;
        }





        // Метод для вычисления интеграла по формуле трапеций
        static double TrapezoidalRuleMethod(double a, double b, double epsilon, int decimalPlaces)
        {
            double I0 = 0;
            double I1 = double.MaxValue;
            int n = 5;

            while (Math.Abs(I1 - I0) > epsilon)
            {
                n *= 2;
                I0 = I1;
                double h = (b - a) / n;
                I1 = 0;

                for (int i = 0; i < n; i++)
                {
                    double x = a + i * h;
                    double fx = f(x);
                    I1 += fx;
                }

                I1 *= h;
            }

            return I1;
        }

        // Метод для вычисления интеграла методом прямоугольников
        static double RectanglesMethod(double a, double b, double epsilon, int decimalPlaces, string method)
        {
            double I0 = 0;
            double I1 = double.MaxValue;
            int n = 5;

            while (Math.Abs(I1 - I0) > epsilon)
            {
                n *= 2;
                I0 = I1;
                double h = (b - a) / n;
                I1 = 0;

                for (int i = 0; i < n; i++)
                {
                    double x;
                    if (method == "left")
                        x = a + i * h;  // Левые прямоугольники
                    else if (method == "right")
                        x = a + (i + 1) * h;  // Правые прямоугольники
                    else
                        x = a + (i + 0.5) * h;  // Средние прямоугольники

                    double fx = f(x);
                    I1 += fx;
                }

                I1 *= h;
            }

            return I1;
        }

        static double f(double x)
        {
            return Math.Sin(x) + Math.Log(1 + x) + Math.Sqrt(x);
        }
    } }
