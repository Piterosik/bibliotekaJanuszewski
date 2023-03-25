// See https://aka.ms/new-console-template for more information
using bibModelJanuszewski;
using ConsoleTables;

namespace bibAdmJanuszewski
{
    class Program
    {
        static void Main(string[] args)
        {
            BDLibrary db = new(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\__ukw"
                );

            db.TestData();

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
                        ShowData(db);
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

        static void ShowData(BDLibrary db)
        {

            Console.WriteLine("Autorzy LINQ");
            var authorsLQ = db.ReportDataLQAuthors();
            if (authorsLQ != null)
            {
                var table = new ConsoleTable("ID", "Nazwisko", "Imie", "RokUrodzenia");
                foreach (var item in authorsLQ)
                {
                    table.AddRow(item.id, item.lastName, item.firstName, item.year);
                }
                table.Write(Format.Alternative);
                Console.WriteLine();
            }

            Console.WriteLine("Wydawcy LINQ");
            var publishersLQ = db.ReportDataLQPublishers();
            if (publishersLQ != null)
            {
                var table = new ConsoleTable("ID", "Nazwa", "Strona"); ;
                foreach (var item in publishersLQ)
                {
                    table.AddRow(item.id, item.name, item.website);
                }
                table.Write(Format.Alternative);
                Console.WriteLine();
            }

            Console.WriteLine("Ksiazki LINQ");
            var booksLQ = db.ReportDataLQBooks();
            if (booksLQ != null)
            {
                var table = new ConsoleTable("ID", "Tytul", "Autor", "Cena", "ISBN", "Wydawnictwo"); ;
                foreach (var item in booksLQ)
                {
                    table.AddRow(item.id, item.title, item.authorLastName + " " + item.authorFirstName, item.price, item.ISBN, item.publisherName);
                }
                table.Write(Format.Alternative);
                Console.WriteLine();
            }



        }
    }
}
