using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class Bullet : Area3D
{

    private static PackedScene template;

    [Export]
    ///The diameter of the bullet in meters.
    public float bulletSize = 0;
    [Export]
    public Vector3 bulletInitialVelocity = new Vector3(0, 0, 0);
    [Export]
    public Godot.Color bulletColor = new("#ffffff");
    [Export]
    public float gravityAcceleration = -ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public Guid firedBy; //The player that fired the bullet.
    public Guid team; //TODO: Implement teams

    private CollisionShape3D collider;
    private MeshInstance3D meshInstance;
    private Vector3 velocity;
    private float mass;
    private Vector3 nextPosition;


    public static Bullet CreateBullet()
    {
        if(template == null)
        {
            template = GD.Load<PackedScene>("res://Resources/Bullet/Bullet.tscn");
        }
        Bullet bullet = (Bullet)template.Instantiate();
        return bullet;
    }
    /// <summary>
    /// Add this bullet to the same scene tree as an object
    /// As in, place the bullet in the "universe" that <c>item</c> is in.
    /// </summary>
    /// <param name="item">The object that the bullet will "share" a "universe" with. </param>
    public void AddToGameplay(Node item)
    {
        Node root = item.GetTree().Root;
        Node3D bulletContainer = root.GetNode<Node3D>("/root/Gameplay/BulletsContainer");
        if (bulletContainer == null)
        {
            GD.Print("Failed to find the bullet container.");
            return;
        }
        bulletContainer.AddChild(this);
    }

    #region Godot functions
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

        velocity = bulletInitialVelocity;

        AddToGroup("bullets");


    }

    public override void _PhysicsProcess(double delta)
    {
        velocity.Y += gravityAcceleration * (float)delta;

        nextPosition = Position + velocity * (float)delta;

        DetectCollision(delta);

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }
    #endregion

    private void InitializeMesh()
    {
        meshInstance = GetNode<MeshInstance3D>("Mesh");
        BaseMaterial3D material = new StandardMaterial3D();
        SphereMesh sphereMesh = new();
        sphereMesh.Radius = bulletSize / 2;
        sphereMesh.Height = bulletSize;

        material.AlbedoColor = bulletColor;


        sphereMesh.Material = material;
        meshInstance.Mesh = sphereMesh;
    }

    private void DetectCollision(double delta)
    {

        PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
        PhysicsShapeQueryParameters3D queryParameters = new();
        queryParameters.Shape = collider.Shape;
        queryParameters.Transform = Transform;
        queryParameters.Motion = velocity * (float)delta;
        queryParameters.CollisionMask = (1 << 1);

        Array<Dictionary> collisions = spaceState.IntersectShape(queryParameters,4);

        if(collisions.Count > 0)
        {
            foreach(Dictionary result in collisions)
            {
                Node3D collider = (Node3D)result["collider"];
                OnBodyEntered(collider);
            }
        }
        Position = nextPosition;
    }

    private void OnBodyEntered(Node3D body)
    {
        //GD.Print(body.GetType());
        //return; //Currently do nothing
        if (body.GetGroups().Contains("players"))
        {
            Player player = (Player)body;
            if (player.UUID == firedBy) //Prevent the player from dying to their own shot
                return;
            
            player.TakeDamage(30); //TODO: Implement accurate bullet damage based on bullet size. See Descriptive #1 for more details.
        }
        if (body.GetGroups().Contains("bullets"))
        {
            //TODO: Handle bullet-bullet collisions.
            //As of now, they just pass right through eachother like thin air.
            return;
        }
        //TODO: Add splat effect and stuff idk
        QueueFree();

    }
}
