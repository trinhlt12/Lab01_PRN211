using AutomobileLibrary.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomobileLibrary.DataAccess
{
    public class CarDBContext
    {
        private static List<Car> CarList = new List<Car>()
        {
            new Car(){ CarID=1, CarName="CRV", Manufacturer="Honda", Price=30000, ReleaseYear=2021 },
            new Car(){ CarID=2, CarName="Ford Focus", Manufacturer="Ford", Price=15000, ReleaseYear=2020 }
        };

        private static CarDBContext instance = null;
        private static readonly object instanceLock = new object();

        private CarDBContext() { }

        public static CarDBContext Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CarDBContext();
                    }
                    return instance;
                }
            }
        }

        public List<Car> GetCarList => CarList;

        public Car GetCarByID(int carID)
        {
            return CarList.SingleOrDefault(car => car.CarID == carID);
        }

        public void AddNew(Car car)
        {
            if (GetCarByID(car.CarID) == null)
            {
                CarList.Add(car);
            }
            else
            {
                throw new InvalidOperationException("Car already exists.");
            }
        }

        public void Update(Car car)
        {
            Car existingCar = GetCarByID(car.CarID);
            if (existingCar != null)
            {
                int index = CarList.IndexOf(existingCar);
                CarList[index] = car;
            }
            else
            {
                throw new InvalidOperationException("Car does not exist.");
            }
        }

        public void Remove(int carID)
        {
            Car carToRemove = GetCarByID(carID);
            if (carToRemove != null)
            {
                CarList.Remove(carToRemove);
            }
            else
            {
                throw new InvalidOperationException("Car does not exist.");
            }
        }
    }
}
