using Mario.Application.Interfaces;
using Mario.Application.Services;

public static class AllServices
{
    public static IGameDataService GameDataService => ServiceLocator.Current.Get<IGameDataService>();
    public static ICoinService CoinService => ServiceLocator.Current.Get<ICoinService>();
    public static IScoreService ScoreService => ServiceLocator.Current.Get<IScoreService>();
    public static ITimeService TimeService => ServiceLocator.Current.Get<ITimeService>();
    public static ICharacterService CharacterService => ServiceLocator.Current.Get<ICharacterService>();
}
