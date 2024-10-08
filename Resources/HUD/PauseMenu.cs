﻿using Godot;
using System;

public partial class PauseMenu : Control
{

	private FlowContainer pauseUI;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pauseUI = GetNode<FlowContainer>("PauseUI");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("hud_pause") && App.GameMode == GameModeEnum.InGame && !App.IsPaused)
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{

		//GD.Print(Input.IsActionJustPressed("hud_pause")," ",App.GameMode);

		App.IsPaused = !App.IsPaused;
        Visible = App.IsPaused;
        if (App.IsPaused)
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
        else
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }
		//GD.Print(Input.MouseMode);
    }

	public void OnResumeGamePressed()
	{
		//GD.Print("Oh boy.");
		TogglePause();
	}
}
