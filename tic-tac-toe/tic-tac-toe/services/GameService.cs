using tic_tac_toe.models;

namespace tic_tac_toe.services;

public static class GameService
{
    public static void RunGame()
    {
        var grid = new List<List<char>>();
        bool isGameOver = false;
        bool isGameWon = false;
        
        grid.Add(['1','2','3']);
        grid.Add(['4','5','6']);
        grid.Add(['7','8','9']);
        
        ConsoleService.Log("welcome to tic tac toe!");
        ConsoleService.Log("lets play!");
        
        ConsoleService.Log(" --- ||| ---");
        ConsoleService.ShowGrid(grid);
        ConsoleService.Log(" --- ||| ---");
        ConsoleService.Log("first of all, lets introduce our players!");
        ConsoleService.Log(" --- ||| ---");
        
        Player player1 = new Player();
        var p1Name = ConsoleService.RecordInput<string>("player 1, please tell us your name.");
        if (p1Name == "!")
        {
            ConsoleService.Log("game aborted! see you next time!");
            return;
        } 
        player1.Name = p1Name;
        
        var p1Type = ConsoleService.RecordInput<char>($"{player1.Name}, please choose your X or 0.")[0];
        if (p1Type == '!')
        {
            ConsoleService.Log("game aborted! see you next time!");
            return;
        }  
        player1.Type = p1Type;
        
        Player player2 = new Player();
        var p2Name = ConsoleService.RecordInput<string>("player 2, please tell us your name.");
        if (p2Name == "!")
        {
            ConsoleService.Log("game aborted! see you next time!");
            return;
        }
        player2.Name = p2Name;
        player2.Type = player1.Type == 'X' ? '0' : 'X';
        
        ConsoleService.Log($"alright! {player2.Name} will play {player2.Type}");
        ConsoleService.Log("lets get started!");

        List<Turn> gameTurns = [];
        Game game = new Game();
        
        while (!isGameOver && !isGameWon)
        {
            Turn turn = new Turn();

            if (!gameTurns.Any())
            {
                turn.Player = player1;
            }
            else
            {
                turn.Player = gameTurns.Last().Player == player1 ? player2 : player1;
            }
            
            ConsoleService.Log(" --- ||| ---");
            ConsoleService.ShowGrid(grid);
            ConsoleService.Log(" --- ||| ---");

            var attemptTurn = ConsoleService.RecordInput<int>($"{turn.Player.Name}, please select a square number to make your move").ToString()[0];
            if (attemptTurn == '!')
            {
                ConsoleService.Log("game aborted! see you next time!");
                return;
            }
            while (!IsValidMove(grid, attemptTurn))
            {
                attemptTurn = ConsoleService.RecordInput<int>($"that square is already taken {turn.Player.Name}, please try again.").ToString()[0];
                if (attemptTurn == '!')
                {
                    ConsoleService.Log("game aborted! see you next time!");
                    return;
                }
            }
            
            grid = MakeMove(grid, turn.Player.Type, attemptTurn);
            
            ConsoleService.Log(" --- ||| ---");
            ConsoleService.ShowGrid(grid);
            ConsoleService.Log(" --- ||| ---");
            
            turn.Position = attemptTurn;
            gameTurns.Add(turn);
            
            isGameWon = IsGameWon(grid);
            isGameOver = IsGameOver(grid);

            if (isGameWon)
            {
                game.FinalTurn = turn;
                game.NumberOfMoves =  gameTurns.Count();
                break;
            }

            if (isGameOver)
            {
                game.FinalTurn = turn;
                game.NumberOfMoves =  gameTurns.Count();
                game.IsDraw = true;
                break;
            }

        }
        
        ConsoleService.PrintGameReport(game, player1, player2);
        ConsoleService.ShowGrid(grid);
            
        return;
    }

    private static bool IsGameOver(List<List<char>> grid)
    {
        if(grid.All(x=> IsRowFull(x))) return true;

        return false;
    }

    private static bool IsGameWon(List<List<char>> grid)
    {
        foreach (var row in grid)
        {
            if(IsThreeMatch(row)) return true;
        } 
        
        if(IsThreeMatch([grid[0][0], grid[1][0], grid[2][0]])) return true;
        if(IsThreeMatch([grid[0][1], grid[1][1], grid[2][1]])) return true;
        if(IsThreeMatch([grid[0][2], grid[1][2], grid[2][2]])) return true;
        
        if(IsThreeMatch([grid[0][0], grid[1][1], grid[2][2]])) return true;
        if(IsThreeMatch([grid[0][2], grid[1][1], grid[2][0]])) return true;
        
        return false;
    }

    private static bool IsThreeMatch(List<char> toMatch)
    {
        if( toMatch.All(x => x == '0'))  return true;
        if( toMatch.All(x => x == 'X'))  return true;
        
        return false;
    }
    private static bool IsRowFull(List<char> row)
    {
        return !row.Any(x=>x != '0' && x != 'X');
    }
    
    private static bool IsValidMove(List<List<char>> grid, char position)
    {
        foreach (var row in grid)
        {
            if (row.Contains(position)) return true;
        }
        
        return false;
    }

    private static List<List<char>> MakeMove(List<List<char>> grid, char type, char position)
    {
        foreach (var row in grid)
        {
            for (var i = 0; i < row.Count; i++)
            {
                if (row[i] == position) row[i] = type;
            }
        }

        return grid;
    }
}