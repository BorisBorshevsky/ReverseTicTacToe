namespace ReverseTicTacToeLogic
{
    public class Player
    {
        public Player(ePlayerType i_PlayerType, eSymbol i_Symbol, string i_PlayerName)
        {
            PlayerType = i_PlayerType;
            Symbol = i_Symbol;
            PlayerName = i_PlayerName;
        }

        public void AddWinToScore()
        {
            Score++;
        }

        public int Score { get; private set; }

        public string PlayerName { get; private set; }

        public ePlayerType PlayerType { get; private set; }

        public eSymbol Symbol { get; private set; }
    }
}
