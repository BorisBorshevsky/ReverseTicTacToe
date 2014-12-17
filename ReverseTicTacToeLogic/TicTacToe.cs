using System.Drawing;
using ReverseTicTacToeLogic.Algorithms;

namespace ReverseTicTacToeLogic
{
    public class TicTacToe
    {
        private readonly ScoreBoard r_scoreBoard;

        public Board Board { get; private set; }

        public TicTacToe(int i_Size, Player i_Player1, Player i_Player2)
        {
            r_scoreBoard = new ScoreBoard(i_Player1, i_Player2);
            Board = new Board(i_Size);
            Board.InitializeBoard();
        }

        public eCellState TryPlayTurn(Player i_Player)
        {
            eSymbol opponentSymbol = i_Player.Symbol == eSymbol.O ? eSymbol.X : eSymbol.O;
            Point computerMove = ArtificialIntelligenceAlgorithm.GetMove(Board, i_Player.Symbol, opponentSymbol);
            
            return TryPlayTurn(computerMove, i_Player);
        }

        public eCellState TryPlayTurn(Point i_Coordinates, Player i_Player)
        {
            eCellState cellState = Board.GetCellState(i_Coordinates);
          
            if (cellState == eCellState.Empty)
            {
                Board.SetSymbol(i_Player.Symbol, i_Coordinates);
            }

            if (Board.HasWinner())
            {
                AddScoreToOpponent(i_Player);
            }

            return cellState;
        }

        private void AddScoreToOpponent(Player i_CurrentPlayer)
        {
            if (r_scoreBoard.GetScores().Player1 == i_CurrentPlayer)
            {
                r_scoreBoard.AddWinToPlayer2();
            }
            else
            {
                r_scoreBoard.AddWinToPlayer1();
            }
        }

        public ScoreBoard.Scores GetScores()
        {
            return r_scoreBoard.GetScores();
        }

        public void Surrender(Player i_Player)
        {
            AddScoreToOpponent(i_Player);
        }
    }
}