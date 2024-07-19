using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export]
	public bool current = false;
	[Export]
	public bool mouseActivated = false;

	[Export]
	private float moveSpeed = 5.0f;
	[Export]
	private float jumpForce = 10.0f;
	[Export]
	private float gravityAcceleration = -ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	[Export]
	private float mouseSensitivity = 0.1f;

	private Camera3D camera;
	private Vector3 _velocity = Vector3.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (current)
		{
			camera = GetNode<Camera3D>("Camera");
			camera.Current = true;
		}
	}

    public override void _Input(InputEvent @event)
    {
		if (current && mouseActivated)
		{
			PlayerRotation(@event);
		}
    }

    public override void _PhysicsProcess(double delta)
    {
		_velocity.X = 0;
		_velocity.Z = 0;

		if (current)
		{
			PlayerMovement(delta);
		} else
		{
			MultiplayerMovement(delta);
			MultiplayerRotation(delta);
		}

		if(!IsOnFloor())
		{
			_velocity.Y += gravityAcceleration * (float) delta;
		} else
		{
			_velocity.Y = 0;
			if (Input.IsActionJustPressed("movement_jump"))
			{
				_velocity.Y = jumpForce;
			}
		}

		Velocity = GlobalTransform.Basis * _velocity;

		MoveAndSlide();
    }

    #region Client movement

    private void PlayerMovement(double delta)
	{
		if (Input.IsActionPressed("movement_forward"))
		{
			_velocity.Z -= moveSpeed;
		}
        if (Input.IsActionPressed("movement_backward"))
        {
            _velocity.Z += moveSpeed;
        }
        if (Input.IsActionPressed("movement_left"))
        {
            _velocity.X -= moveSpeed;
        }
        if (Input.IsActionPressed("movement_right"))
        {
            _velocity.X += moveSpeed;
        }
    }

	private void PlayerRotation(InputEvent @event)
	{
        if (@event is InputEventMouseMotion mouseEvent)
        {
            RotateY(Mathf.DegToRad(-mouseEvent.Relative.X * mouseSensitivity));
            camera.RotateX(Mathf.DegToRad(-mouseEvent.Relative.Y * mouseSensitivity));
            Vector3 cameraRotation = camera.Rotation;
			cameraRotation.X = Mathf.Clamp(cameraRotation.X, -Mathf.Pi/2, Mathf.Pi/2);
			camera.Rotation = cameraRotation;
        }
    }

    #endregion

    #region Multiplayer movement
    private void MultiplayerMovement(double delta) { }

	private void MultiplayerRotation(double delta) { }

	#endregion

}
