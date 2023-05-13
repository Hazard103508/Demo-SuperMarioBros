using Mario.Application.Interfaces;

namespace Mario.Application.Services
{
    public class CharacterService : ICharacterService
    {
        public CharacterService()
        {
            AllowMove = true;
        }

        public bool AllowMove { get; set; }
    }
}