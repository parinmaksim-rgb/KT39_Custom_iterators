using System;
using System.Collections;
using System.Collections.Generic;

public class CircularBuffer<T> : IEnumerable<T>
{
    private readonly T[] _buffer;
    private int _head;
    private int _tail;
    private int _size;

    public int Capacity => _buffer.Length;
    public int Count => _size;

    public CircularBuffer(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));

        _buffer = new T[capacity];
        _head = 0;
        _tail = 0;
        _size = 0;
    }

    public void Add(T item)
    {
        if (_buffer.Length == 0) return;

        _buffer[_tail] = item;
        _tail = (_tail + 1) % _buffer.Length;

        if (_size < _buffer.Length)
        {
            _size++;
        }
        else
        {
            _head = (_head + 1) % _buffer.Length;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _size; i++)
        {
            int index = (_head + i) % _buffer.Length;
            yield return _buffer[index];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<T> Reverse()
    {
        for (int i = _size - 1; i >= 0; i--)
        {
            int index = (_head + i) % _buffer.Length;
            yield return _buffer[index];
        }
    }
}

class Program
{
    static void Main()
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
                if (choice == "1")
                {
                    RunDemoTest();
                }
                else if (choice == "2")
                {
                    RunInteractiveMode();
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка: введено неверное значение. Пожалуйста, выберите от 1 до 3.");
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

    static void RunDemoTest()
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

    static void RunInteractiveMode()
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

            if (subChoice == "1")
            {
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
            }
            else if (subChoice == "2")
            {
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
            }
            else if (subChoice == "3")
            {
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
            }
            else if (subChoice == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Ошибка: неверный пункт меню.");
            }
        }
    }
}