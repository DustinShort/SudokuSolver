using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Solver
{

    public class Program
    {
        static void Main(string[] args)
        {
            //puzzle p = new puzzle("default");
            int[] inputSudoku = new int[81]  {8,0,0,0,0,0,0,0,6,
                                              0,9,1,0,7,6,2,3,0,
                                              0,6,7,1,0,0,5,9,0,
                                              0,8,0,0,2,0,4,0,0,
                                              0,1,0,8,0,9,0,5,0,
                                              0,0,6,0,1,0,0,8,0,
                                              0,3,5,0,0,1,8,2,0,
                                              0,2,8,7,5,0,1,4,0,
                                              1,0,0,0,0,0,0,0,5};
            puzzle p = new puzzle(inputSudoku);
            Console.WriteLine();
            //p.Print();
            /*p.Initiate();

            p.PrintPencilMarks(1, 9); // location based intuitivly, ie starting a 1
            p.PrintPencilMarks(9, 7);
            p.PrintPencilMarks(5, 5);
            p.PrintPencilMarks(7, 4);
            p.PrintPencilMarks(8, 8);
            p.PrintPencilMarks(1, 1);
*/
            p.BruteForce();
            p.Print();

            Console.WriteLine();
        }
    }
}