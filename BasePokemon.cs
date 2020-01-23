//Matt Obrigewitsch
//CS3
//Base Pokemon
//Used to store all data behind every pokemon in the game

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BasePokemon : MonoBehaviour
{
    public List<LearnableMoves> learnableMoves = new List<LearnableMoves>(); //creates a list serialized manually that determines all moves a set pokemon can learn, and at what level

    //Set up all values
    public string PName;
    public Sprite image;
    public BiomeList biomeFound;
    public PokemonType type;
    public Rarity rarity;
    public int HP;
    public int maxHP;
    public int AttackStat;
    public int DefenceStat;
    public int SpeedStat;
    public int SpAttackStat;
    public int SpDefenceStat;
    public int EvasionStat;
    public int HPGain;
    public int AttackGain;
    public int DefenceGain;
    public int SpeedGain;
    public int SpAttackGain;
    public int SpDefenceGain;
    public int EvasionGain;
    public int XP;
    public int XPtoLvl;
    public int Move1C;
    public int Move1PP;
    public int Move2C;
    public int Move2PP;
    public int Move3C;
    public int Move3PP;
    public int Move4C;
    public int Move4PP;

    public bool canEvolve;
    public PokemonEvolution evolveTo; //used to determine the pokemon this pokemon changes into after evolving

    public int level;

    void Start() //these are a necessity for all codes derived from monobehaviour
    {

    }
    void Update() //these are a necessity for all codes derived from monobehaviour
    {

    }

    public void AddMember(BasePokemon bp)
    {
        //inherits all values of parent into a clone of the manually serialized prefab
        this.PName = bp.PName;
        this.image = bp.image;
        this.biomeFound = bp.biomeFound;
        this.type = bp.type;
        this.rarity = bp.rarity;
        this.HP = bp.HP;
        this.maxHP = bp.maxHP;
        this.AttackStat = bp.AttackStat;
        this.DefenceStat = bp.DefenceStat;
        this.SpeedStat = bp.SpeedStat;
        this.SpAttackStat = bp.SpAttackStat;
        this.SpDefenceStat = bp.SpDefenceStat;
        this.EvasionStat = bp.EvasionStat;
        this.HPGain = bp.HPGain;
        this.AttackGain = bp.AttackGain;
        this.DefenceGain = bp.DefenceGain;
        this.SpeedGain = bp.SpeedGain;
        this.SpAttackGain = bp.SpAttackGain;
        this.SpDefenceGain = bp.SpDefenceGain;
        this.XP = bp.XP;
        this.XPtoLvl = bp.XPtoLvl;
        this.canEvolve = bp.canEvolve;
        this.evolveTo = bp.evolveTo;
        this.level = bp.level;
        this.learnableMoves = bp.learnableMoves;
        this.Move1C = bp.Move1C;
        this.Move2C = bp.Move2C;
        this.Move3C = bp.Move3C;
        this.Move4C = bp.Move4C;
        this.Move1PP = bp.Move1PP;
        this.Move2PP = bp.Move2PP;
        this.Move3PP = bp.Move3PP;
        this.Move4PP = bp.Move4PP;
    }
}

public enum Rarity
{
    //creates an enum for how rare the pokemon is, used in LongGrass
    VeryCommon,
    Common,
    SemiRare,
    Rare,
    VeryRare
}

public enum PokemonType
{
    //Creates an enum to determine pokemon type, used to determine effectiveness of moves
    Flying,
    Ground,
    Rock,
    Steel,
    Fire,
    Water,
    Grass,
    Ice,
    Electric,
    Psychic,
    Dark,
    Dragon,
    Fighting,
    Normal
}

[System.Serializable]
public class PokemonEvolution
{
    //used to determine evolution level and changed pokemon
    public BasePokemon nextEvolution;
    public int levelUpLevel;
}

[System.Serializable]
public class LearnableMoves
{
    //Class listed by learnableMoves, determines moved learned and at what level.
    //All moves correspond to matching values in allMoves on GameManager script.
    public int MoveCode;
    public int LearnedLevel;
}
