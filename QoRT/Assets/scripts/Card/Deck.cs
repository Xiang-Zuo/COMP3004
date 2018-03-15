using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {

    private List<Card> advanture = new List<Card>();
    private List<QuestCard> questCard = new List<QuestCard>();
    private List<EventCard> eventCard = new List<EventCard>();
    private List<TournamentCard> tournCard = new List<TournamentCard>();
    private List<Card> rank = new List<Card>();
    private List<ShieldCard> shieldCard = new List<ShieldCard>();


    System.IO.StreamReader cardFile = new System.IO.StreamReader(Application.dataPath + @"/scripts/Card/cards.txt");
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
				//Quests
                case "        SQ":
                    //line = line.Substring(11, 5);
                    QuestCard aQuestCard = new QuestCard(line, Kind.QUEST, int.Parse(line.Substring(17, 1)));
                    questCard.Add(aQuestCard);
                    break;
				//Touranaments
                case "        ST":
                    //line = line.Substring(11, 2);
                    TournamentCard aTournamentCard = new TournamentCard(line, Kind.TOURNAMENT, int.Parse(line.Substring(14, 1)));
                    tournCard.Add(aTournamentCard);
                    break;
				//events
                case "        SE":
                    //line = line.Substring(11, 2);
                    EventCard aEventCard = new EventCard(line, Kind.EVENT);
                    eventCard.Add(aEventCard);
                    break;
				//rank deck
                case "        RD":
                    //line = line.Substring(11, 2);
                    RankCard aRankCard = new RankCard(line, Kind.RANK, setRank(int.Parse(line.Substring(14, 2))));
                    rank.Add(aRankCard);
                    break;
                case "    shield":
                    ShieldCard aShieldCard = new ShieldCard(line, Kind.SHIELD);
                    shieldCard.Add(aShieldCard);
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
        List<QuestCard> sfed_que_deck = new List<QuestCard>();
        List<EventCard> sfed_eve_deck = new List<EventCard>();
        List<TournamentCard> sfed_tor_deck = new List<TournamentCard>();
        System.Random rnd = new System.Random();
        while (advanture.Count != 0)
        {
            int index = rnd.Next(0, advanture.Count);
            sfed_adv_deck.Add(advanture[index]);
            advanture.RemoveAt(index);
        }
        while (questCard.Count != 0)
        {
            int index = rnd.Next(0, questCard.Count);
            sfed_que_deck.Add(questCard[index]);
            questCard.RemoveAt(index);
        }
        while (eventCard.Count != 0)
        {
            int index = rnd.Next(0, eventCard.Count);
            sfed_eve_deck.Add(eventCard[index]);
            eventCard.RemoveAt(index);
        }
        while (tournCard.Count != 0)
        {
            int index = rnd.Next(0, tournCard.Count);
            sfed_tor_deck.Add(tournCard[index]);
            tournCard.RemoveAt(index);
        }
        advanture = sfed_adv_deck;
        questCard = sfed_que_deck;
        eventCard = sfed_eve_deck;
        tournCard = sfed_tor_deck;
    }

    public List<Card> getAdvantureDeck()
    {
        return advanture;
    }

    public List<QuestCard> getQuestDeck()
    {
        return questCard;
    }

    public List<EventCard> getEventDeck()
    {
        return eventCard;
    }
    public List<TournamentCard> getTournDeck()
    {
        return tournCard;
    }

    public List<Card> getRankDeck()
    {
        return rank;
    }

    public List<ShieldCard> getShieldDeck()
    {
        return shieldCard;
    }

    Rank setRank(int atk)
    {
        if (atk == 5)
        {
            return Rank.SQUIRE;
        }
        else if (atk == 10)
        {
            return Rank.KNIGHT;
        }
        else if (atk == 20)
        {
            return Rank.CHAMPIONKNIGHT;
        }

        return Rank.SQUIRE;
    }
}

