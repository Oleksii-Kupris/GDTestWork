public class MovingState : GroundState
{
    public MovingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        speed = player.MovementSpeed;
        rotationSpeed = player.RotationSpeed;
    
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
       
    }


}
