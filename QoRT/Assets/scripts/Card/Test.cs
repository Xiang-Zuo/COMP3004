using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    private List<Card> advantureDeck = new List<Card>();
    private List<Card> storyDeck = new List<Card>();
    private List<Card> rankDeck = new List<Card>();
    private List<Card>[] hands = new List<Card>[4];

    public Transform p1_card_transform;
    public Transform p2_card_transform;
    public Transform p3_card_transform;
    public Transform p4_card_transform;

    private List<Card> hand1 = new List<Card>();
    private List<Card> hand2 = new List<Card>();
    private List<Card> hand3 = new List<Card>();
    private List<Card> hand4 = new List<Card>();

    private int MAXPLAYERNUM = 4;
    private int advantureCard_count = 0;
    private float nextActionTime = 0.0f;
    private float period = 0.2f;

    // Use this for initialization
    void Start () {
        loadDeckSys();
        setHands();
        dealing(advantureDeck);
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void loadDeckSys()
    {
        Deck aDeck = new Deck();
        aDeck.loadCard();
        aDeck.shuffle();
        advantureDeck = aDeck.getAdvantureDeck();
        storyDeck = aDeck.getStoryDeck();
        rankDeck = aDeck.getRankDeck();
       // dealing(advantureDeck);
    }
    void dealing(List<Card> alist)
    {       
        while (advantureCard_count < 28)
        {       
            for (int i=0; i<MAXPLAYERNUM; i++)
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

    void setHands()
    {
        hands[0] = hand1;
        hands[1] = hand2;
        hands[2] = hand3;
        hands[3] = hand4;     
    }
}
