﻿using System;
using System.Windows.Media;
using Wuthering_Waves_comfort_vision.Data.Hero;

public class Character
{
    [NonSerialized]
    public CharacterVariant nameEnum;
    [NonSerialized]
    public ImageSource image;
    public string imagePath;
    public string name;
    public bool isAvaible;
    public float shiftDelay = 1f;

    public string tier;

    public Ability intro = new();
    public Ability echo = new();
    public Ability resonance = new();
    public Ability ultimate = new();
    public Ability outro = new();
    public Ability element = new();
    public Ability inherit = new();
    public Ability weapon = new();

    public Ability cooldownIntro = new();
    public Ability cooldownEcho = new();
    public Ability cooldownResonance = new();
    public Ability cooldownUltimate = new();
    public Ability cooldownOutro = new();
    public Ability cooldownElement = new();
    public Ability cooldownInherit = new();
    public Ability cooldownWeapon = new();



    public Character()
    {


        intro.buffedCharacter = this;
        echo.buffedCharacter = this;
        resonance.buffedCharacter = this;
        ultimate.buffedCharacter = this;
        outro.buffedCharacter = this;
        element.buffedCharacter = this;
        inherit.buffedCharacter = this;
        weapon.buffedCharacter = this;

        cooldownIntro.buffedCharacter = this;
        cooldownEcho.buffedCharacter = this;
        cooldownResonance.buffedCharacter = this;
        cooldownUltimate.buffedCharacter = this;
        cooldownOutro.buffedCharacter = this;
        cooldownElement.buffedCharacter = this;
        cooldownInherit.buffedCharacter = this;
        cooldownWeapon.buffedCharacter = this;
    }

    public void SaveSkillData()
    {
        intro.SaveData();
        echo.SaveData();
        resonance.SaveData();
        ultimate.SaveData();
        outro.SaveData();
        element.SaveData();
        inherit.SaveData();
        weapon.SaveData();

        cooldownIntro.SaveData();
        cooldownEcho.SaveData();
        cooldownResonance.SaveData();
        cooldownUltimate.SaveData();
        cooldownOutro.SaveData();
        cooldownElement.SaveData();
        cooldownInherit.SaveData();
        cooldownWeapon.SaveData();
    }

    public void ResetData()
    {
        intro.ResetData();
        echo.ResetData();
        resonance.ResetData();
        ultimate.ResetData();
        outro.ResetData();
        element.ResetData();
        inherit.ResetData();
        weapon.ResetData();

        cooldownIntro.ResetData();
        cooldownEcho.ResetData();
        cooldownResonance.ResetData();
        cooldownUltimate.ResetData();
        cooldownOutro.ResetData();
        cooldownElement.ResetData();
        cooldownInherit.ResetData();
        cooldownWeapon.ResetData();
    }
}
