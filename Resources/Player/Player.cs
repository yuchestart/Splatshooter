using Godot;
using System;

public partial class Player : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{



	}

	public void PlayerMovement(double delta)
	{
		if (Input.IsActionPressed("movement_move_forward"))
		{
			this.ApplyForce(new Vector3(0.0f, 0.0f, 1.0f));
		} else if (Input.IsActionPressed("movement_move_backward"))
		{
			this.ApplyForce(new Vector)
		}
	}
}
