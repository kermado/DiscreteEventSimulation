using System;

namespace DiscreteSimulation
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Simulation sim = new Simulation();

			sim.Schedule(new Event(new TimeSpan(0, 0, 5), (Event e) => Console.WriteLine(e)));
			sim.Schedule(new Event(new TimeSpan(0, 0, 2), (Event e) => {
				Console.WriteLine(e);
				sim.Schedule(new Event(new TimeSpan(0, 0, 4), (Event ev) => Console.WriteLine(ev)));
			}));
			sim.Schedule(new Event(new TimeSpan(0, 0, 3), (Event e) => Console.WriteLine(e)));
			sim.Schedule(new Event(new TimeSpan(0, 0, 1), (Event e) => Console.WriteLine(e)));

			Console.WriteLine(sim);

			sim.Run();
		}
	}
}
