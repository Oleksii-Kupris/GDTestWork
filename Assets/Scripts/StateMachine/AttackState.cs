
public class AttackState : GroundState
{
    public AttackState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        player.ResetParameters();
    }
    public override void Exit()
    {
        base.Exit();
        player.ResetParameters();
    }
   
}
