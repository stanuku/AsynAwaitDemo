using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Composition
{
	class Program
	{
		private static int ThreadId => Thread.CurrentThread.ManagedThreadId;

		static void Main(string[] args)
		{
			var sw = new Stopwatch();

			sw.Start();


			var color = Console.ForegroundColor;

			
			Console.ForegroundColor = ConsoleColor.Yellow;
			OperationOne();
			OperationTwo();
			sw.Stop();
			Console.ForegroundColor = color;
			Console.WriteLine($"All non-async task completed in {sw.ElapsedMilliseconds}" +
							  "(Thread #" + $"{ThreadId}) \n");

			/*
			sw.Restart();
			

			Console.ForegroundColor = ConsoleColor.Green;
			Task taskOne = OperationOneAsync();
			Task taskTwo = OperationTwoAsync();

			Task.WhenAll(new[] { taskOne, taskTwo })
				.ContinueWith(t =>
				{
					sw.Stop();
					Console.ForegroundColor = color;
					Console.WriteLine($"All async task completed in {sw.ElapsedMilliseconds}" +
									  "(Thread #" + $"{ThreadId}) \n");
				}).Wait();
			*/

		}

		private static void OperationOne()
		{
			Console.WriteLine("OperationOne - Starting (Thread #" + $"{ThreadId}) \n");

			Thread.Sleep(6000);
			Console.WriteLine("OperationOne - Complete (Thread #" + $"{ThreadId}) \n");
		}

		private static void OperationTwo()
		{
			Console.WriteLine("OperationTwo - Starting (Thread #" + $"{ThreadId}) \n");
			Thread.Sleep(6000);
			Console.WriteLine("OperationTwo - Complete (Thread #" + $"{ThreadId}) \n");
		}

		private static async Task OperationOneAsync()
		{
			Console.WriteLine("OperationOneAsync - Starting (Thread #" + $"{ThreadId}) \n");
			await Task.Delay(6000);
			Console.WriteLine("OperationOneAsync - Complete (Thread #" + $"{ThreadId}) \n");
		}

		private static async Task OperationTwoAsync()
		{
			Console.WriteLine("OperationTwoAsync - Starting (Thread #" + $"{ThreadId}) \n");
			await Task.Delay(6000);
			Console.WriteLine("OperationTwoAsync - Complete (Thread #" + $"{ThreadId}) \n");
		}
	}
}