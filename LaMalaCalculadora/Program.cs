using System;
using System.Collections.Generic;
using System.Globalization;

// EJERCICIO DE CALCULADORA C#
// DIANA RAMIREZ - SAMUEL DIAZ
namespace BuenaCalculadora
{
    // Antes se usaba DoIt con lógica redundante y trucos inseguros.
    // Ahora cada operación está separada en un metodo claro y mantenible.
    static class Operaciones
    {
        public static double Sumar(double a, double b) => a + b;
        public static double Restar(double a, double b) => a - b;
        public static double Multiplicar(double a, double b) => a * b;

        // Antes se hacía un "hack" con B+0.0000001, ahora lanzamos excepción clara.
        public static double Dividir(double a, double b)
        {
            const double epsilon = 1e-10; // margen de tolerancia
            if (Math.Abs(b) < epsilon)
                throw new DivideByZeroException("No se puede dividir por un valor cercano a cero");

            return a / b;
        }

        // Potencia usando Math.Pow en vez de bucles manuales.
        public static double Potenciar(double a, double b) => Math.Pow(a, b);
        public static double Modular(double a, double b) => a % b;

        // Antes se usaba TrySqrt con Newton-Raphson y bucles innecesarios.
        public static double Raiz(double a) => Math.Sqrt(a);
    }

    class Program
    {
        static void Main(string[] args)
        {
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
                        case "1": result = Operaciones.Sumar(a, b); break;
                        case "2": result = Operaciones.Restar(a, b); break;
                        case "3": result = Operaciones.Multiplicar(a, b); break;
                        case "4": result = Operaciones.Dividir(a, b); break;
                        case "5": result = Operaciones.Potenciar(a, b); break;
                        case "6": result = Operaciones.Modular(a, b); break;
                        case "7": result = Operaciones.Raiz(a); break;
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