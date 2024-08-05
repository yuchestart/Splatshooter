using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: Come up with a better name for this
public enum GameStateEnum
{
    Menu,
    InGame
}

//This class pretty much stores the app's state.
public static class App
{
    public static bool IsPaused = false;

    //TODO: Come up with a better name for this variable.
    public static GameStateEnum GameState = GameStateEnum.InGame;

}


