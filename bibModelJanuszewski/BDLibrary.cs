using bibModelJanuszewski.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;



namespace bibModelJanuszewski
{
    public class BDLibrary
    {
        readonly string authorsFile, publishersFile, booksFile;

        public BDLibrary(string path, string authorsFile = null, string publishersFile = null, string booksFile = null)
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
            Autorzy autorzy = new Autorzy
            {
                Autor = new AutorzyAutor[]
            {
                new AutorzyAutor() { id = 1, imię = "Jan", nazwisko = "Kowalski", rokUr = 1999 },
                new AutorzyAutor() { id = 2, imię = "Adam", nazwisko = "Nowak", rokUr = 1999 },
                new AutorzyAutor() { id = 3, imię = "Ewelina", nazwisko = "Kątna", rokUr = 1999 },
                new AutorzyAutor() { id = 4, imię = "Dawid", nazwisko = "Kopacz", rokUr = 1999 },
                new AutorzyAutor() { id = 5, imię = "Piotr", nazwisko = "Januszewski", rokUr = 1997 },
            }
            };

            Console.WriteLine("Saving authors file: " + authorsFile);
            if (!SaveFile(authorsFile, autorzy))
            {
                return false;
            }

            Wydawcy wydawcy = new Wydawcy
            {
                Wydawca = new WydawcyWydawca[]
            {
                new WydawcyWydawca() { id=1, nazwa="Dom Wydawniczy Rebis", strona="www.rebis.com.pl" },
                new WydawcyWydawca() { id=2, nazwa="Wydawnictwo Albatros", strona="www.wydawnictwoalbatros.com" },
                new WydawcyWydawca() { id=3, nazwa="Wydawnictwo Czarne", strona="http://czarne.com.pl" },
                new WydawcyWydawca() { id=4, nazwa="Wydawnictwo Literackie", strona="www.wydawnictwoliterackie.pl" },
                new WydawcyWydawca() { id=5, nazwa="Fabryka Słów", strona="www.fabrykaslow.com.pl" }
            }
            };

            Console.WriteLine("Saving publishers file: " + publishersFile);
            if (!SaveFile(publishersFile, wydawcy))
            {
                return false;
            }

            Ksiazki ksiazki = new Ksiazki
            {
                Ksiazka = new KsiazkiKsiazka[]
            {
                new KsiazkiKsiazka() { id=1, tytul="Lalka", cena=2.50f, idAutora=1, idWydawnictwa=5,ISBN="" },
                new KsiazkiKsiazka() { id=2, tytul="Wesele", cena=2.50f, idAutora=2, idWydawnictwa=4,ISBN="" },
                new KsiazkiKsiazka() { id=3, tytul="Placówka", cena=2.50f, idAutora=3, idWydawnictwa=3,ISBN="" },
                new KsiazkiKsiazka() { id=4, tytul="Inny świat", cena=2.50f, idAutora=4, idWydawnictwa=2,ISBN="" },
                new KsiazkiKsiazka() { id=5, tytul="Powrót z gwiazd", cena=2.50f, idAutora=5, idWydawnictwa=1,ISBN="" },
                new KsiazkiKsiazka() { id=6, tytul="Heban", cena=2.50f, idAutora=5, idWydawnictwa=5,ISBN="" },
                new KsiazkiKsiazka() { id=7, tytul="Faraon", cena=2.50f, idAutora=4, idWydawnictwa=4,ISBN="" },
                new KsiazkiKsiazka() { id=8, tytul="Ziemia obiecana", cena=2.50f, idAutora=3, idWydawnictwa=3,ISBN="" },
                new KsiazkiKsiazka() { id=9, tytul="Ogniem i mieczem", cena=2.50f, idAutora=2, idWydawnictwa=2,ISBN="" },
                new KsiazkiKsiazka() { id=10, tytul="Dzieła zebrane", cena=2.50f, idAutora=1, idWydawnictwa=1,ISBN="" }
            }
            };

            Console.WriteLine("Saving books file: " + booksFile);
            if (!SaveFile(booksFile, ksiazki))
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Usuń nieużywane prywatne składowe", Justification = "<Oczekujące>")]
        private static bool GenerateBooks(string path)
        {
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                new XComment("Przykładowe dane ksiązek"),
                new XElement("Ksiazki",
                    new XAttribute("wersja", "2.0"),
                    new XElement("Ksiazka",
                        new XAttribute("id", "1"),
                        new XAttribute("tytul", "Lalka"),
                        new XAttribute("idAutora", ""),
                        new XAttribute("ISBN", ""),
                        new XAttribute("cena", ""),
                        new XAttribute("idWydawnictwa", "")
                    )
                )
            );


            string[,] bookData = new string[,]
            {
                {"Bolesław Prus", "Lalka" },
                {"Stanisław Wyspiański", "Wesele" },
                {"Bolesław Prus", "Placówka" },
                {"Gustaw Herling-Grudziński", "Inny świat" },
                {"Stanisław Lem", "Powrót z gwiazd" },
                {"Ryszard Kapuściński", "Heban" },
                {"Bolesław Prus", "Faraon" },
                {"Władysław Stanisław Reymont", "Ziemia obiecana" },
                {"Henryk Sienkiewicz", "Ogniem i mieczem" },
                {"Piotr Januszewski", "Dzieła zebrane" }
            };

            for (int i = 0; i < bookData.Length; i++)
            {
                doc.Root.Add(
                    new XElement("Ksiazka",
                        new XAttribute("id", i + 1),
                        new XAttribute("tytul", bookData[i, 1]),
                        new XAttribute("idAutora", ""),
                        new XAttribute("ISBN", ""),
                        new XAttribute("cena", 15.00f + (0.5f * i)),
                        new XAttribute("idWydawnictwa", "")
                    )
                );
            }


            Console.WriteLine("Saving books file: " + path);
            try
            {
                if (!File.Exists(path))
                {
                    doc.Save(path);
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

        public string[,] ReportData()
        {
            string[,] result = new string[,]
            {
                {"Autorzy:",XDocument.Load(authorsFile).ToString() },
                {"Wydawcy:", XDocument.Load(publishersFile).ToString() },
                {"Ksiazki:", XDocument.Load(booksFile).ToString() },
            };
            return result;
        }

        public T ReportData2<T>()
        {
            T result;

            XmlSerializer xs = new XmlSerializer(typeof(T));

            string path = typeof(T) == typeof(Autorzy) ? authorsFile : typeof(T) == typeof(Wydawcy) ? publishersFile : booksFile;

            using (StreamReader sr = new StreamReader(path))
            {
                result = (T) xs.Deserialize(sr);
            }

            return result;
        }

        private T DeserializeXML<T>()
        {
            T result;
            XmlSerializer xs = new XmlSerializer(typeof(T));

            string path = typeof(T) == typeof(Autorzy) ? authorsFile : typeof(T) == typeof(Wydawcy) ? publishersFile : booksFile;

            using (StreamReader sr = new StreamReader(path))
            {
                result = (T) xs.Deserialize(sr);
            }
            return result;
        }

        public IOrderedEnumerable<AutorzyAutor> ReportDataLQAutorzy()
        {
            var autorzy = DeserializeXML<Autorzy>();
            return from item in autorzy.Autor orderby item.nazwisko select item;
        }

        public IOrderedEnumerable<WydawcyWydawca> ReportDataLQWydawcy()
        {
            var wydawcy = DeserializeXML<Wydawcy>();
            return from item in wydawcy.Wydawca orderby item.nazwa select item;
        }

        public List<KsiążkiKsiążkaExt> ReportDataLQKsiązki()
        {
            var autorzy = ReportDataLQAutorzy();
            var wydawcy = ReportDataLQWydawcy();
            var ksiazki = DeserializeXML<Ksiazki>();

            return (from item in ksiazki.Ksiazka
                    join author in autorzy on item.idAutora equals author.id
                    join publisher in wydawcy on item.idWydawnictwa equals publisher.id
                    orderby item.tytul
                    select new KsiążkiKsiążkaExt()
                    {
                        id = item.id,
                        tytul = item.tytul,
                        autorNazwisko = author.nazwisko,
                        autorImie = author.imię,
                        cena = item.cena,
                        ISBN = item.ISBN,
                        wydawnictwoNazwa = publisher.nazwa
                    }
                    ).ToList();


        }
    }
}
