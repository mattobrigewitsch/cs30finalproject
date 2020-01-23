using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public BattleMenu currentmenu; //Used in switch below.
    private GameObject enemyPoke; //legacy value, previously cloned old enemy pokemon, kept in case of revert.

    //initialises values with fancy headers below
    [Header("Selection")]
    public GameObject SelectionMenu;
    public GameObject SelectionInfo;
    public Text SelectionInfoText;
    public Text fight;
    private string fightT;
    public Text bag;
    private string bagT;
    public Text pokemon;
    private string pokemonT;
    public Text run;
    private string runT;

    [Header("Moves")]
    public GameObject movesMenu;
    public GameObject MovesDetails;
    public Text PP;
    private string PPT;
    public Text PowerT;
    public Text pType;
    public Text moveO;
    private string moveOT;
    public Text moveT;
    private string moveTT;
    public Text moveTH;
    private string moveTHT;
    public Text moveF;
    private string moveFT;

    [Header("Info")]
    public GameObject InfoMenu;
    public Text InfoText;
    public Text ename;
    public Text pname;
    public Text epokehealthmin;
    public Text epokehealthmax;
    public Text ppokehealthmin;
    public Text ppokehealthmax;
    public Text elevel;
    public Text plevel;

    [Header("Misc")]
    public int currentSelection;
    public GameObject gamemanage;
    public GameObject player;
    public bool runused;
    public GameManager gm;
    public int damageDealt;
    public int damage1;
    public float damage2;
    private float timeStamp;
    private float timeStamp2;
    public int emCounter;
    public int activeMoveCode;
    public bool stepCheck;
    public int failSafe;
    public bool spaceUp;
    public bool attackStep1;
    public bool attackStep2;
    public bool attackStep3;
    public bool attackStep4;

    // Use this for initialization
    void Start()
    {
        //sets up all values
        runused = false;
        //all text below is legacy code, learned .text can be immediatly converted
        fightT = fight.text;
        bagT = bag.text;
        pokemonT = pokemon.text;
        runT = run.text;
        moveOT = moveO.text;
        moveTT = moveT.text;
        moveTHT = moveTH.text;
        moveFT = moveF.text;
        PPT = PP.text;
        //sets gm to already existing object derived from GameManager script
        gm = gamemanage.GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        //below are switch controls.
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spaceUp = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentSelection < 4)
            {
                currentSelection++;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentSelection > 0)
            {
                currentSelection--;
            }
            if (currentSelection == 0)
            {
                currentSelection = 1;
            }

        }
        switch (currentmenu)
            //uses a switch to control the menu
        {

            case BattleMenu.Fight:
                //case one is if the fight menu is open. Allows the player to choose a move, and then uses 
                switch (currentSelection)
                {
                    //all cases describe which move is active. They all have the available keys space and 
                    //escape, which either use the move, if its empty say its empty, or exit fight with escape
                    case 1:
                        moveO.text = "> " + moveOT;
                        moveT.text = moveTT;
                        moveTH.text = moveTHT;
                        moveF.text = moveFT;

                        PP.text = gm.activePoke.Move1PP.ToString();
                        PowerT.text = gm.allMoves[gm.activePoke.Move1C].power.ToString();
                        pType.text = gm.allMoves[gm.activePoke.Move1C].moveType.ToString();
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ChangeMenu(BattleMenu.Selection);
                        }
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            if (gm.activePoke.Move1PP != 0)
                            {
                                PokeAttack();
                            }
                            else if (gm.activePoke.Move1PP == 0)
                            {
                                ChangeMenu(BattleMenu.Info);
                                InfoText.text = "You have no PP for that move!";
                            }
                        }
                        break;
                    case 2:
                        moveO.text = moveOT;
                        moveT.text = "> " + moveTT;
                        moveTH.text = moveTHT;
                        moveF.text = moveFT;
                        if (gm.activePoke.Move2C != 0)
                        {
                            PP.text = gm.activePoke.Move2PP.ToString();
                            PowerT.text = gm.allMoves[gm.activePoke.Move2C].power.ToString();
                            pType.text = gm.allMoves[gm.activePoke.Move2C].moveType.ToString();
                        }
                        else if (gm.activePoke.Move2C == 0)
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                ChangeMenu(BattleMenu.Info);
                                InfoText.text = "There is no move here!";
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ChangeMenu(BattleMenu.Selection);
                        }
                        break;
                    case 3:
                        moveO.text = moveOT;
                        moveT.text = moveTT;
                        moveTH.text = "> " + moveTHT;
                        moveF.text = moveFT;
                        if (gm.activePoke.Move3C != 0)
                        {
                            PP.text = gm.activePoke.Move3PP.ToString();
                            PowerT.text = gm.allMoves[gm.activePoke.Move3C].power.ToString();
                            pType.text = gm.allMoves[gm.activePoke.Move3C].moveType.ToString();
                        }
                        else if (gm.activePoke.Move3C == 0)
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                ChangeMenu(BattleMenu.Info);
                                InfoText.text = "There is no move here!";
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ChangeMenu(BattleMenu.Selection);
                        }
                        break;
                    case 4:
                        moveO.text = moveOT;
                        moveT.text = moveTT;
                        moveTH.text = moveTHT;
                        moveF.text = "> " + moveFT;
                        if (gm.activePoke.Move4C != 0)
                        {
                            PP.text = gm.activePoke.Move4PP.ToString();
                            PowerT.text = gm.allMoves[gm.activePoke.Move4C].power.ToString();
                            pType.text = gm.allMoves[gm.activePoke.Move4C].moveType.ToString();
                        }
                        else if (gm.activePoke.Move4C == 0)
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                ChangeMenu(BattleMenu.Info);
                                InfoText.text = "There is no move here!";
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ChangeMenu(BattleMenu.Selection);
                        }
                        break;
                }

                break;
            case BattleMenu.Selection:
                //selection is the hub menu which lets the player decide to either switch pokemon, use an item,
                //or run away.
                switch (currentSelection)
                {
                    case 1:
                        fight.text = "> " + fightT;
                        bag.text = bagT;
                        pokemon.text = pokemonT;
                        run.text = runT;
                        SelectionInfoText.text = "Use a move with the current Pokemon";
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            ChangeMenu(BattleMenu.Fight);
                        }
                        break;
                    case 2:
                        fight.text = fightT;
                        bag.text = "> " + bagT;
                        pokemon.text = pokemonT;
                        run.text = runT;
                        SelectionInfoText.text = "Select an item from your bag";
                        break;
                    case 3:
                        fight.text = fightT;
                        bag.text = bagT;
                        pokemon.text = "> " + pokemonT;
                        run.text = runT;
                        SelectionInfoText.text = "Change out your pokemon";
                        break;
                    case 4:
                        fight.text = fightT;
                        bag.text = bagT;
                        pokemon.text = pokemonT;
                        run.text = "> " + runT;
                        SelectionInfoText.text = "Run away!";
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            InfoText.text = "Got away safely!";
                            runused = true;
                            ChangeMenu(BattleMenu.Info);
                        }
                        break;
                }
                break;

            case BattleMenu.Info:
                //info describes the last action. As it turns into a sort of "final destination" for all actions,
                //and all actions use space bar, it also acts as the change to allow all actions to progress without
                //skipping steps.
                {
                    if (Input.GetKeyDown(KeyCode.Space) & runused)
                    {
                        //destroys both pokemon if run is used.
                        gamemanage.GetComponent<GameManager>().battleCamera.SetActive(false);
                        gamemanage.GetComponent<GameManager>().playerCamera.SetActive(true);
                        player.GetComponent<PlayerMovement2>().isAllowedToMove = true;
                        Destroy(GameObject.FindWithTag("epoke"));
                        Destroy(GameObject.FindWithTag("ppoke"));
                    }
                    //phases attacks.
                    if (Input.GetKeyDown(KeyCode.Space) & attackStep1)
                    {
                        PokeAttack1();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & attackStep2 & spaceUp)
                    {
                        PokeAttack2();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & attackStep3 & spaceUp)
                    {
                        PokeAttack3();
                    }
                }
                break;
        }


    }

    public void ChangeMenu(BattleMenu m)
    {
        //switches the menu depending on the current choice in the UI. Actives objects accordingly.
        currentmenu = m;
        currentSelection = 1;
        switch (m)
        {
            case BattleMenu.Selection:
                SelectionMenu.gameObject.SetActive(true);
                SelectionInfo.gameObject.SetActive(true);
                movesMenu.gameObject.SetActive(false);
                MovesDetails.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(false);
                break;
            case BattleMenu.Fight:
                SelectionMenu.gameObject.SetActive(false);
                SelectionInfo.gameObject.SetActive(false);
                movesMenu.gameObject.SetActive(true);
                MovesDetails.gameObject.SetActive(true);
                InfoMenu.gameObject.SetActive(false);
                break;
            case BattleMenu.Info:
                SelectionMenu.gameObject.SetActive(false);
                SelectionInfo.gameObject.SetActive(false);
                movesMenu.gameObject.SetActive(false);
                MovesDetails.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(true);
                break;
        }

    }

    public void EnemyMoveCounter()
    {
        //used to set up enemy moves, deciding which moves exist or not.
        if (gm.activeEPoke.Move1C != 0)
        {
            emCounter = emCounter + 1;
        }
        if (gm.activeEPoke.Move2C != 0)
        {
            emCounter = emCounter + 1;
        }
        if (gm.activeEPoke.Move3C != 0)
        {
            emCounter = emCounter + 1;
        }
        if (gm.activeEPoke.Move4C != 0)
        {
            emCounter = emCounter + 1;
        }
    }
    public void ConvCounterToMoveCode()
    {
        //converts the counter into a move code.
        if (emCounter == 0)
        {
            activeMoveCode = gm.activeEPoke.Move1C;
        }
        if (emCounter == 1)
        {
            activeMoveCode = gm.activeEPoke.Move2C;
        }
        if (emCounter == 2)
        {
            activeMoveCode = gm.activeEPoke.Move3C;
        }
        if (emCounter == 3)
        {
            activeMoveCode = gm.activeEPoke.Move4C;
        }
    }

    public void PokeAttack()
    {
        //phase one of attacking. calculates damage with the formula:
        //Damage = ((((2*level)/5)+2)*movepower*(attack/defence))/50
        spaceUp = false;
        ChangeMenu(BattleMenu.Info);
        InfoText.text = gm.activePoke.PName + " used " + gm.allMoves[gm.activePoke.Move1C].MoveName + "!";
        damage1 = gm.activePoke.AttackStat / gm.activeEPoke.DefenceStat;
        damage2 = 2 * gm.activePoke.level / 5 + 2 * gm.allMoves[gm.activePoke.Move1C].power * damage1 / 50;
        damageDealt = (int)damage2;
        attackStep1 = true;
    }
    public void PokeAttack1()
    {
        //displays damage in phase 2.
        attackStep1 = false;
        spaceUp = false;
        ChangeMenu(BattleMenu.Info);
        InfoText.text = gm.activeEPoke.PName + " recieved " + damageDealt + " damage!";
        gm.activeEPoke.HP = gm.activeEPoke.HP - damageDealt;
        gm.activePoke.Move1PP = gm.activePoke.Move1PP - 1;
        attackStep2 = true;
    }
    public void PokeAttack2()
    {
        //calculates enemy attack, and possible enemy death in phase 3.
        spaceUp = false;
        attackStep2 = false;
        if (gm.activeEPoke.HP > 0)
        {
            Debug.Log("test1");
            epokehealthmin.text = gm.activeEPoke.HP.ToString();
            EnemyMoveCounter();
            int eMoveChoice = Random.Range(0, emCounter - 1);
            ConvCounterToMoveCode();
            InfoText.text = gm.activeEPoke.PName + " used " + gm.allMoves[activeMoveCode].MoveName + "!";
            Debug.Log(gm.allMoves[activeMoveCode].MoveName);
            attackStep3 = true;
        }
        else if (gm.activeEPoke.HP <= 0)
        {
            epokehealthmin.text = "0";
            InfoText.text = gm.activeEPoke.PName + " fainted!";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gamemanage.GetComponent<GameManager>().battleCamera.SetActive(false);
                gamemanage.GetComponent<GameManager>().playerCamera.SetActive(true);
                player.GetComponent<PlayerMovement2>().isAllowedToMove = true;
                Destroy(GameObject.FindWithTag("epoke"));
                Destroy(GameObject.FindWithTag("ppoke"));
            }
        }
    }
    public void PokeAttack3()
    {
        //resets in phase 4.
        spaceUp = false;
        attackStep3 = false;
        ChangeMenu(BattleMenu.Selection);
    }
}

public enum BattleMenu
{
    //enum used to derive the switch
    Selection,
    Pokemon,
    Bag,
    Fight,
    Info
}





