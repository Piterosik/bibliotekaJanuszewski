using bibModelJanuszewski;
using bibModelJanuszewski.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace bibKliJanuszewski
{
    public class BDLibraryUWP
    {
        private StorageFolder pathUWP = KnownFolders.DocumentsLibrary;
        private readonly string authorsFile = "__ukw\\" + DefaultFileNames.defAuthors + ".xml";
        private readonly string publishersFile = "__ukw\\" + DefaultFileNames.defPublishers + ".xml";
        private readonly string booksFile = "__ukw\\" + DefaultFileNames.defBooks + ".xml";

        public enum fileNames
        {
            AUTHORS, PUBLISHERS, BOOKS,
        }

        public List<AuthorsAuthor> authorsAuthorList;
        public List<PublishersPublisher> publishersPublisherList;
        public List<BooksBook> booksBookList;

        public async void TestData()
        {
            StorageFile authors = (StorageFile) await pathUWP.TryGetItemAsync(authorsFile);
            if (authors == null)
            {
                ContentDialog contentDialog = new ContentDialog()
                {
                    Title = "Błąd",
                    Content = "Brak pliku z danymi autorów",
                    CloseButtonText = "Ok"
                };
                await contentDialog.ShowAsync();

                App.Current.Exit();
            }

            authorsAuthorList = Deserialize<Authors>(authors).Author.ToList();

            StorageFile publishers = (StorageFile) await pathUWP.TryGetItemAsync(publishersFile);
            if (publishers == null)
            {
                ContentDialog contentDialog = new ContentDialog()
                {
                    Title = "Błąd",
                    Content = "Brak pliku z danymi wydawcami",
                    CloseButtonText = "Ok"
                };
                await contentDialog.ShowAsync();

                App.Current.Exit();
            }

            publishersPublisherList = Deserialize<Publishers>(publishers).Publisher.ToList();

            StorageFile books = (StorageFile) await pathUWP.TryGetItemAsync(booksFile);
            if (books == null)
            {
                ContentDialog contentDialog = new ContentDialog()
                {
                    Title = "Błąd",
                    Content = "Brak pliku z danymi ksiązkami",
                    CloseButtonText = "Ok"
                };
                await contentDialog.ShowAsync();

                App.Current.Exit();
            }

            booksBookList = Deserialize<Books>(books).Book.ToList();
        }

        private T Deserialize<T>(StorageFile file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));

            try
            {
                using (Stream reader = file.OpenStreamForReadAsync().Result)
                {
                    return (T) xs.Deserialize(reader);
                }
            }
            catch
            {
                return default;
            }
        }

        public async void Serialize<T>(T obj, fileNames fileName)
        {
            StorageFile file = await pathUWP.CreateFileAsync(GetFilePath(fileName), CreationCollisionOption.ReplaceExisting);

            XmlSerializer xs = new XmlSerializer(typeof(T));
            try
            {
                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    xs.Serialize(stream, obj);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        private string GetFilePath(fileNames fileName)
        {
            switch (fileName)
            {
                case fileNames.AUTHORS:
                    return authorsFile;
                case fileNames.BOOKS:
                    return booksFile;
                case fileNames.PUBLISHERS:
                    return publishersFile;
                default:
                    throw new ArgumentException("Invalid fileName");
            }
        }

    }
}
