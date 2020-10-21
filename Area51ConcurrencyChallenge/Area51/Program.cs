using System;
using System.Collections.Generic;

namespace Area51
{
    class Program
    {
        static List<IPerson> Personnels = new List<IPerson>();
        static void Main(string[] args)
        {
            for (int i = 1; i <= 20; i++)
            {
                Personnels.Add(Factory.CreatePerson(5, 4, i));
            }

            Console.WriteLine("Current personnel:");
            foreach (IPerson person in Personnels)
            {
                Console.WriteLine(person.Id + " on floor: " + person.SpawnFloor);
            }
        }
    }
}
