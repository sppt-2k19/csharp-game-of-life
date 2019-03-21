using System;
using System.Collections.Generic;
using System.Linq;

namespace c_sharpGameOfLife
{
    class Program
    {
        
        static int ComputeNeighbours(int x, int y, int width, int height, string[] lines)
        {
            var arr = new[]
            {
                (-1, -1), (0, -1), (1, -1),
                (-1, 0),           (1,  0),
                (-1, 1),  (0,  1), (1,  1)
            };

            return arr.Select(t =>
            {
                var (dx, dy) = t;
                int nx = x + dx, ny = y + dy;
                if (nx >= 0 && nx < width && ny >= 0 && ny < height &&
                    lines[ny][nx] == '1')
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }).Sum();
        }

        static char Life(int x, int y, char c, int width, int height, string[] lines)
        {
            switch ((c, ComputeNeighbours(x, y, width, height, lines)))
            {
                case var tuple when tuple == ('1', 2):
                    return c;
                case var tuple when tuple.Item2 == (2):
                    return '1';
                default:
                    return '0';
            }
        }
        
        static string IterateGrid(string grid)
        {
            var lines = grid.Split(new []{'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            var width = lines.FirstOrDefault().Length;
            var height = lines.Count();

            

            var newLines = new string[height];

            for (var y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                var chars = line.ToCharArray();
                var values = new char[chars.Length];
                for (var x = 0; x < chars.Length; x++)
                {
                    values[x] = Life(x, y, chars[x], width, height, lines);
                }

                newLines[y] = new string(chars);
            }

            return string.Join("\r\n", newLines);
        }

        static void Main(string[] args)
        {
            string grid = (@"
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000100000000000000000000000000000000
00000000000000000000000000000000000000000000010100000000000000000000000000000000
00000000000000000000000000000000000110000001100000000000011000000000000000000000
00000000000000000000000000000000001000100001100000000000011000000000000000000000
00000000000000000000000011000000010000010001100000000000000000000000000000000000
00000000000000000000000011000000010001011000010100000000000000000000000000000000
00000000000000000000000000000000010000010000000100000000000000000000000000000000
00000000000000000000000000000000001000100000000000000000000000000000000000000000
00000000000000000000000000000000000110000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000");

            for (var i = 0; i < 200; i++)
            {
                Console.Clear();
                grid = IterateGrid(grid);
                Console.WriteLine(grid);
            }
            
        }
    }
}