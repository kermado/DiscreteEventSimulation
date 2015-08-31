using System;

namespace DiscreteSimulation
{
	/// <summary>
	/// Exception that is raised when an <see cref="DiscreteSimulation.Event"/> has been scheduled to occur at some
	/// time prior to the current simulation <see cref="DiscreteSimulation.Simulation.Time"/>.
	/// </summary>
	public class PastEventException : Exception
	{
		public PastEventException() {}
		public PastEventException(string message) : base(message) {}
		public PastEventException(string message, Exception inner) : base(message, inner) {}
	}

	/// <summary>
	/// Represents a single discrete time simulation that runs scheduled events.
	/// </summary>
	public class Simulation
	{
		/// <summary>
		/// Whether the simulation is currently running (i.e. processing events).
		/// </summary>
		private bool _running;

		/// <summary>
		/// The event queue.
		/// </summary>
		private PriorityQueue<Event> _event_queue;

		/// <summary>
		/// The current simulation time.
		/// </summary>
		/// <value>The current simulation time.</value>
		public TimeSpan Time { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DiscreteSimulation.Simulation"/> class.
		/// </summary>
		public Simulation ()
		{
			_running = false;
			_event_queue = new PriorityQueue<Event>();
		}

		/// <summary>
		/// Schedules the specified provided event to occur.
		/// </summary>
		/// <param name="e">Event to schedule.</param>
		public void Schedule(Event e)
		{
			_event_queue.Enqueue(e);
		}

		/// <summary>
		/// Runs the simulation to completion or until the <see cref="DiscreteSimulation.Simulation.Stop"/>
		/// method is called.
		/// </summary>
		public void Run()
		{
			if (!_running) {
				_running = true;
				while (_running && _event_queue.Count > 0) {
					Event e = _event_queue.Dequeue();
					if (e.Time >= Time) {
						Time = e.Time;
						e.Run ();
					} else {
						throw new PastEventException("The event " + e + " was scheduled to occur before the current simulation time, " + Time);
					}
				}
				_running = false;
			}
		}

		/// <summary>
		/// Pauses the simulation.
		/// </summary>
		public void Stop()
		{
			_running = false;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="DiscreteSimulation.Simulation"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="DiscreteSimulation.Simulation"/>.</returns>
		public override string ToString()
		{
			return "Simulation(" + _event_queue.Count + " events pending)";
		}
	}
}

