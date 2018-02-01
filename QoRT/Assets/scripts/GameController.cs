using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(int))]
public class GameController : MonoBehaviour{

    public GameObject cardPrefabs;
    public Transform card_initial_transform;
    public Transform card_final_transform;
    public GameObject[] cards;
    public int index = 0;
    private int cardnum_count = 0;
    public const int MAXCARDNUM = 12;
    private float nextActionTime = 0.0f;
    public float period = 1f;

    //private Deck aDeck;
   
    
    void Start()
    {
        GameObject theFoeCard = GameObject.Find("QuestsFoes1_0");
        Card card = theFoeCard.GetComponent<Card>();
        print(card.atk);
    //string assetName = "QuestsFoes1_0";
    }
	
	// Update is called once per frame
	void Update () {
        while (cardnum_count<MAXCARDNUM && Time.time > nextActionTime)
        {
            nextActionTime += period;
            cardPrefabs = cards[index++];
            RandomGenerateCard();
            cardnum_count++;
        }
    }

   void RandomGenerateCard()
    {
        GameObject aCard = Instantiate(cardPrefabs);
        aCard.transform.position = card_initial_transform.position;
        iTween.MoveTo(aCard,card_final_transform.position, 3f);
        card_final_transform.Translate(Vector2.right * 4);
       
    }

    
}
