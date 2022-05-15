using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());

            var car1 = new Entities.Concrete.Car { BrandId = 1, ColorId = 123, DailyPrice = 1233, Description = "Güzel araba", ModelYear = 1998, Id = 123, Name = "Corsa" };

            carManager.Add(car1);


            //foreach (var car in carManager.GetAll())
            //{
            //    Console.WriteLine(car.Name);
            //}
            //Console.ReadLine();


            //CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.DailyPrice);
            }
        }
    }
}