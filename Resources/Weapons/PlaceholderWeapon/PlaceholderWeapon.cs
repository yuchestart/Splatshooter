using Godot;
using System;
using System.Transactions;

public partial class PlaceholderWeapon : Weapon
{

    public PlaceholderWeapon(Player parent) : base(parent)
    {
    }


    public override void Create()
    {
    }

    public override void PrimaryFire()
    {
        if (!Input.IsActionJustPressed("weapon_primary_fire"))
            return;
        Bullet mybullet = Bullet.CreateBullet();
        mybullet.Position = parent.Position; //Fire the bullet
        mybullet.bulletSize = 0.3f; //30 cm diameter
        mybullet.bulletInitialVelocity = parent.aim * 5f; //Plz don't go crazy...
        mybullet.firedBy = parent.UUID;
       // mybullet.gravityAcceleration = 0; //Make it no gravity so I can see it :)

        mybullet.AddToGameplay(parent);

        GD.Print("pew.");

    }

    public override void SecondaryFire()
    {

    }

    public override void Reload()
    {

    }

}
