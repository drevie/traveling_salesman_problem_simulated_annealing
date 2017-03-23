using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Simulated_Annealing
{
    class Route
    {

        public List<City> routePath = new List<City>();
        public float cost = float.NegativeInfinity; 

        public Route rndSwap()
        {
            Route neighbor = new Route();
            List<City> newRoutePath = new List<City>(routePath);
            int swapVal_A = 0;
            int swapVal_B;

            while (swapVal_A == 0)
            {
                swapVal_A = (int) (tsp_problem.GetRandomNumber(0, 1) * (newRoutePath.Count - 1));
                // Console.WriteLine("A LOOP");
            }

            swapVal_B = swapVal_A;

            while (swapVal_B == swapVal_A || swapVal_B == 0)
            {
                swapVal_B = (int) (tsp_problem.GetRandomNumber(0, 1) * (newRoutePath.Count - 1));
                // Console.WriteLine("B LOOP");
            }

            // Console.WriteLine("a val = " + swapVal_A);
            // Console.WriteLine("b val = " + swapVal_B);

            City A = newRoutePath[swapVal_A];
            City B = newRoutePath[swapVal_B];

            newRoutePath[swapVal_A] = B;
            newRoutePath[swapVal_B] = A;

            neighbor.routePath = newRoutePath;
            neighbor.calculateRouteCost();

            return neighbor;
            

        }

        private void calculateRouteCost()
        {
            cost = 0.0f;
            City curr = null;
            City next = null;

            for(int i = 0; i < routePath.Count - 1; i++)
            {
                curr = routePath[i];
                next = routePath[i + 1];
                // Console.WriteLine("Cost = " + cost);
                cost +=  (float) curr.dMap[next];
            }
        }

        public void printRoute()
        {
            Console.Write("Path = ");
            foreach (City city in routePath)
                Console.Write(city.ID + " ");
            Console.Write("\n");
        }

        public float getCost()
        {
            if (cost == float.NegativeInfinity)
                calculateRouteCost();

            return cost;
        }
                 





    }
}
