// See https://aka.ms/new-console-template for more information
using bibModelJanuszewski;
using bibModelJanuszewski.Model;
using ConsoleTables;

namespace bibAdmJanuszewski
{
    class Program
    {
        enum SHOWMETHOD
        {
            XDOC,
            XS
        }
        static void Main(string[] args)
        {
            //var autor1 = new bibModelJanuszewski.Model.AutorzyAutor()
            //{ id = 1, imię = "Adam", nazwisko = "Mickiewicz", rokUr = 1798 };
            //var autor2 = new bibModelJanuszewski.Model.AutorzyAutor()
            //{ id = 2, imię = "Juliusz", nazwisko = "Słowacki", rokUr = 1809 };
            //var listaAutor = new bibModelJanuszewski.Model.Autorzy()
            //{
            //    Autor = new bibModelJanuszewski.Model.AutorzyAutor[] { autor1, autor2 }
            //};
            //var x1 = listaAutor.Autor[1].nazwisko; //Słowacki
            //var x0 = listaAutor.Autor[0].nazwisko; //Mickiewicz

            Console.WriteLine("Hello, World!");

            BDLibrary db = new(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\__ukw"
                );

            //Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            //Console.WriteLine(db.TestData());

            for (; ; )
            {
                Console.Clear();
                ShowMenu();
                char selection = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");

                switch (selection)
                {
                    case 'w':
                    case 'W':
                        Console.WriteLine("Wybrałeś opcję W");
                        ShowData(db, SHOWMETHOD.XS);
                        break;
                    case 'a':
                    case 'A':
                        Console.WriteLine("Wybrałeś opcję A");
                        break;
                    case 'x':
                    case 'X':
                        return;

                    default:
                        Console.WriteLine("Nieprawidłowa opcja");
                        break;
                }

                Console.Write("Oczekiwanie na wcisniecie klawisza...");
                Console.ReadKey();
            }

        }

        static void ShowMenu()
        {
            string[] options = new string[] {
                "W - wyświetl dane (wszystkie)",
                "A - ksiażki dla podanego autora",
                "",
                "X - koniec"
            };

            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
            Console.Write("Wybierz opcje: ");
        }

        static void ShowData(BDLibrary db, SHOWMETHOD method = SHOWMETHOD.XDOC)
        {
            if (method == SHOWMETHOD.XDOC)
            {
                foreach (string model in db.ReportData())
                {
                    Console.WriteLine(model);
                    Console.WriteLine();
                }
            }
            else if (method == SHOWMETHOD.XS)
            {
                Console.WriteLine("Autorzy:");
                Autorzy autorzy = db.ReportData2<Autorzy>();
                if (autorzy != null && autorzy.Autor.Length > 0)
                {
                    var table = new ConsoleTable("ID", "Nazwisko", "Imie", "RokUrodzenia");
                    foreach (AutorzyAutor autor in autorzy.Autor)
                    {
                        table.AddRow(autor.id, autor.nazwisko, autor.imię, autor.rokUr);
                    }
                    table.Write(Format.Alternative);
                    Console.WriteLine();
                }

                Console.WriteLine("Wydawcy:");
                Wydawcy wydawcy = db.ReportData2<Wydawcy>();
                if (autorzy != null && autorzy.Autor.Length > 0)
                {
                    var table = new ConsoleTable("ID", "Nazwa", "Strona");
                    foreach (WydawcyWydawca wydawca in wydawcy.Wydawca)
                    {
                        table.AddRow(wydawca.id, wydawca.nazwa, wydawca.strona);
                    }
                    table.Write(Format.Alternative);
                    Console.WriteLine();
                }

                Console.WriteLine("Ksiazki:");
                Ksiazki ksiazki = db.ReportData2<Ksiazki>();
                if (autorzy != null && autorzy.Autor.Length > 0)
                {
                    var table = new ConsoleTable("ID", "Tytul", "ISBN", "Cena", "idAutora", "idWydawcy");
                    foreach (KsiazkiKsiazka ksiazka in ksiazki.Ksiazka)
                    {
                        table.AddRow(ksiazka.id, ksiazka.tytul, ksiazka.ISBN, ksiazka.cena, ksiazka.idAutora, ksiazka.idWydawnictwa);
                    }
                    table.Write(Format.Alternative);
                    Console.WriteLine();
                }

            }

        }
    }
}
