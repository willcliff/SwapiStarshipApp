# SwapiStarshipApp

A C# Console Application which determines the number of stops for resupply required, by each starship, to travel a required distance.

## Basic Usage
The application will request the user to input a distance to be travelled in MGLT (Megalights).
The application will then return the number of stops for resupply required by each starship.
"Unknown" will be returned where insufficient information is known about the starships.
```C#
static void Main(string[] args)
        {
            Console.WriteLine("Please input the distance required to travel in MGLT:");
            string input = Console.ReadLine();
            int distance = Convert.ToInt32(input);

            Console.WriteLine("The following data conveys the Starships along with the required stops 
                               needed to travel {0} MGLT:", string.Format("{0:n0}", distance));
            Console.WriteLine("'Unknown' is displayed for the Starships where the speed or the consumeables value is unknown");
            
            ...
                        
```
For more info, visit the documentation of SWAPI: [SWAPI/Documentation](http://swapi.co/documentation)
