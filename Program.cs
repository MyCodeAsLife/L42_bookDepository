using System;
using System.Collections.Generic;

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
                    Console.Clear();

                    switch (menuNumber)
                    {
                        case CommandAddBook:
                            depository.AddBook();
                            break;

                        case CommandRemoveBook:
                            depository.RemoveBook();
                            break;

                        case CommandFindBooks:
                            depository.SearchByParametrs();
                            break;

                        case CommandShowAllBooks:
                            depository.ShowAllBooks();
                            break;

                        case CommandExit:
                            isOpen = false;
                            continue;

                        default:
                            Error.Show();
                            break;
                    }
                }
                else
                {
                    Error.Show();
                }

                Console.WriteLine("\nДля для возврата в меню нажмите любую клавишу...");
                Console.ReadKey(true);
            }
        }
    }

    class Error
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("Вы ввели некорректные данные.");
        }
    }

    class Book
    {
        public Book(string name, string author, string yearIssue)
        {
            Name = name;
            Author = author;
            YearIssue = yearIssue;
        }

        public string Name { get; private set; }

        public string Author { get; private set; }

        public string YearIssue { get; private set; }

        public static Book CreateNew()
        {
            Console.Write("Введите имя книги: ");
            string bookName = Console.ReadLine();

            Console.Write("Введите автора книги: ");
            string author = Console.ReadLine();

            Console.Write("Введите год написания книги: ");
            string yearIssue = Console.ReadLine();

            return new Book(bookName, author, yearIssue);
        }
    }

    class Depository
    {
        private List<Book> _books = new List<Book>();

        public Depository()
        {
            Fill();
        }

        public int Count
        {
            get
            {
                return _books.Count;
            }
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

        public void AddBook()
        {
            Console.Write("Введите имя книги: ");
            string bookName = Console.ReadLine();

            Console.Write("Введите автора книги: ");
            string author = Console.ReadLine();

            Console.Write("Введите год написания книги: ");
            string yearIssue = Console.ReadLine();

            _books.Add(new Book(bookName, author, yearIssue));
        }

        public void RemoveBook()
        {
            Console.Write("Введите номер книги которую необходимо удалить: ");

            if (int.TryParse(Console.ReadLine(), out int numberBook))
            {
                numberBook--;

                if (numberBook >= 0 && numberBook < Count)
                    _books.RemoveAt(numberBook);
                else
                    Error.Show();
            }
        }

        public void SearchByParametrs()
        {
            Console.Write("Введите данные о книгах которые нужно найти\n(Автор, Название, Год выпуска): ");
            string userInput = Console.ReadLine();

            FindBooks(userInput);
        }

        private void FindBooks(string parameter)
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
                    string[] wordArray = _books[i].Author.Split();

                    foreach (string word in wordArray)
                        if (word.ToLower() == parameter.ToLower())
                        {
                            ShowBook(i);
                            isFind = true;
                        }
                }
            }

            if (isFind == false)
            {
                Console.Clear();
                Console.WriteLine("По указанному параметру не найдено ни одной книги.");
            }
        }

        private void Fill()
        {
            _books.Add(new Book("Двойник", "Достоевский Федр Михайлович", "1846"));
            _books.Add(new Book("Война и Мир", "Толстой Лев Николаевич", "1867"));
            _books.Add(new Book("Памятник", "Пушкин Александр Сергеевич", "1841"));
            _books.Add(new Book("Мцыри", "Лермонтов Михаил Юрьевич", "1840"));
        }
    }
}
