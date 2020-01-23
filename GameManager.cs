//Matt Obrigewitsch
//CS3
//Game Manager
//The core file that manages running the game

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //sets up cameras, from UnityEngine library.
    public GameObject playerCamera;
    public GameObject battleCamera;
    public GameObject titleCamera;

    //determines the player's character and stores as player
    public GameObject player;

    public List<BasePokemon> allPokemon = new List<BasePokemon>(); //Creates a manually serializable list that stores all prefab pokemon
    public List<PokemonMoves> allMoves = new List<PokemonMoves>(); //Creates a manually serializable list that stores all prefab pokemon moves

    public Transform defencePodium; //takes a vector called defencePodium
    public Transform attackPodium; //takes a vector called attackPodium
    public GameObject pokebase;

    public int EntLvlMax; //creates an interger for the pokemon encounter's max possible level, grabbed from LongGrass
    public int EntLvlMin; //creates an interger for the pokemon encounter's minimum possible level, grabbed from LongGrass

    public BattleManager bm; //stores an instance of the BattleManager script as bm

    public BasePokemon activePoke; //stores an instance of the BasePokemon script as activePoke, for the player pokemon
    public BasePokemon activeEPoke; //stores an instance of the BasePokemon script as activeEPoke, for the enemy pokemon

    void Start() //on code initialization
    {
        playerCamera.SetActive(true); //enables the player camera
        battleCamera.SetActive(false); //disables the battle camera
        bm = battleCamera.GetComponent<BattleManager>(); //sets bm to the already existing script stored in the battleCamera object

        //initialises the starting pokemon owned by the player
        BasePokemon startPoke = new BasePokemon();
        startPoke.AddMember(allPokemon[1]);
        player.GetComponent<Player>().ownedPokemon[0].pokemon = startPoke;
        startPoke.level = player.GetComponent<Player>().ownedPokemon[0].level;
        startPoke.maxHP = startPoke.HPGain * startPoke.level + startPoke.maxHP;
        startPoke.HP = startPoke.maxHP;
        startPoke.AttackStat = startPoke.AttackGain * startPoke.level + startPoke.AttackStat;
        startPoke.DefenceStat = startPoke.DefenceGain * startPoke.level + startPoke.DefenceStat;
        startPoke.SpeedStat = startPoke.SpeedGain * startPoke.level + startPoke.SpeedStat;
        startPoke.SpAttackStat = startPoke.SpAttackGain * startPoke.level + startPoke.SpAttackStat;
        startPoke.SpDefenceStat = startPoke.SpDefenceStat * startPoke.level + startPoke.SpDefenceStat;
        startPoke.XPtoLvl = 50 * startPoke.level + startPoke.XPtoLvl;
        startPoke.Move1C = startPoke.learnableMoves[0].MoveCode;
        startPoke.Move1PP = allMoves[startPoke.Move1C].PP;
    }

    void Update() //these are a necessity for all codes derived from monobehaviour
    {

    }

    public void EnterBattle(Rarity rarity)
    {
        //changes active camera
        playerCamera.SetActive(false);
        battleCamera.SetActive(true);

        BasePokemon battlePokemon = GetRandomPokemonFromList(GetPokemonByRarity(rarity)); //runs 2 functions to get a random enemy pokemon

        Debug.Log(battlePokemon.PName); //write to console pokemon name
        bm.ename.text = battlePokemon.PName; //write to console enemy name
        player.GetComponent<PlayerMovement2>().isAllowedToMove = false; //make sure player doesnt move while in battle

        GameObject aPoke = Instantiate(pokebase, attackPodium.transform.position, Quaternion.identity) as GameObject; //create object aPoke
        GameObject dPoke = Instantiate(pokebase, defencePodium.transform.position, Quaternion.identity) as GameObject; //create object dPoke

        //creates clones of all of the players pokemon
        OwnedPokemon opoke1 = player.GetComponent<Player>().ownedPokemon[0];
        OwnedPokemon opoke2 = player.GetComponent<Player>().ownedPokemon[1];
        OwnedPokemon opoke3 = player.GetComponent<Player>().ownedPokemon[2];
        OwnedPokemon opoke4 = player.GetComponent<Player>().ownedPokemon[3];
        OwnedPokemon opoke5 = player.GetComponent<Player>().ownedPokemon[4];
        OwnedPokemon opoke6 = player.GetComponent<Player>().ownedPokemon[5];

        //decides which clone is the currently active pokemon
        BasePokemon ppoke = aPoke.AddComponent<BasePokemon>() as BasePokemon;
        ppoke = opoke1.pokemon;
        if (opoke1.pokemon.HP > 0)
        {
            ppoke = opoke1.pokemon;
        }
        else if (opoke2.pokemon.HP > 0)
        {
            ppoke = opoke2.pokemon;
        }
        else if (opoke3.pokemon.HP > 0)
        {
            ppoke = opoke3.pokemon;
        }
        else if (opoke4.pokemon.HP > 0)
        {
            ppoke = opoke4.pokemon;
        }
        else if (opoke5.pokemon.HP > 0)
        {
            ppoke = opoke5.pokemon;
        }
        else if (opoke6.pokemon.HP > 0)
        {
            ppoke = opoke6.pokemon;
        }
        else
        {
            Debug.Log("No Poke Error");
        }
        Debug.Log(ppoke.PName);
        bm.pname.text = ppoke.PName;

        Vector3 pokeLocalPos = new Vector3(0, 1, 0); //lets all pokemon know own vector

        //sets all active pokemon to platform vector
        dPoke.transform.parent = defencePodium;
        dPoke.transform.localPosition = pokeLocalPos;
        aPoke.transform.parent = attackPodium;
        aPoke.transform.localPosition = pokeLocalPos;

        //sets up enemy pokemon stats, aswell as tags all pokemon so they can be deleted later.
        //enemy stats are derived from BasePokemon, and updated for level*statgain+basestat
        BasePokemon tempPoke = dPoke.AddComponent<BasePokemon>() as BasePokemon;
        tempPoke.AddMember(battlePokemon);
        tempPoke.level = Random.Range(EntLvlMin, EntLvlMax);
        bm.elevel.text = tempPoke.level.ToString();
        tempPoke.maxHP = tempPoke.HPGain * tempPoke.level + tempPoke.maxHP;
        tempPoke.HP = tempPoke.maxHP;
        tempPoke.AttackStat = tempPoke.AttackGain * tempPoke.level + tempPoke.AttackStat;
        tempPoke.DefenceStat = tempPoke.DefenceGain * tempPoke.level + tempPoke.DefenceStat;
        tempPoke.SpeedStat = tempPoke.SpeedGain * tempPoke.level + tempPoke.SpeedStat;
        tempPoke.SpAttackStat = tempPoke.SpAttackGain * tempPoke.level + tempPoke.SpAttackStat;
        tempPoke.SpDefenceStat = tempPoke.SpDefenceStat * tempPoke.level + tempPoke.SpDefenceStat;
        tempPoke.XPtoLvl = 50 * tempPoke.level + tempPoke.XPtoLvl;
        dPoke.tag = "epoke";
        aPoke.tag = "ppoke";
        bm.plevel.text = ppoke.level.ToString();

        //draws the health stat in the UI
        bm.epokehealthmin.text = tempPoke.HP.ToString();
        bm.epokehealthmax.text = tempPoke.maxHP.ToString();
        bm.ppokehealthmin.text = ppoke.HP.ToString();
        bm.ppokehealthmax.text = ppoke.maxHP.ToString();

        //derives player moves
        if (ppoke.Move1C != 0)
        {
            bm.moveO.text = allMoves[ppoke.Move1C].MoveName;
        }
        else
        {
            bm.moveO.text = "Empty";
        }
        if (ppoke.Move2C != 0)
        {
            bm.moveT.text = allMoves[ppoke.Move2C].MoveName;
        }
        else
        {
            bm.moveT.text = "Empty";
        }
        if (ppoke.Move3C != 0)
        {
            bm.moveTH.text = allMoves[ppoke.Move3C].MoveName;
        }
        else
        {
            bm.moveTH.text = "Empty";
        }
        if (ppoke.Move2C != 0)
        {
            bm.moveF.text = allMoves[ppoke.Move2C].MoveName;
        }
        else
        {
            bm.moveF.text = "Empty";
        }
        
        //derives player moves
        List<LearnableMoves> possibleMoves = new List<LearnableMoves>();
        foreach (LearnableMoves lm in tempPoke.learnableMoves)
        {
            if (lm.LearnedLevel <= tempPoke.level)
            {
                possibleMoves.Add(lm);
            }
        }
        
        //more deriving enemy moves
        if (possibleMoves.Count > 0)
        {
            int pmindex = Random.Range(0, possibleMoves.Count - 1);
            tempPoke.Move1C = possibleMoves[pmindex].MoveCode;
            possibleMoves.RemoveAt(pmindex);
        }
        if (possibleMoves.Count > 0)
        {
            int pmindex = Random.Range(0, possibleMoves.Count - 1);
            tempPoke.Move2C = possibleMoves[pmindex].MoveCode;
            possibleMoves.RemoveAt(pmindex);
        }
        if (possibleMoves.Count > 0)
        {
            int pmindex = Random.Range(0, possibleMoves.Count - 1);
            tempPoke.Move3C = possibleMoves[pmindex].MoveCode;
            possibleMoves.RemoveAt(pmindex);
        }
        if (possibleMoves.Count > 0)
        {
            int pmindex = Random.Range(0, possibleMoves.Count - 1);
            tempPoke.Move4C = possibleMoves[pmindex].MoveCode;
            possibleMoves.RemoveAt(pmindex);
        }

        //adds finishing touches to scene, rendering everything, and resetting the battle setup if in second fight
        dPoke.GetComponent<SpriteRenderer>().sprite = battlePokemon.image;
        aPoke.GetComponent<SpriteRenderer>().sprite = ppoke.image;
        activePoke = ppoke; 
        activeEPoke = tempPoke;
        bm.ChangeMenu(BattleMenu.Selection);
        bm.runused = false;
        
    }

    public List<BasePokemon> GetPokemonByRarity(Rarity rarity)
    {
        //searches all pokemon, and returns every pokemon within the rarity derived in LongGrass
        List<BasePokemon> returnPokemon = new List<BasePokemon>();
        foreach (BasePokemon Pokemon in allPokemon)
        {
            if (Pokemon.rarity == rarity)
                returnPokemon.Add(Pokemon);
        }
        return returnPokemon;
    }

    public BasePokemon GetRandomPokemonFromList(List<BasePokemon> pokeList)
    {
        //randomly picks one pokemon from previously derived list
        BasePokemon poke = new BasePokemon();
        int pokeIndex = Random.Range(0, pokeList.Count - 1);
        poke = pokeList[pokeIndex];
        return poke;
    }
}

[System.Serializable]
public class PokemonMoves
{
    //this class has all the properties of all moves in the game. it is used in allMoves list.
    public string MoveName;
    public MoveType category;
    public PokemonType moveType;
    public int PP;
    public float power;
    public float accuracy;
}

[System.Serializable]
public class Stat
{
    //test class, not currently used. Kept in incase of future use.
    public float minimum;
    public float maximum;
}

public enum MoveType
{
    //enum that decides the moves type. used in PokemonMoves.
    Physical,
    Special,
    Status
}


