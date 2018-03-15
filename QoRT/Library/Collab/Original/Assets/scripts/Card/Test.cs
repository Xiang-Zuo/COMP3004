using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
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
    private List<Card> sponsorUsedCard = new List<Card>();

    private int MAXPLAYERNUM = 4;
    private int advantureCard_count = 0;

    private List<Card> discardListAdv = new List<Card>();

    private bool rigging = true;

    private QuestGame.Logger gameLog = new QuestGame.Logger();

    // Use this for initialization
    void Start()
    {
        //event.click.numberof player
        
        //set 1 player and 3 AI
        
        setHands_players();
        //event.click.start
        load_dealing();

      
        storyEvent(rigging);
       


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

        p1 = new Player("p1", hand1, Rank.SQUIRE, PlayerType.PLAYER,shield1);
        p2 = new Player("p2AI", hand2, Rank.SQUIRE, PlayerType.AI,shield2);
        p3 = new Player("p3AI", hand3, Rank.SQUIRE, PlayerType.AI,shield3);
        p4 = new Player("p4AI", hand4, Rank.SQUIRE, PlayerType.AI,shield4);
        
        players[0] = p1;
        players[1] = p2;
        players[2] = p3;
        players[3] = p4;

        p1.setShield(shield1);
        p2.setShield(shield2);
        p3.setShield(shield3);
        p4.setShield(shield4);
    }
    void load_dealing()
    {
        loadDeckSys();
        dealing(advantureDeck,rigging);
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
    void dealing(List<Card> alist, bool isRigging)
    {
        if (isRigging == true)
        {
            string[] card1 = { "Saxons", "Boar", "Sword", "Dagger" };
            string[] card2 = {  };
            string[] card3 = { "Dagger", "Excalibur", "Amour", "Horse" };
            string[] card4 = { "Battleax", "Lance", "Thieves" };
            List<string[]> cardList = new List<string[]>();
            cardList.Add(card1);
            cardList.Add(card2);
            cardList.Add(card3);
            cardList.Add(card4);
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < cardList[j].Length; i++)
                {
                    Card specificCard = advantureDeck.Find(x => x.getName().Contains(cardList[j][i]));
                    if (specificCard == null) gameLog.error("failure to add " + cardList[j][i] + "to players' hand");
                    else
                    {
                        hands[j].Add(specificCard);
                        gameLog.info(players[j].getName() + " get a card called " + specificCard.getName());
                        advantureDeck.Remove(specificCard);
                    }
                }
                while (hands[j].Count < 12)
                {
                    List<Card> temp = hands[j];
                    temp.Add(alist[0]);
                    gameLog.info(players[j].getName() + " get a card called " + alist[0].getName());
                    alist.Remove(alist[0]);
                }
                gameLog.info(players[j].getName() + " has " + hands[j].Count + " cards");
            }
        } else {
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

    void storyEvent(bool isRigging)
    {
        Player theSponsor;
        if (rigging == true)
        {
            theSponsor = p1;
            QuestCard theQuest = questDeck.Find(x => x.getName().Contains("BH"));
            gameLog.info("the story card is " + theQuest.getName());
            gameLog.info(theSponsor.getName() + " is the sponsor");
            handleQuestEventSponsor(theQuest, theSponsor);
            handleQuestEventPlayer(theQuest, theSponsor);
            for (int i = 0; i < (sponsorUsedCard.Count + theQuest.getStageNum()); i++)
                drawACard(p1, null,null,null);
            questDeck.Remove(theQuest);
            gameLog.info("rigging mode first story event ends here\n");
            EventCard theEvent = eventDeck.Find(x => x.getName().Contains("SE_PT"));
            //print(p1.getHand().Count + ""+ p2.getHand().Count +""+ p3.getHand().Count +""+ p4.getHand().Count);
            handleEventEvent(theEvent);
            gameLog.info("rigging mode second story event ends here\n");
            theEvent = eventDeck.Find(x => x.getName().Contains("SE_CD"));
            handleEventEvent(theEvent);
            gameLog.info("rigging mode third story event ends here\n");
            theQuest = questDeck.Find(x => x.getName().Contains("BH"));
            theSponsor = p2;
            gameLog.info("the story card is " + theQuest.getName());
            gameLog.info(theSponsor.getName() + " is the sponsor");
            handleQuestEventSponsor(theQuest, theSponsor);

        }
        else
        {
            string currentStoryCardName = null;
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

            theSponsor = players[0];
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
        bool testValid = true;
        QuestCard aCard = aQuestcard;
        int stageNum = aCard.getStageNum();
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
        

        if (rigging == true && aSponsor.getType()==PlayerType.PLAYER)
        {
            gameLog.info("this quest has " + stageNum + " stages");
            Card theQuest;
            theQuest = aSponsor.getHand().Find(x => x.getName().Contains("Saxons"));
            stages[0].Add(theQuest);
            sponsorUsedCard.Add(theQuest);
            gameLog.info(aSponsor.getName() + " add " + theQuest.getName() + " to stage1");
            theQuest = aSponsor.getHand().Find(x => x.getName().Contains("Boar"));
            stages[1].Add(theQuest);
            sponsorUsedCard.Add(theQuest);
            gameLog.info(aSponsor.getName() + " add " + theQuest.getName() + " to stage2");
            theQuest = aSponsor.getHand().Find(x => x.getName().Contains("Dagger"));
            gameLog.info(aSponsor.getName() + " add " + theQuest.getName() + " to stage2");
            stages[1].Add(theQuest);
            sponsorUsedCard.Add(theQuest);
            theQuest = aSponsor.getHand().Find(x => x.getName().Contains("Sword"));
            stages[1].Add(theQuest);
            sponsorUsedCard.Add(theQuest);
            gameLog.info(aSponsor.getName() + " add " + theQuest.getName() + " to stage2");
            calculateATK(aCard, stageNum, stages);
            if (checkAtkValid(sponsorStageATK, stageNum) == true)
            {
                gameLog.info("the sponsor's stage is valid.");
                for (int i = 0; i < sponsorUsedCard.Count; i++)
                {
                    aSponsor.discard(sponsorUsedCard[i]);
                }
            }
            else
            {
                gameLog.error("the sponsor's stage is not valid");              
            }
        }
        else if (rigging==true && aSponsor.getType() == PlayerType.AI)
        {
            List<Card>[] AISponsorStage=new List<Card>[aQuestcard.getStageNum()];

            Context context = new Context(new DoISponsorAQuestB(),aQuestcard,players,aSponsor);
            AISponsorStage=context.DoISponsorAQuest();
            if (AISponsorStage == null)
            {
                gameLog.info("AI does not sponsor");
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
            else
            {
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
                                    sponsorUsedCard.Add(theCard);
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
                                sponsorUsedCard.Add(theCard);
                                aSponsor.discard(theCard);
                                print(theCard.getName());
                                foeValid = true;
                            }
                            else
                            {
                                stages[i].Add(theCard);
                                sponsorUsedCard.Add(theCard);
                                aSponsor.discard(theCard);
                                print(theCard.getName());
                            }
                        }
                        print("stage" + (i + 1) + "end here");
                    }

                    calculateATK(aCard, stageNum, stages);

                    if (checkAtkValid(sponsorStageATK, stageNum) == true)
                    {
                        valid = true;
                        //print(aSponsor.getHand().Count);
                        for (int i = 0; i < stageNum; i++)
                        {
                            print("stage " + (i + 1) + "total atk is " + sponsorStageATK[i]);
                        }
                        print(aSponsor.getHand().Count);
                        for (int i = 0; i < sponsorUsedCard.Count; i++)
                        {
                            discardListAdv.Add(sponsorUsedCard[i]);
                        }
                        sponsorUsedCard.Clear();
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
                        for (int i = 0; i < sponsorUsedCard.Count; i++)
                        {
                            aSponsor.draw(sponsorUsedCard[i]);
                        }
                        sponsorUsedCard.Clear();
                        print("not valid due to atk require");
                    }
                }
            }
        }
    }

    void calculateATK(Card aQuestCard, int stageNum, List<Card>[] stages)
    {
        for (int i = 0; i < stageNum; i++)   //check atk valid
        {
            if (stages[i].Count==0) sponsorStageATK[i] = 0;
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
                    if (aQuestCard.getName().Contains("BH") && stages[i][j].getName().Contains("Boar")) {
                        FoeCard aFoeCard = (FoeCard)stages[i][j];
                        sponsorStageATK[i] += aFoeCard.getAtkSpecial();
                    }else
                    {
                        FoeCard aFoeCard = (FoeCard)stages[i][j];
                        sponsorStageATK[i] += aFoeCard.getAtk();
                    }
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
        for (int i=0; i<stageNum; i++)
        {
            gameLog.info("stage " + i + "'s total attack is " + sponsorStageATK[i]);
        }
    }
    
    int calculatePlayerATK(QuestCard aQuestCard, List<Card> aPlayerList) {
        int totalATK = 0;
        if (aPlayerList.Count == 0) return totalATK;
        else
        {
            for (int i = 0; i < aPlayerList.Count; i++)
            {
                if (aPlayerList[i].getName().Contains("QuestsWeap"))
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
        return totalATK;
        
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
        List<Card> pq1=new List<Card>();
        List<Card> pq2=new List<Card>();
        List<Card> pq3=new List<Card>();
        //bool pass1=true, pass2=true, pass3 = true;
        List<Card>[] playerQuest = { pq1, pq2, pq3 };
       // bool[] passList = { pass1, pass2, pass3 };
        if (rigging == true)
        {
            for (int i = 0; i < MAXPLAYERNUM; i++)
            {
                if (players[i].getName() != theSponsor.getName())
                {
                    playerInQuest.Add(players[i]);
                    gameLog.info(players[i].getName() + " wants to join the questGame");
                    players[i].setPass(true);
                }
            }
            for (int i=0; i < playerInQuest.Count; i++)
            {
                drawACard(playerInQuest[i],null,null,null);
            }
            Card aCard;
            aCard = p3.getHand().Find(x => x.getName().Contains("Horse"));
            pq2.Add(aCard);
            p3.discard(aCard);
            //print(aCard.getName());
            aCard = p4.getHand().Find(x => x.getName().Contains("Battleax"));
            pq3.Add(aCard);
            p4.discard(aCard);
            //print(aCard.getName());
            for (int i=0; i < playerInQuest.Count; i++)
            {
                if (playerQuest[i].Count == 0) gameLog.info(playerInQuest[i].getName() + " plays nothing");
                else
                {
                    for (int j=0; j<playerQuest[i].Count; j++)
                    {
                        gameLog.info(playerInQuest[i].getName() + " plays a" + playerQuest[i][j].getName()+" card");                        
                    }
                }
                int atk = calculatePlayerATK(aQuestcard, playerQuest[i]) + playerInQuest[i].getAtk();
                gameLog.info(playerInQuest[i].getName() + "'s total ATK for stage 1 is " +  atk);
                if ((atk) < sponsorStageATK[0])
                {
                    gameLog.info(playerInQuest[i].getName() + " is eliminated");
                    playerInQuest[i].setPass(false);
                }else gameLog.info(playerInQuest[i].getName() + " passes this stage");
            }
            for (int i=0; i < playerInQuest.Count; i++)
            {
                if (playerInQuest[i].getPass() != false)
                    drawACard(playerInQuest[i], null,null,null);

            }
            pq2.Clear();
            pq3.Clear();
            aCard = p3.getHand().Find(x => x.getName().Contains("Excalibur"));
            pq2.Add(aCard);
            p3.discard(aCard);
            aCard = p4.getHand().Find(x => x.getName().Contains("Lance"));
            pq3.Add(aCard);
            p4.discard(aCard);
            for (int i = 0; i < playerInQuest.Count; i++)
            {
                if (playerInQuest[i].getPass() == false) { }
                else
                {
                    if (playerQuest[i].Count == 0) gameLog.info(playerInQuest[i].getName() + " plays nothing");
                    else
                    {
                        for (int j = 0; j < playerQuest[i].Count; j++)
                        {
                            gameLog.info(playerInQuest[i].getName() + " plays a" + playerQuest[i][j].getName() + " card");
                        }
                    }
                    int atk = calculatePlayerATK(aQuestcard, playerQuest[i])+ playerInQuest[i].getAtk();
                    gameLog.info(playerInQuest[i].getName() + "'s total ATK for stage 2 is " + atk);
                    if ((atk) < sponsorStageATK[1])
                    {
                        gameLog.info(playerInQuest[i].getName() + " is eliminated");
                        playerInQuest[i].setPass(false);
                    }
                    else gameLog.info(playerInQuest[i].getName() + " passes this stage");
                }
            }
            for (int i=0; i < players.Length; i++)
            {
                print(players[i].getPass());
                if (players[i].getPass() == true)
                {
                    gameLog.info(players[i].getName() + " is the winner in the quest");
                    for (int j = 0; j < aQuestcard.getStageNum(); j++)
                    {
                        players[i].addAShield(shieldDeck[0]);
                        shieldDeck.Remove(shieldDeck[0]);
                    }
                    gameLog.info(players[i].getName() + " gets " + aQuestcard.getStageNum() + " shields");
                    gameLog.info(players[i].getName() + " has " + players[i].getShield().Count + " shields");
                }
            }         
        }
        else
        {
            for (int i = 0; i < MAXPLAYERNUM; i++)
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
                    }
                    else if (cardName.Substring(14, cardName.Length - 14) == "Questing_Beast")
                    {
                        if (aQuestcard.getName() == "        SQ_SFTQB_4_1") discardNUM = 4;
                        else discardNUM = 1;
                    }
                    else discardNUM = 1;

                    Debug.Log("stages " + i + " is a test");
                    for (int j = 0; j < playerInQuest.Count; j++)
                    {
                        bool pass;
                        drawACard(playerInQuest[j],null,null,null);
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
                   
                    Debug.Log("stages " + i + " is a quest");
                    for (int j = 0; j < playerInQuest.Count; j++)
                    {
                        drawACard(playerInQuest[j], null,null,null);

                        
                    }
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
        if (rigging == true)
        {
            if (aCard.getName().Contains("SE_PT"))
            {
                gameLog.info("the story card is Prosperity Throughout the Realm");
                gameLog.info("all player get 2 adventure cards");
                drawACard(p1, null, null, null);
                drawACard(p1, null, null, null);
                drawACard(p2, null, null, null);
                drawACard(p2, null,"Weap", null);
                drawACard(p3, null, null, null);
                drawACard(p3, null, null, "Amou");
                drawACard(p4, null, null, null);
                drawACard(p4, null,"Foe", null);
            }
            else if (aCard.getName().Contains("SE_CD"))
            {
                float[] rankLevel = new float[4];
                for (int i=0; i < MAXPLAYERNUM; i++)
                {
                    if (players[i].getRank() == Rank.SQUIRE)
                        rankLevel[i] = 0 + (players[i].getShield().Count / 10f);
                }
                float min = rankLevel[0];
                for (int i=1; i < rankLevel.Length; i++)
                {
                    if (rankLevel[i] < min)
                        min = rankLevel[i];
                }
                for (int i=0; i < MAXPLAYERNUM; i++)
                {
                    if (rankLevel[i] == min)
                    {
                        for (int j=0; j < 3; j++)
                        {
                            players[i].addAShield(shieldDeck[0]);
                            shieldDeck.Remove(shieldDeck[0]);
                        }
                        gameLog.info(players[i].getName() + " gets three shield");
                    }
                    gameLog.info(players[i].getName() + " has " + players[i].getShield().Count + " shields");
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
        while (aPlayer.getHand().Count > 12)
        {
            if (discardTheCard == null)
            {
                gameLog.warn("you have more than 12 cards on hand, discard...");
                gameLog.info("discard " + aPlayer.getHand()[11].getName());
                aPlayer.discard(aPlayer.getHand()[11]);
            }
            else
            {
                gameLog.warn("you have more than 12 cards on hand, discard...");
                Card aCard = aPlayer.getHand().Find(x => x.getName().Contains(discardTheCard));
                if (aCard != null) {
                    aPlayer.discard(aCard);
                    gameLog.info("discard " + aCard.getName());
                }
                else
                {
                    gameLog.info("failure to discard the specific card " + discardTheCard);
                    aPlayer.discard(aPlayer.getHand()[11]);
                }
            }
        }
    }

    void playAmour(Player aPlayer, AmourCard aAmourCard)
    {
        if (aPlayer.getHand().Find(x=> x.getName() == aAmourCard.getName()) == null)
            {
                gameLog.error("The Amour Card " + aAmourCard.getName() + " not found");
            }
        else
            {
                aPlayer.setAllayOrAmour(aAmourCard);
                gameLog.info(aPlayer.getName() + " uses an Amour Card called " + aAmourCard.getName());
                gameLog.info("his current ATK is " + aPlayer.getAtk());
                aPlayer.discard(aAmourCard);
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
