using System.Text.RegularExpressions;
using tic_tac_toe.models;

namespace tic_tac_toe.services;

public static class ConsoleService
{
    public static void Log(string log)
    {
        Console.WriteLine(log);
    }

    public static void PrintGameReport(Game game, Player player1, Player player2)
    {
        if (game.IsDraw)
        {
            Console.WriteLine("game is over! we have a draw!");
        }
        else
        {
            Console.WriteLine($"game is over! we have a winner - {game.FinalTurn.Player.Type}, played by {game.FinalTurn.Player.Name}!");
        }
        
        Console.WriteLine($"game report: " +
                          $"\nplayers - " +
                          $"\n{player1.Type}, played by {player1.Name}" +
                          $"\n{player2.Type}, played by {player2.Name}" +
                          $"\n\nnumber of turns - " +
                          $"\n{game.NumberOfMoves}\n");
        Console.WriteLine("hope you had a good time. thanks for playing and see you next time!");
    }

    public static string RecordInput<TOut>(string message)
    {
        string playerInput = string.Empty;
        string prompt = message;
        
        while (string.IsNullOrEmpty(playerInput))
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                prompt = "invalid input received. please try again.";
                continue;
            }
            
            if (input.ToLower() == "exit")
            {
                playerInput = "!";
                break;
            }
            
            if(typeof(TOut) == typeof(string))
            {
                playerInput = input;
                break;
            }
            else if (typeof(TOut) == typeof(int))
            {
                if (input.Length != 1 || !Regex.IsMatch(input, @"^[0-9]{1}$") || !int.TryParse(input, out var intInput))
                {
                    prompt = "invalid input received. please input X or 0.";
                    continue;
                }

                playerInput = input;
                break;
            }
            else if (typeof(TOut) == typeof(char))
            {
                if (input.Length != 1 || !Regex.IsMatch(input, @"^[X0]{1}$"))
                {
                    prompt = "invalid input received. please input X or 0.";
                    continue;
                }

                playerInput = input;
                break;
            }
            throw new ArgumentOutOfRangeException($"invalid return type {typeof(TOut)}");
        }
        
        return playerInput;
    }
    
    public static void ShowGrid(List<List<char>> grid)
    {
        Console.WriteLine($" {grid[0][0]} | {grid[0][1]} | {grid[0][2]}");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {grid[1][0]} | {grid[1][1]} | {grid[1][2]}");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {grid[2][0]} | {grid[2][1]} | {grid[2][2]}");
    }
}