using Microsoft.EntityFrameworkCore;
using onta_nicusor_lab2.Models;
using System;
using System.Linq;

namespace onta_nicusor_lab2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                // Verifica daca exista deja date in baza de date
                if (context.Books.Any())
                {
                    return; // Baza de date a fost creata anterior
                }

                // Datele pe care dorim sa le introducem in Orders, Publishers si PublishedBook
                // Pentru a ne asigura ca datele sunt consistente
                var orders = new Order[]
                {
                    new Order{BookID=1,CustomerID=1050,OrderDate=DateTime.Parse("2021-02-25")},
                    new Order{BookID=3,CustomerID=1045,OrderDate=DateTime.Parse("2021-09-28")},
                    // alte obiecte Order
                };

                foreach (Order e in orders)
                {
                    context.Orders.Add(e);
                }

                context.SaveChanges();

                var publishers = new Publisher[]
                {
                    new Publisher{PublisherName="Humanitas",Address="Str. Aviatorilor, nr. 40, Bucuresti"},
                    new Publisher{PublisherName="Nemira",Address="Str. Plopilor, nr. 35, Ploiesti"},
                    // alte obiecte Publisher
                };

                foreach (Publisher p in publishers)
                {
                    context.Publishers.Add(p);
                }

                context.SaveChanges();

                var books = context.Books;

                var publishedbooks = new PublishedBook[]
                {
                    new PublishedBook { BookID = books.Single(c => c.Title == "Maytrei" ).ID, PublisherID = publishers.Single(i => i.PublisherName == "Humanitas").ID },
                    new PublishedBook { BookID = books.Single(c => c.Title == "Enigma Otiliei" ).ID, PublisherID = publishers.Single(i => i.PublisherName == "Humanitas").ID },
                    // alte obiecte PublishedBook
                };

                foreach (PublishedBook pb in publishedbooks)
                {
                    context.PublishedBooks.Add(pb);
                }

                context.SaveChanges();
            }
        }
    }
}
