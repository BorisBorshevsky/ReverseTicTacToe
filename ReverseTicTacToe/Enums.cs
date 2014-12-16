namespace ReverseTicTacToe
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
        Invalid
    }

    public enum eGameState
    {
        BoardFull,
        HasWinner,
        Active
    }
}