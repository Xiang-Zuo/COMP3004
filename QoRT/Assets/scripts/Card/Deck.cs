using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {

    private List<Card> advanture = new List<Card>();
    private List<Card> story = new List<Card>();
    private List<Card> rank = new List<Card>();

    System.IO.StreamReader cardFile = new System.IO.StreamReader(Application.dataPath + @"\scripts\Card\cards.txt");
    private string line;
    
    public void loadCard()
    {
       while ((line = cardFile.ReadLine()) != null)
        {
            switch (line.Substring(0,10))
            {
                case "QuestsFoes":
                    //line = line.Substring(20, (line.Length - 20));
                    FoeCard aFoeCard = new FoeCard(line, Kind.FOE, int.Parse(line.Substring(11, 2)), int.Parse(line.Substring(14, 2)));
                    advanture.Add(aFoeCard);
                    break;
                case "QuestsWeap":
                    //line = line.Substring(20, (line.Length - 20));
                    WeaponCard aWeaponCard = new WeaponCard(line, Kind.WEAPON, int.Parse(line.Substring(11, 2)));
                    advanture.Add(aWeaponCard);
                    break;
                case "QuestsTest":
                    //line = line.Substring(14, (line.Length - 14));
                    TestCard aTestCard = new TestCard(line, Kind.TEST);
                    advanture.Add(aTestCard);
                    break;
                case "QuestsAlly":
                    //line = line.Substring(17, (line.Length - 17));
                    AllyCard aAllyCard = new AllyCard(line, Kind.ALLY, int.Parse(line.Substring(11, 2)), int.Parse(line.Substring(14, 2)));
                    advanture.Add(aAllyCard);
                    break;
                case "QuestsAmou":
                    //line = line.Substring(17, (line.Length - 17));
                    AmourCard aAmourCard = new AmourCard(line, Kind.AMOUR, int.Parse(line.Substring(11, 2)));
                    advanture.Add(aAmourCard);
                    break;
                case "        SQ":
                    //line = line.Substring(11, 5);
                    QuestCard aQuestCard = new QuestCard(line, Kind.QUEST, int.Parse(line.Substring(17, 1)));
                    story.Add(aQuestCard);
                    break;
                case "        ST":
                    //line = line.Substring(11, 2);
                    TournamentCard aTournamentCard = new TournamentCard(line, Kind.TOURNAMENT, int.Parse(line.Substring(14, 1)));
                    story.Add(aTournamentCard);
                    break;
                case "        SE":
                    //line = line.Substring(11, 2);
                    EventCard aEventCard = new EventCard(line, Kind.EVENT);
                    story.Add(aEventCard);
                    break;
                case "        RD":
                    //line = line.Substring(11, 2);
                    RankCard aRankCard = new RankCard(line, Kind.RANK, int.Parse(line.Substring(14, 2)));
                    rank.Add(aRankCard);
                    break;
                default:
                    Debug.Log("Error, fail to read card infor for text");
                    break;                       
            }
        }
    }

    public void shuffle()
    {
        List<Card> sfed_adv_deck = new List<Card>();
        System.Random rnd = new System.Random();
        while (advanture.Count != 0)
        {
            int index = rnd.Next(0, advanture.Count);
            sfed_adv_deck.Add(advanture[index]);
            advanture.RemoveAt(index);
        }
        advanture = sfed_adv_deck;
    }

   

    public List<Card> getAdvantureDeck()
    {
        return advanture;
    }

    public List<Card> getStoryDeck()
    {
        return story;
    }

    public List<Card> getRankDeck()
    {
        return rank;
    }
}

