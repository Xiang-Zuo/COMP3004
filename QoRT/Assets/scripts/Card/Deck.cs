using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {

    private List<Card> advanture = new List<Card>();
    private string cardName;

    System.IO.StreamReader cardFile = new System.IO.StreamReader(Application.dataPath + @"\scripts\Card\cards.txt");
    private string line;
    
    public void loadCard()
    {
       while ((line = cardFile.ReadLine()) != null)
        {
            Debug.Log(line);
            switch (line.Substring(0,10))
            {
                case "QuestsFoes":
                    cardName = line.Substring(20, (line.Length - 20));
                    FoeCard aFoeCard = new FoeCard(cardName, Kind.FOE, int.Parse(line.Substring(11, 2)), int.Parse(line.Substring(14, 2)));
                    advanture.Add(aFoeCard);
                    break;
                case "QuestsWeap":
                    cardName = line.Substring(20, (line.Length - 20));
                    WeaponCard aWeaponCard = new WeaponCard(cardName, Kind.WEAPON, int.Parse(line.Substring(11, 2)));
                    advanture.Add(aWeaponCard);
                    break;
                case "QuestsTest":
                    cardName = line.Substring(14, (line.Length - 14));
                    TestCard aTestCard = new TestCard(cardName, Kind.TEST);
                    advanture.Add(aTestCard);
                    break;
                case "QuestsAlly":
                    cardName = line.Substring(17, (line.Length - 17));
                    AllyCard aAllyCard = new AllyCard(cardName, Kind.ALLY, int.Parse(line.Substring(11, 2)), int.Parse(line.Substring(14, 2)));
                    advanture.Add(aAllyCard);
                    break;
                case "QuestsAmou":
                    cardName = line.Substring(17, (line.Length - 17));
                    AmourCard aAmourCard = new AmourCard(cardName, Kind.AMOUR, int.Parse(line.Substring(11, 2)));
                    advanture.Add(aAmourCard);
                    break;


                default:
                    Debug.Log("fff"+line);
                    break;    
                    
            }
        }
    }

    public void shuffle()
    {
        
    }

    public void dealing()
    {

    }

    public List<Card> returnList(string type)
    {
        if (type.CompareTo("advanture") == 0) {
            return this.advanture;
        }
        return null;
    }
}

