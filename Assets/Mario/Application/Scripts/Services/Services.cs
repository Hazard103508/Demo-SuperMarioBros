using Mario.Application.Interfaces;

namespace Mario.Application.Services
{
    public static class Services
    {
        public static IAddressablesService AddressablesService { get; private set; }
        public static IPoolService PoolService { get; private set; }
        public static IGameDataService GameDataService { get; private set; }
        public static ICoinService CoinService { get; private set; }
        public static IScoreService ScoreService { get; private set; }
        public static ITimeService TimeService { get; private set; }
        public static IPlayerService PlayerService { get; private set; }
        public static ISceneService SceneService { get; private set; }
        public static IMusicService MusicService { get; private set; }

        public static void Load()
        {
            AddressablesService = ServiceLocator.Current.Get<IAddressablesService>();
            PoolService = ServiceLocator.Current.Get<IPoolService>();
            GameDataService = ServiceLocator.Current.Get<IGameDataService>();
            CoinService = ServiceLocator.Current.Get<ICoinService>();
            ScoreService = ServiceLocator.Current.Get<IScoreService>();
            TimeService = ServiceLocator.Current.Get<ITimeService>();
            PlayerService = ServiceLocator.Current.Get<IPlayerService>();
            SceneService = ServiceLocator.Current.Get<ISceneService>();
            MusicService = ServiceLocator.Current.Get<IMusicService>();
        }
    }
}