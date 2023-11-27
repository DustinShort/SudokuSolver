using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Solver
{
    // one cell of grid
    public class cell
    {
        public int Item { get; set; }
        public List<int> pencilMarks { get; set; }  // possible values for a cell

        //constructor to create cell with certain item
        public cell(int itm)
        {
            Item = itm;
            int[] PMs = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            pencilMarks = new List<int>();
            pencilMarks.AddRange(PMs);
        }
    }


    // The suduku grid including the number.
    // 0 indicates empty cell
    public class puzzle
    {
        // datamembers
        Stack<(int r, int c, int i)> s = new Stack<(int, int, int)>();
        public cell[,] grid = new cell[9, 9]; // a grid of cells (ie an array of nodes).

        //constructors
        // This constructor simply puts the value of the row of a cell as the value in that cell
        public puzzle()
        {
            // loop through rows
            for (int rs = 0; rs < 9; rs++)
            {
                for (int clms = 0; clms < 9; clms++)
                {
                    /* keep in mind, each cell in array is currently pointing to
                    null cell, so have to add a new one */
                    grid[rs, clms] = new cell(rs);
                }
            }

        }

        // This constructor takes in a custom sudoku grid as a list of elements
        public puzzle(int[] x)
        {
            // Since x is just a long list, have to go through each cell of grid[,] and figure out which element of x belongs there.
            // loop through rows 
            for (int rs = 0; rs < 9; rs++)
            {
                // loop through columns
                for (int clms = 0; clms < 9; clms++)
                {
                    /* keep in mind, each cell in array is currently pointing to
                    null cell, so have to add a new one */
                    grid[rs, clms] = new cell(x[9 * rs + clms]);
                }
            }
        }

        public puzzle(string dummy)
        {
            // a dummy/default/test suduko from wikipedia
            /*int[] x = new int[81]{5,3,0,0,7,0,0,0,0,
                                  6,0,0,1,9,5,0,0,0,
                                  0,9,8,0,0,0,0,6,0,
                                  8,0,0,0,6,0,0,0,3,
                                  4,0,0,8,0,3,0,0,1,
                                  7,0,0,0,2,0,0,0,6,
                                  0,6,0,0,0,0,2,8,0,
                                  0,0,0,4,1,9,0,0,5,
                                  0,0,0,0,8,0,0,7,9};*/
            //int[] x = new int[81] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            int[] x = new int[81]  {0,0,0,2,0,0,1,0,4,
                                    0,0,0,6,0,0,9,0,0,
                                    2,0,1,0,0,0,0,8,0,
                                    8,0,0,0,0,0,5,0,3,
                                    0,0,0,8,4,7,0,0,0,
                                    6,0,2,0,0,0,0,0,9,
                                    0,5,0,0,0,0,3,0,1,
                                    0,0,6,0,0,8,0,0,0,
                                    9,0,3,0,0,1,0,0,0};// puzzle 29 from red belt soduku

            // Since x is just a long list, have to go through each cell of grid[,] and figure out which element of x belongs there.
            // loop through rows 
            for (int rs = 0; rs < 9; rs++)
            {
                // loop through columns
                for (int clms = 0; clms < 9; clms++)
                {
                    /* keep in mind, each cell in array is currently pointing to
                    null cell, so have to add a new one */
                    grid[rs, clms] = new cell(x[9 * rs + clms]);
                }
            }
        }

        public void Initiate()
        {
            for (int rs = 0; rs < 9; rs++)
            {
                for (int clms = 0; clms < 9; clms++)
                {
                    if (grid[rs, clms].Item != 0)
                    {
                        FixPMByRow(rs, clms);
                        FixPMByColm(rs, clms);
                        FixPMByBox(rs, clms);
                    }
                }
            }

        }

        //---------------- FIX PENCIL MARK METHODS ------------------------------------- 
        // fix pencil marks by row for cells affected by a specific cell.
        public void FixPMByRow(int row, int column)
        {
            int curr = grid[row, column].Item; // store value at current location
            grid[row, column].pencilMarks.Clear(); // clear pencil marks

            // loop through columns in 'row' and remove pencil mark for curr, if needed
            for (int xposition = 0; xposition < 9; xposition++) //loop through columns in 'row'
            {
                if (grid[row, xposition].Item == 0 && grid[row, xposition].pencilMarks.Contains(curr))
                {
                    grid[row, xposition].pencilMarks.Remove(curr);

                    //------
                    if (grid[row, xposition].pencilMarks.Count == 1)
                    {
                        grid[row, xposition].Item = grid[row, xposition].pencilMarks[0];
                        FixPMByRow(row, xposition);
                        FixPMByColm(row, xposition);
                        FixPMByBox(row, xposition);
                    }
                    //------
                }
            }
        }

        // fix pencil marks by column for cells affected by a specific cell.
        public void FixPMByColm(int row, int column)
        {
            int curr = grid[row, column].Item; // store value at current location
            grid[row, column].pencilMarks.Clear(); // clear pencil marks

            // loop through rows in 'column' and remove pencil marks for curr, if needed
            for (int yposition = 0; yposition < 9; yposition++)
            {
                if (grid[yposition, column].Item == 0 && grid[yposition, column].pencilMarks.Contains(curr))
                {
                    grid[yposition, column].pencilMarks.Remove(curr);

                    //------
                    if (grid[yposition, column].pencilMarks.Count == 1)
                    {
                        grid[yposition, column].Item = grid[yposition, column].pencilMarks[0];
                        FixPMByRow(yposition, column);
                        FixPMByColm(yposition, column);
                        FixPMByBox(yposition, column);
                    }
                    //------
                }
            }

        }

        public void FixPMByBox(int row, int column)
        {
            int curr = grid[row, column].Item; // store value at current location
            grid[row, column].pencilMarks.Clear(); // clear pencil marks

            int boxRow = Convert.ToInt32(Math.Floor(Convert.ToDouble(row) / 3.0));
            int boxColumn = Convert.ToInt32(Math.Floor(Convert.ToDouble(column) / 3.0));

            for (int rs = boxRow * 3; rs < ((boxRow * 3) + 3); rs++)
            {
                for (int clms = boxColumn * 3; clms < ((boxColumn * 3) + 3); clms++)
                {
                    if (grid[rs, clms].Item == 0 && grid[rs, clms].pencilMarks.Contains(curr))
                    {
                        grid[rs, clms].pencilMarks.Remove(curr);

                        //------
                        if (grid[rs, clms].pencilMarks.Count == 1)
                        {
                            grid[rs, clms].Item = grid[rs, clms].pencilMarks[0];
                            FixPMByRow(rs, clms);
                            FixPMByColm(rs, clms);
                            FixPMByBox(rs, clms);
                        }
                        //------
                    }
                }
            }
        }
        //--------------------------------------------------------------------

        //------------------- CHECK CORRECTNESS METHODS ----------------------
        /* Methods to check that when a cell is assumed to be a value, the row, column
        box are still valid (ie, no duplicates, or empty pencil marks). If not, remove assumption from pencil 
        marks and make another assumption.*/
        public bool checkRow(int row, int column)
        {
            int curr = grid[row, column].Item;

            for (int clms = 0; clms < 9; clms++)
            {
                // if you're not at the current position and there is already a curr in the row, return false
                // or, if you're not at the current position and an empty cell has no pencil marks, return false
                if (clms != column && (grid[row, clms].Item == curr))//|| (grid[row, clms].Item == 0 && grid[row, clms].pencilMarks.Count == 0))) //note, skip over position grid['row', 'column']
                {
                    return false;

                }
            }

            return true;
        }
        public bool checkColumn(int row, int column)
        {
            int curr = grid[row, column].Item;

            for (int rs = 0; rs < 9; rs++)
            {
                // if you're not at the current position and there is already a curr in the column, return false
                // or, if you're not at the current position and an empty cell has no pencil marks, return false
                if (rs != row && (grid[rs, column].Item == curr))// || (grid[rs, column].Item == 0 && grid[rs, column].pencilMarks.Count == 0))) //note, skip over position grid['row', 'column']
                {
                    return false;
                }
            }
            return true;
        }
        public bool checkBox(int row, int column)
        {
            int curr = grid[row, column].Item;

            int boxRow = Convert.ToInt32(Math.Floor(Convert.ToDouble(row) / 3.0)); // find first row number of current box
            int boxColumn = Convert.ToInt32(Math.Floor(Convert.ToDouble(column) / 3.0)); // find first column number of current box

            // have to loop through two dimensions, thus two for loops
            for (int rs = boxRow * 3; rs < ((boxRow * 3) + 3); rs++)
            {
                for (int clms = boxColumn * 3; clms < ((boxColumn * 3) + 3); clms++)
                {
                    // if you're not at the current position and there is already a curr in the box, return false
                    // or, if you're not at the current position and an empty cell has no pencil marks, return false
                    if ((rs != row && clms != column) && (grid[rs, clms].Item == curr))//|| (grid[rs, clms].Item == 0 && grid[rs, clms].pencilMarks.Count == 0))) //note, skip over position grid['row', 'column']
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //--------------------------------------------------------------------

        public void solve()
        {
            Stack<(int, int, cell)> Q = new Stack<(int, int, cell)>();

            (int, int) loc;
            int row = 0;
            int column = 0;

            // find the first empty cell
            while (grid[row, column].Item != 0)
            {
                row++;
                if (row == 10)
                {
                    row = 0;
                    column++;
                }
            }
            loc = (row, column);

            // take first pencil mark
            int curr = grid[row, column].pencilMarks[0];
            grid[row, column].Item = curr; // assume that pencil mark is proper value
            grid[row, column].pencilMarks.Remove(curr); // remove pencil mark

            // fix pencil marks based on last assumption:

            //TODO - this will break; have to somehow put pencil marked changes in stack


            // if this doesn't break the puzzle, continue
            if (checkRow(row, column) && checkColumn(row, column) && checkBox(row, column))
            {

            }

        }

        public void BruteForce(int i = 1)
        {

            //(int r, int c) loc;
            int row = 0;
            int column = 0;
            int j = 1;

            //=========

            while (row < 9 && column < 9)
            {
                if (j > 9)
                {
                    //Console.WriteLine("aaaaaaaaaaaaaa");
                    (row, column, j) = s.Pop();
                    grid[row, column].Item = 0;
                    j++;

                }
                if (grid[row, column].Item == 0)
                {

                    if (j > 9)
                    {
                        //Console.WriteLine("aaaaaaaaaaaaaa");
                        (row, column, j) = s.Pop();
                        grid[row, column].Item = 0;
                        j++;

                    }
                    grid[row, column].Item = j;
                    s.Push((row, column, j));


                    if (checkRow(row, column) && checkColumn(row, column) && checkBox(row, column))
                    {
                        row++;
                        if (row == 9)
                        {
                            row = 0;
                            column++;
                        }
                        j = 1;
                        // s.Push();
                    }
                    else
                    {
                        (row, column, j) = s.Pop();
                        grid[row, column].Item = 0;
                        j++;

                    }
                }
                else
                {
                    row++;
                    if (row == 9)
                    {
                        row = 0;
                        column++;
                    }
                }
                // back to while
                //Console.WriteLine("-------------------");
                //Console.WriteLine("r: " + row + ", c: " + column + ", j: " + j);
                //Print();

            }
            //========
            /*

                        // find the first empty cell
                        while (grid[row, column].Item != 0)
                        {
                            row++;
                            if (row == 9)
                            {
                                row = 0;
                                column++;
                            }
                        }
                        loc = (row, column);

                        // Start at 1, try every combo
                        //int curr = 1;

                        s.Push(loc);
                        grid[row, column].Item = i;
                        // if this doesn't break the puzzle, continue. // TODO might need while loop here.
                        if (checkRow(row, column) && checkColumn(row, column) && checkBox(row, column))
                        {
                            Console.WriteLine("Baaa");
                            BruteForce();
                            //Print();

                        }
                        else
                        {
                            //Print();
                            Console.WriteLine("------------------------------");
                            BruteForce(i);
                        }
            */

        }
        //-------------------------- PRINT METHODS ---------------------------
        public void Print()
        {
            for (int rs = 0; rs < 9; rs++)
            {
                for (int clms = 0; clms < 9; clms++)
                {
                    Console.Write(grid[rs, clms].Item + " ");

                    if ((clms + 1) % 3 == 0 && clms != 8)
                    {
                        Console.Write("|| ");
                    }
                }
                Console.WriteLine();
                if ((rs + 1) % 3 == 0 && rs != 8)
                {
                    Console.WriteLine("- - - - - - - - - - - -");
                }
            }
            Console.WriteLine();
        }

        // print pencil mark at a location (row and column go from 1-9; ie intuitivly)
        public void PrintPencilMarks(int row, int column)
        {
            // code starts rows and column at 0, so have to subtract 1
            int r = row - 1;
            int c = column - 1;

            Console.Write("Pencil Marks at " + row + ", " + column);

            // look at pencil marks of position grid[r, c]
            Console.WriteLine();
            for (int i = 0; i < grid[r, c].pencilMarks.Count; i++)
            {
                Console.Write(grid[r, c].pencilMarks[i]);
            }
            Console.WriteLine();
        }
        // -------------------------------------------------------------------

    }
}