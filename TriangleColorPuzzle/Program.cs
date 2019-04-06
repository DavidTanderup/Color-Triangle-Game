using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleColorPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Rules only like colored dots can touch each other.            


            /// Colors: Total Quantity
            /// Blue: 8, Green: 14, Red: 8, Black: 10, Yellow: 8, White: 12
            /// Sides: 3 / Dots per side: 4
            /// Pieces: 16 / Dots per piece
            /// 
            /// create the board
            /// Define the positions on the board
            /// Define the pieces

            /// Problem: there are three possible color positions for each of the 16 piece positions
            /// There are a lot of possible combinations

            /// TODO: Create piece has already been used method. See Chapter 10 using arrays

            TryingThePieces tryingThePieces = new TryingThePieces();
            tryingThePieces.sample();
            Console.ReadLine();
        }
    }

    // establishes a set range of colors that can be compared in a boolian statement
    // Black, Blue, Green, Red ,White, Yellow

    public enum color { Black, Blue, Green, Red, White, Yellow }

    public struct Piece
    {
        public color Dot1;
        public color Dot2;
        public color Dot3;
        public color Dot4;

        // constructor that will create the puzzle piece
        public Piece(color dot1, color dot2, color dot3)
        {
            Dot1 = dot1;
            Dot2 = dot2;
            Dot3 = dot3;
            Dot4 = dot1;
        }

        // constructor creates the sides of the puzzle.
        public Piece(color dot1, color dot2, color dot3, color dot4)
        {
            Dot1 = dot1;
            Dot2 = dot2;
            Dot3 = dot3;
            Dot4 = dot4;
        }


    }



    /// <summary>
    /// This class describes the physical structure of the puzzle. There are three major elements that make up the game.
    /// 
    /// 1) The playing piece which is a triangle that contains three colored dots. One dot for every side.
    /// 2) The walls which provide a physical structure for the game. Each wall has four colored dots.
    /// 3) The playing position. There are sixteen triangle pieces which are placed together to form a single collective triangle.
    ///    the playing position is the space on the board that a triangle must go to complete the collective triangle
    /// </summary>
    public class GameBoard
    {
        /// <summary>
        /// Method returns a list that holds the pieces to the puzzle. Pieces are defined by the Piece Struct.
        /// </summary>
        /// <returns></returns>
        public List<Piece> PlayingPiece()
        {
            // Black, Blue, Green, Red ,White, Yellow
            // creating pieces with new instances of the Piece Struct
            // position || dot 1 will be the determined alphabetically then will move clockwise
            Piece piece1 = new Piece(color.Blue, color.Blue, color.White);
            Piece piece2 = new Piece(color.Blue, color.White, color.White);
            Piece piece3 = new Piece(color.Black, color.White, color.Blue); 
            Piece piece4 = new Piece(color.Blue, color.Yellow, color.Green);
            Piece piece5 = new Piece(color.Black, color.Yellow, color.Green);
            Piece piece6 = new Piece(color.Black, color.Blue, color.Yellow);
            Piece piece7 = new Piece(color.Green, color.White, color.Red);
            Piece piece8 = new Piece(color.Black, color.Red, color.Green);
            Piece piece9 = new Piece(color.Black, color.Black, color.Green);
            Piece piece10 = new Piece(color.Green, color.Yellow, color.White);
            Piece piece11 = new Piece(color.Red, color.White, color.Yellow);
            Piece piece12 = new Piece(color.Black, color.White, color.Blue);
            Piece piece13 = new Piece(color.Green, color.Yellow, color.Red);
            Piece piece14 = new Piece(color.Black, color.Green, color.Red);
            Piece piece15 = new Piece(color.Green, color.Yellow, color.White);
            Piece piece16 = new Piece(color.Black, color.Green, color.Red);

            // List containing all the puzzle pieces
            List<Piece> GamePieces = new List<Piece>() {piece1,piece2,piece3,piece4,piece5,piece6,piece7,piece8, piece9,
                                                        piece10,piece11,piece12,piece13,piece14,piece15,piece16 };


            return GamePieces;
        }


        /// <summary>
        /// Method returns a list containing the sides or wall of the game. The corners of the triangle are labled A,B,C. The triangle is set with A as the apex, B as left base, C right base.
        /// The sides are labled AB, AC, BC. The index of the colors starts from left to right when the side of the triangle is viewed horizontally and the first letter of the side lable is on the left.
        /// </summary>
        /// <returns></returns>
        public List<Piece> Sides()
        {

            Piece SideAB = new Piece(color.White, color.Red, color.White, color.Yellow);
            Piece SideAC = new Piece(color.Blue, color.Red, color.Green, color.Black);
            Piece SideBC = new Piece(color.Green, color.Green, color.White, color.Green);


            List<Piece> sides = new List<Piece>() { SideAB, SideAC, SideBC };

            return sides;
        }

    }

    public class TryingThePieces
    {
        public const int numPieces = 16;

        public void sample()
        {
            // corner lists will contain the pieces for each phase that can possibly fit that section
            List<Tuple<Piece,color>> cornerA = new List<Tuple<Piece,color>>();
            List<Piece> possiblePieces = new List<Piece>(); //pieces that fit the match criteria

            GameBoard gameBoard = new GameBoard();
            List<Piece> pieces = gameBoard.PlayingPiece();
            List<Piece> sides = gameBoard.Sides();

            Piece SideAB = sides[0];
            Piece SideAC = sides[1];
            Piece SideBC = sides[2];

            // evaluate each piece
            // point 1 and point 2 == comparison values
            color Point1 = SideAC.Dot4;
            color Point2 = SideBC.Dot4;
            for (int index = 0; index < pieces.Count; index++)
            {
                if (pieces[index].Dot1 == Point1 && pieces[index].Dot2 == Point2)
                {
                    cornerA.Add(new Tuple<Piece,color> (pieces[index],pieces[index].Dot3));
                }

                else if (pieces[index].Dot2 == Point1 && pieces[index].Dot3 == Point2)
                {
                    cornerA.Add(new Tuple<Piece, color>(pieces[index], pieces[index].Dot1));
                }


                else if (pieces[index].Dot3 == Point1 && pieces[index].Dot1 == Point2)
                {
                    cornerA.Add(new Tuple<Piece, color>(pieces[index], pieces[index].Dot2));
                }
            }
            // pieces are in corner A
            foreach (var piece in cornerA)
            {
                Console.WriteLine($"Piece: {piece.Item1.Dot1} || {piece.Item1.Dot2} || {piece.Item1.Dot3}");
            }
            /// TODO: Relable pieces
        }





    }


}
