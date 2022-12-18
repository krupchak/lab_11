using System;
using System.Collections.Generic;

namespace p2
{
    class King
    {
        public string name = "No name"; 

        public delegate void ReactionDel(); 
        public event ReactionDel reaction; 

        List<RoyalGuard> guards = new List<RoyalGuard>(); 
        List<Footman> footmen = new List<Footman>(); 


        public King(string name) 
        {
            this.name = name; 
            reaction = delegate () { Console.WriteLine($"King {this.name} is under attack!"); }; 
        }


        public void Add(RoyalGuard? royalGuard = null, Footman? footman = null) 
        {
            if (royalGuard != null) 
            {
                guards.Add(royalGuard); 
                reaction += royalGuard.Reaction; 
            }
            else if (footman != null) 
            {
                footmen.Add(footman);
                reaction += footman.Reaction; 
            }
        }
        public void Remove(string name) 
        {
            for (int i = 0; i < guards.Count; i++) 
                if (guards[i].name == name) 
                {
                    reaction -= guards[i].Reaction; 
                    guards.Remove(guards[i]);
                }
            for (int i = 0; i < footmen.Count; i++) 
                if (footmen[i].name == name) 
                {
                    reaction -= footmen[i].Reaction; 
                    footmen.Remove(footmen[i]); 
                }
        }

        public void ReactionToAttack()
        {
            reaction?.Invoke();
        }
    }

    class Footman
    {
        public string name;

        public Footman(string name) => this.name = name;

        public void Reaction() => Console.WriteLine($"Footman {name} is panicking!");
    }

    class RoyalGuard
    {
        public string name;

        public RoyalGuard(string name) => this.name = name; 

        public void Reaction() => Console.WriteLine($"Royal Guard {name} is defending!");
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            King king = new King("");

            bool end = false;
            for (int i = 0; !end; i++)
            {
                string[]? input = Console.ReadLine()?.Split(' ');

                if (input != null)
                {
                    if (input[0] == "END")
                    {
                        end = true;
                    }
                    else if (i == 0)
                        king = new King(input[0]);
                    else if (i == 1)
                        foreach (string item in input)
                            king.Add(new RoyalGuard(item));
                    else if (i == 2)
                        foreach (string item in input)
                            king.Add(null, new Footman(item));
                    else if (i > 2)
                    {
                        if (input[0].ToLower() == "attack" && input[1].ToLower() == "king")
                            king.ReactionToAttack();
                        else if (input[0].ToLower() == "kill")
                            king.Remove(input[1]);
                    }
                }
            }
        }
    }
}
