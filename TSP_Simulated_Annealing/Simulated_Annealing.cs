using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Simulated_Annealing
{
    class Simulated_Annealing
    {   
        public static string Run(double initTemp, double finalTemp, double alpha, Route route)
        {
            Stopwatch sw = new Stopwatch();

            Console.WriteLine("Run");
            double currTemp = initTemp;

            float currValue, newValue;

            Route bestRoute = route;

            Route currRoute = route;
            Route nextRoute;

            List<float> res = new List<float>();

            int count = 0;

            sw.Start();
            // Until we reach our temperature threshold
            while(currTemp > finalTemp)
            {
                nextRoute = currRoute.rndSwap();
                currValue = currRoute.getCost();
                newValue = nextRoute.getCost();

                // Console.WriteLine("newValue = " + newValue + " currValue = " + currValue + " currTemp = " + currTemp);
                // Console.WriteLine("Accept Prob = " + acceptProbability(currValue, newValue, currTemp));
                // Console.Read();

                if (acceptProbability(currValue, newValue, currTemp) > tsp_problem.GetRandomNumber(0, 1))
                {
                    currRoute = nextRoute;
                    // Console.Write("accept prob higher");
                    // Console.Read();
                    if (currRoute.getCost() < bestRoute.getCost())
                    {
                        bestRoute = nextRoute;
                        // Console.WriteLine("Best route set");
                        // Console.Read();
                    }
                }

                // count++;
                if (count++ % 100000 == 0)
                    res.Add(currRoute.getCost());

                currTemp *= (1 - alpha);
            }

            long time = sw.ElapsedMilliseconds;

            bestRoute.printRoute();
            // Console.Read();
            // return "Size=" + (bestRoute.routePath.Count - 1) + " Bestcost=" + bestRoute.getCost() + " Time=" + time + "ms\n";

            return ((bestRoute.routePath.Count - 1).ToString()) + " " + (bestRoute.getCost().ToString()) + " " + time.ToString() + " " + count.ToString() + "\n" ;
        }

        private static double acceptProbability(float currValue, float newValue, double currTemp)
        {
            // Console.WriteLine("Check Temp");
            if (currValue > newValue)
                return 1.0f;
            else
                return Math.Exp((currValue - newValue) / currTemp);
        }
    }
}
 