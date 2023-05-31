using Mario.Application.Interfaces;

namespace Mario.Application.Services
{
    public static class AllServices
    {
        public static IGameDataService GameDataService { get; private set; }
        public static ICoinService CoinService { get; private set; }
        public static IScoreService ScoreService { get; private set; }
        public static ITimeService TimeService { get; private set; }
        public static IItemsService ItemsService { get; private set; }
        public static IPlayerService PlayerService { get; private set; }
        public static IAssetReferencesService AssetReferencesService { get; private set; }

        public static void Load()
        {
            GameDataService = ServiceLocator.Current.Get<IGameDataService>();
            CoinService = ServiceLocator.Current.Get<ICoinService>();
            ScoreService = ServiceLocator.Current.Get<IScoreService>();
            TimeService = ServiceLocator.Current.Get<ITimeService>();
            ItemsService = ServiceLocator.Current.Get<IItemsService>();
            PlayerService = ServiceLocator.Current.Get<IPlayerService>();
            AssetReferencesService = ServiceLocator.Current.Get<IAssetReferencesService>();
        }
    }
}