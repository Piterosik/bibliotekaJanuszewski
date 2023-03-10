using bibModelJanuszewski.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace bibModelJanuszewski
{
    public class BDLibrary
    {
        string authorsFile, publishersFile, booksFile;

        public BDLibrary(string path, string authorsFile = null, string publishersFile = null, string booksFile=null)
        {
            if (authorsFile == null) authorsFile = DefaultFileNames.defAuthors;
            if (publishersFile == null) publishersFile = DefaultFileNames.defPublishers;
            if (booksFile == null) booksFile = DefaultFileNames.defBooks;

            this.authorsFile = path + authorsFile;
            this.publishersFile = path + publishersFile;
            this.booksFile = path + booksFile;
        }

        public void TestData()
        {
            Autorzy autorzy = new Autorzy();
            autorzy.Autor = new AutorzyAutor[5] {
                new AutorzyAutor() { id = 1, imię = "Jan", nazwisko = "Kowalski", rokUr = 1999 },
                new AutorzyAutor() { id = 2, imię = "Jan", nazwisko = "Kowalski", rokUr = 1999 },
                new AutorzyAutor() { id = 3, imię = "Jan", nazwisko = "Kowalski", rokUr = 1999 },
                new AutorzyAutor() { id = 4, imię = "Jan", nazwisko = "Kowalski", rokUr = 1999 },
                new AutorzyAutor() { id = 5, imię = "Piotr", nazwisko = "Januszewski", rokUr = 1997 },
            };

            //XmlSerializer xs = new XmlSerializer();
        }
    }
}
