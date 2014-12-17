namespace ReverseTicTacToeLogic
{
    public enum ePlayerType
    {
        User = 1,
        Computer = 2
    }

    public enum eCellState
    {
        Empty,
        Used,
        OutOfRange
    }

    public enum eGameState
    {
        BoardFull,
        HasWinner,
        Active
    }

    public enum eSymbol
    {
        Blank,
        X,
        O
    }
}