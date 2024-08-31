using Godot;
using System;

public partial class PlaceholderWeapon : Weapon
{
    public override void PrimaryFire()
    {
        Bullet mybullet = new Bullet();
    }

    public override void SecondaryFire()
    {
        throw new NotImplementedException();
    }

    public override void Reload()
    {
        throw new NotImplementedException();
    }
    
}
