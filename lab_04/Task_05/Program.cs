using System;
using System.Collections.Generic;

// Клас, що представляє текстовий документ
class TextDocument
{
    public string Content { get; set; }

    public TextDocument(string content)
    {
        Content = content;
    }

    // Метод для створення мементо
    public TextDocumentMemento CreateMemento()
    {
        return new TextDocumentMemento(Content);
    }

    // Метод для відновлення збереженого стану
    public void RestoreMemento(TextDocumentMemento memento)
    {
        Content = memento.Content;
    }
}

// Клас, що представляє мементо для текстового документа
class TextDocumentMemento
{
    public string Content { get; }

    public TextDocumentMemento(string content)
    {
        Content = content;
    }
}

// Клас редактора тексту, який виконує операції з документом
class TextEditor
{
    private TextDocument document;
    private Stack<TextDocumentMemento> history;

    public TextEditor(string initialContent)
    {
        document = new TextDocument(initialContent);
        history = new Stack<TextDocumentMemento>();
    }

    // Метод для збереження стану документа
    public void Save()
    {
        history.Push(document.CreateMemento());
    }

    // Метод для відновлення попереднього стану документа
    public void Undo()
    {
        if (history.Count > 0)
        {
            var memento = history.Pop();
            document.RestoreMemento(memento);
        }
        else
        {
            Console.WriteLine("Nothing to undo.");
        }
    }

    // Метод для встановлення нового вмісту документа
    public void SetContent(string content)
    {
        document.Content = content;
    }

    // Метод для виведення поточного вмісту документа
    public void PrintContent()
    {
        Console.WriteLine("Current content:");
        Console.WriteLine(document.Content);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення текстового редактора з початковим вмістом
        TextEditor editor = new TextEditor("Hello, World!");

        // Збереження початкового стану
        editor.Save();

        // Внесення змін у документ
        editor.SetContent("Hello, Universe!");

        // Виведення поточного вмісту
        editor.PrintContent();

        // Скасування змін і відновлення попереднього стану
        editor.Undo();

        // Виведення вмісту після скасування
        editor.PrintContent();
    }
}

