using Mario.Application.Interfaces;

namespace Mario.Application.Services
{
    public class CharacterService : ICharacterService
    {
        private bool _allowMove;

        public CharacterService()
        {
            _allowMove = true;
        }


        public bool CanMove() => _allowMove;
        public void StopMovement() => _allowMove = false;
        public void ResumeMovement() => _allowMove = true;
    }
}