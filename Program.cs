using System.Text.Json;

bool applicationStatus = true;
var library = new LibraryArchive();
string filePath = "libraryArchive.json";
if (!File.Exists("libraryArchive.json"))
{
  File.WriteAllText("libraryArchive.json", "[]");
}

App.MainMenu(applicationStatus, filePath, library.Books);


System.Console.WriteLine("Application has closed, please press any button to exit.");
Console.ReadKey();

public class App
{
  public static void MainMenu(bool applicationStatus, string filePath, List<Book> library)
  {
    do
    {
    System.Console.WriteLine("What would you like to do?");
    System.Console.WriteLine("Press [A] to add to your library");
    System.Console.WriteLine("Press [R] to read your library");
    System.Console.WriteLine("Press [D] to delete from your library");
    System.Console.WriteLine("Press [S] to save library");
    var userInput = Console.ReadLine();
    char userInputCharacter = char.Parse(userInput);

      switch (userInputCharacter)
      {
        case 'A':
        case 'a':
          AddToLibrary.Add(filePath, library);
          break;
        case 'R':
        case 'r':
          ReadLibrary.Read(filePath, library);
          break;
        case 'D':
        case 'd':
          DeletionFromLibrary.DeleteFromLibrary(filePath, library);
          break;
        case 'S':
        case 's':
          WriteToJsonLibrary.Write(filePath, library);
          break;
        default:
          {
            applicationStatus = false;
          }
          break;
      }
    } while (applicationStatus = true);
  }
}

public class LibraryArchive
{
  public List<Book> Books { get; } = new List<Book>();
}

public class Book
{
  public string Title { get; }
  public string Author { get; }
  public int ReleaseYear { get; }
  public decimal Rating { get; }

  public Book(string title, string author, int releaseYear, decimal rating)
  {
    Title = title;
    Author = author;
    ReleaseYear = releaseYear;
    Rating = rating;
  }

  public override string ToString() => $"{Title} written by {Author}\r\n{ReleaseYear} {Rating}/5";
}

public static class ReadLibrary
{
  public static List<Book> Read(string filePath, List<Book> library)
  {
    List<Book> updatedLibrary = library;
    var fileContents = File.ReadAllText(filePath);
    List<Book> jsonLibrary = JsonSerializer.Deserialize<List<Book>>(fileContents);
    if (jsonLibrary.Count > 0)
    {
      foreach (Book book in jsonLibrary)
      {
        System.Console.WriteLine(book);
      }
    }
    else
    {
      System.Console.WriteLine("Your library is empty homie🕸️");
      System.Console.WriteLine();
    }
    return updatedLibrary;
  }
}

public static class AddToLibrary
{
  public static void Add(string filePath, List<Book> library)
  {
    System.Console.WriteLine("What is the title of the book you would like to add?📘");
    string title = Console.ReadLine();
    System.Console.WriteLine("Who was the author?✍️");
    string author = Console.ReadLine();
    System.Console.WriteLine("What year was it released?⌛");
    int releaseYear = int.Parse(Console.ReadLine());
    System.Console.WriteLine("What would you rate it 1 - 5?⭐");
    decimal rating = decimal.Parse(Console.ReadLine());

    Book newBook = new Book(title, author, releaseYear, rating);
    library.Add(newBook);
    System.Console.WriteLine($"{newBook.Title} has been added to your library 😄");

  }
}

public static class DeletionFromLibrary
{
  public static List<Book> DeleteFromLibrary(string filePath, List<Book> library)
  {
    System.Console.WriteLine("Which book would you like to remove from your library?");
    ReadLibrary.Read(filePath, library);
    System.Console.WriteLine();
    var userInput = Console.ReadLine();

    foreach (Book book in library)
    {
      if (userInput == book.Title)
      {
        library.Remove(book);
      }
    }
    return library;
  }
}

public static class WriteToJsonLibrary
{

  public static void Write(string filePath, List<Book> library)
  {
    var serializedLibrary = JsonSerializer.Serialize(library);
    File.WriteAllText(filePath, serializedLibrary);
  }

}

public static class ReadFromJsonLibrary
{
  public static void Read(string filePath)
  {
    var library = JsonSerializer.Deserialize<List<Book>>(filePath);
    foreach (Book book in library)
    {
      System.Console.WriteLine(book);
    }
  }
}