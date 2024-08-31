using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//All crosshairs must extend this class or somethin idk
public abstract partial class Crosshair : Control
{
    public abstract void PrimaryFire();

    public abstract void SecondaryFire();

    public abstract void Reload();

}

