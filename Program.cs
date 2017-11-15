using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

	class Matrix
	{

		private int[,] matstore;
		private int matrows;
		private int matcols;

		public Matrix(int rows, int cols)
		{

 			matstore = new int[rows,cols];
			matrows = rows;
			matcols = cols;
			for (int r = 0; r < rows; r++) for (int c = 0; c < cols; c++) matstore[r, c] = 0;

		}

		public void setfromstring(int rows,int cols, string plaintext)
		{

			int count = 0;
			plaintext = plaintext.ToUpper();

			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < cols; c++)
				{
					matstore[r, c] = ((int) plaintext[count] - 64);
					count++;
				}
			}

		}

		public void display()
		{

			for (int r = 0; r < matrows; r++)
			{
				for (int c = 0; c < matcols; c++)
				{
					if (matstore[r, c] == 0) Console.Write("Z ");
					else Console.Write((char)(matstore[r, c] + 64) + " ");
				}
				Console.WriteLine();
			}

		}

		public void set(int rows, int cols, int value)
		{

			matstore[rows, cols] = value;

		}

		public int get(int rows, int cols)
		{

			if (matstore[rows, cols] == 0) return 26;
			else return matstore[rows, cols];

		}

		public Matrix multiply(Matrix key)
		{

			int e = matstore[0, 0];
			int f = matstore[1, 0];
			int result1 = (key.get(0, 0) * e + key.get(0, 1) * f) % 26;
			int result2 = (key.get(1, 0) * e + key.get(1, 1) * f) % 26;
			Matrix result = new Matrix(2, 1);
			result.set(0, 0, result1);
			result.set(1, 0, result2);
			return result;

		}

	}

	class Program
	{
		static void Main(string[] args)
		{
			string plaintext = "";
			int option = 0;
			string ciphertext = "";
			Matrix square = new Matrix(26, 26);
			square = InitialiseVigenereSquare(square);
			string key = InitialiseEncryption(out plaintext, out option);
			Console.WriteLine(key + " " + plaintext);
			if (option == 1) ciphertext = HillEncryption(plaintext, key);
			else if (option == 2) ciphertext = HillEncryption(plaintext, key);
			Console.WriteLine(ciphertext);
			Console.ReadKey();
		}

		static string InitialiseEncryption(out string plaintext, out int option)
		{
			bool valid = true;
			string k = "";
			string key = "";

			do
			{
				Console.Write("1. Hill\n2. Vigenere\nPlease choose which cipher you would like to use: ");
				valid = int.TryParse(Console.ReadLine(), out option);
				if (!valid || option < 1 || option > 2) Console.WriteLine("The option you have entered is invalid. Please use the numbers 1 and 2 only.");
			} while (!valid || option < 1 || option > 2);

			do
			{
				Console.Write("Please enter a keyword: ");
				key = Console.ReadLine();
				valid = k.Any(char.IsDigit);
				//Console.WriteLine(valid);
				if (valid || key.Length < 4) Console.WriteLine("The keyword you have entered is invalid. Please use letters only and a word that has at least 4 letters.");
			} while (valid);

			do
			{
				Console.Write("\nPlease enter a string of text to be encrypted: ");
				plaintext = Console.ReadLine();
				valid = plaintext.Any(char.IsDigit);
				//Console.WriteLine(valid);
				if (valid || plaintext.Length < 2) Console.WriteLine("The text you have entered is invalid. Please use letters only and a text that is at least 2 letters long.");
			} while (valid);

			return key;

		}

		static string HillEncryption(string plaintext, string key)
		{

			Matrix keyword = new Matrix(2, 2);
			keyword.setfromstring(2, 2, key);
			keyword.display();

			string ciphertext = "";
			string digraphPair = "";
			Matrix digraph = new Matrix(2, 1);
			Matrix result;

			if (plaintext.Length % 2 == 1) plaintext += "X";

			for (int i = 0; i < plaintext.Length; i += 2)
			{
				digraphPair = "" + plaintext[i] + plaintext[i + 1];
				digraph.setfromstring(2, 1, digraphPair);
				result = digraph.multiply(keyword);
				Console.WriteLine(plaintext[i] + " to " + (char)(result.get(0, 0) + 64));
				Console.WriteLine(plaintext[i + 1] + " to " + (char)(result.get(1, 0) + 64));
				ciphertext += "" + (char)(result.get(0, 0) + 64) + (char)(result.get(1, 0) + 64);
			}

			return ciphertext;

		}

		static string VigenereEncryption(string plaintext, string key)
		{
			string ciphertext = "";

			//for (int i = 0; i < (plaintext.Length / key.Length); i++)
			//{

			//}

			return ciphertext;
		}

		static Matrix InitialiseVigenereSquare(Matrix square)
		{
			string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			for (int i = 0; i < 26; i++)
			{
				for (int j = 0; j < 26; j++)
				{
					square.set(i, j, ((alphabet[j] - 64 + i) % 26));
				}
			}

			square.display();
			return square;
		}

	}

	class Vigenere
	{

	}

	class Hill
	{

	}

}
