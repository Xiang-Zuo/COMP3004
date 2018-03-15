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
    private List<Card> sponserCardOnDeck = new List<Card>();

    private List<Card> hand1 = new List<Card>();
    private List<Card> hand2 = new List<Card>();
    private List<Card> hand3 = new List<Card>();
    private List<Card> hand4 = new List<Card>();



    GameObject[] tempObj_adventure = new GameObject[125];
    GameObject[] tempObj_story = new GameObject[28];
    List<GameObject> cardObjs_p1 = new List<GameObject>();
    List<GameObject> cardObjs_p2 = new List<GameObject>();
    List<GameObject> cardObjs_p3 = new List<GameObject>();
    List<GameObject> cardObjs_p4 = new List<GameObject>();
    List<GameObject>[] cardObjs = new List<GameObject>[4];

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




	private int viewIndex = 1; //which player's view is displayed now, 0,1,2,3 for p1,p2,p3,p4;
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
    public GameObject msgBar;
	public Text msgBarTXT;
    public GameObject sponsorCard; //Sponsor Card msg
    public GameObject YES_btn;
    public GameObject CONFIRM_btn;
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


    private List<ShieldCard> shield1 = new List<ShieldCard>();
    private List<ShieldCard> shield2 = new List<ShieldCard>();
    private List<ShieldCard> shield3 = new List<ShieldCard>();
    private List<ShieldCard> shield4 = new List<ShieldCard>();

    private List<Player> playerInQuest = new List<Player>();

    private Player p1;
    private Player p2;
    private Player p3;
    private Player p4;

    private List<Card>[] stages = new List<Card>[5];
    private int[] sponsorStageATK = new int[5];
    private int stageAtk1 = 0;
    private int stageAtk2 = 0;
    private int stageAtk3 = 0;
    private int stageAtk4 = 0;
    private int stageAtk5 = 0;
    private List<Card> sponsorUsedCard = new List<Card>();

    private int MAXPLAYERNUM = 4;
    private int advantureCard_count = 0;

    private List<Card> discardListAdv = new List<Card>();
    private QuestGame.Logger gameLog = new QuestGame.Logger();

    // Use this for initialization
    void Start()
    {
        setHands_players();

		msgBar.SetActive (false);
		askJoinBTN.SetActive (false);
		askSponsorBTN.SetActive (false);
		discardBTN.SetActive (false);
		sponsorEnd.SetActive(false);
        playCardMsg.SetActive(false);
        YES_btn.SetActive(false);
        sponsorCard.SetActive(false);
        disCardMsg.SetActive(false);
        disCardMsg2.SetActive(false);
        continueMsg.SetActive(false);
        next_story.SetActive(false);
        guideMSG.SetActive(false);
        wantJoinMSG.SetActive(false);
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


        rankUpdate();
        playerNamePosChange();
        shieldDisplayUpdate(currentView);
        cardsNumDisplayUpdate(currentView);

        checkRankUpdate(players[0]);
        checkRankUpdate(players[1]);
        checkRankUpdate(players[2]);
        checkRankUpdate(players[3]);

        for (int i = 0; i < MAXPLAYERNUM; i++)
        {
            if (players[i] != null)
            {
                if (players[i].getRank() == Rank.KING)
                {
                    gameLog.info(players[i].getName() + " is the winner, ggwp");
                }
            }

        }

		if (gameIsOn) //to true by call activateGame() from touchShield.cs(when the last player chose a shield)
        {
            load_dealing();
			next_story.SetActive (true);
            gameIsOn = false;
        }
			

    }
		

    void setHands_players()
    {
        hands[0] = hand1;
        hands[1] = hand2;
        hands[2] = hand3;
        hands[3] = hand4;

        p1 = new Player("p1", hand1, Rank.SQUIRE, PlayerType.PLAYER, shield1);
        p2 = new Player("p2", hand2, Rank.SQUIRE, PlayerType.PLAYER, shield2);
        p3 = new Player("p3AI", hand3, Rank.SQUIRE, PlayerType.AI, shield3);
        p4 = new Player("p4AI", hand4, Rank.SQUIRE, PlayerType.AI, shield4);

        players[0] = p1;
        players[1] = p2;
        players[2] = p3;
        players[3] = p4;

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
        while (advantureCard_count < 48)
        {
            for (int i = 0; i < MAXPLAYERNUM; i++)
            {
                List<Card> aCard = hands[i];
                aCard.Add(alist[0]);
                if (i == 0)
                {
                    string cardName = alist[0].getName();
                    getCard(5, cardName, 1);
                }
                else
                {
                    string cardName = alist[0].getName();
                    getCard(0, cardName, (i + 1));
                }
                alist.Remove(alist[0]);
                advantureCard_count++;
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
            getCard(5, questNow.getName(), 0);
			msgBar.SetActive(true);
			askSponsorBTN.SetActive (true);
            handleQuestEventSponsor(questNow, sponsorNow);
            questDeck.Remove(questDeck[0]);
        }
        else if (randomEvent == 1)
        {
            tournNow = tournDeck[0];
            print("now handle tourncard " + tournNow.getName());
            getCard(5, tournNow.getName(), 0);
            handleTouraEvent(tournNow);
            tournDeck.Remove(tournDeck[0]);
        }
        else if (randomEvent == 2)
        {
            eventNow = eventDeck[0];
            print("now handle eventcard " + eventNow.getName());
            getCard(5, eventNow.getName(), 0);
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
		if (aSponsor != null) {
			if (aSponsor.getType () == PlayerType.PLAYER) {
				gameLog.info ("this quest has " + stageNum + " stages");
				Card theQuest;
				//	
				theQuest = aSponsor.getHand ().Find (x => x.getName ().Contains ("Saxons"));

				for (int i = 0; i < stageNum; i++) {
					for (int j = 0; j < theStages [i].Count; j++) {
						if (theStages [i] [j].name != null)
							print (theStages [i] [j].name);
					}
				}
			} else if (aSponsor.getType () == PlayerType.AI) {
				List<Card>[] AISponsorStage = new List<Card>[aQuestcard.getStageNum ()];

				Context context = new Context (new DoISponsorAQuestA (), aQuestcard, players, aSponsor);
				AISponsorStage = context.DoISponsorAQuest ();
				if (AISponsorStage == null) {
					gameLog.info ("AI does not sponsor");
				} else {
					print ("fff");
				}
				//next 

			} else {
				if (aSponsor.getType () == PlayerType.AI) {
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

    bool checkAtkValid(int[] anArray, int length)           //check quest the atk is increasing
    {
        for (int i = 1; i < length; i++)
        {
            if (anArray[i - 1] > anArray[i])
                return false;
            else if (anArray[i - 1] == anArray[i])
                return false;
        }
        return true;
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
        for (int i = 0; i < MAXPLAYERNUM; i++)
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

            for (int i = 0; i < MAXPLAYERNUM; i++)
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

            for (int i = 0; i < MAXPLAYERNUM; i++)
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
            for (int i = 0; i < MAXPLAYERNUM; i++)
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
                    getCard(5, theCard.getName(), 1);
                    sortCard(p1);
                }
                if (aPlayer.getName() == p2.getName())
                {
                    getCard(5, theCard.getName(), 2);
                }
                if (aPlayer.getName() == p3.getName())
                {

                    getCard(5, theCard.getName(), 3);
                }
                if (aPlayer.getName() == p4.getName())
                {

                    getCard(5, theCard.getName(), 4);
                }
            }
            else if (currentView == 1)
            {
                if (aPlayer.getName() == p1.getName())
                {
                    getCard(5, theCard.getName(), 4);
                }
                if (aPlayer.getName() == p2.getName())
                {
                    getCard(5, theCard.getName(), 1);
                    sortCard(p2);
                }
                if (aPlayer.getName() == p3.getName())
                {

                    getCard(5, theCard.getName(), 2);
                }
                if (aPlayer.getName() == p4.getName())
                {

                    getCard(5, theCard.getName(), 3);
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
        if (thePlayer.getRank() == Rank.KNIGHT)
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
        if (thePlayer.getRank() == Rank.SQUIRE)
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

    public void getCard(float distance, string aName, int which)
    {
        GameObject toMove = GameObject.Find(aName);
        if (which == 1)
        {
            Vector3 toPos = pos_p1 + new Vector3(distance, 0, 1f) * (p1.getHand().Count - 1);
            iTween.MoveTo(toMove, toPos, 2);

        }
        if (which == 0)
        { //to center
            Vector3 toPos = pos_story;
            iTween.MoveTo(toMove, toPos, 0.8f);
        }

        if (which == 2)
        {
            iTween.MoveTo(toMove, pos_p2, 2);
        }

        if (which == 3)
        {
            iTween.MoveTo(toMove, pos_p3, 2);
        }
        if (which == 4)
        {
            iTween.MoveTo(toMove, pos_p4, 2);
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

        if (sponsorNow.getName() == p1.getName())
        {
            for (int i = 0; i < p1.getHand().Count; i++)
            {
                cards.Add(GameObject.Find(p1.getHand()[i].getName()));

            }
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
            Vector3 toPosition = pos_p1 + new Vector3(5, 0, 1) * i;
            iTween.MoveTo(temp_player_hand[i], toPosition, 0.5f);
        }
    }

    public void endSponsor()
    {
		sponsorEnd.SetActive(true);
		sponsorCard.SetActive (false);
    }


    public void askIfJoin()
    {
		sponsorEnd.SetActive(false);
        switchView(sponsorNow);
		msgBarTXT.text = "Would you like to join this Quest?";
		msgBar.SetActive(true);
		askJoinBTN.SetActive (true);
    }


    public void startLetOthersPlay()
    {
		msgBar.SetActive (false);
		askJoinBTN.SetActive (false);
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
		discardBTN.SetActive (true);
        List<GameObject> cards = new List<GameObject>();
        //Card theDiscardCard;
        for (int i = 0; i < p2.getHand().Count; i++)
        {
            cards.Add(GameObject.Find(p2.getHand()[i].getName()));

            cards[i].GetComponent<TouchCard>().activateCard();
        }

    }


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
            //switchView (p1);
            //storyEvent2 ();

        }
    }

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
        yield return new WaitForSeconds(1.0f);
        guideTxt.text = "Hints: The total attack of Stage2 must larger than Stage1.";
        guideMSG.SetActive(true);
        StartCoroutine(guideChange());
    }

    IEnumerator guideChange()
    {
        yield return new WaitForSeconds(3.0f);
        guideTxt.text = "Hints: You cannot put two same Weapon cards in one Stage.";
        StartCoroutine(guideHidden());
    }

    IEnumerator guideHidden()
    {
        yield return new WaitForSeconds(2.0f);
        guideMSG.SetActive(false);
    }
		



    public void switchView(Player currentPlayer)
    {
        //print ("in switch");
        int position = 2;
        int currentPlayerIndex = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].getName() == currentPlayer.getName())
                currentPlayerIndex = i;
        }
        if (currentPlayerIndex == 0)
        {
            currentView = 1;
            for (int j = 0; j < players[0].getHand().Count; j++)
            {
                string cardName = players[0].getHand()[j].getName();
                getCard(0, cardName, 4);
            }
            for (int j = 0; j < players[1].getHand().Count; j++)
            {
                string cardName = players[1].getHand()[j].getName();
                getCard(5, cardName, 1);
            }
            for (int j = 0; j < players[2].getHand().Count; j++)
            {
                string cardName = players[2].getHand()[j].getName();
                getCard(0, cardName, 2);
            }
            for (int j = 0; j < players[3].getHand().Count; j++)
            {
                string cardName = players[3].getHand()[j].getName();
                getCard(0, cardName, 3);
            }
            sortCard(players[1]);
            currentView = 1;
        }

        if (currentPlayerIndex == 1)
        {
            currentView = 1;
            for (int j = 0; j < players[0].getHand().Count; j++)
            {
                string cardName = players[0].getHand()[j].getName();
                getCard(0, cardName, 1);
            }
            for (int j = 0; j < players[1].getHand().Count; j++)
            {
                string cardName = players[1].getHand()[j].getName();
                getCard(5, cardName, 2);
            }
            for (int j = 0; j < players[2].getHand().Count; j++)
            {
                string cardName = players[2].getHand()[j].getName();
                getCard(0, cardName, 3);
            }
            for (int j = 0; j < players[3].getHand().Count; j++)
            {
                string cardName = players[3].getHand()[j].getName();
                getCard(0, cardName, 4);
            }
            sortCard(players[0]);
            currentView = 0;
        }

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



	void setSponsorNow(){
		if (viewIndex == 1) {
			sponsorNow = p1;
		}

		if (viewIndex == 2) {
			sponsorNow = p2;
		}
		if (viewIndex == 3) {
			sponsorNow = p3;
		}
		if (viewIndex == 4){
			sponsorNow = p4;
		}
	}



	public void sponsorYESorNO(bool isSpon){ //if current player click Yes, show sponsor msg, switch to next if No 
		if (isSpon) {
			msgBar.SetActive (false);
			askSponsorBTN.SetActive (false);
			sponsorCard.SetActive (true);
			setSponsorNow ();
			for (int i = 0; i < players.Length; i++) //make player's card able to choose(click)
			{
				if (players[i].getName() == sponsorNow.getName())
				{
					List<GameObject> tempP1Obj = new List<GameObject>();
					for (int k = 0; k < p1.getHand().Count; k++)
					{
						tempP1Obj.Add(GameObject.Find(p1.getHand()[k].getName()));
						tempP1Obj[k].GetComponent<TouchCard>().activateCard();
					}
				}
			}
		} else {
			switchView (p1);
		}
	}





    public int getWhichStageNowIs()
    {
        return inWhichStage;
    }


    public void toNextStage()
    {
        YES_btn.SetActive(true);
        CONFIRM_btn.SetActive(false);
    }

    /*public void toNextStage(){
		YES_btn.SetActive(true);
		CONFIRM_btn.SetActive (false);
	}*/



    void shieldDisplayUpdate(int curr)
    {
        if (curr == 0)
        {
            p1shieldNum.text = p1.getShield().Count + "";
            p2shieldNum.text = p2.getShield().Count + "";
            p3shieldNum.text = p3.getShield().Count + "";
            p4shieldNum.text = p4.getShield().Count + "";
        }
        if (curr == 1)
        {
            p1shieldNum.text = p2.getShield().Count + "";
            p2shieldNum.text = p3.getShield().Count + "";
            p3shieldNum.text = p4.getShield().Count + "";
            p4shieldNum.text = p1.getShield().Count + "";
        }
    }

    void cardsNumDisplayUpdate(int curr)
    {
        if (curr == 0)
        {
            p2cardsNum.text = p2.getHand().Count + "";
            p3cardsNum.text = p3.getHand().Count + "";
            p4cardsNum.text = p4.getHand().Count + "";
        }
        if (curr == 1)
        {
            p2cardsNum.text = p3.getHand().Count + "";
            p3cardsNum.text = p4.getHand().Count + "";
            p4cardsNum.text = p1.getHand().Count + "";
        }
    }

    public void sortSponsor()
    {
        List<GameObject> sponCards = new List<GameObject>();
        for (int i = 0; i < sponsorNow.getHand().Count; i++)
        {
            sponCards.Add(GameObject.Find(sponsorNow.getHand()[i].getName()));
            sponCards[i].GetComponent<Transform>().transform.localScale = new Vector3(1f, 1f, 1f);
        }


        sortCard(sponsorNow);

    }

    public Vector3 placeSponsorInRightPos(int whichStage)
    {
        string posName = "s_";
        posName += numOfStagesNow;
        posName += "_";
        posName += whichStage;
        return (GameObject.Find(posName).GetComponent<Transform>().position);
    }

    public List<Card>[] getStages()
    {
        return stages;
    }

    public void othersPlay()
    {
        for (int i = 0; i < playerInQuest.Count; i++)
        {
            if (playerInQuest[i].getPass() == true)
                gameLog.info(playerInQuest[i].getName() + " still in the quest, continue...");
        }

        otherPlayersInQuestPlayCards(roundNow);

    }


    void otherPlayersInQuestPlayCards(int round)
    {

        List<GameObject> ThisPlayerCards = new List<GameObject>();
        List<Card> pq1 = new List<Card>();
        List<Card> pq2 = new List<Card>();
        List<Card> pq3 = new List<Card>();
        List<Card> temp = new List<Card>();
        //List<Card>[] playerQuest;
        int atk1 = 0;
        int atk2 = 0;
        int atk3 = 0;
        //int[] playerATK;


        if (sponsorNow == p1 && round == 1)
        {
            List<Card>[] playerQuest = { pq1, pq2, pq3 };
            int[] playerATK = { atk1, atk2, atk3 };
            p2.setPass(true);
            p3.setPass(true);
            p4.setPass(true);
            for (int i = 0; i < p2.getHand().Count; i++)
            {
                ThisPlayerCards.Add(GameObject.Find(p2.getHand()[i].getName()));
            }

            for (int i = 0; i < ThisPlayerCards.Count; i++)
            {
                if (ThisPlayerCards[i].GetComponent<TouchCard>().isChosen())
                {
                    if (ThisPlayerCards[i].name.Contains("Test") || ThisPlayerCards[i].name.Contains("Foe"))
                    {
                        gameLog.warn("You are not allow to use Test and Foe as a player in Quest");
                        return;
                    }
                    else
                    {
                        Card aCard = p2.getHand().Find(x => x.getName() == ThisPlayerCards[i].name);
                        temp.Add(aCard);
                    }
                }
            }
            if (temp.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    pq1.Add(temp[i]);
                    p2.getHand().Remove(temp[i]);
                    Vector3 usedAdven_POS = GameObject.Find("USED_ADVENTURE").transform.position;
                    ThisPlayerCards.Find(x => x.name == temp[i].getName()).transform.position = usedAdven_POS;
                }
            }
            Card theCard = p3.getHand().Find(x => x.getName().Contains("Horse"));
            pq2.Add(theCard);
            if (theCard != null)
            {
                gameLog.info(p3.getName() + " add " + theCard.getName() + " to stage1");
            }
            p3.getHand().Remove(theCard);

            theCard = p4.getHand().Find(x => x.getName().Contains("Battleax"));
            pq3.Add(theCard);
            if (theCard != null)
            {
                gameLog.info(p4.getName() + " add " + theCard.getName() + " to stage1");
            }
            p4.getHand().Remove(theCard);


            for (int i = 0; i < playerQuest.Length; i++)
            {
                playerATK[i] = calculatePlayerATK(questNow, playerQuest[i], playerInQuest[i]);
            }

            for (int i = 0; i < playerATK.Length; i++)
            {
                if (playerATK[i] < sponsorStageATK[round - 1])
                {
                    playerInQuest[i].setPass(false);

                }
            }
            switchView(p2);
            for (int i = 0; i < playerInQuest.Count; i++)
            {
                if (playerInQuest[i].getPass() == false)
                {
                    gameLog.info(playerInQuest[i].getName() + " did not pass the quest");
                    ctnMsgStr = "";
                    ctnMsgStr += playerInQuest[i].getName();
                    ctnMsgStr += " did not pass this round";
                    continueTxt.text = ctnMsgStr;
                }
                else
                    gameLog.info(playerInQuest[i].getName() + " passes the quest");
            }
            continueMsg.SetActive(true);
            playCardMsg.SetActive(false);

            roundNow = 2;
        }
        if (sponsorNow == p1 && round == 2)
        {
            string winnerName = "";
            List<Card>[] playerQuest = { pq1, pq2 };
            int[] playerATK = { atk1, atk2 };
            gameLog.info("This is stage " + round);

            for (int i = 0; i < playerInQuest.Count; i++)
            {
                if (playerInQuest[i].getPass() == true)
                {
                    //gameLog.info (playerInQuest [i].getName () + " continue playing");
                }
                else
                    playerInQuest.Remove(playerInQuest[i]);

            }
            drawACard(p3, null, null, null);
            drawACard(p4, null, null, null);

            Card theCard = p3.getHand().Find(x => x.getName().Contains("Excalibur"));
            pq1.Add(theCard);
            if (theCard != null)
            {
                gameLog.info(p3.getName() + " add " + theCard.getName() + " to stage2");
            }
            p3.getHand().Remove(theCard);

            theCard = p4.getHand().Find(x => x.getName().Contains("Lance"));
            pq2.Add(theCard);
            if (theCard != null)
            {
                gameLog.info(p4.getName() + " add " + theCard.getName() + " to stage2");
            }
            p4.getHand().Remove(theCard);

            for (int i = 0; i < playerQuest.Length; i++)
            {
                playerATK[i] = calculatePlayerATK(questNow, playerQuest[i], playerInQuest[i]);
            }
            for (int i = 0; i < playerATK.Length; i++)
            {
                if (playerATK[i] < sponsorStageATK[round - 1])
                {
                    playerInQuest[i].setPass(false);
                }
            }
            for (int i = 0; i < playerInQuest.Count; i++)
            {
                if (playerInQuest[i].getPass() == false)
                {
                    gameLog.info(playerInQuest[i].getName() + " did not pass the quest");
                    ctnMsgStr = "";
                    ctnMsgStr += playerInQuest[i].getName();
                    ctnMsgStr += " did not pass this round";
                    finishQuestStr[0] = ctnMsgStr;
                    continueTxt.text = ctnMsgStr;
                }
                else
                {
                    winnerName += playerInQuest[i].getName() + " ";
                    gameLog.info(playerInQuest[i].getName() + " win the quest");
                    for (int j = 0; j < questNow.getStageNum(); j++)
                    {
                        playerInQuest[i].addAShield(shieldDeck[0]);
                        shieldDeck.Remove(shieldDeck[0]);
                    }
                    gameLog.info("p3 has " + p3.getShield().Count + " shields");

                }
            }
            ctnMsgStr = "";
            ctnMsgStr += winnerName;
            ctnMsgStr += " is the winner";
            finishQuestStr[1] = ctnMsgStr;
            continueTxt.text = ctnMsgStr;

            gameLog.info("quest 1 ends");
            for (int i = 0; i < sponsorUsedCard.Count; i++)
            {
                gameLog.info("the sponsor " + sponsorNow.getName() + " discard " + sponsorUsedCard[i].getName());
            }
            for (int i = 0; i < (sponsorUsedCard.Count + questNow.getStageNum()); i++)
            {
                drawACard(sponsorNow, null, null, null);
            }
            for (int i = 0; i < sponsorNow.getHand().Count; i++)
            {
                GameObject.Find(sponsorNow.getHand()[i].getName()).GetComponent<TouchCard>().activateCard();
            }
            //continueTxt.text="Sponsor choose card to discard";
            continueMsg.SetActive(false);
            finishQuestStr[2] = "Sponsor choose card to discard";
            disCardMsg.SetActive(true);
        }
    }

    public void thisStageAllFinish()
    {
        if (sponsorNow.getName() == p1.getName())
        {

        }
        else if (sponsorNow.getName() == p2.getName())
        {

        }
        else if (sponsorNow.getName() == p3.getName())
        {

        }
        else if (sponsorNow.getName() == p4.getName())
        {

        }


        string msgUpdate = "After Stage ";
        msgUpdate += roundNow + " , ";

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
    }

    IEnumerator wait2secFor()
    {
        yield return new WaitForSeconds(2f);
        switchView(p2);
        //sortCard (p1);
        print(p1.getHand().Count);
        disCardMsg2.SetActive(true);
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

        if (currentView == 1)
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

    }



}
