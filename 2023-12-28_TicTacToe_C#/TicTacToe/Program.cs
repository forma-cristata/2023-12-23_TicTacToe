/*
 * Name: Kaci Craycraft
 * South Hills Username: kcraycraft45
 */

using System.Numerics;
namespace TicTacToe
{
    public class Program
    {
        public const int BOARD_SIZE = 3;
        public static char[][] board = new char[BOARD_SIZE][];
        public static void Main()
        {
            char player1 = 'X';
            int j = 0;
            char player2 = 'O';

            board = PrepareBoard();
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintBoard();
            for (int i = 0; i < (BOARD_SIZE * BOARD_SIZE); i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                if (TheGame("Player 1", player1)) break;
                j++;
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (TheGame("Player2", player2)) break;
                j++;
            }
            int k = j % 2;
            Console.ForegroundColor = ConsoleColor.Green;
            switch(k)
            {
                case 0:
                    Console.WriteLine("Player 1 won!!!");
                    break;
                case 1:
                    Console.WriteLine("Player 2 won!!!");
                    break;
            }
        }
        public static char[][] PrepareBoard()
        {
            char[][] board = new char[BOARD_SIZE][];

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                board[i] = new char[BOARD_SIZE];
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    board[i][j] = ' ';
                }
            }
            return board;
        }
        public static void PrintBoard()
        {//Argument is the current board, it should print every time somebody plays
            for (int i = 0; i < BOARD_SIZE; i++)//Board_SIZE = 3
            {//" 1 2 3"
                Console.Write(" " + (i + 1));
            }
            //"1 | | "
            //" -----"
            //"2 | | "
            //" -----"
            //"3 | | "
            //This is what this section prints \/
            Console.Write(Environment.NewLine);
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                Console.WriteLine((i + 1) + String.Join('|', board[i]));
                //if i < 2
                //Write Line " " + "-"*5
                //Between each row
                if (i < BOARD_SIZE - 1)
                {
                    Console.WriteLine(" " + new String('-', BOARD_SIZE * 2 - 1));
                    //new String(char , count)
                }
            }
        }
        public static bool TheGame(String message, char player)
        {
            int[] chosenCoordinates = new int[2];//Their chosen coordinates are an array of length 2

            (chosenCoordinates[0], chosenCoordinates[1]) = ChooseYourSpot(message);
            board[chosenCoordinates[0] - 1][chosenCoordinates[1] - 1] = player;//The coordinate they choose is one more than 'computer speak'.  Here we print their X or O
            Console.Clear();//Clear the console after the user has chosen new coordinates
            PrintBoard();//Then immediately reprint the updated board
            Thread.Sleep(1000);
            return IsWinner();//Method checks all verts diags and horzs for winners
        }
        public static (int, int) ChooseYourSpot(String player)
        {
            bool[] isValidisInRangeisRightLength = new bool[4];//Allows four booleans to be checked....
            int[] coordinatesAsIntegers = new int[2];//Their choice of coordinates as integers

            (isValidisInRangeisRightLength, coordinatesAsIntegers) = InputValidationRepeatedCode(player + ", please input your selection, separated by a comma, in an open space: ");
            while (!isValidisInRangeisRightLength[0] || !isValidisInRangeisRightLength[1] || !isValidisInRangeisRightLength[2] || !isValidisInRangeisRightLength[3])
            {//If any of the booleans are not true, then the input is invalid.
               
                (isValidisInRangeisRightLength, coordinatesAsIntegers) = InputValidationRepeatedCode("Invalid input, please enter coordinates within 1 - 3 separated by a comma!\nMake sure the space isn't taken....\nWhere would you like to place your piece? ");
            }//Keep looping until the input is valid
            return (coordinatesAsIntegers[0], coordinatesAsIntegers[1]);//return the valid chosen coordinates in integer form
        }
        public static (bool[], int[]) InputValidationRepeatedCode(String message)
        {
            String userInput;
            bool isValidInput = true;
            bool isInputWithinRange = true;
            bool isInputTheRightLength = true;
            bool isSpaceTaken = true;

            Console.Write(message);
            userInput = Console.ReadLine();
            String[] coordinates = userInput.Split(',');//Split their input by commas
            int[] coordinatesAsIntegers = new int[coordinates.Length];//Need an array as integers rather than string
            for (int i = 0; i < coordinates.Length; i++)//For each coordinate that the user chose
            {
                isValidInput = int.TryParse(coordinates[i], out coordinatesAsIntegers[i]);//Try to parse their inputs into integers
                if (isValidInput)
                {//If they are integers
                    if (coordinatesAsIntegers[i] < 1 || coordinatesAsIntegers[i] > 3)//Then are they out of range
                    {
                        isInputWithinRange = false;
                        break;
                    }
                }
                //ELSE// BREAK??
            }
            if (coordinates.Length != 2)//If they put in more than two coordinates
            {
                isInputTheRightLength = false;//They put in too many coordinates
            }
            if (isValidInput && isInputWithinRange && isInputTheRightLength && !String.IsNullOrWhiteSpace(board[coordinatesAsIntegers[0] - 1][coordinatesAsIntegers[1] - 1].ToString()))
            {//If their input is only two integers that are in range and tha space is not taken, then it is a valid input
                isSpaceTaken = false;//Can only check if space is taken if all other bools are true.
            }
            bool[] isValidInRangeisRightLength = { isValidInput, isInputWithinRange, isInputTheRightLength, isSpaceTaken };
            return (isValidInRangeisRightLength, coordinatesAsIntegers);
        }
        public static bool IsWinner()
        {
            // Check verticals
            bool verticalWin = true;
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                verticalWin = true;
                if (board[0][i] == ' ')
                {
                    verticalWin = false;
                }
                else
                {
                    for (int j = 1; j < BOARD_SIZE; j++)
                    {
                        if (board[j][i] != board[0][i])
                        {
                            verticalWin = false;
                            break;
                        }
                    }
                    if(verticalWin)
                    {
                        break;
                    }
                }
            }
            // Check horizontals
            bool horizontalWin = true;
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                horizontalWin = true;
                if (board[i][0] == ' ')
                {
                    horizontalWin = false;
                }
                else
                {
                    for (int j = 1; j < BOARD_SIZE; j++)
                    {
                        if (board[i][j] != board[i][0])
                        {
                            horizontalWin = false;
                            break;
                        }
                    }
                    if (horizontalWin)
                    {
                        break;
                    }
                }
            }
            // Check diagonals   
            bool diagonalWin = true;
            char mainDiagonalFirst = board[0][0];
            //board [0][0] is the top left element of the board
            for (int i = 1; i < BOARD_SIZE; i++) //iterate three times
            {
                if (board[i][i] == ' ' || board[i][i] != mainDiagonalFirst)
                {//1,1 or 2,2
                    //1,1 is the middle spot.
                    //1,1 must match 0,0 if the diagonal is top left to bottom right
                    //If this is not true, then does the middle spot match the top left? 
                    //if not, then there is no top left to bottom right diagonal
                    //2,2 is next
                    //2,2 is the bottom right spot.  
                    // Gotta check that someone played there,
                    //If they did, then does it match the top left spot?
                    //If not, then there is no diagonal win
                    diagonalWin = false;
                    break;
                }
            }
            if (diagonalWin) return true;// return diagonalWin only if true -- breakpoint
                                         //We only checked the top left to bottom right diagonal win so far.
                                         //Now we need to check the other possibility
            diagonalWin = true;
            //here we do the exact same thing, but for the bottom left to top right diagonal
            char antiDiagonalFirst = board[0][BOARD_SIZE - 1];
            for (int i = BOARD_SIZE - 2; i >= 0; i--)
            {
                if (board[BOARD_SIZE - i - 1][i] == ' ' || board[BOARD_SIZE - i - 1][i] != antiDiagonalFirst)
                {
                    diagonalWin = false;
                    break;
                }
            }
            return (diagonalWin || horizontalWin || verticalWin);
        }
        
        
    }
}
