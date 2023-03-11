﻿using bibModelJanuszewski.Model;
using System;
using System.IO;
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
				new KsiazkiKsiazka() { id=1, tytul="Lalka", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" },
				new KsiazkiKsiazka() { id=2, tytul="Wesele", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" },
				new KsiazkiKsiazka() { id=3, tytul="Placówka", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" },
				new KsiazkiKsiazka() { id=4, tytul="Inny świat", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" },
				new KsiazkiKsiazka() { id=5, tytul="Powrót z gwiazd", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" },
				new KsiazkiKsiazka() { id=6, tytul="Heban", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" },
				new KsiazkiKsiazka() { id=7, tytul="Faraon", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" },
				new KsiazkiKsiazka() { id=8, tytul="Ziemia obiecana", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" },
				new KsiazkiKsiazka() { id=9, tytul="Ogniem i mieczem", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" },
				new KsiazkiKsiazka() { id=10, tytul="Dzieła zebrane", cena=2.50f, idAutora="", idWydawnictwa="",ISBN="" }
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
	}
}
