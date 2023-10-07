using Mario.Application.Interfaces;

namespace Mario.Application.Services
{
    public static class Services
    {
        #region Properties
        public static ILevelService GameDataService { get; private set; }
        #endregion

        #region Public Methods
        public static void Load()
        {
            GameDataService = ServiceLocator.Current.Get<ILevelService>();
        }
        #endregion
    }
}