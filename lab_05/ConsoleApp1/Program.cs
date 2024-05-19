using System;
using System.Collections;
using System.Collections.Generic;

namespace LightHTML
{
    // Абстрактний клас LightNode
    abstract class LightNode
    {
        public abstract void Accept(IVisitor visitor);
    }

    // Клас LightTextNode, що представляє текстовий вузол
    class LightTextNode : LightNode
    {
        public string Text { get; set; }

        public LightTextNode(string text)
        {
            Text = text;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // Клас LightElementNode, що представляє елемент вузол
    class LightElementNode : LightNode, IEnumerable<LightNode>
    {
        public string TagName { get; }
        public bool IsBlockElement { get; }
        public bool IsSelfClosing { get; }
        public List<string> CssClasses { get; }
        private List<LightNode> children;

        public LightElementNode(string tagName, bool isBlockElement, bool isSelfClosing)
        {
            TagName = tagName;
            IsBlockElement = isBlockElement;
            IsSelfClosing = isSelfClosing;
            CssClasses = new List<string>();
            children = new List<LightNode>();
        }

        public void AddChild(LightNode child)
        {
            children.Add(child);
        }

        public IEnumerator<LightNode> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var child in children)
            {
                child.Accept(visitor);
            }
        }
    }

    // Інтерфейс IVisitor для шаблону Відвідувач
    interface IVisitor
    {
        void Visit(LightTextNode textNode);
        void Visit(LightElementNode elementNode);
    }

    // Ітератор для перебору вузлів у глибину
    class DepthFirstIterator : IEnumerator<LightNode>
    {
        private Stack<IEnumerator<LightNode>> stack = new Stack<IEnumerator<LightNode>>();

        public DepthFirstIterator(LightNode root)
        {
            stack.Push(new SingleElementEnumerator(root));
        }

        public LightNode Current
        {
            get
            {
                return stack.Peek().Current;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (stack.Count == 0)
                return false;

            var enumerator = stack.Peek();
            if (!enumerator.MoveNext())
            {
                stack.Pop();
                return MoveNext();
            }

            if (Current is LightElementNode elementNode)
            {
                stack.Push(elementNode.GetEnumerator());
            }

            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    // Ітератор для перебору вузлів у ширину
    class BreadthFirstIterator : IEnumerator<LightNode>
    {
        private Queue<IEnumerator<LightNode>> queue = new Queue<IEnumerator<LightNode>>();

        public BreadthFirstIterator(LightNode root)
        {
            queue.Enqueue(new SingleElementEnumerator(root));
        }

        public LightNode Current
        {
            get
            {
                return queue.Peek().Current;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (queue.Count == 0)
                return false;

            var enumerator = queue.Peek();
            if (!enumerator.MoveNext())
            {
                queue.Dequeue();
                return MoveNext();
            }

            if (Current is LightElementNode elementNode)
            {
                queue.Enqueue(elementNode.GetEnumerator());
            }

            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    // Клас для одного елемента
    class SingleElementEnumerator : IEnumerator<LightNode>
    {
        private LightNode node;
        private bool isFirst = true;

        public SingleElementEnumerator(LightNode node)
        {
            this.node = node;
        }

        public LightNode Current => node;

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (isFirst)
            {
                isFirst = false;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            isFirst = true;
        }
    }

    // Інтерфейс команди
    interface ICommand
    {
        void Execute();
    }

    // Клас елементів HTML з підтримкою команди
    class LightElementNodeWithCommand : LightElementNode
    {
        private Dictionary<string, List<ICommand>> eventListeners = new Dictionary<string, List<ICommand>>();

        public LightElementNodeWithCommand(string tagName, bool isBlockElement, bool isSelfClosing)
            : base(tagName, isBlockElement, isSelfClosing) { }

        public void AddEventListener(string eventType, ICommand command)
        {
            if (!eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType] = new List<ICommand>();
            }
            eventListeners[eventType].Add(command);
        }

        public void TriggerEvent(string eventType)
        {
            if (eventListeners.ContainsKey(eventType))
            {
                foreach (var command in eventListeners[eventType])
                {
                    command.Execute();
                }
            }
        }
    }

    // Приклад команди для обробки події кліку
    class ClickCommand : ICommand
    {
        private string message;

        public ClickCommand(string message)
        {
            this.message = message;
        }

        public void Execute()
        {
            Console.WriteLine(message);
        }
    }

    // Інтерфейс стану
    interface IState
    {
        void Handle(LightElementNodeWithState element);
    }

    // Клас елементів HTML з підтримкою стану
    class LightElementNodeWithState : LightElementNode
    {
        public IState CurrentState { get; set; }

        public LightElementNodeWithState(string tagName, bool isBlockElement, bool isSelfClosing)
            : base(tagName, isBlockElement, isSelfClosing) { }

        public void Request()
        {
            CurrentState.Handle(this);
        }
    }

    // Реалізація конкретного стану
    class DefaultState : IState
    {
        public void Handle(LightElementNodeWithState element)
        {
            Console.WriteLine($"Element {element.TagName} is in default state.");
        }
    }

    class HoverState : IState
    {
        public void Handle(LightElementNodeWithState element)
        {
            Console.WriteLine($"Element {element.TagName} is in hover state.");
        }
    }

    // Клас елементів HTML з шаблонним методом
    abstract class LightElementNodeWithHooks : LightElementNode
    {
        protected LightElementNodeWithHooks(string tagName, bool isBlockElement, bool isSelfClosing)
            : base(tagName, isBlockElement, isSelfClosing) { }

        public void Render()
        {
            OnCreated();
            OnInserted();
            OnStylesApplied();
            OnClassListApplied();
            OnTextRendered();
        }

        protected virtual void OnCreated() { }
        protected virtual void OnInserted() { }
        protected virtual void OnStylesApplied() { }
        protected virtual void OnClassListApplied() { }
        protected virtual void OnTextRendered() { }
    }

    // Конкретний елемент з хуками життєвого циклу
    class LightParagraphWithHooks : LightElementNodeWithHooks
    {
        public LightParagraphWithHooks()
            : base("p", true, false) { }

        protected override void OnCreated()
        {
            Console.WriteLine("Paragraph created.");
        }

        protected override void OnInserted()
        {
            Console.WriteLine("Paragraph inserted.");
        }

        protected override void OnStylesApplied()
        {
            Console.WriteLine("Paragraph styles applied.");
        }

        protected override void OnClassListApplied()
        {
            Console.WriteLine("Paragraph class list applied.");
        }

        protected override void OnTextRendered()
        {
            Console.WriteLine("Paragraph text rendered.");
        }
    }

    // Реалізація відвідувача для друку елементів
    class PrintVisitor : IVisitor
    {
        public void Visit(LightTextNode textNode)
        {
            Console.WriteLine($"Text: {textNode.Text}");
        }

        public void Visit(LightElementNode elementNode)
        {
            Console.WriteLine($"Element: <{elementNode.TagName}>");
        }
    }

    // Головна програма для демонстрації роботи
    class Program
    {
        static void Main(string[] args)
        {
            // Створення елементів
            var body = new LightElementNode("body", true, false);
            var div = new LightElementNode("div", true, false);
            var p = new LightParagraphWithHooks();
            var text = new LightTextNode("Hello, World!");

            // Додавання дочірніх елементів
            body.AddChild(div);
            div.AddChild(p);
            p.AddChild(text);

            // Демонстрація шаблонного методу
            p.Render();

            // Демонстрація ітератора
            Console.WriteLine("Depth-first iteration:");
            var depthIterator = new DepthFirstIterator(body);
            while (depthIterator.MoveNext())
            {
                var node = depthIterator.Current;
                if (node is LightElementNode element)
                {
                    Console.WriteLine($"Element: <{element.TagName}>");
                }
                else if (node is LightTextNode textNode)
                {
                    Console.WriteLine($"Text: {textNode.Text}");
                }
            }

            Console.WriteLine("Breadth-first iteration:");
            var breadthIterator = new BreadthFirstIterator(body);
            while (breadthIterator.MoveNext())
            {
                var node = breadthIterator.Current;
                if (node is LightElementNode element)
                {
                    Console.WriteLine($"Element: <{element.TagName}>");
                }
                else if (node is LightTextNode textNode)
                {
                    Console.WriteLine($"Text: {textNode.Text}");
                }
            }

            // Демонстрація шаблону Команда
            var button = new LightElementNodeWithCommand("button", false, false);
            button.AddEventListener("click", new ClickCommand("Button clicked!"));
            button.TriggerEvent("click");

            // Демонстрація шаблону Стейт
            var hoverState = new HoverState();
            var defaultState = new DefaultState();

            var elementWithState = new LightElementNodeWithState("div", true, false);
            elementWithState.CurrentState = defaultState;
            elementWithState.Request();

            elementWithState.CurrentState = hoverState;
            elementWithState.Request();

            // Демонстрація шаблону Відвідувач
            var visitor = new PrintVisitor();
            body.Accept(visitor);
        }
    }
}
