using Godot;
using System;

public partial class Player : CharacterBody3D
{

	[Export]
	private float MoveForce = 1.0f;

	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		PlayerMovement(delta);


	}

	public void PlayerMovement(double delta)
	{
		if (Input.IsActionPressed("movement_move_forward"))
		{
			this.ApplyForce(new Vector3(0.0f, 0.0f, -1 * MoveForce));
		} else if (Input.IsActionPressed("movement_move_backward"))
		{
			this.ApplyForce(new Vector3(0.0f, 0.0f, MoveForce));
		}
		if (Input.IsActionPressed("movement_move_left"))
		{
			this.ApplyForce(new Vector3(-1 * MoveForce, 0.0f, 0.0f));
		}
        if (Input.IsActionPressed("movement_move_right"))
        {
            this.ApplyForce(new Vector3(MoveForce, 0.0f, 0.0f));
        }
		if (Input.IsActionPressed("movement_jump"))
		{
			this.ApplyForce(new Vector3(0.0))
		}
    }
}
