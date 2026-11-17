using System;
using KT39_Custom_iterators;

public class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(" КОЛЬЦЕВОЙ БУФЕР ");
            Console.WriteLine("1. Запустить автоматический тест (Вариант 1: ёмкость 3, добавление 1, 2, 3, 4, 5)");
            Console.WriteLine("2. Запустить интерактивный режим");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите действие (1-3): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        RunDemoTest();
                        break;
                    case "2":
                        RunInteractiveMode();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Ошибка: введено неверное значение. Пожалуйста, выберите от 1 до 3.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nПроизошла ошибка выполнения: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }
    }

    private static void RunDemoTest()
    {
        Console.WriteLine("--- Автоматический тест по варианту ---");
        Console.WriteLine("Создаем буфер с ёмкостью 3.");
        var buffer = new CircularBuffer<int>(3);

        Console.WriteLine("Добавляем элементы по очереди: 1, 2, 3, 4, 5");
        buffer.Add(1);
        buffer.Add(2);
        buffer.Add(3);
        buffer.Add(4);
        buffer.Add(5);

        Console.WriteLine("\nРезультат обхода через foreach (от старого к новому):");
        foreach (var item in buffer)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        Console.WriteLine("\nРезультат обхода через Reverse() (от нового к старому):");
        foreach (var item in buffer.Reverse())
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    private static void RunInteractiveMode()
    {
        Console.Write("Введите ёмкость буфера (целое положительное число): ");
        if (!int.TryParse(Console.ReadLine(), out int capacity) || capacity <= 0)
        {
            throw new ArgumentException("Ёмкость должна быть положительным целым числом.");
        }

        var buffer = new CircularBuffer<int>(capacity);
        Console.WriteLine($"Буфер успешно создан. Ёмкость: {capacity}.");

        while (true)
        {
            Console.WriteLine("\n--- Меню интерактивного режима ---");
            Console.WriteLine("1. Добавить элемент в буфер");
            Console.WriteLine("2. Вывести элементы (foreach)");
            Console.WriteLine("3. Вывести элементы в обратном порядке (Reverse)");
            Console.WriteLine("4. Вернуться в главное меню");
            Console.Write("Выберите действие (1-4): ");

            string subChoice = Console.ReadLine();
            Console.WriteLine();

            switch (subChoice)
            {
                case "1":
                    Console.Write("Введите целое число для добавления: ");
                    if (int.TryParse(Console.ReadLine(), out int value))
                    {
                        buffer.Add(value);
                        Console.WriteLine($"Элемент {value} успешно добавлен.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: введено некорректное число.");
                    }
                    break;

                case "2":
                    Console.Write("Содержимое буфера (от старого к новому): ");
                    if (buffer.Count == 0)
                    {
                        Console.WriteLine("[Буфер пуст]");
                    }
                    else
                    {
                        foreach (var item in buffer)
                        {
                            Console.Write(item + " ");
                        }
                        Console.WriteLine();
                    }
                    break;

                case "3":
                    Console.Write("Содержимое буфера в обратном порядке (от нового к старому): ");
                    if (buffer.Count == 0)
                    {
                        Console.WriteLine("[Буфер пуст]");
                    }
                    else
                    {
                        foreach (var item in buffer.Reverse())
                        {
                            Console.Write(item + " ");
                        }
                        Console.WriteLine();
                    }
                    break;

                case "4":
                    return; // Возврат в главное меню

                default:
                    Console.WriteLine("Ошибка: неверный пункт меню.");
                    break;
            }
        }
    }
}