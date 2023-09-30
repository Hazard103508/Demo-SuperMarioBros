using Mario.Application.Interfaces;
using Mario.Application.Services;

namespace Mario.Game.Player
{
    public abstract class PlayerStateBuff : PlayerState
    {
        #region Objects
        private readonly ISoundService _soundService;
        #endregion

        #region Constructor
        public PlayerStateBuff(PlayerController player) : base(player)
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff";
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Movable.enabled = false;
            _soundService.Play(Player.Profile.Buff.SoundFX);
        }
        #endregion
    }
}