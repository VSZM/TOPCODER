using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BusinessTasks
{
	public String getTask(String[] list, int n)
	{
		LinkedList<String> tasks = new LinkedList<string>( list );

		LinkedListNode<String> actual_task = tasks.First, next;

		while ( tasks.Count > 1 )
		{
			for ( int i = 1 ; i < n ; i++ )
			{
				actual_task = actual_task.Next ?? tasks.First;
			}
			next = actual_task.Next ?? tasks.First;
			tasks.Remove( actual_task );
			actual_task = next;
		}

		return tasks.First.Value;
	}

	/*static void Main()
	{
		var solver = new BusinessTasks();
		solver.getTask(new string[]{"a","b","c","d"},2);
	}*/

}
