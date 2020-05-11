using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer
{
    public sealed class DataStore
    {
        private static DataStore _instance;
        private static readonly object Padlock = new object();

        private DataStore()
        {
            SeedData();
        }

        public static State State { get; set; }

        public static DataStore Instance
        {
            get
            {
                lock (Padlock)
                {
                    _instance ??= new DataStore();

                    return _instance;
                }
            }
        }

        private static void SeedData()
        {
            State = new State(users: new List<User>
                {
                    new User
                    {
                        Email = "jzborowska@gmail.com", FirstName = "Julia", LastName = "Zborowska", Phone = "666666666"
                    },

                    new User
                    {
                        Email = "mikewazowski@gmail.com", FirstName = "Mike", LastName = "Wazowski", Phone = "555555555"
                    }
                },
                books: new List<Book>()
                {
                    new Book()
                    {
                        Author = "Marta Zaborowska",
                        Title = "Gwiazdozbiór",
                        Price = 27.31m,
                        Publisher = "Czarna Owca",
                        ReleaseYear = 2015
                    },
                    new Book()
                    {
                        Author = "Marta Zaborowska",
                        Title = "Jej wszystkie śmierci",
                        Price = 37.21m,
                        Publisher = "Czarna Owca",
                        ReleaseYear = 2015
                    },
                    new Book()
                    {
                        Author = "Marta Zaborowska",
                        Title = "Uśpienie",
                        Price = 21.37m,
                        Publisher = "Edipresse",
                        ReleaseYear = 2013
                    }
                }
                );
        }
    }
}