using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRounds : MonoBehaviour {
    bool quit = false;
	int stat;
    GameObject[] adventure;
    GameObject[] knight;
    GameObject[] championknight;
    GameObject[] squire;


    /**
	 * stat:
	 * 0 for Initial page
	 * 1 for draw a card from story deck
	 * 2 for Quest
	 * 3 for Tournament
	 * 4 for Event
	 * 5 for process Q,T,E
	 * 6 for player's change after a turn 
	 * 7 for check win, return 1 if not
	**/

    // Use this for initialization
    void Start () {
        stat = 0;
        squire = GameObject.FindGameObjectsWithTag("squire");
        knight = GameObject.FindGameObjectsWithTag("knight");
        championknight = GameObject.FindGameObjectsWithTag("championknight");
        for (int i=0; i<4; i++)
        {
            knight[i].SetActive(false);
            championknight[i].SetActive(false);
        }

        //adventure=GameObject.
	}
	
	// Update is called once per frame
	void Update () {
           // playing(stat);
    }

	void playing(int stat)
	{
        if (stat == 0)
        {
            print("0");
            stat = 7;
            playing(stat);
        }
            if (stat == 1)
            {
            print("1");
            stat = 2;
            playing(stat);
            }

            if (stat == 2)
            {
            print("2");
            stat = 7;
            playing(stat);
            }

            if (stat == 3)
            {

            }

            if (stat == 4)
            {

            }

            if (stat == 5)
            {

            }

            if (stat == 6)
            {

            }

            if (stat == 7)
            {
            SceneManager.LoadScene(0);
            }
        
	}
}
