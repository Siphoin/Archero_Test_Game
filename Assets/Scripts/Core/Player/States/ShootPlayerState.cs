namespace Archero.States
{
    public class ShootPlayerState : OwneringState<IPlayer>
    {
        public override void Enter()
        {
            // TODO: rewrite to shoot to enemies

            Owner.Shoot();
        }

        public override void Exit()
        {
        }
    }
}
