﻿// See https://aka.ms/new-console-template for more information
using bibModelJanuszewski;




namespace bibAdmJanuszewski
{
	class Program
	{
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

			BDLibrary db = new BDLibrary(
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\__ukw"
				);

			Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
			Console.WriteLine(db.TestData());

		}

	}
}
