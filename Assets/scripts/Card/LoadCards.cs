using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(int))]
public class LoadCards : MonoBehaviour{

    public GameObject cardPrefab;
    public Transform card_initial_transform;
    public Transform card_final_transform_p1;
    public Transform card_final_transform_p2;
    public Transform card_final_transform_p3;
    public Transform card_final_transform_p4;


    public GameObject[] cards = new GameObject[130]; // or sue list ,if use list, need card.cs and can use remove at
    //public int index = 0;
    private int cardnum_count = 0;
    public const int MAXCARDNUM = 48;
    public const int MAXPLAYERNUM = 4;
    private float nextActionTime = 0.0f;
    private float period = 0.2f;
    private int index = 117;

	private string[] cardName = {"QuestsFoes_15_15_01_Robber","QuestsFoes_10_20_01_Saxons","QuestsFoes_05_15_01_Boar",
		"QuestsFoes_05_05_01_Thieves","QuestsFoes_25_40_01_Green","QuestsFoes_25_35_01_Black","QuestsFoes_20_30_01_Evil",
		"QuestsFoes_15_25_01_Saxon","QuestsFoes_50_70_01_Dragon","QuestsFoes_40_40_01_Giant","QuestsFoes_30_30_01_Mordred",
		"QuestsWeap_10_10_01_Horse","QuestsWeap_10_10_01_Sword","QuestsWeap_05_05_01_Dagger","QuestsWeap_30_30_01_Excalibur",
		"QuestsWeap_20_20_01_Lance","QuestsWeap_15_15_01_Battleax","QuestsAlly_10_20_01_SirGawain","QuestsAlly_05_20_01_SirPercival",
		"QuestsAlly_00_00_01_Merlin","QuestsAlly_10_20_01_SirTristan","QuestsAlly_15_25_01_SirLancelot","QuestsAlly_15_15_01_SirGalahad",
		"QuestsAlly_10_10_01_KingArthur_Bids_2","QuestsAlly_00_00_01_QueenGuinevere_Bids_3","QuestsAlly_10_10_01_KingPellinore_Bids_4",
		"QuestsAlly_00_00_01_QueenIseult_Bids_24","QuestsAmou_10_10_01_Amour_Bids_1","QuestsFoes_15_15_02_Robber","QuestsFoes_15_15_03_Robber",
		"QuestsFoes_15_15_04_Robber","QuestsFoes_15_15_05_Robber","QuestsFoes_15_15_06_Robber","QuestsFoes_15_15_07_Robber","QuestsFoes_10_20_02_Saxons",
		"QuestsFoes_10_20_03_Saxons","QuestsFoes_10_20_04_Saxons","QuestsFoes_10_20_05_Saxons","QuestsFoes_05_15_02_Boar","QuestsFoes_05_15_03_Boar",
		"QuestsFoes_05_15_04_Boar","QuestsFoes_05_05_02_Thieves","QuestsFoes_05_05_03_Thieves","QuestsFoes_05_05_04_Thieves","QuestsFoes_05_05_05_Thieves",
		"QuestsFoes_05_05_06_Thieves","QuestsFoes_05_05_07_Thieves","QuestsFoes_05_05_08_Thieves","QuestsFoes_25_40_02_Green","QuestsFoes_25_35_02_Black",
		"QuestsFoes_25_35_03_Black","QuestsFoes_20_30_02_Evil","QuestsFoes_20_30_03_Evil","QuestsFoes_20_30_04_Evil","QuestsFoes_20_30_05_Evil",
		"QuestsFoes_20_30_06_Evil","QuestsFoes_15_25_02_Saxon","QuestsFoes_15_25_03_Saxon","QuestsFoes_15_25_04_Saxon","QuestsFoes_15_25_05_Saxon",
		"QuestsFoes_15_25_06_Saxon","QuestsFoes_15_25_07_Saxon","QuestsFoes_15_25_08_Saxon","QuestsFoes_40_40_02_Giant","QuestsFoes_30_30_02_Mordred",
		"QuestsFoes_30_30_03_Mordred","QuestsFoes_30_30_04_Mordred","QuestsWeap_10_10_02_Horse","QuestsWeap_10_10_03_Horse","QuestsWeap_10_10_04_Horse",
		"QuestsWeap_10_10_05_Horse","QuestsWeap_10_10_06_Horse","QuestsWeap_10_10_07_Horse","QuestsWeap_10_10_08_Horse","QuestsWeap_10_10_09_Horse",
		"QuestsWeap_10_10_10_Horse","QuestsWeap_10_10_11_Horse","QuestsWeap_10_10_02_Sword","QuestsWeap_10_10_03_Sword","QuestsWeap_10_10_04_Sword",
		"QuestsWeap_10_10_05_Sword","QuestsWeap_10_10_06_Sword","QuestsWeap_10_10_07_Sword","QuestsWeap_10_10_08_Sword","QuestsWeap_10_10_09_Sword",
		"QuestsWeap_10_10_10_Sword","QuestsWeap_10_10_11_Sword","QuestsWeap_10_10_12_Sword","QuestsWeap_10_10_13_Sword","QuestsWeap_10_10_14_Sword",
		"QuestsWeap_10_10_15_Sword","QuestsWeap_10_10_16_Sword","QuestsWeap_05_05_02_Dagger","QuestsWeap_05_05_03_Dagger","QuestsWeap_05_05_04_Dagger",
		"QuestsWeap_05_05_05_Dagger","QuestsWeap_05_05_06_Dagger","QuestsWeap_30_30_02_Excalibur","QuestsWeap_20_20_02_Lance","QuestsWeap_20_20_03_Lance",
		"QuestsWeap_20_20_04_Lance","QuestsWeap_20_20_05_Lance","QuestsWeap_20_20_06_Lance","QuestsWeap_15_15_02_Battleax","QuestsWeap_15_15_03_Battleax",
		"QuestsWeap_15_15_04_Battleax","QuestsWeap_15_15_05_Battleax","QuestsWeap_15_15_06_Battleax","QuestsWeap_15_15_07_Battleax",
		"QuestsWeap_15_15_08_Battleax","QuestsAmou_10_10_02_Amour_Bids_1","QuestsAmou_10_10_03_Amour_Bids_1","QuestsAmou_10_10_04_Amour_Bids_1",
		"QuestsAmou_10_10_05_Amour_Bids_1","QuestsAmou_10_10_06_Amour_Bids_1","QuestsAmou_10_10_07_Amour_Bids_1","QuestsAmou_10_10_08_Amour_Bids_1"};
    
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
        card_final_transform_p1.Translate(Vector3.back * 0.1f);

    }

    void SetRandomValue()
    {
        int randomindex = (int)Random.Range(0, index - 1);
        //print("Rindex is " + randomindex);
        //print("index is" + (index-1));
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
