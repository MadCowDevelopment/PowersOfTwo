﻿using System.Windows.Input;

namespace PowersOfTwo
{
    public interface IMovementCommandProvider
    {
        ICommand DownCommand
        {
            get;
        }

        ICommand LeftCommand
        {
            get;
        }

        ICommand RightCommand
        {
            get;
        }

        ICommand UpCommand
        {
            get;
        }
    }
}