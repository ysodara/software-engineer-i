using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace HW3
{

    class Program
    {

        private int WrapSimply(IQueueInterface<string> words, int columnLength, string outputFilename)
        {
            StreamWriter outFile;

            try
            {
                outFile = new StreamWriter(outputFilename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Cannot create or open ", outputFilename,
                    " for writing.  Using standard output instead.");
                outFile = new StreamWriter(Console.OpenStandardOutput());
                throw e;
            }

            int col = 1;
            int spacesRemaining = 0;

            while (!words.IsEmpty())
            {
                string str = words.Peek();
                int len = str.Length;

                //see if we need to wrap to the next line
                if (col == 1)
                {
                    outFile.Write(str);
                    col += len;
                    words.Pop();
                }
                else if ((col + len) >= columnLength)
                {
                    //go to next line
                    outFile.WriteLine();
                    spacesRemaining += (columnLength - col) + 1;
                    col = 1;

                }
                else
                {
                    //typical case if printing the next word one the same line
                    outFile.Write(' ');
                    outFile.Write(str);
                    col += (len + 1);
                    words.Pop();
                }

            }
            outFile.WriteLine();
            outFile.Flush();
            outFile.Close();

            return spacesRemaining;
        }
        private void PrintUsage()
        {
            Console.WriteLine("Usage is:\n" +
            "\tjava Main C inputfile outputfile\n\n" +
            "Where:" +
            "  C is the column number to fit to\n" +
            "  inputfile is the input text file \n" +
            "  outputfile is the new output file base name containing the wrapped text.\n" +
            "e.g. java Main 72 myfile.txt myfile_wrapped.txt");
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            Program p1 = new Program();
            Program p2 = new Program();

            int C = 72;                     // Column length to wrap to
            string inputFilename = "";
            string outputFilename = "output.txt";

            if (args.Length != 3)
            {
                p.PrintUsage();
                Environment.Exit(1);
            }

            try
            {
                C = int.Parse(args[0]);
                inputFilename = args[1];
                outputFilename = args[2];
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find the input file.");
                Environment.Exit(1);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something is wrong with the input.");
                p1.PrintUsage();
                Environment.Exit(1);
            }

            IQueueInterface<string> words = new LinkedQueue<string>();

            string[] wordsIn = null;

            using (StreamReader input = new StreamReader(new FileStream(inputFilename, FileMode.Open)))
            {
                wordsIn = input.ReadToEnd().Split(' ', '\n', '\r');

                for (int i = 0; i < wordsIn.Length; i++)
                {
                    if (wordsIn[i] != "")
                    {
                        words.Push(wordsIn[i]);
                    }
                }
            }

            // At this point the input text file has now been placed, word by word, into a FIFO queue
            // Each word does not have whitespaces included, but does have punctuation, numbers, etc.

            /* ------------------ Start here ---------------------- */

            // As an example, do a simple wrap
            int spacesRemaining = p2.WrapSimply(words, C, outputFilename);
            Console.WriteLine("Total spaces remaining (Greedy): " + spacesRemaining);

            //End Main
        }






    }
}
