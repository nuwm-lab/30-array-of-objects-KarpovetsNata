using System;
using System.Globalization;

class Pryama
{
    // --- Поля ---
    private double a, b, c; // коефіцієнти рівняння ax + by + c = 0

    // --- Властивості ---
    public double A { get => a; set => a = value; }
    public double B { get => b; set => b = value; }
    public double C { get => c; set => c = value; }

    // --- Конструктор ---
    public Pryama(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    // --- Метод для перевірки, чи належить точка прямій ---
    public bool NalPoint(double x, double y)
    {
        return Math.Abs(a * x + b * y + c) < 1e-6;
    }

    // --- Перевизначення ToString ---
    public override string ToString()
    {
        string sa = a.ToString(CultureInfo.InvariantCulture);
        string sb = b >= 0 ? $"+ {b.ToString(CultureInfo.InvariantCulture)}" : $"- {Math.Abs(b).ToString(CultureInfo.InvariantCulture)}";
        string sc = c >= 0 ? $"+ {c.ToString(CultureInfo.InvariantCulture)}" : $"- {Math.Abs(c).ToString(CultureInfo.InvariantCulture)}";
        return $"{sa}x {sb}y {sc} = 0";
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Лабораторна робота №3 ===");
        Console.WriteLine("Тема: Масиви об’єктів");
        Console.WriteLine("Завдання: визначити прямі, яким належить хоча б одна із двох точок\n");

        int n;
        while (true)
        {
            Console.Write("Введіть кількість прямих: ");
            string? line = Console.ReadLine();
            if (int.TryParse(line, out n) && n > 0) break;
            Console.WriteLine("Некоректне значення. Введіть додатнє ціле число.");
        }

        Pryama[] pr = new Pryama[n];

        Console.WriteLine("\nВведіть коефіцієнти a, b, c для кожної прямої:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nПряма #{i + 1}:");

            double a, b, c;
            // helper local function
            double ReadDouble(string prompt)
            {
                while (true)
                {
                    Console.Write(prompt);
                    string? s = Console.ReadLine();
                    if (double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double v)) return v;
                    Console.WriteLine("Некоректне число. Використовуйте крапку як десятковий роздільник (наприклад: 1.23). Спробуйте ще раз.");
                }
            }

            a = ReadDouble("a = ");
            b = ReadDouble("b = ");
            c = ReadDouble("c = ");

            if (Math.Abs(a) < 1e-12 && Math.Abs(b) < 1e-12)
            {
                Console.WriteLine("Увага: коефіцієнти a і b одночасно нульові — це не пряма. Збережемо як недійсну пряму.");
            }

            pr[i] = new Pryama(a, b, c);
        }

        Console.WriteLine("\nВведіть координати двох точок:");
        double ReadCoord(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? s = Console.ReadLine();
                if (double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double v)) return v;
                Console.WriteLine("Некоректне число. Використовуйте крапку як десятковий роздільник. Спробуйте ще раз.");
            }
        }

        double x1 = ReadCoord("x1 = ");
        double y1 = ReadCoord("y1 = ");
        double x2 = ReadCoord("x2 = ");
        double y2 = ReadCoord("y2 = ");

        Console.WriteLine("\nРезультат перевірки:");
        bool found = false;

        for (int i = 0; i < n; i++)
        {
            bool p1 = pr[i].NalPoint(x1, y1);
            bool p2 = pr[i].NalPoint(x2, y2);

            if (p1 || p2)
            {
                Console.WriteLine($"Пряма #{i + 1}: {pr[i]} — належить хоча б одній точці");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Жодна з точок не належить жодній прямій.");
        }

        Console.WriteLine("\nПеревірку завершено.");
    }
}
