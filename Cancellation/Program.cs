using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cancellation
{
	class Program
	{
		private static int ThreadId => Thread.CurrentThread.ManagedThreadId;

		static void Main(string[] args)
		{
			var cts = new CancellationTokenSource();
			var ct = cts.Token;

			Console.WriteLine("Main - Calling DoSomethingLong() (Thread #" + $"{ThreadId}) \n");
			var longTask = DoSomethingLong(ct);

			Thread.Sleep(2000); // Wait 3 secs and cancel

			Console.WriteLine("Cancelling the long operation (Thread #" + $"{ThreadId}) \n");
			cts.Cancel();
			//longTask.Wait();

			Console.WriteLine("Main - Complete (Thread #" + $"{ThreadId}) \n");
		}

		private static async Task DoSomethingLong(CancellationToken ct)
		{
			Console.WriteLine("\tStarting - LongOperation (Thread #" + $"{ThreadId}) \n");

			try
			{
				await Task.Delay(8000, ct); // Some long operation
			}
			catch (TaskCanceledException)
			{
				Console.WriteLine("\n Cancelled - LongOperation (Thread #" + $"{ThreadId}) \n");
			}
		}
	}
}