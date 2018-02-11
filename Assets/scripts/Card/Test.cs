﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private List<Card> advantureDeck = new List<Card>();
    private List<QuestCard> questDeck = new List<QuestCard>();
    private List<TournamentCard> tournDeck = new List<TournamentCard>();
    private List<EventCard> eventDeck = new List<EventCard>();
    private List<Card> rankDeck = new List<Card>();
    private List<Card>[] hands = new List<Card>[4];
    private List<Card> sponserCardOnDeck = new List<Card>();


    public Transform p1_card_transform;
    public Transform p2_card_transform;
    public Transform p3_card_transform;
    public Transform p4_card_transform;

    private List<Card> hand1 = new List<Card>();
    private List<Card> hand2 = new List<Card>();
    private List<Card> hand3 = new List<Card>();
    private List<Card> hand4 = new List<Card>();

    private Player p1;
    private Player p2;
    private Player p3;
    private Player p4;

    private int MAXPLAYERNUM = 4;
    private int advantureCard_count = 0;
    private bool testValid = true;


    // Use this for initialization
    void Start()
    {
        //event.click.numberof player
        setHands();
        //event.click.start
        load_dealing();

        for (int i = 0; i < 5; i++)  //to test if all story have been used
        {
            storyEvent();
        }


    }
    // Update is called once per frame
    void Update()
    {

    }
    void setHands()
    {
        hands[0] = hand1;
        hands[1] = hand2;
        hands[2] = hand3;
        hands[3] = hand4;
    }
    void load_dealing()
    {
        loadDeckSys();
        dealing(advantureDeck);
        p1 = new Player("p1", hand1);
        p2 = new Player("p2", hand2);
        p3 = new Player("p3", hand3);
        p4 = new Player("p4", hand4);
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
        int randomEvent;
        randomEvent = randomTheEventDeck();
        if (randomEvent == 0)
        {
            currentStoryCardName = questDeck[0].getName();
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
            print("all card been used");
            //reload
        }
        if (currentStoryCardName != null)
        {
            switch (currentStoryCardName.Substring(0, 10))
            {
                case "        SQ":
                    //handleQuestEvent (questDeck [0], p1);
                    handleQuestEventSponsor(questDeck[0], p1);
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
        //先get stage判断sponosor要出几张牌
        QuestCard aCard = aQuestcard;
        int stageNum = aCard.getStageNum();
        print(aCard.getName());
        print("stagenum " + stageNum);
        //string cardName = aCard.getName ().Substring(0,16);
        Card sponsorCard1 = new Card();
        Card sponsorCard2 = new Card();
        Card sponsorCard3 = new Card();
        Card sponsorCard4 = new Card();
        Card sponsorCard5 = new Card();

        List<Card> stage1 = new List<Card>();
        List<Card> stage2 = new List<Card>();
        List<Card> stage3 = new List<Card>();
        List<Card> stage4 = new List<Card>();
        List<Card> stage5 = new List<Card>();

        List<Card>[] stages = new List<Card>[5];
        stages[0] = stage1;
        stages[1] = stage2;
        stages[2] = stage3;
        stages[3] = stage4;
        stages[4] = stage5;


        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Card theCard = getRandomCardFromSponsor(aSponsor);
                if (theCard.getKind() == Kind.TEST && testValid != false)
                {
                    stages[i].Add(theCard);
                    print(theCard.getName());
                    testValid = false;
                }
                else if (theCard.getKind() == Kind.TEST && testValid == false)
                {
                    j--;
                    //print("already test --------------------------------");
                }
                else
                {
                    stages[i].Add(theCard);
                    print(theCard.getName());
                }
            }
            print("stage" + (i + 1) + "end here");
        }

        testValid = true;
    }

    void handleQuestEvent(QuestCard aQuestcard, Player aPlayer)
    {

        /*
			首先一个player主持这个quest
			get这个quest的stage
			player放牌
			我们检测是否合法

			---合法
			其他几个player加入这个quest
			

		*/
        QuestCard aCard = aQuestcard;
        int stage = aCard.getStageNum();
        string cardName = aCard.getName();

        if (stage == 2)
        {
            print("===============================" + stage);
            switch (cardName.Substring(0, 16))
            {
                //repel the saxon raiders
                //Foe: All Saxons
                case "        SQ_RTSR ":
                    print("----------------------" + cardName);
                    break;
                //boar hunt
                //Foe:Boar
                case "        SQ_BH   ":
                    print("----------------------" + cardName);
                    break;
            }

        }
        else if (stage == 3)
        {
            print("===============================" + stage);
            switch (cardName.Substring(0, 16))
            {
                //Journey Thtough The Enchanted Forest
                //Foe: Evil Knight
                case "        SQ_JTTEF":
                    print("----------------------" + cardName);
                    break;
                //Vanquish King Arthur's Enemies
                case "        SQ_VKAE ":
                    print("----------------------" + cardName);
                    break;
                //slay the dragon			
                //Foe: Dragon
                case "        SQ_STD  ":
                    print("----------------------" + cardName);
                    break;
                //rescue the fair maiden		
                //Foe: Black Knight
                case "        SQ_RTFM ":
                    print("----------------------" + cardName);
                    break;

            }

        }
        else if (stage == 4)
        {
            print("===============================" + stage);
            switch (cardName.Substring(0, 16))
            {
                //search for the questing beast
                case "        SQ_SFTQB":
                    print("----------------------" + cardName);
                    break;
                //defend the queen's honor
                //Foe:All
                case "        SQ_DTQH ":
                    print("----------------------" + cardName);
                    break;

                //Test of the green knight
                //Foe:green knight
                case "        SQ_TOTGK":
                    print("----------------------" + cardName);
                    break;
            }

        }
        else if (stage == 5)
        {
            print("===============================" + stage);
            print("----------------------" + cardName);
        }


        //print(aCard.getName().Substring(0,16));


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
}
