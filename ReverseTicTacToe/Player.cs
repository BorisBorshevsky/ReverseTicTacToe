using System;
using System.Collections.Generic;
using System.Text;
using ReverseTicTacToeLogic;

namespace ReverseTicTacToe
{
    public class Player
    {
        public Player(ePlayerType i_PlayerType, eSymbol i_Symbol)
        {
            PlayerType = i_PlayerType;
            Symbol = i_Symbol;
        }
        
        
        public ePlayerType PlayerType { get; set; }


        public eSymbol Symbol { get; set; }

    }
}
