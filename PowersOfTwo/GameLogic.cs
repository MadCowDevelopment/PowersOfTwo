using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PowersOfTwo
{
    public class GameLogic
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public GameLogic(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            Cells = new List<NumberCell>();
            for (int i = 0; i < 16; i++)
            {
                Cells.Add(new NumberCell());
            }

            AddRandomCell();
            AddRandomCell();
        }

        public List<NumberCell> Cells { get; private set; }

        public event Action<int> CellsMatched;

        private void RaiseCellsMatched(int points)
        {
            var handler = CellsMatched;
            if (handler != null) handler(points);
        }

        public void MoveLeft()
        {
            for (int row = 0; row < Rows; row++)
            {
                var emptyCellIndex = row * Columns;
                NumberCell lastCellWithNumber = null;
                for (int column = 0; column < Columns; column++)
                {
                    var currentCellIndex = row * Columns + column;
                    var currentCell = Cells[currentCellIndex];
                    var emptyCell = Cells[emptyCellIndex];

                    if (currentCell.Number == null)
                    {
                        continue;
                    }

                    if (lastCellWithNumber != null && currentCell.Number == lastCellWithNumber.Number)
                    {
                        RaiseCellsMatched(lastCellWithNumber.Number.Value);
                        lastCellWithNumber.Number *= 2;
                        currentCell.Number = null;
                    }
                    else
                    {
                        emptyCell.Number = currentCell.Number;
                        lastCellWithNumber = emptyCell;
                        if (emptyCell != currentCell) currentCell.Number = null;
                        emptyCellIndex++;
                    }
                }
            }

            AddRandomCell();
        }

        public void MoveRight()
        {
            for (int row = Rows - 1; row >= 0; row--)
            {
                var emptyCellIndex = (row + 1) * Columns - 1;
                NumberCell lastCellWithNumber = null;
                for (int column = Columns - 1; column >= 0; column--)
                {
                    var currentCellIndex = row * Columns + column;
                    var currentCell = Cells[currentCellIndex];
                    var emptyCell = Cells[emptyCellIndex];

                    if (currentCell.Number == null)
                    {
                        continue;
                    }

                    if (lastCellWithNumber != null && currentCell.Number == lastCellWithNumber.Number)
                    {
                        RaiseCellsMatched(lastCellWithNumber.Number.Value);
                        lastCellWithNumber.Number *= 2;
                        currentCell.Number = null;
                    }
                    else
                    {
                        emptyCell.Number = currentCell.Number;
                        lastCellWithNumber = emptyCell;
                        if (emptyCell != currentCell) currentCell.Number = null;
                        emptyCellIndex--;
                    }
                }
            }

            AddRandomCell();
        }

        public void MoveUp()
        {
            for (int column = 0; column < Columns; column++)
            {
                var emptyCellIndex = column;
                NumberCell lastCellWithNumber = null;
                for (int row = 0; row < Rows; row++)
                {
                    var currentCellIndex = row * Columns + column;
                    var currentCell = Cells[currentCellIndex];
                    var emptyCell = Cells[emptyCellIndex];

                    if (currentCell.Number == null)
                    {
                        continue;
                    }

                    if (lastCellWithNumber != null && currentCell.Number == lastCellWithNumber.Number)
                    {
                        RaiseCellsMatched(lastCellWithNumber.Number.Value);
                        lastCellWithNumber.Number *= 2;
                        currentCell.Number = null;
                    }
                    else
                    {
                        emptyCell.Number = currentCell.Number;
                        lastCellWithNumber = emptyCell;
                        if (emptyCell != currentCell) currentCell.Number = null;
                        emptyCellIndex += Columns;
                    }
                }
            }

            AddRandomCell();
        }

        public void MoveDown()
        {
            for (int column = Columns - 1; column >= 0; column--)
            {
                var emptyCellIndex = Rows * (Columns - 1) + column;
                NumberCell lastCellWithNumber = null;
                for (int row = Rows - 1; row >= 0; row--)
                {
                    var currentCellIndex = row * Columns + column;
                    var currentCell = Cells[currentCellIndex];
                    var emptyCell = Cells[emptyCellIndex];

                    if (currentCell.Number == null)
                    {
                        continue;
                    }

                    if (lastCellWithNumber != null && currentCell.Number == lastCellWithNumber.Number)
                    {
                        RaiseCellsMatched(lastCellWithNumber.Number.Value);
                        lastCellWithNumber.Number *= 2;
                        currentCell.Number = null;
                    }
                    else
                    {
                        emptyCell.Number = currentCell.Number;
                        lastCellWithNumber = emptyCell;
                        if (emptyCell != currentCell) currentCell.Number = null;
                        emptyCellIndex -= Columns;
                    }
                }
            }

            AddRandomCell();
        }

        private void AddRandomCell()
        {
            if (Cells.Any(p => p.Number == null))
            {
                NumberCell randomCell;
                do
                {
                    randomCell = Cells[RNG.Next(0, 16)];
                } while (randomCell.Number != null);

                randomCell.Number = 2;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}