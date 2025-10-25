namespace tic_tac_toe.models;

public class Turn
{
    public Player Player { get; set; } = default!;   
    public char Position { get; set; } = '-';
}