using System;
using Wuthering_Waves_comfort_vision.Data.Hero;

namespace Wuthering_Waves_comfort_vision.Data.Elemental
{
    public class Elemental
    {
        [NonSerialized]
        public CharacterVariant echoEnum;
        public string name;
        public Ability skill = new();
    }
}
