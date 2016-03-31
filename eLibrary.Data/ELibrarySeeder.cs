//#define TEST_SEED
//#define FORCE_RECREATE

using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data
{
    public class ELibrarySeeder
    {
        private ELibraryContext _ctx;

        public ELibrarySeeder(ELibraryContext ctx)
        {
            _ctx = ctx;
        }

        public void Seed()
        {
#if !(TEST_SEED || FORCE_RECREATE)
            if (_ctx.Authors.Count() > 0)
                return;

#endif

#if TEST_SEED || FORCE_RECREATE
      ExecuteQueries(
        "DELETE FROM TagBooks",
        "DELETE FROM [Library].[OrderEntry]",
        "DELETE FROM Book.Tag",
        "DELETE FROM Book.Book",
        "DELETE FROM Book.Author",
        "DELETE FROM [Library].[Order]"
      );
#endif


            SeedTags();
            SeedBooks();
            SeedOrders();
            SeedOrderEntries();


        }

        void SeedTags()
        {
            _ctx.Tags.AddOrUpdate(x => x.Name,
                new Tag { Name = "Programming" },
                new Tag { Name = "C#" },
                new Tag { Name = ".Net" },
                new Tag { Name = "Software Design & Engineering" },
                new Tag { Name = "Enterprise Applications" },
                new Tag { Name = "Web Design" },
                new Tag { Name = "JavaScript"},
                new Tag { Name = "Data Structures" },
                new Tag { Name = "Windows Server" },
                new Tag { Name = "Unix" }
                );

            _ctx.SaveChanges();
        }

        void SeedOrders()
        {
            _ctx.Orders.AddOrUpdate(x=>x.CurrentDate,
                new Order { UserName="testuser", CurrentDate=new DateTime(2016, 1, 3) }
                );
            _ctx.SaveChanges();
        }

        void SeedBooks()
        {

            //Add book CLR via C#, Second Edition
            var book = new Book
            {
                Title = "CLR via C#, Second Edition",
                Author = new Author { Name = "Jeffrey Richter" },
                Description = @"Dig deep and master the intricacies of the common language runtime, C#, and .NET development. Led by programming expert Jeffrey Richter, a longtime consultant to the Microsoft .NET team - you’ll gain pragmatic insights for building robust, reliable, and responsive apps and components.",
                Pages = 736,
                Price = 39.99m,
                Stock = 9,
                Year = 2006
            };

            var tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "C#").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == ".Net").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);


            //Add book Code Complete: A Practical Handbook of Software Construction, Second Edition
            book = new Book
            {
                Title = "Code Complete: A Practical Handbook of Software Construction, Second Edition",
                Author = new Author { Name = "Steve McConnell" },
                Description = @"Widely considered one of the best practical guides to programming, Steve McConnell’s original CODE COMPLETE has been helping developers write better software for more than a decade. Now this classic book has been fully updated and revised with leading-edge practices—and hundreds of new code samples—illustrating the art and science of software construction. Capturing the body of knowledge available from research, academia, and everyday commercial practice, McConnell synthesizes the most effective techniques and must-know principles into clear, pragmatic guidance. No matter what your experience level, development environment, or project size, this book will inform and stimulate your thinking—and help you build the highest quality code.",
                Pages = 960,
                Price = 34.11m,
                Stock = 7,
                Year = 2004
            };

            tag = _ctx.Tags.Where(f => f.Name == "Software Design & Engineering").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "Enterprise Applications").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);


            //Add book Web Design with HTML, CSS, JavaScript and jQuery Set
            book = new Book
            {
                Title = "Web Design with HTML, CSS, JavaScript and jQuery Set",
                Description = @"This two-book set combines the titles HTML & CSS: Designing and Building Web Sites and JavaScript & jQuery: Interactive Front-End Development. Together these two books form an ideal platform for anyone who wants to master HTML and CSS before stepping up to JavaScript and jQuery.",
                Author = new Author { Name = "Jon Duckett" },
                Pages = 1152,
                Price = 33.95m,
                Stock = 7,
                Year = 2014
            };

            tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "Web Design").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);

            //Add book JavaScript: The Good Parts
            book = new Book
            {
                Title = "JavaScript: The Good Parts",
                Description = @"Most programming languages contain good and bad parts, but JavaScript has more than its share of the bad, having been developed and released in a hurry before it could be refined. This authoritative book scrapes away these bad features to reveal a subset of JavaScript that's more reliable, readable, and maintainable than the language as a whole—a subset you can use to create truly extensible and efficient code.",
                Author = new Author { Name = "Douglas Crockford" },
                Pages = 176,
                Price = 20.81m,
                Stock = 11,
                Year = 2008
            };

            tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "Web Design").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "JavaScript").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);

            //Add book Clean Code: A Handbook of Agile Software Craftsmanship
            book = new Book
            {
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                Description = @"Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code: A Handbook of Agile Software Craftsmanship . Martin has teamed up with his colleagues from Object Mentor to distill their best agile practice of cleaning code “on the fly” into a book that will instill within you the values of a software craftsman and make you a better programmer—but only if you work at it.",
                Author = new Author { Name = "Robert C. Martin" },
                Pages = 464,
                Price = 33.53m,
                Stock = 5,
                Year = 2008
            };

            tag = _ctx.Tags.Where(f => f.Name == "Software Design & Engineering").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);


            //Add book Cracking the Coding Interview, 6th Edition: 189 Programming Questions and Solutions

            book = new Book
            {
                Title = "Cracking the Coding Interview, 6th Edition: 189 Programming Questions and Solutions",
                Description = @"I am not a recruiter. I am a software engineer. And as such, I know what it's like to be asked to whip up brilliant algorithms on the spot and then write flawless code on a whiteboard. I've been through this as a candidate and as an interviewer. Cracking the Coding Interview, 6th Edition is here to help you through this process, teaching you what you need to know and enabling you to perform at your very best. I've coached and interviewed hundreds of software engineers. The result is this book.",
                Author = new Author { Name = "Gayle Laakmann McDowell" },
                Pages = 687,
                Price = 31.70m,
                Stock = 15,
                Year = 2015
            };

            tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);


            tag = _ctx.Tags.Where(f => f.Name == "Data Structures").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);


            //Add book Windows Internals: Including Windows Server 2008 and Windows Vista, Fifth Edition

            book = new Book
            {
                Title = "Windows Internals: Including Windows Server 2008 and Windows Vista, Fifth Edition",
                Description = @"See how the core components of the Windows operating system work behind the scenes—guided by a team of internationally renowned internals experts. Fully updated for Windows Server(R) 2008 and Windows Vista(R), this classic guide delivers key architectural insights on system design, debugging, performance, and support—along with hands-on experiments to experience Windows internal behavior firsthand.", 
                Author = new Author { Name = "Mark Russinovich" },
                Pages = 1232,
                Price = 43.43m,
                Stock = 5,
                Year = 2009
            };

            tag = _ctx.Tags.Where(f => f.Name == "Windows Server").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);


            _ctx.Books.Add(book);


            //Add book Learn Windows PowerShell in a Month of Lunches

            book = new Book
            {
                Title = "Learn Windows PowerShell in a Month of Lunches",
                Description = @"Learn Windows PowerShell in a Month of Lunches, Second Edition is an innovative tutorial designed for administrators. Just set aside one hour a day—lunchtime would be perfect—for a month, and you'll be automating Windows tasks faster than you ever thought possible. You'll start with the basics—what is PowerShell and what can you do with it. Then, you'll move systematically through the techniques and features you'll use to make your job easier and your day shorter.",
                Author = new Author { Name = "Don Jones" },
                Pages = 368,
                Price = 34.70m,
                Stock = 9,
                Year = 2012
            };

            tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);


            _ctx.Books.Add(book);


            //Add book The Linux Programming Interface: A Linux and UNIX System Programming Handbook

            book = new Book
            {
                Title = "The Linux Programming Interface: A Linux and UNIX System Programming Handbook",
                Description = @"The Linux Programming Interface is the definitive guide to the Linux and UNIX programming interface—the interface employed by nearly every application that runs on a Linux or UNIX system. In this authoritative work, Linux programming expert Michael Kerrisk provides detailed descriptions of the system calls and library functions that you need in order to master the craft of system programming, and accompanies his explanations with clear, complete example programs.",
                Author = new Author { Name = "Michael Kerrisk" },
                Pages = 1552,
                Price = 63.25m,
                Stock = 21,
                Year = 2010
            };

            tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "Unix").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);


            //Add book The Design and Implementation of the FreeBSD Operating System (2nd Edition) 

            book = new Book
            {
                Title = "The Design and Implementation of the FreeBSD Operating System (2nd Edition)",
                Description = @"The most complete, authoritative technical guide to the FreeBSD kernel’s internal structure has now been extensively updated to cover all major improvements between Versions 5 and 11. Approximately one-third of this edition’s content is completely new, and another one-third has been extensively rewritten. Three long-time FreeBSD project leaders begin with a concise overview of the FreeBSD kernel’s current design and implementation. Next, they cover the FreeBSD kernel from the system-call level down–from the interface to the kernel to the hardware. Explaining key design decisions, they detail the concepts, data structures, and algorithms used in implementing each significant system facility, including process management, security, virtual memory, the I/O system, filesystems, socket IPC, and networking.",
                Author = new Author { Name = "Marshall Kirk McKusick" },
                Pages = 928,
                Price = 56.17m,
                Stock = 11,
                Year = 2014
            };

            tag = _ctx.Tags.Where(f => f.Name == "Unix").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);


            //Add book Network Programming with Perl  

            book = new Book
            {
                Title = "Network Programming with Perl",
                Description = @"This is a superb book. Another splendid book from Lincoln, whose mastery and lucid exposition make this a must-have for the serious Perl programmer. --Jon Orwant, Chief Technology Officer, OReilly & Associates Founder of The Perl Journal, author of Mastering Algorithms with Perl, (OReilly & Associates)and co-author of Programming Perl, Third Edition (OReilly & Associates) Network Programming with Perl is a comprehensive, example-rich guide to creating network-based applications using the Perl programming language. Among its many capabilities, modern Perl provides a straightforward and powerful interface to TCP/IP, and this book shows you how to leverage these capabilities to create robust, maintainable, and efficient custom client/server applications. The book quickly moves beyond the basics to focus on high-level, application programming concepts, tools, and techniques. Readers will find a review of basic networking concepts and Perl fundamentals, including Perls I/O functions, process model, and object-oriented extensions.",
                Author = new Author { Name = "Lincoln D. Stein" },
                Pages = 784,
                Price = 36.84m,
                Stock = 4,
                Year = 2001
            };

            tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);


            //Add book Understanding the Linux Kernel, Third Edition 

            book = new Book
            {
                Title = "Understanding the Linux Kernel, Third Edition",
                Description = @"In order to thoroughly understand what makes Linux tick and why it works so well on a wide variety of systems, you need to delve deep into the heart of the kernel. The kernel handles all interactions between the CPU and the external world, and determines which programs will share processor time, in what order. It manages limited memory so well that hundreds of processes can share the system efficiently, and expertly organizes data transfers so that the CPU isn't kept waiting any longer than necessary for the relatively slow disks.",
                Author = new Author { Name = "Daniel P. Bovet" },
                Pages = 944,
                Price = 37.67m,
                Stock = 3,
                Year = 2005
            };

            tag = _ctx.Tags.Where(f => f.Name == "Unix").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            tag = _ctx.Tags.Where(f => f.Name == "Programming").FirstOrDefault();
            tag.Books.Add(book);
            book.Tags.Add(tag);

            _ctx.Books.Add(book);

            //Save all changes
            _ctx.SaveChanges();

        }


        void SeedOrderEntries()
        {
            var order = _ctx.Orders.Where(f => f.UserName == "testuser" && f.CurrentDate == new DateTime(2016, 1, 3)).FirstOrDefault(); 

            var entry = new OrderEntry
            {
                Quantity = 1
            };
            entry.BookItem = _ctx.Books.Where(f => f.Title == "JavaScript: The Good Parts").FirstOrDefault();
            entry.Order = order;

            _ctx.OrderEntries.Add(entry);

            entry = new OrderEntry
            {
                Quantity = 1
            };
            entry.BookItem = _ctx.Books.Where(f => f.Title == "Web Design with HTML, CSS, JavaScript and jQuery Set").FirstOrDefault();
            entry.Order = order;

            _ctx.OrderEntries.Add(entry);

            entry = new OrderEntry
            {
                Quantity = 1
            };
            entry.BookItem = _ctx.Books.Where(f => f.Title == "CLR via C#, Second Edition").FirstOrDefault();
            entry.Order = order;

            _ctx.OrderEntries.Add(entry);


            _ctx.SaveChanges();
        }

        void ExecuteQueries(params string[] sqlStatements)
        {
            foreach (var sql in sqlStatements)
            {
                _ctx.Database.ExecuteSqlCommand(sql);
            }

        }
    }
}
