namespace Mario.Game.Interfaces
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}