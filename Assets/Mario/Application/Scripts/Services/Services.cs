using Mario.Application.Interfaces;

namespace Mario.Application.Services
{
    public static class Services
    {
        #region Properties
        public static ILevelService GameDataService { get; private set; }
        public static ITimeService TimeService { get; private set; }
        public static IPlayerService PlayerService { get; private set; }
        #endregion

        #region Public Methods
        public static void Load()
        {
            GameDataService = ServiceLocator.Current.Get<ILevelService>();
            TimeService = ServiceLocator.Current.Get<ITimeService>();
            PlayerService = ServiceLocator.Current.Get<IPlayerService>();
        }
        #endregion
    }
}