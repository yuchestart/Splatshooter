using Godot;
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
		if (Input.IsActionJustPressed("hud_pause") && App.GameState == GameStateEnum.InGame)
		{
			App.IsPaused = !App.IsPaused;
			Visible = App.IsPaused;
			if (App.IsPaused && Input.MouseMode == Input.MouseModeEnum.Captured)
			{
				Input.MouseMode = Input.MouseModeEnum.Visible;
			}
		}
	}

	public void OnResumeGamePressed()
	{
		App.IsPaused = false;
		Visible = false;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}
}
