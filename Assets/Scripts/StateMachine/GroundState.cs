using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : State
{
    protected float speed;
    protected float rotationSpeed;

    protected float horizontalInput;
    protected float verticalInput;

    public GroundState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        horizontalInput = verticalInput = 0;
    }
    public override void Exit()
    {
        base.Exit();
        player.ResetParameters();
    }
    public override void HandleInput()
    {
        base.HandleInput();
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (verticalInput <= 0)
        {
            verticalInput = 0;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.Move(verticalInput * speed, horizontalInput * rotationSpeed);
       
    }

}
