using System;
using Wuthering_Waves_comfort_vision.Data.Hero;

public class Echo
{
    [NonSerialized]
    public CharacterVariant echoEnum;
    public string name;
    public Ability skill = new();
}