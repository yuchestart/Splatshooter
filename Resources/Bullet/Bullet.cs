using Godot;
using System;
using System.Diagnostics;

public partial class Bullet : Area3D
{

    [Export]
    public float bulletSize = 0; //Bullet sizes will be in diameter, not radius.
    [Export]
    public Vector3 bulletInitialVelocity = new Vector3(0, 0, 0);
    [Export]
    public Godot.Color bulletColor = new("#ffffff");
    [Export]
    public float gravityAcceleration = -ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    private CollisionShape3D collider;
    private MeshInstance3D meshInstance;
    private Vector3 _velocity;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collider = GetNode<CollisionShape3D>("Collider");
        meshInstance = GetNode<MeshInstance3D>("Mesh");

        SphereShape3D colliderShape = new();
        colliderShape.Radius = bulletSize / 2;

        collider.Shape = colliderShape;

        //TODO: Implement metaball shading
        BaseMaterial3D material = new StandardMaterial3D();
        SphereMesh sphereMesh = new();
        sphereMesh.Radius = bulletSize / 2;
        sphereMesh.Height = bulletSize;

        material.AlbedoColor = bulletColor;


        sphereMesh.Material = material;
        meshInstance.Mesh = sphereMesh;

        _velocity = bulletInitialVelocity;


    }

    public override void _PhysicsProcess(double delta)
    {
        _velocity.Y += gravityAcceleration * (float)delta;

        Position += _velocity;


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    private void OnBodyEntered(Node3D body)
    {
        if (body.GetGroups().Contains("players"))
        {
            Player player = (Player)body;
            player.TakeDamage(bulletSize); //TODO: Implement accurate bullet damage based on bullet size. See Descriptive #1 for more details.
        }
        else
        {
            //TODO: Add splat effect and stuff idk
            QueueFree();
        }
    }
}
