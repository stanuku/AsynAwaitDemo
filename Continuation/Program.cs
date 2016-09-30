using System;
using System.Threading;
using System.Threading.Tasks;

namespace Continuation
{
	class Program
	{
		private static int ThreadId => Thread.CurrentThread.ManagedThreadId;

		static void Main(string[] args)
		{
			Console.WriteLine("Main - Calling DoSomethingLong() (Thread #" + $"{ThreadId}) \n");

			DoSomethingLong();

			ThisWillExecute_Before_The_ContinuationOperation();

			Console.WriteLine("Main - All Done (Thread #" + $"{ThreadId}) \n");

			Console.ReadLine();
		}

		private static void DoSomethingLong()
		{
			var taskOne = This_is_A_LongRunningOperation();

			// Attach a continuation task
			Console.WriteLine("\tCalling ContinueWith() (Thread #" + $"{ThreadId}) \n");

			taskOne.ContinueWith(t => This_is_A_ContinuationOperation());
		}

		private static async Task This_is_A_LongRunningOperation()
		{
			Console.WriteLine("\tStarting - This_is_A_LongRunningOperation (Thread #" + $"{ThreadId}) \n");

			await Task.Delay(3000); // Some long operation

			Console.WriteLine("\tCompleted - This_is_A_LongRunningOperation (Thread #" + $"{ThreadId}) \n");
		}

		// Continuation operation that executes on a separate thread.
		private static void This_is_A_ContinuationOperation()
		{
			Console.WriteLine("\tThis_is_A_ContinuationOperation (Thread #" + $"{ThreadId}) \n");
		}

		private static void ThisWillExecute_Before_The_ContinuationOperation()
		{
			Console.WriteLine("ThisWillExecute_Before_The_ContinuationOperation (Thread #" + $"{ThreadId}) \n");
		}
	}
}