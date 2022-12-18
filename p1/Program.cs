using System;

namespace p1
{
    class Dispethcer
    {
        private string name;
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnNameChange(new NameChengeEventArgs(Name));
            }
        }

        public delegate void NameChangeEventHandler(object sender, NameChengeEventArgs args);

        public event NameChangeEventHandler? NameChange;

        public void OnNameChange(NameChengeEventArgs args)
        {
            NameChange?.Invoke(new object(), args);
        }
    }

    class Handler
    {
        public void OnDispatcherNameChange(object sender, NameChengeEventArgs args)
        {
            Console.WriteLine($"Dispatcher`s name changed to {args.Name}.");
        }
    }

    public class NameChengeEventArgs : EventArgs
    {
        public string Name { get; private set; } = "";

        public NameChengeEventArgs(string name)
        {
            Name = name;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Dispethcer dispethcer = new Dispethcer();
            Handler handler = new Handler();
            dispethcer.NameChange += handler.OnDispatcherNameChange;

            bool end = false;
            while (!end)
            {
                string input = Console.ReadLine();
                if (input != null)
                {
                    dispethcer.Name = input;
                }
                else if (input == "END")
                {
                    end = true;
                }
            }
        }
    }
}
