

// Author: FreeDOOM#4231 on Discord


using System;
using System.Collections.Generic;
using System.IO;


namespace ASD___3
{
	class Program
	{
		public const int N = 1_000_000;
		public const int k = 30_000;

		readonly static string[] _fileName_ = new string[] {
			"_test.txt",		   // 0
			"_generatedTest.txt", // 1
			"_1_Pietrzeniuk.txt" // 2
			};

		static void Main(string[] args)
		{
			string fileName = _fileName_[2];


            //CreateTestFile($"RR_in{_fileName_[1]}", N, k);
            List<Lecture> list;
			// Nie jestem pewien czy takie prymitywne łapanie błędów ma wgle sens, ale nie chce mi się tego ruszać :P
			try { list = LoadLectures($"RR_in{fileName}"); }
			catch (Exception e) { Console.WriteLine(e); return; }

            SaveResults($"OD_out{fileName}", Algorithm.Solution(list, k), true);
        }


		#region >>> Obsługa plików <<<

		public static List<Lecture> LoadLectures(string fileName__, bool readToConsole = false)
		{
			Console.WriteLine($">>>Started loading file \"{fileName__}\"...");

			if (!File.Exists(fileName__))
				throw new FileNotFoundException($"There is no file {fileName__} in program directory.");

			string[] lines = File.ReadAllLines(fileName__);

			int count = Convert.ToInt32(lines[0]);
			var resultList = new List<Lecture>(count);
			string[] line;
			for (int i = 1; i <= count; i++)
            {
				line = lines[i].Split(' ');
				resultList.Add(new Lecture(Convert.ToInt32(line[0]), Convert.ToInt32(line[1])));
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($">>>Compleated loading file \"{fileName__}\"!");
			Console.ForegroundColor = ConsoleColor.Gray;

			if (readToConsole)
			{
				Console.WriteLine($"{fileName__} stations:");
				foreach (Lecture lecture in resultList)
					Console.WriteLine(lecture);
			}

			return resultList;
		}

		public static void SaveResults(string fileName__, Tuple<string, int>[] toDisplay, bool readToConsole = false)
		{
			Console.WriteLine($">>>Started saving to file \"{fileName__}\"...");

			if (File.Exists(fileName__))
				File.Delete(fileName__);

			foreach (Tuple<string, int> item in toDisplay)
				File.AppendAllText(fileName__, $"\n{item.Item1} Method: \n{item.Item2}\n");

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($">>>Compleated saving to file \"{fileName__}\"!");
			Console.ForegroundColor = ConsoleColor.Gray;

			if (readToConsole)
			{
				Console.WriteLine($"\n{fileName__} contents:\n");
				Console.WriteLine(File.ReadAllText(fileName__));
			}
		}

		/// <summary>
		/// Saves only data specified in exercise instruction.
		/// </summary>
		/// <param name="fileName__"></param>
		/// <param name="toDisplay"></param>
		public static void SaveResults(string fileName__, int toDisplay, bool readToConsole = false)
		{
			Console.WriteLine($">>>Started saving to file \"{fileName__}\"...");

			if (File.Exists(fileName__))
				File.Delete(fileName__);

			File.WriteAllText(fileName__, $"{toDisplay}");

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($">>>Compleated saving to file \"{fileName__}\"!");
			Console.ForegroundColor = ConsoleColor.Gray;

			if (readToConsole)
			{
				Console.WriteLine($"\n{fileName__} contents:\n");
				Console.WriteLine(File.ReadAllText(fileName__));
			}
		}

		public static void CreateTestFile(string fileName__, int sampleSize = 0, int dataRange = 100, bool readToConsole = false)
		{
			Console.WriteLine($">>>Started creating file \"{fileName__}\"...");

			if (File.Exists(fileName__))
				File.Delete(fileName__);

			Random rng = new Random();

			if (sampleSize == 0)
				sampleSize = rng.Next(30, 50);

			File.AppendAllText(fileName__, $"{sampleSize}");

			int start;
			for (int i = 1; i <= sampleSize; i++)
			{
				start = rng.Next(0, dataRange - 100);
				File.AppendAllText(fileName__, $"\n{start} {start + rng.Next(1, 100)}");
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($">>>Compleated creating file \"{fileName__}\"!");
			Console.ForegroundColor = ConsoleColor.Gray;

			if (readToConsole)
			{
				Console.WriteLine($"\n{fileName__} contents:\n");
				Console.WriteLine(File.ReadAllText(fileName__));
			}
		}

		#endregion
	}

	/// <summary>
	/// Stores start time and end time of lecture.
	/// </summary>
	public struct Lecture
    {
		public int start;
		public int end;

		public int Time => end - start;

		public Lecture(int start, int end)
			=> (this.start, this.end) = (start, end);

        public override string ToString()
			=> $"{start} {end}";
    }
}
