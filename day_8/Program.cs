﻿using System;
using System.IO; 
using System.Collections; 
using System.Collections.Generic;

namespace day_8
{
    class Program
    {
        public static void Main(string[] args)
        {
            var data = new List<string>(); 
            var instructionSet = new List<Instruction>();
            string file = "./test_input.txt";
           // string file = "input.txt";
            parse(file, ref data);
            foreach(string i in data) 
            {
                string[] t = i.Split(" ");
                int arg;
                bool parse = Int32.TryParse(t[1], out arg);
                if (parse == false) throw new Exception("FAILED TO PARSE INT");
                instructionSet.Add(new Instruction(t[0], arg));
            }

            int cnt = 0; 
            foreach(Instruction i in instructionSet) 
            {
                System.Console.WriteLine("DS POS {0}", cnt);
                System.Console.WriteLine("INSTRUCTION {0}", i.Name);
                System.Console.WriteLine("ARG {0}", i.Arg);
                System.Console.WriteLine("------");
                cnt += 1; 
            }
            System.Console.WriteLine("");
            
            bool running = true; 
            int pos = 0; 
            int acc = 0; 
            int nxtPos = 0; 
            while (running) 
            {
                pos += nxtPos; 
                if (pos > instructionSet.Count) 
                {
                    pos = pos % instructionSet.Count;
                }
                if (pos < 0) 
                {
                    pos = instructionSet.Count + (pos % instructionSet.Count);
                }
                Instruction inst = instructionSet[pos]; 
                if (inst.Seen)//Get out now as we have seen this node
                {
                    System.Console.WriteLine("BREAKING {0} {1} {2}", inst.Arg, inst.Name, pos); 
                    break; 
                }
                if (inst.Name == "nop")
                {
                    pos += 1; 
                    nxtPos = 0;
                    inst.Seen = true; 
                    continue; 
                }
                if (inst.Name == "acc") 
                {
                    acc += inst.Arg; 
                    pos += 1; 
                    nxtPos = 0; 
                }
                if (inst.Name == "jmp") 
                {
                    nxtPos = inst.Arg; 
                }
                inst.Seen = true;
            }
        }

        public static void parse(string file, ref List<string> data) 
        {
            if(data == null) return; 
            if(file == null) return; 
            string[] lines = File.ReadAllLines(file);
            foreach(var s in lines) 
            {
                data.Add(s);
            }
        }
    }

    public class Instruction
    {
        public string Name {get; set;}
        public int Arg {get; set;} 
        public bool Seen {get; set;}

        public Instruction(string name, int arg)
        {
            this.Name = name; 
            this.Arg = arg; 
            this.Seen = false; 
        }
    }
}
