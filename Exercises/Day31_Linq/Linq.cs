using Practice;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Practice
{
    class Student
    {
        public int Id;
        public string Name;
        public string Class;
        public int Marks;
    }

    class Employee
    {
        public int Id;
        public string Name;
        public string Dept;
        public double Salary;
        public DateTime JoinDate;
    }

    class Product
    {
        public int Id;
        public string Name;
        public string Category;
        public double Price;
    }
    class Sale
    {
        public int ProductId;
        public int Qty;

    }
    class Book
    {
        public string Title;
        public string Author;
        public string Genre;
        public int Year;
        public double Price;
    }
    class Customer
    {
        public int Id;
        public string Name;
        public string City;
    };
    class Order
    {
        public int OrderId;
        public int CustomerId;
        public double Amount;
    };



    class Movie
    {
        public string Title;
        public string Genre;
        public double Rating;
        public int Year;
    }
    class Transaction
    {
        public int Acc;
        public double Amount;
        public string Type;
    }

    class CartItem
    {
        public string Name;
        public string Category;
        public double Price;
        public int Qty;
    }
    class User
    {
        public int Id;
        public string Name;
        public string Country;
    }
    class Post
    {
        public int UserId;
        public int Likes;
    }

    internal class Linq
    {
        static void Main(string[] args)
        {
            var students = new List<Student>
        {
            new Student{Id=1, Name="Ekta", Class="10A", Marks=95},
            new Student{Id=2, Name="Kritika", Class="10A", Marks=62},
            new Student{Id=3, Name="Kushagra", Class="10B", Marks=90},
            new Student{Id=4, Name="Tushar", Class="10B", Marks=60},
            new Student{Id=5, Name="Neha", Class="10A", Marks=85}
        };
            Console.WriteLine("Student problem 1");
            Console.WriteLine("Top 3 Students");
            var top3Students = students.OrderByDescending(x => x.Marks).Take(3);
            foreach (var student in top3Students)
            {
                Console.WriteLine($"Name: {student.Name} |" +
                    $" ID : {student.Marks} |" +
                    $" Class : {student.Class} |" +
                    $" Marks : {student.Marks}");
            }
            Console.WriteLine("---------------");

            Console.WriteLine("class and Average Marks");
            var avgMarks = students.GroupBy(x => x.Class)
                                   .Select(y => new { Class = y.Key, Avg = y.Average(z => z.Marks) }).ToList();
            foreach (var student in avgMarks)
            {
                Console.WriteLine($"class : {student.Class} : AverageMarks : {student.Avg}");
            }
            Console.WriteLine("---------------");
            Console.WriteLine("students marks below class average");

            var belowAverage = students.GroupBy(x => x.Class)
                                        .SelectMany(y => y
                                        .Where(x => x.Marks < y.Average(z => z.Marks))).ToList();

            foreach (var i in belowAverage)
            {

                Console.WriteLine(i.Name + " || " + i.Marks);
            }


            var orderStudents = students.OrderBy(x => x.Class)
                                        .ThenByDescending(x => x.Marks);
            Console.WriteLine("---------------");
            foreach (var student in orderStudents)
            {
                Console.WriteLine(student.Name + " || " + student.Class);
            }

            var employees = new List<Employee>
        {
            new Employee{Id=1, Name="Rishi", Dept="IT", Salary=90000, JoinDate=new DateTime(2019,1,10)},
            new Employee{Id=2, Name="Aman", Dept="HR", Salary=60000, JoinDate=new DateTime(2021,3,5)},
            new Employee{Id=3, Name="Aniketh", Dept="IT", Salary=120000, JoinDate=new DateTime(2018,7,15)},
            new Employee{Id=4, Name="Riya", Dept="Finance", Salary=80000, JoinDate=new DateTime(2022,9,1)}
        };

            var highestSalary = employees.GroupBy(x => x.Dept)
                                        .Select(y => new { Dept = y.Key, highestSalary = y.Max(s => s.Salary), lowestSaalry = y.Min(z => z.Salary) }).ToList();

            Console.WriteLine("---------------");
            Console.WriteLine("---------------");
            Console.WriteLine("Highest Salary in Each Dept");
            foreach (var i in highestSalary)
            {
                Console.WriteLine(i.Dept + " - " + i.highestSalary);

            }
            Console.WriteLine("---------------");
            Console.WriteLine("Lowest Salary in Each Dept");
            foreach (var i in highestSalary)
            {
                Console.WriteLine(i.Dept + " - " + i.lowestSaalry);

            }
            Console.WriteLine("---------------");

            var deptCount = employees.GroupBy(y => y.Dept).Select(x => new { Dept = x.Key, Count = x.Count() }).ToList();
            Console.WriteLine("Employee in each Dept");
            foreach (var i in deptCount)
            {
                Console.WriteLine(i.Dept + " - " + i.Count);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Employees Joined After 2020");
            var employeeJoined = employees.Where(x => x.JoinDate.Year > 2020);
            foreach (var i in employeeJoined)
            {
                Console.WriteLine(i.Name);
            }

            Console.WriteLine("---------------");
            Console.WriteLine("Name and AnnualSalary");
            var anual = employees.Select(x => new { Name = x.Name, AnualSalary = x.Salary * 12 }).ToList();
            foreach (var i in anual)
            {
                Console.WriteLine(i.Name + " - " + i.AnualSalary);
            }


            var products = new List<Product>
        {
            new Product{Id=1, Name="Laptop", Category="Electronics", Price=50000},
            new Product{Id=2, Name="Phone", Category="Electronics", Price=20000},
            new Product{Id=3, Name="Table", Category="Furniture", Price=5000}
        };
            var sales = new List<Sale>
        {
            new Sale{ProductId=1, Qty=10},
            new Sale{ProductId=2, Qty=20}
        };

            var merge = products.Join(sales, p => p.Id, s => s.ProductId, (p, s) =>
                                        new { ProductName = p.Name, Quantity = s.Qty }).ToList();
            Console.WriteLine("---------------");
            Console.WriteLine("---------------");

            Console.WriteLine("Joining product and Sales");

            foreach (var item in merge)
            {
                Console.WriteLine(item.ProductName + " - " + item.Quantity);
            }

            var totalRevenue = products.Join(sales, s => s.Id, p => p.ProductId, (p, s) => new { productName = p.Name, revenue = p.Price * s.Qty });

            Console.WriteLine("---------------");
            Console.WriteLine("Total Revenue of Products");
            foreach (var i in totalRevenue)
            {
                Console.WriteLine(i.productName + " - " + i.revenue);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Best product");
            var bestProduct = totalRevenue.OrderByDescending(x => x.revenue).First();
            Console.WriteLine(bestProduct.productName + " - " + bestProduct.revenue);

            var zeroSales = products.GroupJoin(sales, p => p.Id, s => s.ProductId, (p, s) => new
            {
                productsName = p.Name,
                totalSales = s.Sum(x => x.Qty * p.Price)
            }).Where(x => x.totalSales == 0);

            Console.WriteLine("---------------");
            Console.WriteLine("Product With Zero Sales");
            foreach (var i in zeroSales)
            {
                Console.WriteLine(i.productsName);
            }


            var books = new List<Book>
        {
            new Book{Title="C# Basics", Author="John", Genre="Tech", Year=2018, Price=500},
            new Book{Title="Java Advanced", Author="Mike", Genre="Tech", Year=2016, Price=700},
            new Book{Title="History India", Author="Raj", Genre="History", Year=2019, Price=400}
        };
            Console.WriteLine("---------------");
            Console.WriteLine("---------------");

            Console.WriteLine("Books Published After 2015");
            var bookspub = books.Where(x => x.Year > 2015);
            foreach (var i in bookspub)
            {
                Console.WriteLine(i.Title);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("groupBy Genre and Count");

            var cnt = books.GroupBy(x => x.Genre).Select(y => new
            {
                genre = y.Key,
                Count = y.Count()
            });
            foreach (var i in cnt)
            {
                Console.WriteLine(i.genre + " - " + i.Count);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Most Expensive Book");
            var expensive = books.GroupBy(x => x.Genre).Select(y => new
            {
                genre = y.Key,
                exp = y.OrderByDescending(z => z.Price).First()
            });

            foreach (var i in expensive)
            {
                Console.WriteLine(i.genre + " - " + i.exp.Title);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Distinct Authors");
            var auth = books.Select(x => x.Author).Distinct();
            foreach (var i in auth)
            {
                Console.WriteLine(i);
            }


            var customers = new List<Customer>
            {
                new Customer{Id=1, Name="Ajay", City="Delhi"},
                new Customer{Id=2, Name="Sunita", City="Mumbai"}
            };

            var orders = new List<Order>
            {
                new Order{OrderId=1, CustomerId=1, Amount=20000},
                new Order{OrderId=2, CustomerId=1, Amount=40000}
            };

            var firstJoin = customers.Join(orders, c => c.Id, o => o.OrderId, (c, o) => new
            {
                Name = c.Name,
                amount = o.Amount
            });

            Console.WriteLine("---------------");
            Console.WriteLine("---------------");
            Console.WriteLine("order AMount Per Customer");
            foreach (var i in firstJoin)
            {
                Console.WriteLine(i.Name + " - " + i.amount);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Customers With No order");
            var zeroOrder = firstJoin.Where(x => x.amount == 0);
            foreach (var i in zeroOrder)
            {
                Console.WriteLine(zeroOrder);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Customers Spent Above 50000");
            var aboveFifty = firstJoin.Where(x => x.amount > 50000);
            foreach (var i in aboveFifty)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Sort Customer By Spendings");
            var custSort = firstJoin.OrderByDescending(x => x.amount);
            foreach (var i in custSort)
            {
                Console.WriteLine(i.Name + " - " + i.amount);
            }


            var movies = new List<Movie>
        {
            new Movie{Title="Inception", Genre="SciFi", Rating=9, Year=2010},
            new Movie{Title="Avatar", Genre="SciFi", Rating=8.5, Year=2009},
            new Movie{Title="Titanic", Genre="Drama", Rating=8, Year=1997}
        };

            Console.WriteLine("---------------");
            Console.WriteLine("---------------");
            Console.WriteLine("Movies With rating above 8");
            var ratin = movies.Where(x => x.Rating > 8);
            foreach (var i in ratin)
            {
                Console.WriteLine(i.Title);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Average Movie Rating in Genre");
            var avgRating = movies.GroupBy(x => x.Genre).Select(y => new
            {
                genre = y.Key,
                average = y.Average(z => z.Rating)
            });
            foreach (var i in avgRating)
            {
                Console.WriteLine(i.genre + " - " + i.average);
            }

            var latestGenre = movies.GroupBy(x => x.Genre).Select(y => new
            {
                genre = y.Key,
                movie = y.OrderByDescending(z => z.Year).First()
            });
            Console.WriteLine("---------------");
            Console.WriteLine("Latest Movie in each genre");

            foreach (var i in latestGenre)
            {
                Console.WriteLine(i.genre + " - " + i.movie.Title);
            }

            Console.WriteLine("---------------");
            Console.WriteLine("Top 5 Highest rated Movies");
            var topMovies = movies.OrderByDescending(x => x.Rating);
            foreach (var i in topMovies)
            {
                Console.WriteLine(i.Title + " - " + i.Rating);
            }

            var transactions = new List<Transaction>
        {
            new Transaction{Acc=101, Amount=5000, Type="Credit"},
            new Transaction{Acc=101, Amount=2000, Type="Debit"},
            new Transaction{Acc=102, Amount=10000, Type="Debit"}
        };

            Console.WriteLine("---------------");
            Console.WriteLine("---------------");
            Console.WriteLine("total Balance per Account");
            var totalBalance = transactions.GroupBy(x => x.Acc).Select(y => new
            {
                acc = y.Key,
                balance = y.Sum(z => z.Amount)
            });

            foreach (var i in totalBalance)
            {
                Console.WriteLine(i.acc + " - " + i.balance);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Suspecious Account");
            var susAcc = transactions.GroupBy(x => x.Acc).Select(y => new
            {
                group = y.Key,
                debit = y.Where(z => z.Type == "Debit").Sum(s => s.Amount),
                credit = y.Where(z => z.Type == "Credit").Sum(s => s.Amount),

            }).Where(a => a.debit > a.credit);

            foreach (var i in susAcc)
            {
                Console.WriteLine($"{i.group} Debit: {i.debit} Credit: {i.credit}");
            }
            Console.WriteLine("---------------");
            Console.WriteLine("highest Transaction per Acc");
            var highestTrans = transactions.GroupBy(y => y.Acc).Select(x => x.OrderByDescending(z => z.Amount).First());
            foreach (var i in highestTrans)
            {
                Console.WriteLine(i.Acc + " - " + i.Amount);
            }

            var cart = new List<CartItem>
        {
            new CartItem{Name="TV", Category="Electronics", Price=30000, Qty=1},
            new CartItem{Name="Sofa", Category="Furniture", Price=15000, Qty=1}
        };

            Console.WriteLine("---------------");
            Console.WriteLine("---------------");
            Console.WriteLine("Tatal Cart Value");
            var totalCartValue = cart.Sum(x => x.Price);
            Console.WriteLine(totalCartValue);

            Console.WriteLine("---------------");
            Console.WriteLine("Group By category And cost");
            var totalcostBycategory = cart.GroupBy(x => x.Category).Select(y => new
            {
                Category = y.Key,
                Amount = y.Sum(z => z.Price * z.Qty)
            });

            foreach (var i in totalcostBycategory)
            {
                Console.WriteLine(i.Category + " - " + i.Amount);
            }

            Console.WriteLine("---------------");
            Console.WriteLine("10% discount on Electronics");
            var discount = cart.Where(z => z.Category == "Electronics").Sum(x => x.Price - (x.Price * 0.10));

            Console.WriteLine(discount);

            var cartItems = cart.GroupBy(x => 1).Select(y => new
            {
                Items = y.Sum(z => z.Qty),
                Total = y.Sum(z => z.Price * z.Qty)
            });
            Console.WriteLine("---------------");
            Console.WriteLine("Dto of cart");
            foreach (var i in cartItems)
            {
                Console.WriteLine(i.Items + " - " + i.Total);
            }
            var users = new List<User>
        {
                new User{Id=1, Name="A", Country="India"},
                new User{Id=2, Name="B", Country="USA"}
        };

            var posts = new List<Post>
        {
                new Post{UserId=1, Likes=100},
                new Post{UserId=1, Likes=50}
        };

            Console.WriteLine("---------------");
            Console.WriteLine("---------------");
            Console.WriteLine("top User By total Likes");
            var TopLiked = users.Join(posts, u => u.Id, p => p.UserId, (u, p) => new
            {
                ID = u.Id,
                Likes = p.Likes

            }).GroupBy(c => c.ID).Select(z => new
            {

                Id = z.Key,
                TotalLikes = z.Sum(s => s.Likes)
            }).OrderByDescending(t => t.TotalLikes).First();

            Console.WriteLine(TopLiked.Id + " - " + TopLiked.TotalLikes);
            Console.WriteLine("---------------");
            Console.WriteLine("Group Users By Country");

            var groupbyCountry = users.GroupBy(x => x.Country).ToList();
            foreach (var i in groupbyCountry)
            {
                Console.WriteLine(i.Key);
                foreach (var j in i)
                {
                    Console.WriteLine("ID -" + j.Id);
                }
            }
            var avgLikes = users.GroupJoin(posts, p => p.Id, u => u.UserId, (p, u) =>
            new
            {
                Id = p.Id,
                AVgLikes = u.Any() ? u.Average(x => x.Likes) : 0
            }).Where(y => y.AVgLikes > 0);
            Console.WriteLine("---------------");
            Console.WriteLine("Average Likes Per Post");
            foreach (var i in avgLikes)
            {
                Console.WriteLine(i.Id + " - " + i.AVgLikes);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Inactive Users No Post");
            var noPost = users.GroupJoin(posts, u => u.Id, p => p.UserId, (u, p) => new
            {
                User = u,
                Posts = !p.Any()
            }).Where(z => z.Posts).Select(x => x.User);

            foreach (var i in noPost)
            {
                Console.WriteLine(i.Id + " - " + i.Name);
            }
        }

    }
}