using System;

namespace Mario.Application.Interfaces
{
    public interface ICoinService : IGameService
    {
        event Action CoinsChanged;
        int Coins { get; }

        void Add();
        void Reset();
    }
}