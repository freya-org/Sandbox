﻿using FluentNHibernateExamples.CompDatabases.SQLCeDatabase;
using FluentNHibernateExamples.CompDatabases.SQLiteDatabase;
using Freya.Util;
using System;


namespace FluentNHibernateExamples.CompDatabases
{
    class Program
    {
        private const int iterations = 10000;

        static void Main()
        {
            // Init databases
            SQLite.InitializeDatabase("SQLite.db", "SQLite.db");
            SQLCe.InitializeDatabase("SQLCe.sdf", "Data Source=SQLCe.sdf");

            // Testing - SQLite
            TimeSpan timeSQLite = StopwatchUtil.Time(() =>
            {
                using (var session = SQLite.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var barginBasin = new SQLiteDatabase.Entities.Store { Name = "Bargin Basin" };

                        var potatoes = new SQLiteDatabase.Entities.Product { Name = "Potatoes", Price = 3.60 };
                        var fish = new SQLiteDatabase.Entities.Product { Name = "Fish", Price = 4.49 };

                        for (int i = 0; i < iterations; i++)
                            barginBasin.AddProducts(potatoes, fish);

                        session.SaveOrUpdate(barginBasin);
                        transaction.Commit();
                    }
                }
            });

            // Testing - SQLCe
            TimeSpan timeSQLCe = StopwatchUtil.Time(() =>
            {
                using (var session = SQLCe.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var barginBasin = new SQLCeDatabase.Entities.Store { Name = "Bargin Basin" };

                        var potatoes = new SQLCeDatabase.Entities.Product { Name = "Potatoes", Price = 3.60 };
                        var fish = new SQLCeDatabase.Entities.Product { Name = "Fish", Price = 4.49 };

                        for (int i = 0; i < iterations; i++)
                            barginBasin.AddProducts(potatoes, fish);

                        session.SaveOrUpdate(barginBasin);
                        transaction.Commit();
                    }
                }
            });

            Console.WriteLine("SQLite: {0}", timeSQLite);
            Console.WriteLine("SQLCe: {0}", timeSQLCe);
           
            Console.ReadKey();
        }        
    }
}
