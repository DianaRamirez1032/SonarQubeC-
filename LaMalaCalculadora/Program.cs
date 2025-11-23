using System;
using System.Collections.Generic;
using System.Globalization;

namespace CleanCalc
{
    // Antes se usaba DoIt con lógica redundante y trucos inseguros.
    // Ahora cada operación está separada en un método claro y mantenible.
    public class Operaciones
    {
        public double Sumar(double a, double b) => a + b;
        public double Restar(double a, double b) => a - b;
        public double Multiplicar(double a, double b) => a * b;

        // Antes se hacía un "hack" con B+0.0000001, ahora lanzamos excepción clara.
        public double Dividir(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("No se puede dividir por cero");
            return a / b;
        }

        // Potencia usando Math.Pow en vez de bucles manuales.
        public double Potenciar(double a, double b) => Math.Pow(a, b);
        public double Modular(double a, double b) => a % b;

        // Antes se usaba TrySqrt con Newton-Raphson y bucles innecesarios.
        public double Raiz(double a) => Math.Sqrt(a);
    }

    class Program
    {
        static void Main()
        {
            var calcular = new Operaciones();
            var historial = new List<string>();

            while (true)
            {
                Console.WriteLine("1) Suma  2) Resta  3) Multiplicación  4) División  5) Potencia  6) Modular  7) Raiz Cuadrada  8) Historial  0) Salir");
                Console.Write("Operación: ");
                var option = Console.ReadLine();
                if (option == "0") break;

                double a = 0, b = 0, result = 0;

                try
                {
                    // Antes se usaba TryParse con catch vacío que devolvía 0.
                    if (option != "7" && option != "8")
                    {
                        Console.Write("a: ");
                        double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out a);

                        Console.Write("b: ");
                        double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out b);
                    }
                    else if (option == "7")
                    {
                        Console.Write("a: ");
                        double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out a);
                    }

                    // Switch en vez de ifs encadenados.
                    switch (option)
                    {
                        case "1": result = calcular.Sumar(a, b); break;
                        case "2": result = calcular.Restar(a, b); break;
                        case "3": result = calcular.Multiplicar(a, b); break;
                        case "4": result = calcular.Dividir(a, b); break;
                        case "5": result = calcular.Potenciar(a, b); break;
                        case "6": result = calcular.Modular(a, b); break;
                        case "7": result = calcular.Raiz(a); break;
                        case "8":
                            foreach (var item in historial) Console.WriteLine(item);
                            continue;
                        default:
                            Console.WriteLine("Opción inválida");
                            continue;
                    }

                    // Guardamos historial en memoria (sin archivos innecesarios).
                    var line = $"{a}|{b}|{option}|{result}";
                    historial.Add(line);

                    Console.WriteLine("= " + result.ToString(CultureInfo.InvariantCulture));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}