// See https://aka.ms/new-console-template for more information
using bibModelJanuszewski;


Console.WriteLine("Hello, World!");

static void Main(string[] args)
{
    var autor1 = new bibModelJanuszewski.Model.AutorzyAutor()
    { id = 1, imię = "Adam", nazwisko = "Mickiewicz", rokUr = 1798 };
    var autor2 = new bibModelJanuszewski.Model.AutorzyAutor()
    { id = 2, imię = "Juliusz", nazwisko = "Słowacki", rokUr = 1809 };
    var listaAutor = new bibModelJanuszewski.Model.Autorzy()
    {
        Autor = new bibModelJanuszewski.Model.AutorzyAutor[] { autor1, autor2 }
    };
    var x1 = listaAutor.Autor[1].nazwisko; //Słowacki
    var x0 = listaAutor.Autor[0].nazwisko; //Mickiewicz
}
