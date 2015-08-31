using System;

namespace DiscreteSimulation
{
	/// <summary>
	/// Event callback delegate.
	/// </summary>
	public delegate void EventCallback(Event e);

	/// <summary>
	/// Represents an event that occurs at some specified <see cref="DiscreteSimulation.Event.Time"/>.
	/// </summary>
	public class Event : IComparable<Event>
	{
		/// <summary>
		/// Time at which the event should occur.
		/// </summary>
		/// <value>The time at which the event should occur.</value>
		public TimeSpan Time { get; }

		/// <summary>
		/// Callback function that handles the event.
		/// </summary>
		/// <value>The callback function that is executed when the event occurs.</value>
		public EventCallback Callback { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DiscreteSimulation.Event"/> class.
		/// </summary>
		/// <param name="time">Time at which the event should occur.</param>
		/// <param name="callback">Callback function to be executed when the event occurs.</param>
		public Event(TimeSpan time, EventCallback callback)
		{
			Time = time;
			Callback = callback;
		}

		/// <Docs>To be added.</Docs>
		/// <para>Returns the sort order of the current instance compared to the specified object.</para>
		/// <summary>
		/// Compares this event to the <paramref name="other"/> event by the times at which they occur.
		/// </summary>
		/// <returns>
		/// Negative if this event occurs before the <paramref name="other"/> event, positive if after and zero if equal.
		/// </returns>
		/// <param name="other">Other.</param>
		public int CompareTo(Event other)
		{
			return Time.CompareTo(other.Time);
		}

		/// <summary>
		/// Runs the event's <see cref="Callback"/> function.
		/// </summary>
		public void Run()
		{
			Callback(this);
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="DiscreteSimulation.Event"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="DiscreteSimulation.Event"/>.</returns>
		public override string ToString()
		{
			return "Event(t = " + Time + ")";
		}
	}
}

