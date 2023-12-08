using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L42_bookDepository
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int CommandAddBook = 1;
            const int CommandRemoveBook = 2;
            const int CommandFindBooks = 3;
            const int CommandShowAllBooks = 4;
            const int CommandExit = 5;

            Depository depository = new Depository();

            Book book1 = new Book("Двойник", "Достоевский Федр Михайлович", "1846");
            Book book2 = new Book("Война и Мир", "Толстой Лев Николаевич", "1867");
            Book book3 = new Book("Памятник", "Пушкин Александр Сергеевич", "1841");
            Book book4 = new Book("Мцыри", "Лермонтов Михаил Юрьевич", "1840");

            depository.AddBook(book1);
            depository.AddBook(book2);
            depository.AddBook(book3);
            depository.AddBook(book4);

            bool isOpen = true;

            while (isOpen)
            {
                int menuNumber;

                Console.Clear();
                Console.WriteLine($"Меню.\n{CommandAddBook} - Добавить книгу.\n{CommandRemoveBook} - Удалить книгу по индексу." +
                                  $"\n{CommandFindBooks} - Найти книги по параметрам(год, автор, название).\n{CommandShowAllBooks}" +
                                  $" - Показать все книги.\n{CommandExit} - Выход.");
                Console.Write("\nВыбирите пункт меню: ");

                if (int.TryParse(Console.ReadLine(), out menuNumber))
                {
                    switch (menuNumber)
                    {
                        case CommandAddBook:
                            depository.AddBook(CreateNewBook());
                            break;

                        case CommandRemoveBook:
                            DeleteByIndex(depository);
                            break;

                        case CommandFindBooks:
                            SearchByParametrs(depository);
                            break;

                        case CommandShowAllBooks:
                            depository.ShowAllBooks();
                            break;

                        case CommandExit:
                            isOpen = false;
                            break;

                        default:
                            ShowError();
                            break;
                    }

                    Console.ReadKey(true);
                }
                else
                {
                    ShowError();
                }
            }
        }

        static Book CreateNewBook()
        {
            Console.Clear();
            Console.Write("Введите имя книги: ");
            string bookName = Console.ReadLine();

            Console.Write("Введите автора книги: ");
            string author = Console.ReadLine();

            Console.Write("Введите год написания книги: ");
            string yearIssue = Console.ReadLine();

            return new Book(bookName, author, yearIssue);
        }

        static void DeleteByIndex(Depository depository)
        {
            Console.Clear();
            Console.Write("Введите номер книги которую необходимо удалить: ");
            int numberBook = GetUserInt() - 1;

            if (numberBook >= 0 && numberBook < depository.Count)
                depository.RemoveBook(numberBook);
            else
                ShowError();
        }

        static void SearchByParametrs(Depository depository)
        {
            Console.Clear();
            Console.Write("Введите данные о книгах которые нужно найти\n(Автор, Название, Год выпуска): ");
            string userInput = Console.ReadLine();

            depository.FindBooks(userInput);
        }

        static int GetUserInt()
        {
            int result;

            if (int.TryParse(Console.ReadLine(), out result))
                return result;
            else
                return -1;
        }

        static void ShowError()
        {
            Console.WriteLine("Вы ввели некорректные данные.\nДля продолжения нажмите любую клавишу...");
            Console.ReadKey(true);
        }

        class Book
        {
            private string _name;
            private string _author;
            private string _yearIssue;

            public Book(string name, string author, string yearIssue)
            {
                _name = name;
                _author = author;
                _yearIssue = yearIssue;
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public string Author
            {
                get
                {
                    return _author;
                }
            }

            public string YearIssue
            {
                get
                {
                    return _yearIssue;
                }
            }
        }

        class Depository
        {
            private List<Book> _books = new List<Book>();

            public int Count
            {
                get
                {
                    return _books.Count;
                }
            }

            public void AddBook(Book book)
            {
                _books.Add(book);
            }

            public void RemoveBook(int index)
            {
                _books.RemoveAt(index);
            }

            public void ShowBook(int index)
            {
                Console.WriteLine($"{index + 1} -\tНаименование: {_books[index].Name}\n\tАвтор: " +
                                  $"{_books[index].Author}\n\tГод написания: {_books[index].YearIssue}");
            }

            public void ShowAllBooks()
            {
                for (int i = 0; i < _books.Count; i++)
                    ShowBook(i);
            }

            public void FindBooks(string parameter)
            {
                bool isFind = false;

                for (int i = 0; i < _books.Count; i++)
                {
                    if (_books[i].Name.ToLower() == parameter.ToLower())
                    {
                        ShowBook(i);
                        isFind = true;
                    }
                    else if (_books[i].YearIssue.ToLower() == parameter.ToLower())
                    {
                        ShowBook(i);
                        isFind = true;
                    }
                    else
                    {
                        var array = _books[i].Author.Split();

                        foreach (var item in array)
                            if (item.ToLower() == parameter.ToLower())
                            {
                                ShowBook(i);
                                isFind = true;
                                break;
                            }
                    }
                }

                if (isFind == false)
                {
                    Console.WriteLine("По указанному параметру нинайде не одной книги.");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
