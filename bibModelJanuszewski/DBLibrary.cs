using bibModelJanuszewski.Model;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;



namespace bibModelJanuszewski
{
    public class DBLibrary
    {
        readonly string authorsFile, publishersFile, booksFile;

        public DBLibrary(string path, string authorsFile = null, string publishersFile = null, string booksFile = null)
        {
            if (authorsFile == null) authorsFile = DefaultFileNames.defAuthors;
            if (publishersFile == null) publishersFile = DefaultFileNames.defPublishers;
            if (booksFile == null) booksFile = DefaultFileNames.defBooks;

            this.authorsFile = path + "\\" + authorsFile + ".xml";
            this.publishersFile = path + "\\" + publishersFile + ".xml";
            this.booksFile = path + "\\" + booksFile + ".xml";
        }

        public bool TestData()
        {
            Authors authors = new Authors
            {
                Author = new AuthorsAuthor[]
            {
                new AuthorsAuthor() { id = 1, firstName = "Jan", lastName = "Kowalski", year = 1999 },
                new AuthorsAuthor() { id = 2, firstName = "Adam", lastName = "Nowak", year = 1999 },
                new AuthorsAuthor() { id = 3, firstName = "Ewelina", lastName = "Kątna", year = 1999 },
                new AuthorsAuthor() { id = 4, firstName = "Dawid", lastName = "Kopacz", year = 1999 },
                new AuthorsAuthor() { id = 5, firstName = "Piotr", lastName = "Januszewski", year = 1997 },
            }
            };

            Console.WriteLine("Saving authors file: " + authorsFile);
            if (!SaveFile(authorsFile, authors))
            {
                return false;
            }

            Publishers publishers = new Publishers
            {
                Publisher = new PublishersPublisher[]
            {
                new PublishersPublisher() { id=1, name="Dom Wydawniczy Rebis", website="www.rebis.com.pl" },
                new PublishersPublisher() { id=2, name="Wydawnictwo Albatros", website="www.wydawnictwoalbatros.com" },
                new PublishersPublisher() { id=3, name="Wydawnictwo Czarne", website="http://czarne.com.pl" },
                new PublishersPublisher() { id=4, name="Wydawnictwo Literackie", website="www.wydawnictwoliterackie.pl" },
                new PublishersPublisher() { id=5, name="Fabryka Słów", website="www.fabrykaslow.com.pl" }
            }
            };

            Console.WriteLine("Saving publishers file: " + publishersFile);
            if (!SaveFile(publishersFile, publishers))
            {
                return false;
            }

            Books books = new Books
            {
                Book = new BooksBook[]
            {
                new BooksBook() { id=1, title="Lalka", price=2.50f, authorId=1, publisherId=5,ISBN="" },
                new BooksBook() { id=2, title="Wesele", price=2.50f, authorId=2, publisherId=4,ISBN="" },
                new BooksBook() { id=3, title="Placówka", price=2.50f, authorId=3, publisherId=3,ISBN="" },
                new BooksBook() { id=4, title="Inny świat", price=2.50f, authorId=4, publisherId=2,ISBN="" },
                new BooksBook() { id=5, title="Powrót z gwiazd", price=2.50f, authorId=5, publisherId=1,ISBN="" },
                new BooksBook() { id=6, title="Heban", price=2.50f, authorId=5, publisherId=5,ISBN="" },
                new BooksBook() { id=7, title="Faraon", price=2.50f, authorId=4, publisherId=4,ISBN="" },
                new BooksBook() { id=8, title="Ziemia obiecana", price=2.50f, authorId=3, publisherId=3,ISBN="" },
                new BooksBook() { id=9, title="Ogniem i mieczem", price=2.50f, authorId=2, publisherId=2,ISBN="" },
                new BooksBook() { id=10, title="Dzieła zebrane", price=2.50f, authorId=1, publisherId=1,ISBN="" }
            }
            };

            Console.WriteLine("Saving books file: " + booksFile);
            if (!SaveFile(booksFile, books))
            {
                return false;
            }

            return true;

        }

        private static bool SaveFile<T>(string path, T obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));

            try
            {
                if (!File.Exists(path))
                {
                    FileInfo fi = new FileInfo(path);
                    fi.Directory.Create();

                    using (StreamWriter s = new StreamWriter(path))
                    {
                        xs.Serialize(s, obj);
                    }
                }
                else
                {
                    Console.WriteLine("File exists");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private T DeserializeXML<T>()
        {
            T result;
            XmlSerializer xs = new XmlSerializer(typeof(T));

            string path = typeof(T) == typeof(Authors) ? authorsFile : typeof(T) == typeof(Publishers) ? publishersFile : booksFile;

            using (StreamReader sr = new StreamReader(path))
            {
                result = (T) xs.Deserialize(sr);
            }
            return result;
        }

        public IOrderedEnumerable<AuthorsAuthor> ReportDataLQAuthors()
        {
            var Authors = DeserializeXML<Authors>();
            return from item in Authors.Author orderby item.lastName select item;
        }

        public IOrderedEnumerable<PublishersPublisher> ReportDataLQPublishers()
        {
            var Publishers = DeserializeXML<Publishers>();
            return from item in Publishers.Publisher orderby item.name select item;
        }

        public IOrderedEnumerable<BooksBookExt> ReportDataLQBooks()
        {
            var authors = ReportDataLQAuthors();
            var publishers = ReportDataLQPublishers();
            var books = DeserializeXML<Books>();

            return (from item in books.Book
                    join author in authors on item.authorId equals author.id
                    join publisher in publishers on item.publisherId equals publisher.id
                    select new BooksBookExt()
                    {
                        id = item.id,
                        title = item.title,
                        authorLastName = author.lastName,
                        authorFirstName = author.firstName,
                        price = item.price,
                        ISBN = item.ISBN,
                        publisherName = publisher.name
                    }).OrderBy(a => a.title);

        }

    }
}
