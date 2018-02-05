using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRank : MonoBehaviour {
    public Transform player1_rank;
    public Transform player2_rank;
    public Transform player3_rank;
    public Transform player4_rank;
    GameObject player1Rank;
    GameObject player2Rank;
    GameObject player3Rank;
    GameObject player4Rank;
    RankCard card1;
    RankCard card2;
    RankCard card3;
    RankCard card4;


    private float nextActionTime = 0.0f;
    private float period = 5f;



    // Use this for initialization
    void Start () {
        /*player1Rank = GameObject.Find("squire_p1");
        card1 = player1Rank.GetComponent<RankCard>();
        player1Rank.transform.position = player1_rank.position;
        card1.atk = 5;
        SetActive(player1Rank, true);
        player1Rank = GameObject.Find("knight_p1");
        card1 = player1Rank.GetComponent<RankCard>();
        player1Rank.transform.position = player1_rank.position;
        card1.atk = 10;
        SetActive(player1Rank, false);
        player1Rank = GameObject.Find("championKnight_p1");
        card1 = player1Rank.GetComponent<RankCard>();
        player1Rank.transform.position = player1_rank.position;
        card1.atk = 20;
        SetActive(player1Rank, false);
        */
        player1Rank = GameObject.Find("squire_p1");
        SetActive(player1Rank, true);
        player1Rank = GameObject.Find("knight_p1");
        SetActive(player1Rank, false);
        player1Rank = GameObject.Find("championKnight_p1");
        SetActive(player1Rank, false);

    }

    // Update is called once per frame
    void Update()
    {
        while (Time.time > nextActionTime)
        {
            nextActionTime += period;
            //SetActive(player1Rank, false);

        }
    }

    void SetActive(GameObject go, bool enable)
    {
        go.active = enable;
        print(go.active);
    }

   
}
