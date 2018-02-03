using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    /* public List<Card> adventureDeck = new List<Card>();

     void Start()
     {
         Insert();
     }
     void Insert()
     {
         FoeCard Robber_Knight = new FoeCard() { name = "Robber_Knight", atk = 15, atkSpecial = -1 };
         adventureDeck.Add(Robber_Knight);
         Debug.Log(adventureDeck.Count);
         Debug.Log(adventureDeck[0].name);
     }*/
    //public List<GameObject> adventureDeck = new List<GameObject>();
    public List<AdventureDeck> aa = new List<AdventureDeck>();
    //public GameObject[] a;
    public class AdventureDeck
    {
        public GameObject acard;
        public int atk;

    }

    private void Start()
    {
        AdventureDeck foe;
        
    }


}



