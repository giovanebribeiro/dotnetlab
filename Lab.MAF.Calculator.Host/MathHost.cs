using Lab.MAF.Calculator.HostView;
using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab.MAF.Calculator.Host
{
    /// <summary>
    /// Complete tutorial: https://msdn.microsoft.com/en-us/library/bb788290(v=vs.110).aspx
    /// Theorical infos: https://blogs.msdn.microsoft.com/sidharth/2009/05/03/working-with-the-managed-addin-framework-system-addin/
    /// </summary>
    class MathHost
    {
        static void Main(string[] args)
        {
            // Assume that the current directory is the application folder and
            // contains the pipeline folder structure.
            String addInRoot = Environment.CurrentDirectory+"\\Pipeline";

            //update the cache files of the pipeline segments and addIns
            string[] warnings = AddInStore.Update(addInRoot);
            foreach(string warning in warnings)
            {
                Console.WriteLine(warning);
            }

            //Search for addins of type ICalculator (the host view of addin)
            Collection<AddInToken> tokens = AddInStore.FindAddIns(typeof(ICalculator), addInRoot);

            //ask the user which add-in they would like to use
            AddInToken calcToken = ChooseCalculator(tokens);

            // Activate the selected AddInToken in a new Application Domain with the Internet Trust level
            ICalculator calc = calcToken.Activate<ICalculator>(AddInSecurityLevel.Internet);

            //run the add-in
            RunCalculator(calc);
        }

        private static AddInToken ChooseCalculator(Collection<AddInToken> tokens)
        {
            if(tokens.Count == 0)
            {
                Console.WriteLine("No calculators available.");
                return null;
            }

            Console.WriteLine("Available calculators: ");
            int tokNumber = 1;
            foreach(AddInToken tok in tokens)
            {
                Console.WriteLine("\t[{0}]: {1} - {2}\n\t{3}\n\t\t {4}\n\t\t {5} - {6}",
                    tokNumber.ToString(),
                    tok.Name,
                    tok.AddInFullName,
                    tok.AssemblyName,
                    tok.Description,
                    tok.Version,
                    tok.Publisher);
                tokNumber++;
            }

            Console.WriteLine("Which calculator do you want to use?");
            String line = Console.ReadLine();

            int selection;
            if(Int32.TryParse(line, out selection))
            {
                if(selection <= tokens.Count)
                {
                    return tokens[selection - 1];
                }
            }

            Console.WriteLine("Invalid selection: {0}. Please choose again.", line);
            return ChooseCalculator(tokens);            
        }

        private static int FindFirstNonDigit(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!(Char.IsDigit(s[i]))) return i;
            }
            return -1;
        }

        private static void RunCalculator(ICalculator calc)
        {
            if(calc == null)
            {
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Available operators: +, -, *, /");
            Console.WriteLine("Request a calculation, such as: 2 + 2");
            Console.WriteLine("Type \"exit\" to exit.");

            String line = Console.ReadLine();
            line = new Regex(@"\s+").Replace(line, ""); // remove all spaces
            while (!line.Equals("exit"))
            {
                try
                {
                    int left;
                    int right;
                    Char operation;

                    int fn = FindFirstNonDigit(line); //finds te operator
                    if (fn < 0) throw new Exception();
                        
                    left = int.Parse(line.Substring(0, fn));
                    right = int.Parse(line.Substring(fn + 1));
                    operation = line[fn];
                        
                    switch (operation)
                    {
                        case '+':
                            Console.WriteLine(calc.Add(left, right));
                            break;
                        case '-':
                            Console.WriteLine(calc.Sub(left, right));
                            break;
                        case '*':
                            Console.WriteLine(calc.Mult(left, right));
                            break;
                        case '/':
                            Console.WriteLine(calc.Div(left, right));
                            break;
                        default:
                            Console.WriteLine("{0} is an invalid command. Valid commands are: +, -, *, /", operation);
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid command: {0}. Commands must be formated: [number] [operation] [number]", line);
                }

                line = Console.ReadLine();
            }
        }
    }

    internal class Parser
    {
        double a;
        double b;
        string action;

        internal Parser(string line)
        {
            string[] parts = line.Split(' ');
            a = double.Parse(parts[0]);
            action = parts[1];
            b = double.Parse(parts[1]);
        }

        public double A
        {
            get
            {
                return a;
            }
        }

        public double B
        {
            get
            {
                return b;
            }
        }

        public string Action
        {
            get
            {
                return action;
            }
        }
    }
}
