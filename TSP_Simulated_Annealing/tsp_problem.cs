using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Simulated_Annealing
{
    class tsp_problem
    {
        List<City> cities = new List<City>();
        public static Random random = new Random();

        public void initializeProblem(System.IO.StreamReader file, int size)
        {
            Console.WriteLine("Initialize Prblem");
            // bool firstFlag = true;
            HashSet<City> unvisited = new HashSet<City>();

            // Iterate through file and add cities to the list
            for (int j = 0; j < size; j++)
            {
                City city = new City(parse_city(file.ReadLine()));
                cities.Add(city);

            }
            createEdges();
        }

        private List<string> parse_city(string line)
        {
            List<string> city_data = line.Split(',').ToList<string>();
            return city_data;
        }

        private void createEdges()
        {
            City c1, c2;
            double dist;

            for (int i = 0; i < cities.Count; i++)
            {
                for (int j = 0; j < cities.Count; j++)
                {
                    if (i == j)
                        continue;

                    //Console.WriteLine("i = " + i + " j = " + j);
                    c1 = cities[i];
                    c2 = cities[j];

                    if (c1.dMap.ContainsKey(c2) || c2.dMap.ContainsKey(c1))
                        continue;

                    dist = getDistanceCity(c1, c2);

                    c1.addCityToMap(c2, dist);
                    c2.addCityToMap(c1, dist);

                }

                //Console.WriteLine("City Conenections = " + firstCity.dMap.Count);
            }

            c1 = null;
            c2 = null;
            //Console.WriteLine("All Cities Count = " + allCities.Count);
            Console.WriteLine("Finished Create Edges");
            sortEdges(cities);
        }

        public double getDistanceCity(City c1, City c2)
        {
            double distance = (Math.Sqrt(Math.Pow((c1.x - c2.x), 2) + Math.Pow((c1.y - c2.y), 2)));
            return distance;
        }

        public void sortEdges(List<City> allCities)
        {
            foreach (City city in allCities)
            {
                Dictionary<City, double> newMap = new Dictionary<City, double>();
                foreach (KeyValuePair<City, double> c in city.dMap.OrderBy(key => key.Value))
                {
                    newMap.Add(c.Key, c.Value);
                }
                city.dMap = newMap;
            }
        }

        public Route createRandomRoute()
        {
            Console.WriteLine("Create random route");
            Route route = new Route();
            List<City> newRoutePath = new List<City>();

            int routeSize = cities.Count;
            int index;

            HashSet<int> seen = new HashSet<int>();
            City city;
            double random; 

            newRoutePath.Add(cities[0]);
            for(int i = 1;  i < routeSize; i++)
            {
                // Console.WriteLine("For loop");
                //Console.Read();
                index = (int) (GetRandomNumber(0, 1) * routeSize);
                while (index == 0 || seen.Contains(index))
                {
                    random = GetRandomNumber(0, 1);
                    // Console.WriteLine("While Loop, random = " + random);
                    index = (int)(random * routeSize);
                    // Console.WriteLine("Index = " + index);
                }

                seen.Add(index);
                city = cities[index];
                newRoutePath.Add(city);
            }

            if (cities.Count > 1)
            {
                // Console.WriteLine("CITY ID: " + cities[0].ID);
                newRoutePath.Add(cities[0]);
            }

            route.routePath = newRoutePath;
            // Console.WriteLine("Finished Random Route");
            return route;
        }

        public static double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
