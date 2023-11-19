using Mario.Game.Npc.Koopa;

namespace Mario.Game.Npc.KoopaRed
{
    public class KoopaRedStateWalk : KoopaStateWalk
    {
        #region Constructor
        public KoopaRedStateWalk(KoopaRed koopa) : base(koopa)
        {
        }
        #endregion

        #region IState Methods
        public override void Update()
        {
            base.Update();
            ((KoopaRed)Koopa).CheckEndFloor();
        }
        #endregion
    }
}