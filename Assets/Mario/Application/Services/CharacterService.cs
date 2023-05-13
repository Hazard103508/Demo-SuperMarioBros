using Mario.Application.Interfaces;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class CharacterService : ICharacterService
    {
        public CharacterService()
        {
            AllowMove = true;
        }

        public bool AllowMove { get; set; }

        public void Update()
        {
        }
    }
}