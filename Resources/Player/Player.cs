using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody3D
{
    #region Editor Fields
    [Export]
	public bool isLocalPlayer = false;

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
	[Export]
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

        AddToGroup("players");

        InitializeClient();
		InitializeServer();
        InitializeWeapons();
    }

    public override void _Input(InputEvent @event)
    {
		if (!isLocalPlayer)
			return;
        if ((!App.IsPaused) && App.GameMode == GameModeEnum.InGame)
		{
			ClientRotation(@event);
		}
    }

    public override void _PhysicsProcess(double delta)
    {

		ClientPhysicsProcess(delta);
		ServerPhysicsProcess(delta);

		Velocity = GlobalTransform.Basis * _velocity;

		MoveAndSlide();
    }

    public override void _Process(double delta)
    {
		ClientProcess(delta);
		ServerProcess(delta);
		
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
		isLocalPlayer = false;
	}
}
