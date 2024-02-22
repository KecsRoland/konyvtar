using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace konyvtar_patkanyok
{
    // A könyvtár alkalmazás
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Könyvtár létrehozása
                Library library = new Library();
                bool loggedIn = false;
                bool isLibrarian = false;

                // Bejelentkezési ciklus
                while (!loggedIn)
                {
                    Console.WriteLine("1. Könyvtáros bejelentkezés");
                    Console.WriteLine("2. Olvasó bejelentkezés / regisztráció");
                    Console.WriteLine("3. Kilépés");

                    // Felhasználói választás fogadása
                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Érvénytelen bemenet. Kérem, adjon meg egy számot.");
                        Console.WriteLine("Nyomjon meg egy billentyűt a folytatáshoz...");
                        Console.ReadKey(true);
                        Console.Clear();
                        continue; // Ciklus újrakezdése
                    }

                    switch (choice)
                    {
                        case 1: // Könyvtáros bejelentkezés
                            Console.Clear();
                            Console.Write("Adja meg a könyvtáros felhasználónevét: ");
                            string librarianUsername = Console.ReadLine();
                            Console.Write("Adja meg a jelszót: ");
                            string librarianPassword = Console.ReadLine();
                            loggedIn = library.LibrarianLogin(librarianUsername, librarianPassword);
                            isLibrarian = loggedIn;
                            break;
                        case 2: // Olvasó bejelentkezés / regisztráció
                            Console.Clear();
                            Console.WriteLine("1. Bejelentkezés");
                            Console.WriteLine("2. Regisztráció");
                            if (!int.TryParse(Console.ReadLine(), out int loginOrRegisterChoice))
                            {
                                Console.WriteLine("Érvénytelen bemenet. Kérem, adjon meg egy számot.");
                                continue; // Ciklus újrakezdése
                            }

                            if (loginOrRegisterChoice == 1)
                            {
                                Console.Clear();
                                Console.Write("Adja meg az olvasó felhasználónevét: ");
                                string readerUsername = Console.ReadLine();
                                Console.Write("Adja meg a jelszót: ");
                                string readerPassword = Console.ReadLine();
                                loggedIn = library.ReaderLogin(readerUsername, readerPassword);
                            }
                            else if (loginOrRegisterChoice == 2)
                            {
                                Console.Clear();
                                Console.Write("Adja meg az új olvasó felhasználónevét: ");
                                string newReaderUsername = Console.ReadLine();
                                Console.Write("Adja meg a jelszót: ");
                                string newReaderPassword = Console.ReadLine();
                                library.RegisterReader(newReaderUsername, newReaderPassword);
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Érvénytelen választás.");
                            }
                            break;
                        case 3: // Kilépés
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Érvénytelen választás. Kérem, próbálja újra.");
                            break;
                    }
                }

                // Bejelentkezett felhasználó műveletei
                while (loggedIn)
                {
                    Console.Clear();
                    Console.WriteLine("1. Összes könyv megjelenítése");
                    Console.WriteLine("2. Könyv keresése");
                    Console.WriteLine("3. Könyv kölcsönzése");
                    Console.WriteLine("4. Könyv visszahozása");
                    if (isLibrarian)
                    {
                        Console.WriteLine("5. Könyvek hozzáadása");
                        Console.WriteLine("6. Könyvek adatainak szerkesztése");
                        Console.WriteLine("7. Könyvek törlése");
                        Console.WriteLine("8. Kijelentkezés");
                    }
                    else
                    {
                        Console.WriteLine("5. Kijelentkezés");
                    }

                    // Felhasználói választás fogadása
                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Érvénytelen bemenet. Kérem, adjon meg egy számot.");
                        continue; // Ciklus újrakezdése
                    }

                    switch (choice)
                    {
                        case 1: // Összes könyv megjelenítése
                            Console.Clear();
                            library.DisplayAllBooks();
                            Console.WriteLine("Opció 1 kiválasztva: Összes könyv megjelenítése");
                            break;
                        case 2: // Könyv keresése
                            Console.Clear();
                            Console.Write("Adja meg a keresési kulcsszót: ");
                            string keyword = Console.ReadLine();
                            library.SearchBooks(keyword);
                            break;
                        case 3: // Könyv kölcsönzése
                            Console.Clear();
                            library.DisplayAvailableBooks();
                            Console.Write("Adja meg a kölcsönözni kívánt könyv azonosítóját: ");
                            if (!int.TryParse(Console.ReadLine(), out int bookIdToBorrow))
                            {
                                Console.WriteLine("Érvénytelen bemenet. Kérem, adjon meg egy számot.");
                                continue; // Ciklus újrakezdése
                            }
                            library.BorrowBook(bookIdToBorrow);
                            break;
                        case 4: // Könyv visszahozása
                            Console.Clear();
                            library.DisplayBorrowedBooks();
                            Console.Write("Adja meg a visszahozni kívánt könyv azonosítóját: ");
                            if (!int.TryParse(Console.ReadLine(), out int bookIdToReturn))
                            {
                                Console.WriteLine("Érvénytelen bemenet. Kérem, adjon meg egy számot.");
                                continue; // Ciklus újrakezdése
                            }
                            library.ReturnBook(bookIdToReturn);
                            break;
                        case 5: // Könyvek hozzáadása vagy kijelentkezés
                            if (isLibrarian)
                            {
                                Console.Clear();
                                library.AddNewBook();
                                Console.WriteLine("Opció 5 kiválasztva: Könyvek hozzáadása");
                            }
                            else
                            {
                                Console.Clear();
                                loggedIn = false;
                                Console.WriteLine("Opció 5 kiválasztva: Kijelentkezés");
                            }
                            break;
                        case 6: // Könyvek adatainak szerkesztése
                            if (isLibrarian)
                            {
                                Console.Clear();
                                library.DisplayAllBooks();
                                library.EditBookData();
                                Console.WriteLine("Opció 6 kiválasztva: Könyvek adatainak szerkesztése");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Érvénytelen választás. Kérem, próbálja újra.");
                            }
                            break;
                        case 7: // Könyvek törlése
                            if (isLibrarian)
                            {
                                Console.Clear();
                                library.DisplayAllBooks();
                                library.DeleteBook();
                                Console.WriteLine("Opció 7 kiválasztva: Könyvek törlése");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Érvénytelen választás. Kérem, próbálja újra.");
                            }
                            break;
                        case 8: // Kijelentkezés
                            Console.Clear();
                            loggedIn = false;
                            Console.WriteLine("Opció 8 kiválasztva: Kijelentkezés");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Érvénytelen választás. Kérem, próbálja újra.");
                            break;
                    }

                    Console.WriteLine("Nyomjon meg egy billentyűt a folytatáshoz...");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }
    }

    // Könyv osztály
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool IsBorrowed { get; set; }

        // Könyv szerializálása
        public string Serialize()
        {
            return $"{Id},{Title},{Author},{Year},{IsBorrowed}";
        }

        // Könyv deszerializálása
        public static Book Deserialize(string data)
        {
            string[] parts = data.Split(',');
            return new Book
            {
                Id = int.Parse(parts[0]),
                Title = parts[1],
                Author = parts[2],
                Year = int.Parse(parts[3]),
                IsBorrowed = bool.Parse(parts[4])
            };
        }
    }

    // Könyvtár osztály
    class Library
    {
        private List<Book> books;
        private const string booksFilePath = "C:\\Users\\MSI GS63 Stealth 8RE\\Desktop\\11\\ikt\\mentes\\konyvek.txt";
        private const string librarianCredentialsFilePath = "C:\\Users\\MSI GS63 Stealth 8RE\\Desktop\\11\\ikt\\mentes\\konyvtaros_adatok.txt";
        private const string readerCredentialsFilePath = "C:\\Users\\MSI GS63 Stealth 8RE\\Desktop\\11\\ikt\\mentes\\olvaso_adatok.txt";

        // Könyvtár konstruktora
        public Library()
        {
            CheckFilesExistence();
            books = LoadBooks();
        }

        // Fájlok létezésének ellenőrzése
        private void CheckFilesExistence()
        {
            if (!File.Exists(booksFilePath))
            {
                File.Create(booksFilePath).Close();
            }

            if (!File.Exists(librarianCredentialsFilePath))
            {
                File.WriteAllText(librarianCredentialsFilePath, "admin,admin");
            }

            if (!File.Exists(readerCredentialsFilePath))
            {
                File.Create(readerCredentialsFilePath).Close();
            }
        }

        // Könyvek betöltése fájlból
        private List<Book> LoadBooks()
        {
            List<Book> loadedBooks = new List<Book>();
            if (File.Exists(booksFilePath))
            {
                string[] lines = File.ReadAllLines(booksFilePath);
                foreach (string line in lines)
                {
                    loadedBooks.Add(Book.Deserialize(line));
                }
            }
            return loadedBooks;
        }

        // Könyvek mentése fájlba
        private void SaveBooks()
        {
            List<string> lines = new List<string>();
            foreach (var book in books)
            {
                lines.Add(book.Serialize());
            }
            File.WriteAllLines(booksFilePath, lines);
        }

        // Könyvtáros bejelentkezése
        public bool LibrarianLogin(string username, string password)
        {
            string[] credentials = File.ReadAllLines(librarianCredentialsFilePath);
            if (credentials.Length > 0)
            {
                string[] parts = credentials[0].Split(',');
                if (parts.Length == 2 && parts[0] == username && parts[1] == password)
                {
                    Console.WriteLine("Könyvtáros bejelentkezés sikeres.");
                    Thread.Sleep(1000);
                    return true;
                }
            }

            Console.WriteLine("Érvénytelen felhasználónév vagy jelszó a könyvtáros számára.");
            Console.WriteLine("Nyomjon meg egy billentyűt a folytatáshoz...");
            Console.ReadKey(true);
            Console.Clear();
            return false;
        }

        // Új könyv hozzáadása
        public void AddNewBook()
        {
            Console.WriteLine("Adja meg a könyv részleteit. Nyomja meg az 'esc' gombot a megszakításhoz.");

            string title = "";
            string author = "";
            int year = 0;

            Console.Write("Adja meg a könyv címét: ");
            ConsoleKeyInfo keyTitle;
            do
            {
                keyTitle = Console.ReadKey(true);
                if (keyTitle.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\nMűvelet megszakítva.");
                    return;
                }
                else if (keyTitle.Key == ConsoleKey.Backspace && title.Length > 0)
                {
                    title = title.Substring(0, title.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyTitle.KeyChar))
                {
                    title += keyTitle.KeyChar;
                    Console.Write(keyTitle.KeyChar);
                }
            } while (keyTitle.Key != ConsoleKey.Enter);
            Console.WriteLine();

            Console.Write("Adja meg a könyv szerzőjét: ");
            ConsoleKeyInfo keyAuthor;
            do
            {
                keyAuthor = Console.ReadKey(true);
                if (keyAuthor.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\nMűvelet megszakítva.");
                    return;
                }
                else if (keyAuthor.Key == ConsoleKey.Backspace && author.Length > 0)
                {
                    author = author.Substring(0, author.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyAuthor.KeyChar))
                {
                    author += keyAuthor.KeyChar;
                    Console.Write(keyAuthor.KeyChar);
                }
            } while (keyAuthor.Key != ConsoleKey.Enter);
            Console.WriteLine();

            Console.Write("Adja meg a kiadás évszámát: ");
            ConsoleKeyInfo keyYear;
            string yearInput = "";
            do
            {
                keyYear = Console.ReadKey(true);
                if (keyYear.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\nMűvelet megszakítva.");
                    return;
                }
                else if (keyYear.Key == ConsoleKey.Backspace && yearInput.Length > 0)
                {
                    yearInput = yearInput.Substring(0, yearInput.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyYear.KeyChar))
                {
                    yearInput += keyYear.KeyChar;
                    Console.Write(keyYear.KeyChar);
                }
            } while (keyYear.Key != ConsoleKey.Enter);
            Console.WriteLine();

            if (!int.TryParse(yearInput, out year))
            {
                Console.WriteLine("Érvénytelen évszám formátum. Művelet megszakítva.");
                return;
            }

            // Generáljon egy új azonosítót a könyvhöz
            int newBookId = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;

            // Létrehozza az új könyvet
            Book newBook = new Book
            {
                Id = newBookId,
                Title = title,
                Author = author,
                Year = year,
                IsBorrowed = false // Alapértelmezés szerint a könyv nincs kikölcsönözve
            };

            // Adja hozzá az új könyvet a listához
            books.Add(newBook);
            SaveBooks();
            Console.WriteLine("Az új könyv sikeresen hozzáadva.");
        }

        // Könyv adatainak szerkesztése
        public void EditBookData()
        {
            Console.Write("Adja meg a szerkeszteni kívánt könyv azonosítóját: ");
            string bookIdToEdit = Console.ReadLine();
            if (string.IsNullOrEmpty(bookIdToEdit))
            {
                Console.WriteLine("Nincs bemenet megadva. Művelet megszakítva.");
            }
            else
            {
                if (!int.TryParse(bookIdToEdit, out int bookIdToEditInt))
                {
                    Console.WriteLine("Érvénytelen bemenet. Kérem, adjon meg egy számot.");
                    return;
                }
                var bookToEdit = books.FirstOrDefault(b => b.Id == bookIdToEditInt);
                if (bookToEdit != null)
                {
                    Console.WriteLine($"A(z) {bookToEdit.Id} azonosítójú könyv szerkesztése");
                    Console.Write("Adja meg az új címet (vagy nyomja meg az Enter-t a meglévő cím megtartásához): ");
                    string newTitle = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newTitle))
                    {
                        bookToEdit.Title = newTitle;
                    }
                    Console.Write("Adja meg az új szerzőt (vagy nyomja meg az Enter-t a meglévő szerző megtartásához): ");
                    string newAuthor = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newAuthor))
                    {
                        bookToEdit.Author = newAuthor;
                    }
                    Console.Write("Adja meg az új kiadás évszámát (vagy nyomja meg az Enter-t a meglévő évszám megtartásához): ");
                    string newYearInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newYearInput))
                    {
                        if (int.TryParse(newYearInput, out int newYear))
                        {
                            bookToEdit.Year = newYear;
                        }
                        else
                        {
                            Console.WriteLine("Érvénytelen évszám formátum. Az évszám nem frissült.");
                        }
                    }
                    SaveBooks();
                    Console.WriteLine("A könyv adatai sikeresen frissítve.");
                }
                else
                {
                    Console.WriteLine("A könyv nem található.");
                }
            }
        }

        // Könyv törlése
        public void DeleteBook()
        {
            Console.Write("Adja meg a törölni kívánt könyv azonosítóját: ");
            string deleteInput = Console.ReadLine();
            if (deleteInput == "")
            {
                Console.WriteLine("Nincs bemenet megadva. Művelet megszakítva.");
            }
            else
            {
                if (!int.TryParse(deleteInput, out int bookIdToDelete))
                {
                    Console.WriteLine("Érvénytelen bemenet. Kérem, adjon meg egy számot.");
                    return;
                }
                var bookToDelete = books.FirstOrDefault(b => b.Id == bookIdToDelete);
                if (bookToDelete != null)
                {
                    books.Remove(bookToDelete);
                    SaveBooks();
                    Console.WriteLine("A könyv sikeresen törölve.");
                }
                else
                {
                    Console.WriteLine("A könyv nem található.");
                }
            }
        }

        // Olvasó bejelentkezése
        public bool ReaderLogin(string username, string password)
        {
            string[] lines = File.ReadAllLines(readerCredentialsFilePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == username && parts[1] == password)
                {
                    Console.WriteLine("Olvasó bejelentkezés sikeres.");
                    Thread.Sleep(1000);
                    return true;
                }
            }
            Console.WriteLine("Érvénytelen felhasználónév vagy jelszó az olvasó számára.");

            Console.WriteLine("Nyomjon meg egy billentyűt a folytatáshoz...");
            Console.ReadKey(true);
            Console.Clear();
            return false;
        }

        // Olvasó regisztrálása
        public void RegisterReader(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("A felhasználónév és a jelszó nem lehet üres.");
                return;
            }

            string[] lines = File.ReadAllLines(readerCredentialsFilePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == username)
                {
                    Console.WriteLine("A felhasználónév már foglalt.");
                    return;
                }
            }

            File.AppendAllText(readerCredentialsFilePath, $"{username},{password}\n");
            Console.WriteLine("Olvasó sikeresen regisztrálva.");
        }

        // Összes könyv megjelenítése
        public void DisplayAllBooks()
        {
            Console.WriteLine("Összes könyv:");
            foreach (var book in books)
            {
                Console.WriteLine($"Azonosító: {book.Id}, Cím: {book.Title}, Szerző: {book.Author}, Kiadás éve: {book.Year}, Kölcsönözhető: {(book.IsBorrowed ? "nem" : "igen")}");
            }
        }

        // Keresés könyvek között
        public void SearchBooks(string keyword)
        {
            List<Book> matchingBooks = books.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) || b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchingBooks.Count > 0)
            {
                Console.WriteLine($"A(z) '{keyword}' keresési eredményei:");
                foreach (var book in matchingBooks)
                {
                    Console.WriteLine($"Azonosító: {book.Id}, Cím: {book.Title}, Szerző: {book.Author}, Kiadás éve: {book.Year}, Kölcsönözhető: {(book.IsBorrowed ? "nem" : "igen")}");
                }
            }
            else
            {
                Console.WriteLine($"Nincs találat a(z) '{keyword}' keresésre.");
            }
        }

        // Kölcsönözhető könyvek megjelenítése
        public void DisplayAvailableBooks()
        {
            List<Book> availableBooks = books.Where(b => !b.IsBorrowed).ToList();
            if (availableBooks.Count > 0)
            {
                Console.WriteLine("Kölcsönözhető könyvek:");
                foreach (var book in availableBooks)
                {
                    Console.WriteLine($"Azonosító: {book.Id}, Cím: {book.Title}, Szerző: {book.Author}, Kiadás éve: {book.Year}");
                }
            }
            else
            {
                Console.WriteLine("Nincs elérhető könyv a kölcsönzéshez.");
            }
        }

        // Kölcsönzött könyvek megjelenítése
        public void DisplayBorrowedBooks()
        {
            List<Book> borrowedBooks = books.Where(b => b.IsBorrowed).ToList();
            if (borrowedBooks.Count > 0)
            {
                Console.WriteLine("Kölcsönzött könyvek:");
                foreach (var book in borrowedBooks)
                {
                    Console.WriteLine($"Azonosító: {book.Id}, Cím: {book.Title}, Szerző: {book.Author}, Kiadás éve: {book.Year}");
                }
            }
            else
            {
                Console.WriteLine("Nincs kölcsönzött könyv.");
            }
        }

        // Könyv kölcsönzése
        public void BorrowBook(int bookId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                if (book.IsBorrowed)
                {
                    Console.WriteLine("A kiválasztott könyv már kölcsönözve van.");
                }
                else
                {
                    book.IsBorrowed = true;
                    SaveBooks();
                    Console.WriteLine("A könyv sikeresen kölcsönözve.");
                }
            }
            else
            {
                Console.WriteLine("Nincs ilyen azonosítójú könyv.");
            }
        }

        // Könyv visszahozása
        public void ReturnBook(int bookId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                if (!book.IsBorrowed)
                {
                    Console.WriteLine("A kiválasztott könyv nincs kölcsönözve.");
                }
                else
                {
                    book.IsBorrowed = false;
                    SaveBooks();
                    Console.WriteLine("A könyv sikeresen visszahozva.");
                }
            }
            else
            {
                Console.WriteLine("Nincs ilyen azonosítójú könyv.");
            }
        }
    }
}
