namespace tic_tac_toe.models;

public class Game
{
    public Turn FinalTurn { get; set; } = default!;
    public bool IsDraw { get; set; } = false;
    public int NumberOfMoves { get; set; }
}