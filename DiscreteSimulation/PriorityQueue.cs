using System;
using System.Collections.Generic;

namespace DiscreteSimulation
{
	/// <summary>
	/// Priority queue using a binary heap, which has the property that parent nodes have a lower priority than their
	/// child nodes. By preseving this property in the <see cref="DiscreteSimulation.PriorityQueue.Enqueue"/> and
	/// <see cref="DiscreteSimulation.PriorityQueue.Dequeue"/> methods, the root node will always hold the lowest
	/// priority element.
	/// </summary>
	public class PriorityQueue<T> where T : IComparable<T>
	{
		private List<T> _data;

		/// <summary>
		/// Accessor for the number of elements in the queue.
		/// </summary>
		/// <value>Size of the queue.</value>
		public int Count { get { return _data.Count; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="DiscreteSimulation.PriorityQueue`1"/> class.
		/// </summary>
		public PriorityQueue()
		{
			_data = new List<T>();
		}

		/// <summary>
		/// Enqueue the specified item. Complexity is O(log n).
		/// </summary>
		/// <param name="item">Item to insert into the ordered queue.</param>
		public void Enqueue(T item)
		{
			// Add the item at the missing leaf, which maps to the next element after the end of the list.
			int index = _data.Count;
			_data.Insert(index, item);

			// Get the index of the item's parent node.
			int parent_index = (index % 2 == 0) ? index/2 - 1 : (index-1)/2;

			// Bubble the item up the heap, switching the item with its parent if it is smaller than its parent.
			while (parent_index >= 0 && item.CompareTo(_data[parent_index]) < 0) {
				_data[index] = _data[parent_index];
				_data[parent_index] = item;
				index = parent_index;
				parent_index = (index % 2 == 0) ? index / 2 - 1 : (index - 1) / 2;
			}
		}

		/// <summary>
		/// Accessor for the lowest priority element.
		/// </summary>
		/// <returns>Lowest priority element in the queue.</returns>
		public T Top()
		{
			T top = _data[0];
			return top;
		}

		/// <summary>
		/// Dequeue and return the lowest priority element. Complexity is O(log n).
		/// </summary>
		/// <returns>Lowest priority element in the queue.</returns>
		public T Dequeue()
		{
			T top = _data[0];

			// Move the value of the last leaf node to the root node.
			int last_index = _data.Count - 1;
			_data[0] = _data[last_index];
			_data.RemoveAt(last_index);

			// Bubble the new root node value down if it has a higher priority than one of its children.
			// We swap the node with the child that has the lowest priority in order to ensure that the parent
			// always has a lower priority than both its children.
			int index = 0;
			int left_index = 2 * index + 1;
			int right_index = 2 * index + 2;
			while (left_index <= last_index-1) {
				if (right_index <= last_index-1 && _data[right_index].CompareTo(_data [left_index]) <= 0 && _data[index].CompareTo(_data[right_index]) > 0) {
					T temp = _data [right_index];
					_data[right_index] = _data [index];
					_data[index] = temp;
					index = right_index;
				} else if (_data[index].CompareTo(_data [left_index]) > 0) {
					T temp = _data [left_index];
					_data[left_index] = _data[index];
					_data[index] = temp;
					index = left_index;
				} else {
					break;
				}

				left_index = 2 * index + 1;
				right_index = 2 * index + 2;
			}

			return top;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="DiscreteSimulation.PriorityQueue`1"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="DiscreteSimulation.PriorityQueue`1"/>.</returns>
		public override string ToString()
		{
			string s = "";

			for (int i = 0; i < _data.Count; ++i) {
				s += _data [i].ToString () + " ";
			}

			return s;
		}
	}
}

