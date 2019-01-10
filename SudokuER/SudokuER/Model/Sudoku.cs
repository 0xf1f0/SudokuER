using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SudokuER.Model
{
    /*
     * C# implementation of the sudoku notation (in Python) by Peter Norvig
     * http://norvig.com/sudoku.html
     */

    public class Sudoku
    {
        public static string Digits => "123456789";
        public static string Rows => "ABCDEFGHI";
        public static string Columns => Digits;
        public static List<string> Squares => Cross(Rows, Columns);
        public static List<List<string>> UnitList => GetUnitList();
        public static List<KeyValuePair<string, List<string>>> UnitKeyValuePairs => GetUnitKeyValuePairs();
        public static List<KeyValuePair<string, List<string>>> Peers => GetPeers();

        private static List<string> Cross(string a, string b)
        {
            //Cross the product of elements in a and b
            var list = new List<string>();
            foreach (var i in a)
            {
                foreach (var j in b)
                {
                    list.Add($"{i}{j}");
                }
            }

            return list;
        }

        private static List<List<string>> GetUnitList()
        {
            var list = new List<List<string>>();
            var rowList = new List<string>
            {
                "ABC",
                "DEF",
                "GHI"
            };

            var columnList = new List<string>
            {
                "123",
                "456",
                "789"
            };

            foreach (var col in Columns)
            {
                var cross1 = Cross(Rows, col.ToString());
                list.Add(cross1);
            }

            foreach (var row in Rows)
            {
                var cross2 = Cross(row.ToString(), Columns);
                list.Add(cross2);
            }

            foreach (var row in rowList)
            {
                foreach (var col in columnList)
                {
                    var cross3 = Cross(row, col);
                    list.Add(cross3);
                }
            }

            return list;
        }

        private static List<KeyValuePair<string, List<string>>> GetUnitKeyValuePairs()
        {
            var result = new List<KeyValuePair<string, List<string>>>();

            foreach (var unit in UnitList)
            {
                foreach (var square in Squares)
                {
                    if (unit.Contains(square))
                    {
                        result.Add(new KeyValuePair<string, List<string>>(square, unit));
                    }
                }
            }

            return result;
        }

        private static List<KeyValuePair<string, List<string>>> GetPeers()
        {
            var result = new List<KeyValuePair<string, List<string>>>();
            var temp = UnitKeyValuePairs;

            foreach (var square in Squares)
            {
                var res = temp.Where(x => x.Key.Equals(square));
                var union = res.SelectMany(x => x.Value)
                    .Distinct()
                    .Except(new List<string> { square })
                    .ToList();
                result.Add(new KeyValuePair<string, List<string>>(square, union));
            }

            return result;
        }
    }
}