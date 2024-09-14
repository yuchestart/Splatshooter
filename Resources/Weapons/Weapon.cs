using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract partial class Weapon : Node
{

    protected Player parent;

    public Weapon(Player parent)
    {
        this.parent = parent;
    }

    public override void _Ready()
    {
        Create();
    }

    public void HandleActions()
    {
        PrimaryFire();
        SecondaryFire();
        Reload();
    }

    //NOTE: To allow for more flexible behavior, these methods will be run each frame. The input handling will be offloaded to the extendee.
    public abstract void PrimaryFire();
    public abstract void SecondaryFire();
    public abstract void Reload();

    /// <summary>
    /// When the weapon enters the scene tree, this function is called to instantiate things like the weapon mesh, HUD, effects, etc. etc.
    /// </summary>
    public abstract void Create();
}
