/*
Задание 2
Создайте класс Car с тремя авто-свойствами: Name, ProductionYear
и MaxSpeed, соответствующими названию, году выпуска и максимальной
скорости соответственно.

Создайте класс CarComparer : IComparer<Car> и реализуйте метод
Compare таким образом, чтобы можно было сортировать массив элементов
Car по названию, году выпуска или максимальной скорости по выбору.

Создайте массив элементов Car и продемонстрируйте сортировку
различными способами.
*/

using System;
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

        Console.WriteLine("Сортировка по названию:");
        Array.Sort(cars, new CarComparer(CarSortCriteria.Name));
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nСортировка по году выпуска:");
        Array.Sort(cars, new CarComparer(CarSortCriteria.ProductionYear));
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nСортировка по максимальной скорости:");
        Array.Sort(cars, new CarComparer(CarSortCriteria.MaxSpeed));
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }
    }
}
