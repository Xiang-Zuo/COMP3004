using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(int))]
public class GameController : MonoBehaviour{

    public GameObject cardPrefab;
    public Transform card_initial_transform;
    public Transform card_final_transform_p1;
    public Transform card_final_transform_p2;
    public Transform card_final_transform_p3;
    public Transform card_final_transform_p4;


    public GameObject[] cards = new GameObject[100]; // or sue list ,if use list, need card.cs and can use remove at
    //public int index = 0;
    private int cardnum_count = 0;
    public const int MAXCARDNUM = 28;
    public const int MAXPLAYERNUM = 4;
    private float nextActionTime = 0.0f;
    private float period = 2f;
    private int index = MAXCARDNUM;

    private string[] cardName = { "QuestsFoes1_0", "QuestsFoes1_1", "QuestsFoes1_2", "QuestsFoes1_3","QuestsFoes1_5", "QuestsFoes1_6", "QuestsFoes1_8",
                                  "QuestsFoes1_9","QuestsFoes2_1","QuestsFoes2_2" ,"QuestsFoes2_3","QuestWeapons_1","QuestWeapons_2","QuestWeapons_3",
                                  "QuestWeapons_6","QuestWeapons_7","QuestWeapons_8","QuestsSpecial1_1","QuestsSpecial1_2","QuestsSpecial1_3",
                                  "QuestsSpecial1_4","QuestsSpecial1_6","QuestsSpecial1_8","QuestsSpecial1_9","QuestsSpecial1_10","QuestsSpecial2_1",
                                  "QuestsSpecial2_2","QuestsSpecial2_3"};
    
    void Start()
    {
        LoadCard();
        
    }
	
	// Update is called once per frame
	void Update () {
        while (cardnum_count<MAXCARDNUM && Time.time > nextActionTime)
        {
            nextActionTime += period;     //deal one card per 0.5 sec
            RandomDealingCard();
            cardnum_count++;
        }
    }

   void RandomDealingCard()
    {
        SetRandomValue();
        GameObject aCard = Instantiate(cardPrefab);
        aCard.transform.position = card_initial_transform.position;
        iTween.MoveTo(aCard,SetFinalPosition().position, 3f);
        card_final_transform_p1.Translate(Vector2.right * 1);
       
    }

    void SetRandomValue()
    {
        int randomindex = (int)Random.Range(0, index - 1);
        print("Rindex is " + randomindex);
        print("index is" + (index-1));
        cardPrefab = cards[randomindex];
        //Destroy(cards[randomindex]);
        cards[randomindex] = cards[index-1];
        cards[index-1] = null;
        index--;
        
    }

    Transform SetFinalPosition()
    {
        Transform[] endposition = new Transform[4];
        endposition[0] = card_final_transform_p1;
        endposition[1] = card_final_transform_p2;
        endposition[2] = card_final_transform_p3;
        endposition[3] = card_final_transform_p4;
        return endposition[cardnum_count % MAXPLAYERNUM];
    }

    void AccessCard()
    {
        GameObject theFoeCard = GameObject.Find("QuestsFoes1_0");
        FoeCard card = theFoeCard.GetComponent<FoeCard>();
        //print(card.atk);
    }

    void LoadCard()
    {
        for (int i=0; i < cardName.Length; i++)
        {
            GameObject newCard = GameObject.Find(cardName[i]);
            cards[i] = newCard;
        }

    }

}
