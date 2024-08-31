using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract partial class Weapon : Node
{
    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }

    //NOTE: To allow for more flexible behavior, these methods will be run each frame. The input handling will be offloaded to the extendee.
    public abstract void PrimaryFire();
    public abstract void SecondaryFire();
    public abstract void Reload();
    
}
