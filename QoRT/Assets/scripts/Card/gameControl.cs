using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameControl : MonoBehaviour
{
    private List<Card> advantureDeck = new List<Card>();
    private List<QuestCard> questDeck = new List<QuestCard>();
    private List<TournamentCard> tournDeck = new List<TournamentCard>();
    private List<EventCard> eventDeck = new List<EventCard>();
    private List<Card> rankDeck = new List<Card>();
    private List<ShieldCard> shieldDeck = new List<ShieldCard>();
    private List<Card>[] hands = new List<Card>[4];
    private Player[] players = new Player[4];
    private List<Card>[] stages = new List<Card>[5];
    private int[] sponsorStageATK = new int[5];

    private Player p1;
    private Player p2;
    private Player p3;
    private Player p4;
    private List<Card> hand1 = new List<Card>();
    private List<Card> hand2 = new List<Card>();
    private List<Card> hand3 = new List<Card>();
    private List<Card> hand4 = new List<Card>();
    private List<ShieldCard> shield1 = new List<ShieldCard>();
    private List<ShieldCard> shield2 = new List<ShieldCard>();
    private List<ShieldCard> shield3 = new List<ShieldCard>();
    private List<ShieldCard> shield4 = new List<ShieldCard>();
    private int stageAtk1 = 0;
    private int stageAtk2 = 0;
    private int stageAtk3 = 0;
    private int stageAtk4 = 0;
    private int stageAtk5 = 0;

    private List<Player> playerInQuest = new List<Player>();
    private List<Card>[] otherPlayersCardsInQuest;
    private List<Card> sponserCardOnDeck = new List<Card>();
    private List<Card> sponsorUsedCard = new List<Card>();
    private int joinCount = 0;

    private int CURRENTPLAYERNUM;

    private List<Card> discardListAdv = new List<Card>();
    GameObject[] tempObj_adventure = new GameObject[125];
    GameObject[] tempObj_story = new GameObject[28];
    List<GameObject>[] cardObjs = new List<GameObject>[4];
    List<GameObject> cardObjs_p1 = new List<GameObject>();
    List<GameObject> cardObjs_p2 = new List<GameObject>();
    List<GameObject> cardObjs_p3 = new List<GameObject>();
    List<GameObject> cardObjs_p4 = new List<GameObject>();

    List<GameObject> adventure_Objs = new List<GameObject>();
    List<GameObject> story_Objs = new List<GameObject>();
    public List<GameObject> used_adventure_Objs = new List<GameObject>();
    List<GameObject> used_story_Objs = new List<GameObject>();
    List<GameObject> stageCards1_objs = new List<GameObject>();
    List<GameObject> stageCards2_objs = new List<GameObject>();
    List<GameObject> stageCards3_objs = new List<GameObject>();
    List<GameObject> stageCards4_objs = new List<GameObject>();
    List<GameObject> stageCards5_objs = new List<GameObject>();
    List<GameObject>[] obj_stages = new List<GameObject>[5];

    //private int viewIndex = 1; //which player's view is displayed now, 0,1,2,3 for p1,p2,p3,p4;
    private int currentView = 0;
    Vector3 pos_p1;
    Vector3 pos_p2;
    Vector3 pos_p3;
    Vector3 pos_p4;
    Vector3 pos_story;
    bool gameIsOn;

    public GameObject discardBTN;
    public GameObject askSponsorBTN;
    public GameObject askJoinBTN;
	public GameObject playCardsInQuestBtn;
    public GameObject msgBar;
    public Text msgBarTXT;
    public GameObject sponsorCard; //Sponsor cards msg
    public GameObject next_story;
    public GameObject wantJoinMSG;
    public Player sponsorNow;
    public Text p1shieldNum;
    public Text p2shieldNum;
    public Text p3shieldNum;
    public Text p4shieldNum;
    public Text p2cardsNum;
    public Text p3cardsNum;
    public Text p4cardsNum;
    public Text namep1;
    public Text namep2;
    public Text namep3;
    public Text namep4;
    public QuestCard questNow;
    public EventCard eventNow;
    public TournamentCard tournNow;
    public int numOfStagesNow;
    private int roundNow = 1; //current round of a Quest
    public int inWhichStage = 1;
    public GameObject sponsorEnd;
    public GameObject playCardMsg;
    public GameObject disCardMsg;
    public GameObject disCardMsg2;
    public GameObject continueMsg;
    public GameObject guideMSG;
    public Text guideTxt;
    public Text continueTxt;
    public Text boardcastTxt;

    string ctnMsgStr;
    string[] finishQuestStr = new string[3];


    private QuestGame.Logger gameLog = new QuestGame.Logger();
    private bool gameBegin = false;
    // Use this for initialization
    void Start()
    {
        //setHands_players();
        CURRENTPLAYERNUM = 4;
        msgBar.SetActive(false);
        askJoinBTN.SetActive(false);
        askSponsorBTN.SetActive(false);
        discardBTN.SetActive(false);
        sponsorEnd.SetActive(false);
        playCardMsg.SetActive(false);
        sponsorCard.SetActive(false);
        disCardMsg.SetActive(false);
        disCardMsg2.SetActive(false);
        continueMsg.SetActive(false);
        next_story.SetActive(false);
        guideMSG.SetActive(false);
        wantJoinMSG.SetActive(false);
		playCardsInQuestBtn.SetActive (false);
        gameIsOn = false;
        tempObj_adventure = GameObject.FindGameObjectsWithTag("adventureDeck");
        tempObj_story = GameObject.FindGameObjectsWithTag("storyDeck");
        for (int i = 0; i < tempObj_adventure.Length; i++)
        {
            adventure_Objs.Add(tempObj_adventure[i]);
        }
        for (int i = 0; i < tempObj_story.Length; i++)
        {
            story_Objs.Add(tempObj_story[i]);
        }
        pos_p1 = GameObject.Find("pos1").GetComponent<Transform>().position;
        pos_p2 = GameObject.Find("pos2").GetComponent<Transform>().position;
        pos_p3 = GameObject.Find("pos3").GetComponent<Transform>().position;
        pos_p4 = GameObject.Find("pos4").GetComponent<Transform>().position;
        pos_story = GameObject.Find("storyPos").GetComponent<Transform>().position;

        currentView = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameBegin)
        {
            rankUpdate();
            checkRankUpdate(players[0]);
            checkRankUpdate(players[1]);
            checkRankUpdate(players[2]);
            checkRankUpdate(players[3]);

            playerNamePosChange();
            shieldDisplayUpdate();
            cardsNumDisplayUpdate();
            for (int i = 0; i < CURRENTPLAYERNUM; i++)
            {
                if (players[i] != null)
                {
                    if (players[i].getRank() == Rank.KING)
                    {
                        gameLog.info(players[i].getName() + " is the winner, ggwp");
                    }
                }
            }

            if (gameIsOn)
            {
                load_dealing();
                next_story.SetActive(true);
                gameIsOn = false;
            }
        }
    }

    public void setHands_players(int playerNum)
    {
        hands[0] = hand1;
        hands[1] = hand2;
        hands[2] = hand3;
        hands[3] = hand4;

        p1 = new Player("p1AI", hand1, Rank.SQUIRE, PlayerType.AI, shield1);
        p2 = new Player("p2AI", hand2, Rank.SQUIRE, PlayerType.AI, shield2);
        p3 = new Player("p3AI", hand3, Rank.SQUIRE, PlayerType.AI, shield3);
        p4 = new Player("p4AI", hand4, Rank.SQUIRE, PlayerType.AI, shield4);
        players[0] = p1;
        players[1] = p2;
        players[2] = p3;
        players[3] = p4;
        for (int i = 0; i < playerNum; i++)
        {
            players[i].setType(PlayerType.PLAYER);
            players[i].setName("p" + (i + 1));
        }



        p1.setShield(shield1);
        p2.setShield(shield2);
        p3.setShield(shield3);
        p4.setShield(shield4);

        cardObjs[0] = cardObjs_p1;
        cardObjs[1] = cardObjs_p2;
        cardObjs[2] = cardObjs_p3;
        cardObjs[3] = cardObjs_p4;

        sponsorStageATK[0] = stageAtk1;
        sponsorStageATK[1] = stageAtk2;
        sponsorStageATK[2] = stageAtk3;
        sponsorStageATK[3] = stageAtk4;
        sponsorStageATK[4] = stageAtk5;
        gameBegin = true;
    }

    void load_dealing()
    {
        loadDeckSys();
        dealing(advantureDeck);
    }
    void loadDeckSys()
    {
        Deck aDeck = new Deck();
        aDeck.loadCard();
        aDeck.shuffle();
        advantureDeck = aDeck.getAdvantureDeck();
        questDeck = aDeck.getQuestDeck();
        tournDeck = aDeck.getTournDeck();
        eventDeck = aDeck.getEventDeck();
        rankDeck = aDeck.getRankDeck();
        shieldDeck = aDeck.getShieldDeck();
    }

    void dealing(List<Card> alist)
    {
        int count = 0;
        while (count < 48)
        {
            for (int i = 0; i < CURRENTPLAYERNUM; i++)
            {
                List<Card> aCard = hands[i];
                aCard.Add(alist[0]);
                if (i == 0)
                {
                    string cardName = alist[0].getName();
                    getCard(players[i], 5, cardName, 1);
                }
                else
                {
                    string cardName = alist[0].getName();
                    getCard(players[i], 0, cardName, (i + 1));
                }
                alist.Remove(alist[0]);
                count++;
            }
        }
        for (int i = 0; i < CURRENTPLAYERNUM; i++) {
            for (int j=0; j<players[i].getHand().Count; j++)
            {
                gameLog.info("players " + players[i].getName() + " has " + players[i].getHand()[j].getName());
            }
        }
    }

    public void storyEvent4()
    {

        gameLog.info("In order to test AIstrategy, upgrade everyone's rank to knight ");
        p1.addAShield(shieldDeck[0]);
        //p1.addAShield (shieldDeck [0]);
        p1.addAShield(shieldDeck[0]);
        //p2.addAShield (shieldDeck [0]);
        p2.addAShield(shieldDeck[0]);
        p2.addAShield(shieldDeck[0]);
        p3.addAShield(shieldDeck[0]);
        p3.addAShield(shieldDeck[0]);
        p3.addAShield(shieldDeck[0]);
        p4.addAShield(shieldDeck[0]);
        p4.addAShield(shieldDeck[0]);
        checkRankUpdate(p1);
        checkRankUpdate(p2);
        checkRankUpdate(p3);
        checkRankUpdate(p4);

        QuestCard aQuestcard = questDeck.Find(x => x.getName().Contains("VKAE"));
        List<Card>[] AISponsorStage = new List<Card>[aQuestcard.getStageNum()];
        Context context = new Context(new DoISponsorAQuestA(), aQuestcard, players, p4);
        gameLog.info("this is atrategyA, use p4 card");
        gameLog.info("p4 cards:");
        for (int i = 0; i < p4.getHand().Count; i++)
        {
            gameLog.info("p4 has " + p4.getHand()[i].getName());
        }
        AISponsorStage = context.DoISponsorAQuest();

        if (AISponsorStage == null)
        {
            gameLog.info("AI does not sponsor");
        }


        aQuestcard = questDeck.Find(x => x.getName().Contains("RTSR"));
        AISponsorStage = new List<Card>[aQuestcard.getStageNum()];
        context = new Context(new DoISponsorAQuestB(), aQuestcard, players, p3);
        gameLog.info("this is atrategyB, use p3 card");
        gameLog.info("p3 cards:");
        for (int i = 0; i < p3.getHand().Count; i++)
        {
            gameLog.info("p3 has " + p3.getHand()[i].getName());
        }
        AISponsorStage = context.DoISponsorAQuest();

        if (AISponsorStage == null)
        {
            gameLog.info("AI does not sponsor");
        }
    }

    public void randomStoryCard()
    {
        //  next_story.SetActive(false);
        questNow = null;
        eventNow = null;
        tournNow = null;
        int randomEvent;
        randomEvent = randomTheEventDeck();
        if (randomEvent == 0)
        {
            questNow = questDeck[0];
            numOfStagesNow = questNow.getStageNum();
            print("now handle questcard " + questNow.getName());
            getCard(null, 5, questNow.getName(), 0);
            msgBar.SetActive(true);
            askSponsorBTN.SetActive(true);
            handleQuestEventSponsor(questNow, sponsorNow);
            questDeck.Remove(questDeck[0]);
        }
        else if (randomEvent == 1)
        {
            tournNow = tournDeck[0];
            print("now handle tourncard " + tournNow.getName());
            getCard(null, 5, tournNow.getName(), 0);
            handleTouraEvent(tournNow);
            tournDeck.Remove(tournDeck[0]);
        }
        else if (randomEvent == 2)
        {
            eventNow = eventDeck[0];
            print("now handle eventcard " + eventNow.getName());
            getCard(null, 5, eventNow.getName(), 0);
            handleEventEvent(eventNow);
            eventDeck.Remove(eventDeck[0]);
        }
        else
        {
            print("all story card have been used, wait for reloading");
            checkDeckCapacity();
            randomStoryCard();   //reload and recall this function
        }
    }


    int randomTheEventDeck()
    {
        int randomEvent = -1;
        if (questDeck.Count > 0 && tournDeck.Count > 0 && eventDeck.Count > 0)
        {
            randomEvent = Random.Range(0, 3);
        }
        if (questDeck.Count == 0 && tournDeck.Count == 0 && eventDeck.Count > 0)
        {
            randomEvent = 2;
        }
        if (questDeck.Count == 0 && tournDeck.Count > 0 && eventDeck.Count == 0)
        {
            randomEvent = 1;
        }
        if (questDeck.Count > 0 && tournDeck.Count == 0 && eventDeck.Count == 0)
        {
            randomEvent = 0;
        }
        if (questDeck.Count > 0 && tournDeck.Count > 0 && eventDeck.Count == 0)
        {
            randomEvent = Random.Range(0, 2);
        }
        if (questDeck.Count == 0 && tournDeck.Count > 0 && eventDeck.Count > 0)
        {
            randomEvent = Random.Range(1, 3);
        }
        if (questDeck.Count > 0 && tournDeck.Count == 0 && eventDeck.Count > 0)
        {
            int temp = Random.Range(0, 2);
            if (temp == 0)
                randomEvent = 0;
            else
                randomEvent = 2;

        }
        return randomEvent;
    }

    void handleQuestEventSponsor(QuestCard aQuestcard, Player aSponsor)
    {
        QuestCard aCard = aQuestcard;
        int stageNum = aCard.getStageNum();


        List<GameObject>[] theStages = { stageCards1_objs, stageCards2_objs, stageCards3_objs, stageCards4_objs, stageCards5_objs };
        if (aSponsor != null)
        {
            if (aSponsor.getType() == PlayerType.PLAYER)
            {
                gameLog.info("this quest has " + stageNum + " stages");
                Card theQuest;
                //	
                theQuest = aSponsor.getHand().Find(x => x.getName().Contains("Saxons"));

                for (int i = 0; i < stageNum; i++)
                {
                    for (int j = 0; j < theStages[i].Count; j++)
                    {
                        if (theStages[i][j].name != null)
                            print(theStages[i][j].name);
                    }
                }
            }
            else if (aSponsor.getType() == PlayerType.AI)
            {
                List<Card>[] AISponsorStage = new List<Card>[aQuestcard.getStageNum()];

                Context context = new Context(new DoISponsorAQuestA(), aQuestcard, players, aSponsor);
                AISponsorStage = context.DoISponsorAQuest();
                if (AISponsorStage == null)
                {
                    gameLog.info("AI does not sponsor");
                }
                else
                {
                    print("fff");
                }
                //next 

            }
            else
            {
                if (aSponsor.getType() == PlayerType.AI)
                {
                    List<Card>[] a;
                    Context context;
                    // context = new Context(new DoISponsorAQuestA(), aQuestcard.getStageNum());
                    //a = context.DoISponsorAQuest();
                }

            }
        }


    }

    void calculateATK(QuestCard aQuestCard, int stageNum, List<Card>[] stages)
    {
        for (int i = 0; i < stages.Length; i++)
        {
            setUpSpecialATK(aQuestCard, stages[i]);
        }
        for (int i = 0; i < stageNum; i++)   //check atk valid
        {
            if (stages[i].Count == 0) sponsorStageATK[i] = 0;
            for (int j = 0; j < stages[i].Count; j++)
            {
                if (j == 0 && stages[i][j].getKind() == Kind.TEST)
                {
                    if (i == 0)
                    {
                        sponsorStageATK[i] = 0;
                    }
                    else
                    {
                        sponsorStageATK[i] = sponsorStageATK[i - 1] + 1;
                    }
                }
                else if (stages[i][j].getKind() == Kind.FOE)
                {
                    /* if (aQuestCard.getName().Contains("BH") && stages[i][j].getName().Contains("Boar"))
                     {
                         FoeCard aFoeCard = (FoeCard)stages[i][j];
                         sponsorStageATK[i] += aFoeCard.getAtkSpecial();
                     }
                     else
                     {*/
                    FoeCard aFoeCard = (FoeCard)stages[i][j];
                    sponsorStageATK[i] += aFoeCard.getAtk();
                    // }
                }
                else if (stages[i][j].getKind() == Kind.ALLY)
                {
                    AllyCard anAllyCard = (AllyCard)stages[i][j];
                    sponsorStageATK[i] += anAllyCard.getAtk();
                }
                else if (stages[i][j].getKind() == Kind.WEAPON)
                {
                    WeaponCard aWeaponCard = (WeaponCard)stages[i][j];
                    sponsorStageATK[i] += aWeaponCard.getAtk();
                }
                else if (stages[i][j].getKind() == Kind.AMOUR)
                {
                    AmourCard anAmourCard = (AmourCard)stages[i][j];
                    sponsorStageATK[i] += anAmourCard.getAtk();
                }
                else
                {
                    print(stages[i][j].getName());
                    gameLog.error("failure to get atk num");
                }
            }
        }
        gameLog.info("test card's is recognized as a card has 1 attack.");
        for (int i = 0; i < stageNum; i++)
        {
            gameLog.info("stage " + i + "'s total attack is " + sponsorStageATK[i]);
        }
    }

    int calculatePlayerATK(QuestCard aQuestCard, List<Card> aPlayerList, Player aPlayer)
    {
        int totalATK = 0;
        if (aPlayerList.Count == 0)
        {
            gameLog.info(aPlayer.getName() + " plays nothing, only his rank ATK is " + aPlayer.getAtk());
            totalATK += aPlayer.getAtk();
            return totalATK;
        }
        else
        {
            setUpSpecialATK(aQuestCard, aPlayerList);
            for (int i = 0; i < aPlayerList.Count; i++)
            {
                if (aPlayerList[i] == null) { }
                else if (aPlayerList[i].getName().Contains("QuestsWeap"))
                {
                    WeaponCard aCard = (WeaponCard)aPlayerList[i];
                    totalATK += aCard.getAtk();
                }
                else if (aPlayerList[i].getName().Contains("QuestsAlly"))
                {
                    AllyCard aCard = (AllyCard)aPlayerList[i];
                    totalATK += aCard.getAtk();
                }
                else if (aPlayerList[i].getName().Contains("QuestsAmou"))
                {
                    AmourCard aCard = (AmourCard)aPlayerList[i];
                    totalATK += aCard.getAtk();
                }
                else gameLog.error("failure to get card atk");
            }
        }
        totalATK += aPlayer.getAtk();
        gameLog.info(aPlayer.getName() + "'s total ATK and add his rank ATK in this stage is " + totalATK);

        return totalATK;

    }

    void setUpSpecialATK(QuestCard aQuestCard, List<Card> thelist)
    {
        List<Card> alist = thelist;
        if (alist[0] == null)
            return;
        for (int i = 0; i < alist.Count; i++)
        {
            if (alist[i].getKind() == Kind.FOE)
            {
                FoeCard theFoe = (FoeCard)alist[i];
                theFoe.initialTheATK();
                if (aQuestCard.getName().Contains("JTTEF"))
                    if (theFoe.getName().Contains("Evil"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("RTSR"))
                    if (theFoe.getName().Contains("Saxons"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("BH"))
                    if (theFoe.getName().Contains("Boar"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("DTQH"))
                    theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("STD"))
                    if (theFoe.getName().Contains("Dragon"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("RTFM"))
                    if (theFoe.getName().Contains("Black"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("SFTHG"))
                    theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("TOTGK"))
                    if (theFoe.getName().Contains("Green"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
            }
            else
            {
            }
        }
    }

    void handleTouraEvent(TournamentCard aTournCard)
    {
        TournamentCard aCard = aTournCard;
        print(aCard.getName());
    }

    void handleEventEvent(EventCard aEventCard)
    {
        EventCard aCard = aEventCard;
        //store players weapon cards and foe cards
        List<Card> thisWeaponList = new List<Card>();
        List<Card> thisFoeList = new List<Card>();

        float[] rankLevel = new float[4];
        for (int i = 0; i < CURRENTPLAYERNUM; i++)
        {
            if (players[i].getRank() == Rank.SQUIRE)
                rankLevel[i] = 0 + (players[i].getShield().Count / 10f);
        }
        float min = rankLevel[0];
        for (int i = 1; i < rankLevel.Length; i++)
        {
            if (rankLevel[i] < min)
                min = rankLevel[i];
        }
        float max = rankLevel[0];
        for (int i = 1; i < rankLevel.Length; i++)
        {
            if (rankLevel[i] > max)
                max = rankLevel[i];
        }

        if (aCard.getName().Contains("SE_PT"))
        {
            gameLog.info("the story card is Prosperity Throughout the Realm");
            gameLog.info("all player get 2 adventure cards");
            drawACard(p1, null, null, null);
            drawACard(p1, null, null, null);
            drawACard(p2, null, null, null);
            drawACard(p2, null, "Weap", null);
            drawACard(p3, null, null, null);
            drawACard(p3, null, null, "Amou");
            drawACard(p4, null, null, null);
            drawACard(p4, null, "Foe", null);
        }
        else if (aCard.getName().Contains("SE_CD"))
        {

            for (int i = 0; i < CURRENTPLAYERNUM; i++)
            {
                if (rankLevel[i] == min)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        players[i].addAShield(shieldDeck[0]);
                        checkRankUpdate(players[i]);
                        shieldDeck.Remove(shieldDeck[0]);
                    }
                    gameLog.info(players[i].getName() + " gets three shield");
                }
                gameLog.info(players[i].getName() + " has " + players[i].getShield().Count + " shields");
            }
        }//pox
        else if (aCard.getName().Contains("PO"))
        {
            //if this player is not sponsor and it has at least one shield
            for (int i = 0; i < players.Length; i++)
            {
                if ((players[i] != sponsorNow) && (players[i].getShield().Count >= 1))
                {
                    List<ShieldCard> temp = players[i].getShield();
                    temp.Remove(temp[0]);
                    players[i].setShield(temp);
                }
            }
        }//Plague
        else if (aCard.getName().Contains("PL"))
        {
            //if this player is drawer and it has at least two shields
            for (int i = 0; i < players.Length; i++)
            {
                if ((players[i] == sponsorNow) && (players[i].getShield().Count >= 2))
                {
                    for (int j = 0; j < 2; j++)
                    {
                        List<ShieldCard> temp = players[i].getShield();
                        temp.Remove(temp[0]);
                        players[i].setShield(temp);
                    }
                }
            }
        }//King's Recognition
        else if (aCard.getName().Contains("KR"))
        {
            //The next player(s) to complete a Quest will receive 2 extra shields
        }//Queen's Favor
        else if (aCard.getName().Contains("QF"))
        {

            for (int i = 0; i < CURRENTPLAYERNUM; i++)
            {
                if (rankLevel[i] == min)
                {
                    drawACard(players[i], null, null, null);
                    discardExtraCard(players[i], null);
                    drawACard(players[i], null, null, null);
                    discardExtraCard(players[i], null);
                }
            }
        }//Court Called to Camelot
        else if (aCard.getName().Contains("CC"))
        {
            List<Card> temp;
            for (int i = 0; i < players.Length; i++)
            {
                temp = players[i].getAllayOrAmour();
                temp.Clear();
                players[i].setAllayOrAmour(temp);
            }

        }
        else if (aCard.getName().Contains("KC"))
        {
            //if this player is the highest rank player
            for (int i = 0; i < CURRENTPLAYERNUM; i++)
            {
                if (rankLevel[i] == max)
                {
                    for (int j = 0; j < players[i].getHand().Count; j++)
                    {
                        //if player has weapon card, add it into list
                        if (players[i].getHand()[j].getKind() == Kind.WEAPON)
                        {
                            thisWeaponList.Add(players[i].getHand()[j]);
                        }
                        //if player has foe card, add it into list
                        if (players[i].getHand()[j].getKind() == Kind.FOE)
                        {
                            thisFoeList.Add(players[i].getHand()[j]);
                        }
                    }
                    //if player has weapon card
                    if (thisWeaponList.Count != 0)
                    {
                        players[i].discard(thisWeaponList[0]);
                    }
                    else
                    {
                        //if player do not have weapon card
                        if (thisFoeList.Count == 0)
                        {
                            //do nothing
                        }
                        else if (thisFoeList.Count >= 2)
                        {
                            players[i].discard(thisFoeList[0]);
                            players[i].discard(thisFoeList[1]);
                        }
                        else
                        {
                            players[i].discard(thisFoeList[0]);
                        }
                    }
                }
            }
        }


    }

    //just for test
    Card getRandomCardFromSponsor(Player aPlayer)
    {
        int randomCard = Random.Range(0, aPlayer.getHand().Count);
        Card firstCard = aPlayer.getHand()[randomCard];
        return firstCard;
    }
    //ready for delete

    bool testDiscard(int num, Player aPlayer)
    {
        int discardNum = 0;
        while (discardNum < num)
        {
            if (aPlayer.getHand().Count < num) return false;
            Debug.LogAssertion("should ask play to choose card to discard and check");

            //event discard
            //if success
            Debug.Log(aPlayer.getName() + "has successfully discard card " + aPlayer.getHand()[0].getName());
            aPlayer.discard(aPlayer.getHand()[0]);
            discardNum++;
        }
        return true;
    }
    //wrong
    void drawACard(Player aPlayer, string getTheCard, string discardTheCard, string allyOrAmour)
    {
        Card theCard;
        if (getTheCard == null) theCard = advantureDeck[0];
        else theCard = advantureDeck.Find(x => x.getName().Contains(getTheCard));
        if (theCard == null) { gameLog.error("failure to add " + getTheCard + " to player's hand"); }
        else
        {
            aPlayer.draw(theCard);
            if (currentView == 0)
            {
                if (aPlayer.getName() == p1.getName())
                {
                    getCard(p1, 5, theCard.getName(), 1);
                    sortCard(p1);
                }
                if (aPlayer.getName() == p2.getName())
                {
                    getCard(p2, 5, theCard.getName(), 2);
                }
                if (aPlayer.getName() == p3.getName())
                {

                    getCard(p3, 5, theCard.getName(), 3);
                }
                if (aPlayer.getName() == p4.getName())
                {

                    getCard(p4, 5, theCard.getName(), 4);
                }
            }
            else if (currentView == 1)
            {
                if (aPlayer.getName() == p1.getName())
                {
                    getCard(p1, 5, theCard.getName(), 4);
                }
                if (aPlayer.getName() == p2.getName())
                {
                    getCard(p2, 5, theCard.getName(), 1);
                    sortCard(p2);
                }
                if (aPlayer.getName() == p3.getName())
                {

                    getCard(p3, 5, theCard.getName(), 2);
                }
                if (aPlayer.getName() == p4.getName())
                {

                    getCard(p4, 5, theCard.getName(), 3);
                }
            }

            gameLog.info("player " + aPlayer.getName() + " draw a " + theCard.getName() + " card");
            advantureDeck.Remove(theCard);
        }
        if (allyOrAmour != null)
        {
            if (allyOrAmour.Contains("Amou"))
            {
                AmourCard aCard = (AmourCard)aPlayer.getHand().Find(x => x.getName().Contains(allyOrAmour));
                if (aCard != null) playAmour(aPlayer, aCard);
            }
        }
    }

    public Card discardExtraCard(Player aPlayer, string discardTheCard)
    {
        Card aCard = new Card();
        if (aPlayer.getHand().Count > 12)
        {
            if (discardTheCard == null && aPlayer.getType() == PlayerType.AI)
            {
                gameLog.warn(aPlayer.getName() + " have more than 12 cards on hand, discard");
                bool valid = false;
                while (!valid)
                {
                    int randomInt = Random.Range(0, aPlayer.getHand().Count);
                    aCard = aPlayer.getHand()[randomInt];
                    if (!aCard.getName().Contains("Excalibur") || !aCard.getName().Contains("Lance"))
                    {
                        discardListAdv.Add(aCard);
                        aPlayer.discard(aCard);
                        gameLog.info(aPlayer.getName() + " discard " + aCard.getName());
                        valid = true;
                    }
                }
                return aCard;
            }
            else if (discardTheCard == null && aPlayer.getType() != PlayerType.AI)
            {
                aCard = aPlayer.getHand()[aPlayer.getHand().Count - 1];
                aPlayer.getHand().Remove(aCard);
            }
            else
            {
                gameLog.warn(aPlayer.getName() + " you have more than 12 cards on hand, discard the chosen card");
                aCard = aPlayer.getHand().Find(x => x.getName().Contains(discardTheCard));
                if (aCard != null)
                {
                    discardListAdv.Add(aCard);
                    aPlayer.discard(aCard);
                    gameLog.info(aPlayer.getName() + " has discard " + aCard.getName());
                    return aCard;
                }
                else
                {
                    gameLog.info("failure to discard the specific card " + discardTheCard);
                    gameLog.warn("random demove card");
                    int randomInt = Random.Range(0, aPlayer.getHand().Count);
                    aCard = aPlayer.getHand()[randomInt];
                    discardListAdv.Add(aCard);
                    aPlayer.discard(aCard);
                    return aCard;
                }
            }

        }
        return null;
    }

    void playAmour(Player aPlayer, AmourCard aAmourCard)
    {
        if (aPlayer.getHand().Find(x => x.getName() == aAmourCard.getName()) == null)
        {
            gameLog.error("The Amour Card " + aAmourCard.getName() + " not found");
        }
        else
        {
            aPlayer.addAllayOrAmour(aAmourCard);
            gameLog.info(aPlayer.getName() + " uses an Amour Card called " + aAmourCard.getName());
            gameLog.info("his current ATK is " + aPlayer.getAtk());
            aPlayer.discard(aAmourCard);
        }


    }

    void checkRankUpdate(Player aPlayer)
    {
        Player thePlayer = aPlayer;
        if (thePlayer.getRank() == Rank.SQUIRE)
        {
            if (thePlayer.getShield().Count >= 5)
            {
                List<ShieldCard> temp = thePlayer.getShield();
                for (int j = 0; j < 5; j++)
                {
                    temp.Remove(temp[0]);
                }
                aPlayer.setShield(temp);
                aPlayer.upgradeRank();
            }
        }
        else if (thePlayer.getRank() == Rank.KNIGHT)
        {
            if (thePlayer.getShield().Count >= 7)
            {
                aPlayer.upgradeRank();
                for (int j = 0; j < 7; j++)
                {
                    thePlayer.getShield().Remove(thePlayer.getShield()[0]);
                }
            }
        }
        else if (thePlayer.getRank() == Rank.CHAMPIONKNIGHT)
        {
            if (thePlayer.getShield().Count >= 10)
            {
                aPlayer.upgradeRank();
                for (int j = 0; j < 10; j++)
                {
                    thePlayer.getShield().Remove(thePlayer.getShield()[0]);
                }
            }
        }
    }

    void checkDeckCapacity()
    {
        if (advantureDeck.Count == 0)
        {
            for (int i = 0; i < discardListAdv.Count; i++)
            {
                advantureDeck.Add(discardListAdv[i]);
            }
            discardListAdv.Clear();
            print("advCard reloading...");
        }
    }

    public void getCard(Player aPlayer, float distance, string aName, int which)
    {
        GameObject toMove = GameObject.Find(aName);
        List<Vector3> position = new List<Vector3> { pos_story, pos_p1, pos_p2, pos_p3, pos_p4 };
        Vector3 toPos;
        if (aPlayer != null) {
            toPos = pos_p1 + new Vector3(distance, 0, 1f) * (aPlayer.getHand().Count - 1);
            position[1] = toPos;
        }
        
        if (which == 0)
        {
            iTween.MoveTo(toMove, position[which], 0.8f);
        }
        else if(which == 1)
        {
            iTween.MoveTo(toMove, position[which], 0f);
        }
        else
        {
            iTween.MoveTo(toMove, position[which], 0f);
        }

    }

    public void resetAllstages()
    {
        stageCards1_objs = new List<GameObject>();
        stageCards2_objs = new List<GameObject>();
        stageCards3_objs = new List<GameObject>();
        stageCards4_objs = new List<GameObject>();
        stageCards5_objs = new List<GameObject>();
    }

    public bool sponsorCardsIn(int stage)
    {
        List<GameObject> cards = new List<GameObject>();
        List<GameObject> used = new List<GameObject>();
        //List<string> thisStageCards = new List<string> ();
        Vector3 usedAdven_POS = GameObject.Find("USED_ADVENTURE").transform.position;
        List<Card> stage1 = new List<Card>();
        List<Card> stage2 = new List<Card>();
        List<Card> stage3 = new List<Card>();
        List<Card> stage4 = new List<Card>();
        List<Card> stage5 = new List<Card>();
        stages[0] = stage1;
        stages[1] = stage2;
        stages[2] = stage3;
        stages[3] = stage4;
        stages[4] = stage5;

        for (int i = 0; i < sponsorNow.getHand().Count; i++)
        {
            cards.Add(GameObject.Find(sponsorNow.getHand()[i].getName()));
        }

        //bool waitThis = false;

        if (stage == 1)
        {
            resetAllstages();
            sponsorUsedCard.Clear();
        }

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetComponent<TouchCard>().isChosen())
            {
                Card aCard = sponsorNow.getHand().Find(x => x.getName() == cards[i].name);
                stages[stage - 1].Add(aCard);
                sponsorUsedCard.Add(aCard);
                cards[i].GetComponent<TouchCard>().stopActivateCard();
            }
        }

        if (checkValidPerStage(stages[stage - 1], (stage - 1)) == true)
        {

            for (int i = 0; i < stages[stage - 1].Count; i++)
            {
                sponsorNow.getHand().Remove(stages[stage - 1][i]);
                GameObject go = new GameObject();
                go = cards.Find(x => x.name == stages[stage - 1][i].getName());
                if (go != null)
                {
                    used_adventure_Objs.Add(go);
                    iTween.MoveTo(go, placeSponsorInRightPos(inWhichStage) + new Vector3(0, -3, 1) * i, 1f);
                    go.GetComponent<TouchCard>().OnMouseExit();
                    StartCoroutine(sorting(sponsorNow));
                }
                else
                    Debug.Log("failure to remove card from hand");
            }
            return true;
        }
        else
        {
            for (int i = 0; i < stages[stage - 1].Count; i++)
            {
                print("this stage is not valid " + (stage - 1));
            }
        }

        return false;
    }

    IEnumerator sorting(Player ap)
    {
        yield return new WaitForSeconds(0.2f);
        sortCard(ap);
    }

    public void sortCard(Player who)
    {
        List<GameObject> temp_player_hand = new List<GameObject>();

        for (int i = 0; i < who.getHand().Count; i++)
        {
            GameObject go = GameObject.Find(who.getHand()[i].getName());
            if (go != null)
                temp_player_hand.Add(go);
            else
                who.getHand()[i].getName();

        }

        for (int i = 0; i < temp_player_hand.Count; i++)
        {
			temp_player_hand [i].transform.localScale = new Vector3 (1f, 1f, 1f);
            Vector3 toPosition = pos_p1 + new Vector3(5, 0, 1) * i;
            iTween.MoveTo(temp_player_hand[i], toPosition, 0.0f);
			temp_player_hand [i].GetComponent<TouchCard> ().activateCard ();
        }
			
    }

    public void endSponsor()
    {
        sponsorEnd.SetActive(true);
    }

	 
	public void askIfJoin(int stat)
    {
		if (joinCount < 2) {
			if (stat == 0) {
				joinCount = 0;
				switchView ();
				sponsorEnd.SetActive (false);
				msgBarTXT.text = "Would you like to join this Quest?";
				msgBar.SetActive (true);
				askJoinBTN.SetActive (false);
				askJoinBTN.SetActive (true);
			}

			if (stat == 1) {
				joinCount++;
				playerInQuest.Add (players [currentView]);
                print(players[currentView].getName() + "join");
				switchView ();
				askJoinBTN.SetActive (false);
				askJoinBTN.SetActive (true);
			}
			if (stat == 2) {
				joinCount++;
				switchView ();
				askJoinBTN.SetActive (false);
				askJoinBTN.SetActive (true);
			}
		} else {
			if (stat == 1) {
				playerInQuest.Add (players [currentView]);
			}
			switchView ();
			msgBar.SetActive (false);
			askJoinBTN.SetActive (false);
			if (playerInQuest.Count == 0) {
				guideTxt.text = "No player wants to join this Quest.";
			} else {
				string resultStr = playerInQuest.Count + " player(s) joined this Quest...";
				guideTxt.text = resultStr;
			}
			msgBarTXT.text = "Play the card for the stage..";
			msgBar.SetActive (true);
			guideMSG.SetActive (true);
			playCardsInQuestBtn.SetActive (true);

            while (players[currentView] != playerInQuest[0])
            {
                switchView();
            }
			otherPlayersCardsInQuest = new List<Card>[playerInQuest.Count];
            StartCoroutine(sorting(players[currentView]));    
			/**call something here
			 * 
			 *1. deal a card to each player joined this quest
			 **/
			StartCoroutine (guideDisplay());
            
		}
    }

    //WRONG
    public void startLetOthersPlay()
    {
        msgBar.SetActive(false);
        askJoinBTN.SetActive(false);
        playerInQuest.Add(p2);
        playerInQuest.Add(p3);
        playerInQuest.Add(p4);
        for (int i = 0; i < playerInQuest.Count; i++)
        {
            if (roundNow == 1)
            {
                playerInQuest[i].setPass(true);
                gameLog.info(playerInQuest[i].getName() + " wants to join the quest");
            }
        }
        drawACard(p3, null, null, null);
        discardExtraCard(p3, null);
        drawACard(p4, null, null, null);
        discardExtraCard(p4, null);
        drawACard(p2, null, null, null);


        sortCard(p2);
        msgBarTXT.text = "You have more than 12 Cards, please discard..";
        msgBar.SetActive(true);
        discardBTN.SetActive(true);
        List<GameObject> cards = new List<GameObject>();
        //Card theDiscardCard;
        for (int i = 0; i < p2.getHand().Count; i++)
        {
            cards.Add(GameObject.Find(p2.getHand()[i].getName()));

            cards[i].GetComponent<TouchCard>().activateCard();
        }

    }

    //wrong
    public void discardThis()
    {
        Player thePlayer = p1;
        if (currentView == 1)
            thePlayer = p2;
        else if (currentView == 0)
            thePlayer = p1;

        List<GameObject> cards = new List<GameObject>();
        Card theDiscardCard;
        for (int i = 0; i < thePlayer.getHand().Count; i++)
        {
            cards.Add(GameObject.Find(thePlayer.getHand()[i].getName()));
            //cards [i].GetComponent<TouchCard> ().activateCard ();
            if (cards[i].GetComponent<TouchCard>().isChosen())
            {
                theDiscardCard = discardExtraCard(thePlayer, cards[i].name);
                if (theDiscardCard != null)
                {
                    GameObject thisCard = GameObject.Find(theDiscardCard.getName());
                    Vector3 aPos = GameObject.Find("USED_ADVENTURE").transform.position;
                    iTween.MoveTo(thisCard, aPos, 2f);
                    sortCard(thePlayer);
                }
                else
                    gameLog.error("failure to discard the chosen card");
            }
        }

        if (currentView == 1)
        {
            playCardMsg.SetActive(true);
            disCardMsg.SetActive(false);
        }
        else
        {
            disCardMsg.SetActive(false);
            Vector3 aPos = GameObject.Find("USED_ADVENTURE").transform.position;
            iTween.MoveTo(GameObject.Find(questNow.getName()), aPos, 1f);
        }
    }
    //wrong
    public void discard()
    {
        Player thePlayer = p1;
        if (currentView == 1)
            thePlayer = p2;
        else if (currentView == 0)
            thePlayer = p1;

        List<GameObject> cards = new List<GameObject>();
        Card theDiscardCard;
        for (int i = 0; i < thePlayer.getHand().Count; i++)
        {
            cards.Add(GameObject.Find(thePlayer.getHand()[i].getName()));
            //cards [i].GetComponent<TouchCard> ().activateCard ();
            if (cards[i].GetComponent<TouchCard>().isChosen())
            {
                theDiscardCard = discardExtraCard(thePlayer, cards[i].name);
                if (theDiscardCard != null)
                {
                    GameObject thisCard = GameObject.Find(theDiscardCard.getName());
                    Vector3 aPos = GameObject.Find("USED_ADVENTURE").transform.position;
                    iTween.MoveTo(thisCard, aPos, 2f);
                    sortCard(thePlayer);
                }
                else
                    gameLog.error("failure to discard the chosen card");
            }
        }
        Vector3 aPos1 = GameObject.Find("USED_ADVENTURE").transform.position;
        iTween.MoveTo(GameObject.Find(eventNow.getName()), aPos1, 1f);
        disCardMsg2.SetActive(false);
        next_story.SetActive(true);


    }



    public void activateGame()
    {
        gameIsOn = true;
    }




	IEnumerator guideDisplay()
    {
        yield return new WaitForSeconds(3.0f);
		guideMSG.SetActive(false);
    }




    public void switchView()
    {
        if (currentView == (CURRENTPLAYERNUM - 1))
            currentView = 0;
        else currentView += 1;
        if (currentView == 0)
        {
            for (int i=0; i<CURRENTPLAYERNUM; i++)
            {
                for (int j = 0; j < players[i].getHand().Count; j++) {
                    if (i == currentView)
                    {
                        getCard(players[i], 5, players[i].getHand()[j].getName(), 1);
                    }
                    else getCard(players[i], 0, players[i].getHand()[j].getName(), (i + 1));
                }
            }
        }
        else if (currentView == 1)
        {
            for (int i = 0; i < CURRENTPLAYERNUM; i++)
            {
                for (int j = 0; j < players[i].getHand().Count; j++)
                {
                    if (i == currentView)
                    {
                        getCard(players[i], 5, players[i].getHand()[j].getName(), 1);
                    }
                    else if (i == 0)
                    {
                        getCard(players[i], 5, players[i].getHand()[j].getName(), 4);
                    }
                    else getCard(players[i], 0, players[i].getHand()[j].getName(), i);
                }
            }
        }
        else if (currentView == 2)
        {
            for (int i = 0; i < CURRENTPLAYERNUM; i++)
            {
                for (int j = 0; j < players[i].getHand().Count; j++)
                {
                    if (i == currentView)
                    {
                        getCard(players[i], 5, players[i].getHand()[j].getName(), 1);
                    }
                    else if (i == 0)
                    {
                        getCard(players[i], 5, players[i].getHand()[j].getName(), 3);
                    }
                    else getCard(players[i], 0, players[i].getHand()[j].getName(), (5-i));
                }
            }
        }
        else
        {
            for (int i = 0; i < CURRENTPLAYERNUM; i++)
            {
                for (int j = 0; j < players[i].getHand().Count; j++)
                {
                    if (i == currentView)
                    {
                        getCard(players[i], 5, players[i].getHand()[j].getName(), 1);
                    }
                    else getCard(players[i], 0, players[i].getHand()[j].getName(), (i+2));
                }
            }
        }
        //StartCoroutine(sorting(players[currentView]));    
		sortCard(players[currentView]);
    }

    public bool checkValidPerStage(List<Card> alist, int stageNum)
    {
        int foeCount = 0;
        bool test = false;

        print("the stage in check is has card num " + alist.Count);
        //print ("the quest has total stagenum is " + questNow.getStageNum ());

        setUpSpecialATK(questNow, alist);

        if (alist.Count == 0)
        {
            print("you should put at least one card in per stage");
            gameLog.warn("you should put at least one card in per stage");
            return false;
        }
        for (int i = 0; i < alist.Count; i++)
        {
            if (alist[i].getKind() == Kind.TEST)
            {
                test = true;
                if (stageNum == 0)
                    sponsorStageATK[stageNum] += 1;
                else
                    sponsorStageATK[stageNum] = sponsorStageATK[stageNum - 1] + 1;
                if (alist.Count != 1)
                {
                    gameLog.error("stage " + (stageNum + 1) + " is not valid because of the test require");
                    print("stage " + (stageNum + 1) + " is not valid because of the test require");
                    alist.Clear();
                    //sortCard (obj_stages [stageNum - 1]);
                    sponsorStageATK[stageNum] = 0;
                    return false;
                }
            }
            if (alist[i].getKind() == Kind.FOE)
            {
                foeCount += 1;
                FoeCard aFoe = (FoeCard)alist[i];
                sponsorStageATK[stageNum] += aFoe.getAtk();
            }
            if (alist[i].getKind() == Kind.WEAPON)
            {
                WeaponCard aWeapon = (WeaponCard)alist[i];
                sponsorStageATK[stageNum] += aWeapon.getAtk();
            }
        }
        if (foeCount != 1 && test != true)
        {
            gameLog.error("stage " + stageNum + " is not valid duo to foeCard num");
            print("stage " + stageNum + " is not valid duo to foeCard num");
            alist.Clear();
            return false;
        }
        if (stageNum > 0)
        {
            if (sponsorStageATK[stageNum] <= sponsorStageATK[stageNum - 1])
            {
                print("the total atk in each stage should increasing");
                gameLog.error("the total atk in each stage should increasing");
                return false;
            }
        }
        print("total ATK in this stage is " + sponsorStageATK[stageNum]);
        gameLog.info("total ATK in this stage is " + sponsorStageATK[stageNum]);
        return true;
    }

    public void sponsorYESorNO(bool isSpon)
    {
        if (isSpon)
        {
            msgBar.SetActive(false);
            askSponsorBTN.SetActive(false);
            sponsorCard.SetActive(true);
            sponsorNow = players[currentView];
            print(sponsorNow.getName());
            List<GameObject> tempP1Obj = new List<GameObject>();
            for (int i = 0; i < sponsorNow.getHand().Count; i++)
            {
                tempP1Obj.Add(GameObject.Find(sponsorNow.getHand()[i].getName()));
                tempP1Obj[i].GetComponent<TouchCard>().activateCard();
            }
        }
        else
        {
            switchView();
        }
    }

    public int getWhichStageNowIs()
    {
        return inWhichStage;
    }

    public Vector3 placeSponsorInRightPos(int whichStage)
    {
        string posName = "s_";
        posName += numOfStagesNow;
        posName += "_";
        posName += whichStage;
        return (GameObject.Find(posName).GetComponent<Transform>().position);
    }

    public void othersPlay()
    {
        for (int i = 0; i < playerInQuest.Count; i++)
        {
            if (playerInQuest[i].getPass() == true)
                gameLog.info(playerInQuest[i].getName() + " still in the quest, continue...");
        }
        checkValidOtherPlayersInQuestPlayCards(roundNow);
    }

    void checkValidOtherPlayersInQuestPlayCards(int round)
    {
        List<GameObject> ThisPlayerCards = new List<GameObject>();
        List<Card> temp = new List<Card>();
        int playerNow = 0;

        //add player handcard to list as gameobject
        for (int i = 0; i < players[currentView].getHand().Count; i++)
        {
            ThisPlayerCards.Add(GameObject.Find(players[currentView].getHand()[i].getName()));
        }

        //check the card put in stage valid or not
        //if valid add to otherPlayersCardsInQuest (its an array which store card)
        for (int i = 0; i < ThisPlayerCards.Count; i++)
        {
            if (ThisPlayerCards[i].GetComponent<TouchCard>().isChosen())
            {
                if (ThisPlayerCards[i].name.Contains("Test") || ThisPlayerCards[i].name.Contains("Foe"))
                {
                    gameLog.warn("You are not allow to use Test and Foe as a player in Quest");
					for (int c = 0; c < ThisPlayerCards.Count; c++) {
						ThisPlayerCards [c].transform.localScale = new Vector3 (1,1,1);
						ThisPlayerCards [c].GetComponent<TouchCard> ().activateCard ();
					}
                    return;
                }
                else
                {
                    Card aCard = players[currentView].getHand().Find(x => x.getName() == ThisPlayerCards[i].name);
                    temp.Add(aCard);
                    otherPlayersCardsInQuest[playerNow].Add(aCard);
                    gameLog.info(players[currentView].getName() + " add " + aCard.getName() + " to stage" + round + 1);
                }
            }
        }

        //remove the chosen cards
        if (temp.Count == 0)
        {
        }
        else
        {
            for (int i = 0; i < temp.Count; i++)
            {
                players[currentView].getHand().Remove(temp[i]);
                Vector3 usedAdven_POS = GameObject.Find("USED_ADVENTURE").transform.position;
                ThisPlayerCards.Find(x => x.name == temp[i].getName()).transform.position = usedAdven_POS;
            }
        }

        switchView();

        //if this player is final view, check 
        if (players[currentView] == playerInQuest[playerInQuest.Count - 1])
        {
            //check if the player pass the stage
            CheckPassOtherPlayersInQuestPlayCards(round, playerNow, temp);

            //check winner and get shield
            if (round == questNow.getStageNum())
            {
                for (int i = 0; i < playerInQuest.Count; i++)
                {
                    for (int j = 0; j < questNow.getStageNum(); j++)
                    {
                        playerInQuest[i].addAShield(shieldDeck[0]);
                        shieldDeck.Remove(shieldDeck[0]);
                    }
                }
            }
        }
        else playCardsInQuestBtn.SetActive(true);
    }

    void CheckPassOtherPlayersInQuestPlayCards(int round, int playerNow, List<Card> pq)
    {
        int atk = 0;

        //get player attack
        atk = calculatePlayerATK(questNow, pq, playerInQuest[playerNow]);

        //if the atk of player put in cards smaller than atk of this stage, set player pass = false
        if (atk < sponsorStageATK[round - 1])
        {
            playerInQuest[playerNow].setPass(false);
            gameLog.info(playerInQuest[playerNow].getName() + " did not pass the quest");
            //remove it from the list
            playerInQuest.Remove(playerInQuest[playerNow]);

        }
        else
        {
            gameLog.info(playerInQuest[playerNow].getName() + " passes the quest");
        }
    }


   

    IEnumerator wait2secAndNextDisplay()
    {
        yield return new WaitForSeconds(3f);
        next_story.SetActive(true);
    }


    void rankUpdate()
    {
        Vector3 pos1 = GameObject.Find("RankHolderP1").transform.position;
        Vector3 pos2 = GameObject.Find("RankHolderP2").transform.position;
        Vector3 pos3 = GameObject.Find("RankHolderP3").transform.position;
        Vector3 pos4 = GameObject.Find("RankHolderP4").transform.position;
        Vector3[] poses = new Vector3[4];
        poses[0] = pos1;
        poses[1] = pos2;
        poses[2] = pos3;
        poses[3] = pos4;

        string[] ranks = new string[4];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].getRank() == Rank.SQUIRE)
            {
                ranks[i] = "s";
            }
            else if (players[i].getRank() == Rank.KNIGHT)
            {
                ranks[i] = "k";
            }
            else if (players[i].getRank() == Rank.CHAMPIONKNIGHT)
            {
                ranks[i] = "c";
            }
            else if (players[i].getRank() == Rank.KING)
            {
                ranks[i] = "king";
            }
        }
        if (currentView == 0)
        {
            for (int i = 0; i < ranks.Length; i++)
            {
                int index = i + 1;
                string aName = "";
                if (ranks[i] == "s")
                {
                    aName += "squires_p";
                    aName += index + "";
                    iTween.MoveTo(GameObject.Find(aName), poses[i], 1f);
                }
                if (ranks[i] == "k")
                {
                    aName += "knight_p";
                    aName += index + "";
                    iTween.MoveTo(GameObject.Find(aName), poses[i], 1f);
                }
                if (ranks[i] == "c")
                {
                    aName += "ck_p";
                    aName += index + "";
                    iTween.MoveTo(GameObject.Find(aName), poses[i], 1f);
                }
            }
        }
        else if (currentView == 3)
        {
            for (int i = 0; i < ranks.Length; i++)
            {
                int index = i + 1;
                string aName = "";
                if (i == 3)
                {
                    if (ranks[i] == "s")
                    {
                        aName += "squires_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[0], 1f);
                    }
                    if (ranks[i] == "k")
                    {
                        aName += "knight_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[0], 1f);
                    }
                    if (ranks[i] == "c")
                    {
                        aName += "ck_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[0], 1f);
                    }
                }
                else
                {
                    if (ranks[i] == "s")
                    {
                        aName += "squires_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[i + 1], 1f);
                    }
                    if (ranks[i] == "k")
                    {
                        aName += "knight_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[i + 1], 1f);
                    }
                    if (ranks[i] == "c")
                    {
                        aName += "ck_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[i + 1], 1f);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < ranks.Length; i++)
            {
                int index = i + 1;
                string aName = "";
                if (i == currentView-1)
                {
                    if (ranks[i] == "s")
                    {
                        aName += "squires_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[3], 1f);
                    }
                    if (ranks[i] == "k")
                    {
                        aName += "knight_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[3], 1f);
                    }
                    if (ranks[i] == "c")
                    {
                        aName += "ck_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[3], 1f);
                    }
                }
                else
                {
                    if (ranks[i] == "s")
                    {
                        aName += "squires_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[Mathf.Abs(i-currentView)], 1f);
                    }
                    if (ranks[i] == "k")
                    {
                        aName += "knight_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[Mathf.Abs(i - currentView)], 1f);
                    }
                    if (ranks[i] == "c")
                    {
                        aName += "ck_p";
                        aName += index + "";
                        iTween.MoveTo(GameObject.Find(aName), poses[Mathf.Abs(i - currentView)], 1f);
                    }
                }
            }
        }
    }

    void shieldDisplayUpdate()
    {
        if (currentView == 0)
        {
            p1shieldNum.text = p1.getShield().Count + "";
            p2shieldNum.text = p2.getShield().Count + "";
            p3shieldNum.text = p3.getShield().Count + "";
            p4shieldNum.text = p4.getShield().Count + "";
        }
        else if (currentView == 1)
        {
            p1shieldNum.text = p2.getShield().Count + "";
            p2shieldNum.text = p3.getShield().Count + "";
            p3shieldNum.text = p4.getShield().Count + "";
            p4shieldNum.text = p1.getShield().Count + "";
        }
        else if (currentView == 2)
        {
            p1shieldNum.text = p3.getShield().Count + "";
            p2shieldNum.text = p4.getShield().Count + "";
            p3shieldNum.text = p1.getShield().Count + "";
            p4shieldNum.text = p2.getShield().Count + "";
        }
        else
        {
            p1shieldNum.text = p4.getShield().Count + "";
            p2shieldNum.text = p1.getShield().Count + "";
            p3shieldNum.text = p2.getShield().Count + "";
            p4shieldNum.text = p3.getShield().Count + "";
        }
    }

    void playerNamePosChange()
    {
        if (currentView == 0)
        {
            namep1.text = "P1";
            namep2.text = "P2";
            namep3.text = "P3";
            namep4.text = "P4";
        }
        if (currentView == 1)
        {
            namep1.text = "P2";
            namep2.text = "P3";
            namep3.text = "P4";
            namep4.text = "P1";
        }
        if (currentView == 2)
        {
            namep1.text = "P3";
            namep2.text = "P4";
            namep3.text = "P1";
            namep4.text = "P2";
        }
        if (currentView == 3)
        {
            namep1.text = "P4";
            namep2.text = "P1";
            namep3.text = "P2";
            namep4.text = "P3";
        }
    }

    void cardsNumDisplayUpdate()
    {
        if (currentView == 0)
        {
            p2cardsNum.text = p2.getHand().Count + "";
            p3cardsNum.text = p3.getHand().Count + "";
            p4cardsNum.text = p4.getHand().Count + "";
        }
        else if (currentView == 1)
        {
            p2cardsNum.text = p3.getHand().Count + "";
            p3cardsNum.text = p4.getHand().Count + "";
            p4cardsNum.text = p1.getHand().Count + "";
        }
        else if (currentView == 2)
        {
            p2cardsNum.text = p4.getHand().Count + "";
            p3cardsNum.text = p1.getHand().Count + "";
            p4cardsNum.text = p2.getHand().Count + "";
        }
        else
        {
            p2cardsNum.text = p1.getHand().Count + "";
            p3cardsNum.text = p2.getHand().Count + "";
            p4cardsNum.text = p3.getHand().Count + "";
        }
    }
}

/*
In sponsor: Error when no card chosen but click confirm button.


*/