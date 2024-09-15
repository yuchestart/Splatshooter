using Godot;
using System;
using System.Collections.Generic;

partial class Player : CharacterBody3D
{
    private void InitializeClient()
    {
        if (!isLocalPlayer)
            return;
        camera = GetNode<Camera3D>("Camera");
        camera.Current = true;
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    private void ClientPhysicsProcess(double delta)
    {
        if (!isLocalPlayer)
            return;

        HandleWeaponInputs();

        _velocity.X = 0;
        _velocity.Z = 0;

        if (isLocalPlayer)
        {
            ClientMovement(delta);
        }
        else
        {
            ServerMovement(delta);
            ServerRotation(delta);
        }

        if (!IsOnFloor())
        {
            _velocity.Y += gravityAcceleration * (float)delta;
        }
        else
        {
            _velocity.Y = 0;
            if (Input.IsActionPressed("movement_jump") && (!App.IsPaused) && App.GameMode == GameModeEnum.InGame)
            {
                _velocity.Y = jumpForce;
            }
        }
    }
    #region Movement
    private void ClientMovement(double delta)
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

    private void ClientRotation(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseEvent)
        {
            RotateY(Mathf.DegToRad(-mouseEvent.Relative.X * mouseSensitivity));
            camera.RotateX(Mathf.DegToRad(-mouseEvent.Relative.Y * mouseSensitivity));
            Vector3 cameraRotation = camera.Rotation;
            cameraRotation.X = Mathf.Clamp(cameraRotation.X, -Mathf.Pi / 2, Mathf.Pi / 2);
            camera.Rotation = cameraRotation;
        }
    }
    #endregion
    private void ClientProcess(double delta)
    {
        if (!isLocalPlayer)
            return;

        healthRegenTimer += delta;
        if (healthRegenTimer >= 0.5)
        {
            health += 1;
            healthRegenTimer = 0;
        }
        aim = -camera.GlobalTransform.Basis.Z;

    }
}