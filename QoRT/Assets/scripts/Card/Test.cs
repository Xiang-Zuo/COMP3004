using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

	/*Test handleQuestEvent*/
	private WeaponCard horse = new WeaponCard("horse",Kind.WEAPON,10); 
	private WeaponCard sword = new WeaponCard("sword",Kind.WEAPON,10); 
	private WeaponCard dagger = new WeaponCard("dagger",Kind.WEAPON,5); 

	/*                     */


    private List<Card> advantureDeck = new List<Card>();
    private List<QuestCard> questDeck = new List<QuestCard>();
    private List<TournamentCard> tournDeck = new List<TournamentCard>();
    private List<EventCard> eventDeck = new List<EventCard>();
    private List<Card> rankDeck = new List<Card>();
    private List<Card>[] hands = new List<Card>[4];
    private Player[] players = new Player[4];
    private List<Card> sponserCardOnDeck = new List<Card>();

    public Transform p1_card_transform;
    public Transform p2_card_transform;
    public Transform p3_card_transform;
    public Transform p4_card_transform;

    private List<Card> hand1 = new List<Card>();
    private List<Card> hand2 = new List<Card>();
    private List<Card> hand3 = new List<Card>();
    private List<Card> hand4 = new List<Card>();

    private List<ShieldCard> shield1 = new List<ShieldCard>();
    private List<ShieldCard> shield2 = new List<ShieldCard>();
    private List<ShieldCard> shield3 = new List<ShieldCard>();
    private List<ShieldCard> shield4 = new List<ShieldCard>();

    private List<Player> playerInQuest = new List<Player> ();

    private Player p1;
    private Player p2;
    private Player p3;
    private Player p4;

    

    private List<Card>[] stages = new List<Card>[5];
    private int[] sponsorStageATK = new int[5];

    private int MAXPLAYERNUM = 4;
    private int advantureCard_count = 0;

    private List<Card> discardListAdv = new List<Card>();



    // Use this for initialization
    void Start()
    {
        //event.click.numberof player
        
        //set 1 player and 3 AI
        setHands_players();
        //event.click.start
        load_dealing();

        for (int i = 0; i < 1; i++)  //to test if all story have been used
        {
            storyEvent();
        }


    }
    // Update is called once per frame
    void Update()
    {

    }
    void setHands_players()
    {
        hands[0] = hand1;
        hands[1] = hand2;
        hands[2] = hand3;
        hands[3] = hand4;

        Debug.LogError("how many players do you have");
        Debug.Log("now set one");
        //setname and type

        p1 = new Player("p1", hand1, Rank.SQUIRE, PlayerType.PLAYER,shield1);
        p2 = new Player("p2AI", hand2, Rank.SQUIRE, PlayerType.AI,shield2);
        p3 = new Player("p3AI", hand3, Rank.SQUIRE, PlayerType.AI,shield3);
        p4 = new Player("p4AI", hand4, Rank.SQUIRE, PlayerType.AI,shield4);
        
        players[0] = p1;
        players[1] = p2;
        players[2] = p3;
        players[3] = p4;

    }
    void load_dealing()
    {
        loadDeckSys();
        dealing(advantureDeck);
		
        //print(advantureCard_count);
        //print(advantureDeck.Count);
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
    }
    void dealing(List<Card> alist)
    {
        while (advantureCard_count < 48)
        {
            for (int i = 0; i < MAXPLAYERNUM; i++)
            {
                List<Card> temp = hands[i];
                temp.Add(alist[0]);
                alist.Remove(alist[0]);
                advantureCard_count++;
            }
        }
    }
    Transform SetFinalPosition()
    {
        Transform[] endposition = new Transform[4];
        endposition[0] = p1_card_transform;
        endposition[1] = p2_card_transform;
        endposition[2] = p3_card_transform;
        endposition[3] = p4_card_transform;
        return endposition[advantureCard_count % MAXPLAYERNUM];
    }

    void storyEvent()
    {
        string currentStoryCardName = null;
        Player theSponsor=p1;
        int randomEvent;
        randomEvent = randomTheEventDeck();
        if (randomEvent == 0)
        {
            currentStoryCardName = questDeck[0].getName();
            print("now handle questcard " + questDeck[0].getName());
        }
        else if (randomEvent == 1)
        {
            currentStoryCardName = tournDeck[0].getName();
        }
        else if (randomEvent == 2)
        {
            currentStoryCardName = eventDeck[0].getName();
        }
        else
        {
            currentStoryCardName = null;
            print("all story card have been used, wait for reloading");
            //reload
        }
        //翻出一张story card

        //ask if p1 want to be sponser
      
                theSponsor =players[0];   //0 is player, 1-3 is AI for now
                print(players[0].getName() + " is the sponsor");
        
        if (currentStoryCardName != null)
        {
            switch (currentStoryCardName.Substring(0, 10))
            {
                case "        SQ":
                    handleQuestEventSponsor(questDeck[0], theSponsor);
					//handleQuestEventPlayer(questDeck[0], theSponsor);
                    questDeck.Remove(questDeck[0]);
                    break;
                    /*case "        ST":
                        handleTouraEvent(tournDeck[0]);
                        tournDeck.Remove(tournDeck[0]);
                        break;
                    case "        SE":
                        handleEventEvent(eventDeck[0]);
                        eventDeck.Remove(eventDeck[0]);
                        break;*/
            }
        }
        else
        {
            print("error");
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
        if (aSponsor.getType() == PlayerType.AI)
        {
            List<Card>[] a;
            Context context;
            context = new Context(new DoISponsorAQuestA(),aQuestcard.getStageNum());
            a=context.DoISponsorAQuest();
        }
        else
        {
            bool testValid = true;
            QuestCard aCard = aQuestcard;
            int stageNum = aCard.getStageNum();
            print("stagenum for this quest" + stageNum);
            //string cardName = aCard.getName ().Substring(0,16);
            int stageAtk1 = 0;
            int stageAtk2 = 0;
            int stageAtk3 = 0;
            int stageAtk4 = 0;
            int stageAtk5 = 0;

            sponsorStageATK[0] = stageAtk1;
            sponsorStageATK[1] = stageAtk2;
            sponsorStageATK[2] = stageAtk3;
            sponsorStageATK[3] = stageAtk4;
            sponsorStageATK[4] = stageAtk5;

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

            List<Card> usedCard = new List<Card>();

            bool valid = false;
            while (!valid)
            {
                for (int i = 0; i < stageNum; i++)         //check insert card
                {
                    bool foeValid = false;
                    for (int j = 0; j < 3; j++)
                    {
                        Card theCard = getRandomCardFromSponsor(aSponsor);              //click test valid
                        if (theCard.getKind() == Kind.TEST)
                        {
                            if (testValid != true)
                            {
                                j--;
                                Debug.LogError("test only one per quest");
                            }
                            else if (j == 0)
                            {
                                j = 3;
                                stages[i].Add(theCard);
                                usedCard.Add(theCard);
                                aSponsor.discard(theCard);
                                testValid = false;
                                print(theCard.getName());
                            }
                            else
                            {
                                j--;
                                Debug.LogError("test only one stage");
                            }
                        }
                        else if (j == 2 && theCard.getKind() != Kind.FOE && foeValid == false)
                        {
                            j--;
                            Debug.LogError("you need at least one foe card per stage");

                        }
                        else if (theCard.getKind() == Kind.FOE)
                        {
                            stages[i].Add(theCard);
                            usedCard.Add(theCard);
                            aSponsor.discard(theCard);
                            print(theCard.getName());
                            foeValid = true;
                        }
                        else
                        {
                            stages[i].Add(theCard);
                            usedCard.Add(theCard);
                            aSponsor.discard(theCard);
                            print(theCard.getName());
                        }
                    }
                    print("stage" + (i + 1) + "end here");
                }

                for (int i = 0; i < stageNum; i++)   //check atk valid
                {
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
                            FoeCard aFoeCard = (FoeCard)stages[i][j];
                            sponsorStageATK[i] += aFoeCard.getAtk();
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
                            Debug.LogError("error, error to get atk num");
                        }
                    }
                }
                if (checkAtkValid(sponsorStageATK, stageNum) == true)
                {
                    valid = true;
                    //print(aSponsor.getHand().Count);
                    for (int i = 0; i < stageNum; i++)
                    {
                        print("stage " + (i + 1) + "total atk is " + sponsorStageATK[i]);
                    }
                    print(aSponsor.getHand().Count);
                    for (int i = 0; i < usedCard.Count; i++)
                    {
                        discardListAdv.Add(usedCard[i]);
                    }
                    usedCard.Clear();
                }
                else
                {
                    testValid = true;
                    valid = false;
                    for (int i = 0; i < stageNum; i++)
                    {
                        print("stage " + (i + 1) + "total atk is " + sponsorStageATK[i]);
                        sponsorStageATK[i] = 0;
                        stages[i].Clear();
                    }
                    for (int i = 0; i < usedCard.Count; i++)
                    {
                        aSponsor.draw(usedCard[i]);
                    }
                    usedCard.Clear();
                    print("not valid due to atk require");
                }
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

    void handleQuestEventPlayer(QuestCard aQuestcard, Player theSponsor)
    {
        for (int i=0; i<MAXPLAYERNUM; i++)
        {
            if (players[i].getName() != theSponsor.getName())
            {
                Debug.LogAssertion("ask if want to join");
                playerInQuest.Add(players[i]);
                Debug.Log(players[i].getName() + " want to join the questGame");

            }      
        }
        int stageNum = aQuestcard.getStageNum();
        for (int i = 0; i < stageNum; i++)
        {
            if (stages[i][0].getKind() == Kind.TEST)                 //if it is a test
            {
                int discardNUM;   
                string cardName = aQuestcard.getName();
                print("its a test" + stages[i][0].getName());
                if (cardName.Substring(14, cardName.Length - 14) == "Morgan_Le_Fey")       //set minium test discard num
                {
                    discardNUM = 3;
                } else if (cardName.Substring(14, cardName.Length - 14) == "Questing_Beast")
                {
                    if (aQuestcard.getName() == "        SQ_SFTQB_4_1") discardNUM = 4;
                    else discardNUM = 1;
                } else discardNUM = 1;

                Debug.Log("stages " + i + " is a test");
                for (int j = 0; j < playerInQuest.Count; j++)
                {
                    bool pass;
                    drawACard(playerInQuest[j]);
                    pass = testDiscard(discardNUM, playerInQuest[j]);
                    discardNUM++;
                    if (pass == true) print(playerInQuest[j].getName() + " has pass the test ");
                    else
                    {
                        print(playerInQuest[j].getName() + " doesn't pass the test ");
                        playerInQuest.Remove(playerInQuest[j]);
                    }                   
                }
            }
            else
            {
                List<Card> usedCard = new List<Card>();
                Debug.Log("stages " + i + " is a quest");
                for (int j = 0; j < playerInQuest.Count; j++)
                {
                    drawACard(playerInQuest[j]);


                }
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
        print(aCard.getName());
    }


    //just for test
    Card getRandomCardFromSponsor(Player aPlayer)
    {
        int randomCard = Random.Range(0, aPlayer.getHand().Count);
        Card firstCard = aPlayer.getHand()[randomCard];
        return firstCard;
    }
    //ready for delete
	void addWeaponCard(List<WeaponCard> w){
		w.Add(horse);
		w.Add (sword);
		w.Add (dagger);
	}
    
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

    void drawACard(Player aPlayer)
    {
        aPlayer.draw(advantureDeck[0]);
        print("player " + aPlayer.getName() + " draw a " + advantureDeck[0].getName() + "card");
        advantureDeck.Remove(advantureDeck[0]);
        while (aPlayer.getHand().Count > 12)
        {
            Debug.LogError("you have more than 12 cards on hand, discard...");
            Debug.Log("discard " + aPlayer.getHand()[0].getName());
            aPlayer.discard(aPlayer.getHand()[0]);            
        }
    } 
    void checkDeckCapacity()
    {
        if (advantureDeck.Count == 0)
        {
            for (int i=0; i<discardListAdv.Count; i++)
            {
                advantureDeck.Add(discardListAdv[i]);
            }
            discardListAdv.Clear();
            print("advCard reloading...");
        }
    }



}
