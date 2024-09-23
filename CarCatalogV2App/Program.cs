/*
Задание 3
Используйте класс Car из задания 2, на его основе создайте класс
CarCatalor, содержащий массив элементов типа Car.

Для класса CarCatalog реализуйте возможность итерации по элементам
массива Car с помощью оператора foreach различными способами: 
1.	Прямой проход с первого элемента до последнего.
2.	Обратный проход от последнего к первому.
3.	Проход по элементам массива с фильтром по году выпуска.
4.	Проход по элементам массива с фильтром по максимальной скорости.

Примечание: для выполнения задания необходимо реализовать различные
итераторы, используя конструкцию yield return. Для п.3 и 4, итератор
должен принимать год выпуска и скорость как параметр, чтобы возвращать
только те элементы коллекции, которые удовлетворяют условию.
*/

using System;
using System.Collections;
using System.Collections.Generic;

public class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }
    
    public Car(string name, int productionYear, int maxSpeed)
    {
        Name = name;
        ProductionYear = productionYear;
        MaxSpeed = maxSpeed;
    }

    public override string ToString()
    {
        return $"{Name} - {ProductionYear} - {MaxSpeed} км/ч";
    }
}

public enum CarSortCriteria
{
    Name,
    ProductionYear,
    MaxSpeed
}

public class CarComparer : IComparer<Car>
{
    private readonly CarSortCriteria _criteria;

    public CarComparer(CarSortCriteria criteria)
    {
        _criteria = criteria;
    }

    public int Compare(Car x, Car y)
    {
        if (x == null || y == null) 
            return 0;

        return _criteria switch
        {
            CarSortCriteria.Name => string.Compare(x.Name, y.Name, StringComparison.Ordinal),
            CarSortCriteria.ProductionYear => x.ProductionYear.CompareTo(y.ProductionYear),
            CarSortCriteria.MaxSpeed => x.MaxSpeed.CompareTo(y.MaxSpeed),
            _ => 0
        };
    }
}

public class CarCatalog : IEnumerable<Car>
{
    private Car[] cars;

    public CarCatalog(Car[] carArray)
    {
        cars = carArray;
    }

    // Прямой проход с первого элемента до последнего
    public IEnumerator<Car> GetEnumerator()
    {
        foreach (var car in cars)
        {
            yield return car;
        }
    }

    // Прямой проход для IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // Обратный проход от последнего к первому
    public IEnumerable<Car> GetReverseEnumerator()
    {
        for (int i = cars.Length - 1; i >= 0; i--)
        {
            yield return cars[i];
        }
    }

    // Проход с фильтром по году выпуска
    public IEnumerable<Car> GetCarsByProductionYear(int year)
    {
        foreach (var car in cars)
        {
            if (car.ProductionYear == year)
            {
                yield return car;
            }
        }
    }

    // Проход с фильтром по максимальной скорости
    public IEnumerable<Car> GetCarsByMaxSpeed(int speed)
    {
        foreach (var car in cars)
        {
            if (car.MaxSpeed >= speed)
            {
                yield return car;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car[] cars = new Car[]
        {
            new Car("Audi A4", 2018, 250),
            new Car("BMW 3 Series", 2020, 240),
            new Car("Mercedes C-Class", 2019, 260),
            new Car("Tesla Model 3", 2021, 260),
            new Car("Opel Astra", 2017, 210)
        };

        CarCatalog catalog = new CarCatalog(cars);

        Console.WriteLine("Прямой проход по элементам:");
        foreach (var car in catalog)
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nОбратный проход по элементам:");
        foreach (var car in catalog.GetReverseEnumerator())
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nФильтр по году выпуска (2020):");
        foreach (var car in catalog.GetCarsByProductionYear(2020))
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nФильтр по максимальной скорости (250 км/ч):");
        foreach (var car in catalog.GetCarsByMaxSpeed(250))
        {
            Console.WriteLine(car);
        }
    }
}
