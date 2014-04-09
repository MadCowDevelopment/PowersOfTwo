using System;
using System.Collections.Generic;
using System.Linq;

namespace PowersOfTwo.Core
{
    public class GameLogic
    {
        #region Constructors

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

        #endregion Constructors

        #region Events

        public event Action<int> CellsMatched;

        public event Action OutOfMoves;

        #endregion Events

        #region Public Properties

        public List<NumberCell> Cells
        {
            get;
            private set;
        }

        public int Columns
        {
            get;
            set;
        }

        public int Rows
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        public void MoveDown()
        {
            bool movedCell = false;
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
                        movedCell = true;
                    }
                    else
                    {
                        emptyCell.Number = currentCell.Number;
                        lastCellWithNumber = emptyCell;
                        if (emptyCell != currentCell)
                        {
                            movedCell = true;
                            currentCell.Number = null;
                        }
                        emptyCellIndex -= Columns;
                    }
                }
            }

            if (movedCell)
            {
                AddRandomCell();
            }

            CheckOutOfMoves();
        }

        public void MoveLeft()
        {
            bool movedCell = false;
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
                        movedCell = true;
                    }
                    else
                    {
                        emptyCell.Number = currentCell.Number;
                        lastCellWithNumber = emptyCell;
                        if (emptyCell != currentCell)
                        {
                            currentCell.Number = null;
                            movedCell = true;
                        }
                        emptyCellIndex++;
                    }
                }
            }

            if (movedCell)
            {
                AddRandomCell();
            }

            CheckOutOfMoves();
        }

        public void MoveRight()
        {
            bool movedCell = false;
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
                        movedCell = true;
                    }
                    else
                    {
                        emptyCell.Number = currentCell.Number;
                        lastCellWithNumber = emptyCell;
                        if (emptyCell != currentCell)
                        {
                            currentCell.Number = null;
                            movedCell = true;
                        }
                        emptyCellIndex--;
                    }
                }
            }

            if (movedCell)
            {
                AddRandomCell();
            }

            CheckOutOfMoves();
        }

        public void MoveUp()
        {
            bool movedCell = false;
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
                        movedCell = true;
                    }
                    else
                    {
                        emptyCell.Number = currentCell.Number;
                        lastCellWithNumber = emptyCell;
                        if (emptyCell != currentCell)
                        {
                            currentCell.Number = null;
                            movedCell = true;
                        }
                        emptyCellIndex += Columns;
                    }
                }
            }

            if (movedCell)
            {
                AddRandomCell();
            }

            CheckOutOfMoves();
        }

        private void CheckOutOfMoves()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns - 1; column++)
                {
                    var cellIndex = row*Columns + column;
                    var cell = Cells[cellIndex];
                    var nextCell = Cells[cellIndex + 1];

                    if (cell.Number == null || nextCell.Number == null) return;
                    if (cell.Number == nextCell.Number) return;
                }
            }

            for (int column = 0; column < Columns; column++)
            {
                for (int row = 0; row < Rows-1; row+=Columns)
                {
                    var cellIndex = row * Columns + column;
                    var cell = Cells[cellIndex];
                    var nextCell = Cells[cellIndex + Columns];

                    if (cell.Number == null || nextCell.Number == null) return;
                    if (cell.Number == nextCell.Number) return;
                }
            }

            RaiseOutOfMoves();
        }

        #endregion Public Methods

        #region Private Methods

        private void AddRandomCell()
        {
            NumberCell randomCell;
            do
            {
                randomCell = Cells[RNG.Next(0, 16)];
            } while (randomCell.Number != null);

            randomCell.Number = 2;
        }

        private void RaiseCellsMatched(int points)
        {
            var handler = CellsMatched;
            if (handler != null) handler(points);
        }

        private void RaiseOutOfMoves()
        {
            var handler = OutOfMoves;
            if (handler != null) handler();
        }

        #endregion Private Methods
    }
}