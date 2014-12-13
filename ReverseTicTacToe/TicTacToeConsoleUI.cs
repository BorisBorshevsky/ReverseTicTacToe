﻿using ReverseTicTacToeLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseTicTacToe
{
    static class TicTacToeConsoleUI
    {
        private static TicTacToe m_game;
        
        public static void Start()
        {
            int size = getBoardSize();
            m_game = new TicTacToe(size);
            playerType playerType = getOpponentType();

            while(true)
            {
                
            }

        }

        private static int getBoardSize()
        {
            Console.WriteLine("Enter board size (3-9)");
            char input = Console.Read();
            while (!Char.IsNumber(input) || Char.GetNumericValue(input) < 3)
            {
                Console.WriteLine("Invalid input, Enter board size (3-9)");
                char input = Console.Read();
            }

            return Char.GetNumericValue(input);
        }

        private static playerType getOpponentType()
        {
            Console.WriteLine("Enter player type (1 = human, 2 = PC)");
            char input = Console.Read();
            while (!Char.IsNumber(input) || Char.GetNumericValue(input) < 1 || Char.GetNumericValue(input) > 2)
            {
                Console.WriteLine("Invalid input, Enter player type (1 = human, 2 = PC)"");
                char input = Console.Read();
            }

            return (playerType)Char.GetNumericValue(input);
        }

        private static void printBoard()
        {
            int rowLength = m_game..GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", arr[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        private static void printScores()
        {

        }

        private static void getMoveFromUser()
        {
            // add q to quit
        }

        private static void printWinner()
        {

        }

        private static void isToStartAnotherGame()
        {
        }

    }
}