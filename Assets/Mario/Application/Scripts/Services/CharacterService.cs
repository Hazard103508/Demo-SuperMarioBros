using Mario.Application.Interfaces;

namespace Mario.Application.Services
{
    public class CharacterService : ICharacterService
    {
        public CharacterService()
        {
            CanMove = true;
        }

        public bool CanMove { get; private set; }

        public void StopMovement() => CanMove = false;
        public void ResumeMovement() => CanMove = true;
    }
}