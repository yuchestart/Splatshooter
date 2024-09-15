using Godot;
using System;
using System.Collections.Generic;

partial class Player : CharacterBody3D
{
    private void InitializeServer()
    {
        if (isLocalPlayer)
            return;

    }

    private void ServerPhysicsProcess(double delta)
    {
        if (isLocalPlayer)
            return;
    }


    private void ServerMovement(double delta) { }

    private void ServerRotation(double delta) { }

    private void ServerProcess(double delta)
    {
        if (isLocalPlayer)
            return;
    }
}