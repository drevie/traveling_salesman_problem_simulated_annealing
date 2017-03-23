using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Simulated_Annealing
{
    class Program
    {
        static string result = "";
        static float decayRate = 0.0005f;

        static void Main(string[] args)
        {
        restart:
            Console.WriteLine("Welcome to TSP Simulated Annealing Experiment... \nPress enter to continue");
            Console.ReadLine();

            // Console.Write("Enter rate of temperature decay (float < 1): ");
            // decayRate = Console.Read();

            // result += "This is the result files using the following constants: \n Decay rate = " + decayRate + "\n------------------------------------\n";
            result += "DecayRate=" + decayRate + "\n";
            result += "Size BestCost Time(ms) Iterations\n";
            // text_files();
            call_reads();




            Console.WriteLine("End of Program... ");
            Console.Read();


        }

        static void text_files()
        {
            int size;

            // For Size 10 
            size = 10;
            write_text_files(size);
            // For Size 25
            size = 25;
            write_text_files(size);
            // For Size = 50 
            size = 50;
            write_text_files(size);
            // For Size = 100 
            size = 100;
            write_text_files(size);
        }

        static void write_text_files(int size)
        {
            string filename_numb = size.ToString() + "-";
            string filename;
            int xCoordinate;
            int yCoordinate;
            bool[,] cities;
            Random rnd = new Random();

            for (int i = 0; i < 25; i++)
            {
                // Create new text file
                cities = new bool[100, 100];
                filename = filename_numb + i;
                string entry = "";
                for (int j = 0; j < size; j++)
                {
                redo:
                    // Create a new city 
                    xCoordinate = rnd.Next(0, 100);
                    yCoordinate = rnd.Next(0, 100);

                    if (!cities[xCoordinate, yCoordinate])
                    {
                        entry += j + ", " + xCoordinate + ", " + yCoordinate + "\n";

                        cities[xCoordinate, yCoordinate] = true;
                    }

                    else
                        goto redo;
                    // Write to new text file
                }
                System.IO.File.WriteAllText(@"C:\Users\Daniel\TSP_Text_Files\" + filename, entry);
            }
        }

        public static void call_reads()
        {
            int size;
            size = 10;
            read_files(size);
            size = 25;
            read_files(size);
            size = 50;
            read_files(size);
            size = 100;
            read_files(size);


        }

        static void read_files(int size)
        {
            System.IO.StreamReader file;
        


            // Iterate through 25 files for each size
            for (int i = 0; i < 25; i++)
            {
                file = new System.IO.StreamReader(@"C:\Users\Daniel\TSP_Text_Files\" + size + "-" + i);
                tsp_problem problem = new tsp_problem();
                Console.WriteLine("Created Prblem");
                problem.initializeProblem(file, size);
                Console.WriteLine("Initialized Problem");
                result += Simulated_Annealing.Run(5000, 1, decayRate, problem.createRandomRoute());
                Console.WriteLine("Finisised: " + size);

            }

            System.IO.File.WriteAllText(@"C:\Users\Daniel\TSP_Text_Files\Results", result);


        }
    }
}
