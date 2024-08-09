using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum GameModeEnum
{
    Menu,
    InGame
}

//This class pretty much stores the app's state.
public static class App
{
    public static bool IsPaused = false;

    public static GameModeEnum GameMode = GameModeEnum.InGame;

}

