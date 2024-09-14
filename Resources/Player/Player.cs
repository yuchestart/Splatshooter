using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody3D
{
    #region Editor Fields
    [Export]
	public bool current = false;

	[Export]
	private float moveSpeed = 5.0f;
	[Export]
	private float jumpForce = 10.0f;
	[Export]
	private float friction = 0.1f;
	[Export]
	private float gravityAcceleration = -ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	[Export]
	private float mouseSensitivity = 0.1f;
    #endregion
    public Guid UUID;
	public Vector3 aim = Vector3.Zero;
	
	public float health = 100;

	private Camera3D camera;
	private Vector3 _velocity = Vector3.Zero;
	private double healthRegenTimer = 0;

	public static Player CreatePlayer()
	{
		Player player = (Player)GD.Load<PackedScene>("res://Resources/Player/Player.tscn").Instantiate();
		return player;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

        UUID = Guid.NewGuid();
        if (current)
        {
            camera = GetNode<Camera3D>("Camera");
            camera.Current = true;
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }

        AddToGroup("players");

        InitializeWeapons();
    }

    public override void _Input(InputEvent @event)
    {
		if (current && (!App.IsPaused) && App.GameMode == GameModeEnum.InGame)
		{
			PlayerRotation(@event);
		}
    }

    public override void _PhysicsProcess(double delta)
    {
		HandleWeaponInputs();

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
			if (Input.IsActionPressed("movement_jump") && (!App.IsPaused) && App.GameMode == GameModeEnum.InGame)
			{
				_velocity.Y = jumpForce;
			}
		}

		Velocity = GlobalTransform.Basis * _velocity;

		MoveAndSlide();
    }

    public override void _Process(double delta)
    {

		if (current)
		{
			healthRegenTimer += delta;
			if (healthRegenTimer >= 0.5)
			{
				health += 1;
				healthRegenTimer = 0;
			}
		}
		aim = -camera.GlobalTransform.Basis.Z;
    }

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
			Die();
	}

	public void Die()
	{
		//TODO: Implement death
		GD.Print("Looks like somebody died💀");
		current = false;
	}

    #region Client

    #region Client movement

    private void PlayerMovement(double delta)
	{
		if (App.IsPaused)
			return;
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


    #endregion

    #region Multiplayer

    #region Multiplayer movement
    private void MultiplayerMovement(double delta) { }

	private void MultiplayerRotation(double delta) { }

    #endregion

    #endregion
}
