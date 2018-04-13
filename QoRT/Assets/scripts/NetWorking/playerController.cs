using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using System.IO;
using System;



public class playerController : NetworkBehaviour
{
    public GameObject storyPrefab;
    public GameObject player_unit; //the rank icon of a player, here used to represent this player
    public Transform p1pos; //p1 position, used to put player_unit in right place
    public Transform handPosition;
    public TextAsset cardFile;

    private List<Card> adventureDeck = new List<Card>();
    private List<QuestCard> questDeck = new List<QuestCard>();
    private List<string> sponsorUsedCard = new List<string>();
    private List<Card> storyDeck = new List<Card>();
    private List<TournamentCard> tournDeck = new List<TournamentCard>();
    private List<EventCard> eventDeck = new List<EventCard>();
    private List<Card> rankDeck = new List<Card>();
    private List<ShieldCard> shieldDeck = new List<ShieldCard>();



    private Deck aDeck = new Deck();

    private int roundNow = 1; //plus 1 when a Story ends, to 0 when is 4    server used ONLY
    public int askCount = 1;//count how many people been asked            server used ONLY

    [SyncVar]
    public string sponsorNow;
    private string questNow;

    private int maxStage = 0;
    private int stageNum = 0;

    public string pathAdvent;
    public string pathStory;
    public string pathSponsorAtk;
    public string pathCardnum;
    public string pathShieldNum;
    public string pathPlayerStagebool;
    public string pathTournATK;
    public string pathLog;
	public string pathAmour;
	public string pathAlly;
    public bool isLogged = false;

    [SyncVar]
    public int storyCount = 0;

    [SyncVar]
    public string storyNow;

    private string playersInThisQuest = "";

    [SyncVar]
    private string myMsg = "Welcome to the game!";

    [SyncVar] //make this name update to all clients
    public string playerName = "XXX";

    [SyncVar]
    public Rank playerRank = Rank.SQUIRE;

    [SyncVar]
    public int stageNow = 1;

    [SyncVar]
    public int shieldsNum = 0;

    [SyncVar]
    public string cardsCount = "";

    public string advString = null;


    public string stoString = null;

    [SyncVar]
    public string handCardPerPerson = null;

    [SyncVar]
    private string perStageSponsorCard = null;

    [SyncVar]
    private string perStagePlayerCard = null;

    [SyncVar]
    private string tournPlayerCard = null;

    [SyncVar]
    public bool askingSponsor = false;

    [SyncVar]
    private bool askingJoin = false;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdSpawnMyUnit();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            GameObject.Find("MsgText").GetComponent<Text>().text = myMsg;
        }

        if (isServer && Input.GetKeyDown(KeyCode.L) && !isLogged)
        {
            loadDeck();
            advString = File.ReadAllText(pathAdvent);
            CmdloadDeck(advString);
            isLogged = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CmdupdateStory();

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            CmdDiscard(playerName, true);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (playerName == sponsorNow && askingSponsor)
            {
                //askCount = 1;
                askingSponsor = false;
                CmdYesOnAskSponsor();

                GameObject[] gos = GameObject.FindGameObjectsWithTag("Finish");
                foreach (GameObject go in gos)
                    go.GetComponent<playerController>().myMsg = playerName + " accept to be sponsor";

            }

            if (askingJoin)
            {
                if (playerName != sponsorNow)
                {
                    //print(playersInThisQuest);
                    CmdAddThisPlayerInQuest(playerName);

                    myMsg = "Waiting other player make decisions...";
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (askingSponsor)
            {
                CmdAskIfSponsor(false);
            }
            if (askingJoin)
            {
                if (isLocalPlayer)
                {
                    myMsg = "Waiting other player make decisions...";
                    CmdPlayerDoNotJoinStage(playerName);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && playerName == sponsorNow)
        {
            CmdChoose();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isLocalPlayer)
                CmdPlayerChoose();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            CmdAskJoin();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isLocalPlayer)
                CmdChooseTournament();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            CmdStageFinish();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CmdQuestFinsih();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            CmdSponsorDraw();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdDiscard(playerName, false);
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Finish");
            string a = sponsorNow;
            int b = Int32.Parse(a[6].ToString());
            b = (b + 1) % 4;
            a = "player" + b;
            for (int i = 0; i < gos.Length; i++)
            {
                if (gos[i].GetComponent<playerController>().playerName == a)
                    gos[i].GetComponent<playerController>().myMsg = "press q for next story";
                else
                    gos[i].GetComponent<playerController>().myMsg = "this quest finish";
            }
        }
    }
    [Command]
    void CmdUpdateStoryTxt()
    {
        string a = File.ReadAllText(pathStory);
        string[] temp = a.Split('*');
        temp = temp.Skip(1).ToArray();
        string newStory = null;
        for (int i = 0; i < temp.Length; i++)
        {
            if (i == (temp.Length - 1))
                newStory += temp[i];
            else
                newStory += temp[i] + "*";
        }
        File.WriteAllText(pathStory, newStory);
    }

    [Command]
    void CmdSpawnMyUnit()
    { 
        Debug.Log("CmdSpawnMyUnit -- Server creates a new spawn, num of players now: " + NetworkServer.connections.Count);  //NetworkServer.connections is the list of online users/

        Vector3 offset = new Vector3(0, -0.04f, 0);//adjust position nicely.
        Vector3 distance = new Vector3(4, 0, 0); //distance between two players.
        GameObject go;
        go = (GameObject)Instantiate(
            player_unit,
            p1pos.position + offset + distance * (NetworkServer.connections.Count - 1),
            p1pos.rotation);
        string name = "player" + NetworkServer.connections.Count;
        gameObject.tag = "Finish";
        go.name = name;
        NetworkServer.Spawn(go);
        playerName = name;
        RpcSetName(name);
        pathAdvent = Application.dataPath + "/Resources/Advent.txt";
        pathStory = Application.dataPath + "/Resources/Story.txt";
        pathSponsorAtk = Application.dataPath + "/Resources/SponsorATK.txt";
        pathCardnum = Application.dataPath + "/Resources/CardNum.txt";
        pathPlayerStagebool = Application.dataPath + "/Resources/PlayerStageBool.txt";
        pathShieldNum = Application.dataPath + "/Resources/ShieldNum.txt";
        pathTournATK = Application.dataPath + "/Resources/Tournment.txt";
        pathLog = Application.dataPath + "/Resources/Log.txt";
		pathAmour = Application.dataPath + "/Resources/Amour.txt";
		pathAlly = Application.dataPath + "/Resources/Ally.txt";
        if (name == "player1")
        {
            File.Delete(pathLog);
            File.Delete(pathAdvent);
            File.Delete(pathCardnum);
            File.Delete(pathPlayerStagebool);
            File.Delete(pathSponsorAtk);
            File.Delete(pathStory);
            File.Delete(pathTournATK);
            File.Delete(pathShieldNum);
        }
        File.AppendAllText(pathLog, name + " just join the game" + Environment.NewLine);

    }

    [ClientRpc]
    void RpcSetName(string aName)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (playerName == "XXX")
        {
            playerName = aName;
        }
        else
        {
            return;
        }
    }

    void loadDeck()
    {
        string tempAdv = null;
        string tempSto = null;
		string[] tempAmour= { "0", "0", "0", "0" };
		string[] tempAlly= { "0", "0", "0", "0" };
        aDeck.loadCard(cardFile);
        if (isServer)
        {
            aDeck.shuffle();
        }
        adventureDeck = aDeck.getAdvantureDeck();
        questDeck = aDeck.getQuestDeck();
        tournDeck = aDeck.getTournDeck();
        eventDeck = aDeck.getEventDeck();
        rankDeck = aDeck.getRankDeck();
        shieldDeck = aDeck.getShieldDeck();
        for (int i = 0; i < questDeck.Count; i++)
        {
            Card aCard = questDeck[i];
            storyDeck.Add(aCard);
        }
        for (int i = 0; i < tournDeck.Count; i++)
        {
            Card aCard = tournDeck[i];
            storyDeck.Add(aCard);
        }
        for (int i = 0; i < eventDeck.Count; i++)
        {
            Card aCard = eventDeck[i];
            storyDeck.Add(aCard);
        }

        Card aQuest = storyDeck.Find(x => x.getName().Contains("BH"));
        Card bQuest = storyDeck.Find(x => x.getName().Contains("ST_"));
        Card cQuest = storyDeck.Find(x => x.getName().Contains("STD"));
        storyDeck.Remove(aQuest);
        storyDeck.Remove(bQuest);
        storyDeck.Remove(cQuest);
        storyDeck.Insert(0, aQuest);
        storyDeck.Insert(1, bQuest);
        storyDeck.Insert(2, cQuest);

        for (int i = 0; i < adventureDeck.Count; i++)
        {
            if (i == (adventureDeck.Count - 1))
                tempAdv += adventureDeck[i].getName();
            else
                tempAdv += adventureDeck[i].getName() + "*";
        }
        for (int i = 0; i < storyDeck.Count; i++)
        {
            if (i == (storyDeck.Count - 1))
                tempSto += storyDeck[i].getName();
            else
                tempSto += storyDeck[i].getName() + "*";
        }
        File.WriteAllText(pathAdvent, tempAdv);
        File.WriteAllText(pathStory, tempSto);
		File.WriteAllLines (pathAmour,tempAmour);
		File.WriteAllLines (pathAlly,tempAlly);
    }

    [Command]
    void CmdloadDeck(string n)
    {
        RpcGetHandBack(n, pathCardnum);
    }

    [ClientRpc]
    void RpcGetHandBack(string cards, string path)
    {
        if (!isLocalPlayer) return;
        CmdupdateAdvTxt();
        string[] cardsTotal;
        cardsTotal = cards.Split('*');
        if (isServer && playerName == "player1")
        {
            for (int i = 0; i < 12; i++)
            {
                if (i == 11)
                    handCardPerPerson += cardsTotal[i];
                else
                    handCardPerPerson += cardsTotal[i] + " ";
            }
            sortCard(cardsTotal.Take(12).ToArray());
            CmdupdateCardNum(playerName, handCardPerPerson, path);
            return;
        }
        if (!isServer && playerName == "player2")
        {
            for (int i = 12; i < 24; i++)
            {
                if (i == 23)
                    handCardPerPerson += cardsTotal[i];
                else
                    handCardPerPerson += cardsTotal[i] + " ";
            }
            cardsTotal = cardsTotal.Skip(12).ToArray();
            sortCard(cardsTotal.Take(12).ToArray());
            CmdupdateCardNum(playerName, handCardPerPerson, path);
            return;
        }
        if (!isServer && playerName == "player3")
        {
            for (int i = 24; i < 36; i++)
            {
                if (i == 35)
                    handCardPerPerson += cardsTotal[i];
                else
                    handCardPerPerson += cardsTotal[i] + " ";
            }
            cardsTotal = cardsTotal.Skip(24).ToArray();
            sortCard(cardsTotal.Take(12).ToArray());
            CmdupdateCardNum(playerName, handCardPerPerson, path);
            return;
        }
        if (!isServer && playerName == "player4")
        {
            for (int i = 36; i < 48; i++)
            {
                if (i == 47)
                    handCardPerPerson += cardsTotal[i];
                else
                    handCardPerPerson += cardsTotal[i] + " ";
            }
            cardsTotal = cardsTotal.Skip(36).ToArray();
            sortCard(cardsTotal.Take(12).ToArray());
            CmdupdateCardNum(playerName, handCardPerPerson, path);
            return;
        }
    }

    [Command]
    void CmdupdateAdvTxt()
    {
        if (playerName == "player1")
        {
            string adv = File.ReadAllText(pathAdvent);
            string[] temp = adv.Split('*');
            temp = temp.Skip(12 * NetworkServer.connections.Count).ToArray();
            print(temp.Length);
            string a = null;
            for (int i = 0; i < temp.Length; i++)
            {
                if (i == (temp.Length - 1))
                {
                    a += temp[i];
                }
                else a += temp[i] + "*";
            }
            File.WriteAllText(pathAdvent, a);
        }
    }

    void sortCard(string[] cards)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (string a in cards)
        {
            GameObject go = GameObject.Find(a);
            if (go != null)
                list.Add(go);
            else
                print(a + " card not found!");
        }
        handPosition.position = new Vector3(-5.4f, -3.0f, 0.0f);
        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.localScale = new Vector3(1f, 1f, 1f);
            Vector3 toPosition = new Vector3();
            if (list.Count <= 12)
                toPosition = handPosition.position + new Vector3(1, 0, 1) * i;
            else
                toPosition = handPosition.position + new Vector3(1, 0, 1) * i * 12 / list.Count;
            iTween.MoveTo(list[i], toPosition, 0.0f);
            list[i].GetComponent<TouchCard>().activateCard();
        }
    }

    [Command]
    void CmdupdateCardNum(string name, string handcard, string path)
    {
        string[] cardNums = {"0", "0", "0", "0" };
        if (File.Exists(path))
        {
            cardNums = File.ReadAllLines(path);
        }
        if (name == "player1")
        {
            cardNums[0] = handcard.Split(' ').Length.ToString();
        }
        else if (name == "player2")
        {
            cardNums[1] = handcard.Split(' ').Length.ToString();
        }
        else if (name == "player3")
        {
            cardNums[2] = handcard.Split(' ').Length.ToString();
        }
        else if (name == "player4")
        {
            cardNums[3] = handcard.Split(' ').Length.ToString();
        }
        else { }
        File.WriteAllLines(path, cardNums);
        RpcUpdateCardNum(cardNums);
    }

    [ClientRpc]
    void RpcUpdateCardNum(string[] cardsPerPerson)
    {
        char c = playerName[6];
        string ca = "numOfCardsText";
        ca += c;
        GameObject go = GameObject.Find(ca);
        int index = Int32.Parse(c.ToString()) - 1;
        go.GetComponent<Text>().text = cardsPerPerson[index] + "";
    }

    [Command]
    void CmdupdateStory()
    {
        //print(playerName + isLocalPlayer + isClient + isServer);
        //if (isLocalPlayer && isServer && isClient)
        //{
        if (!isLocalPlayer)
        {
            if (storyNow.Length > 2)
            {
                print("delete story");
                print(storyNow);
                stoString = File.ReadAllText(pathStory);
                string[] temp = stoString.Split('*');
                temp = temp.Skip(1).ToArray();
                string newStory = null;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i == (temp.Length - 1))
                        newStory += temp[i];
                    else
                        newStory += temp[i] + "*";
                }
                File.WriteAllText(pathStory, newStory);
            }
        }
       // }
        /*else if (isClient && )
        {
            if (storyNow.Length > 2)
            {
                print("delete story");
                print(storyNow);
                stoString = File.ReadAllText(pathStory);
                string[] temp = stoString.Split('*');
                temp = temp.Skip(1).ToArray();
                string newStory = null;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i == (temp.Length - 1))
                        newStory += temp[i];
                    else
                        newStory += temp[i] + "*";
                }
                File.WriteAllText(pathStory, newStory);
            }
        }
        */

        stoString = File.ReadAllText(pathStory);
        /*string tempName = NetworkServer.connections.Count.ToString();

        if (playerName == ("player" + tempName))
        {
            string[] temp = stoString.Split('*');
            temp = temp.Skip(1).ToArray();
            string newStory = null;
            for (int i = 0; i < temp.Length; i++)
            {
                if (i == (temp.Length - 1))
                    newStory += temp[i];
                else
                    newStory += temp[i] + "*";
            }
            File.WriteAllText(pathStory, newStory);
            print("cmd delete current story , server " + playerName + newStory + "\n" + "---" + stoString);
        }*/
        CmdNewStory(stoString);
    }

    [Command]
    void CmdNewStory(string story)
    {
        string[] storyString = story.Split('*');
        string aStory = storyString[0];
        string previousStory = null;
        if (storyNow != null)
        {
            previousStory = storyNow;
        }
        //storyNow = aStory;
        if (playerName == "player"+(NetworkServer.connections.Count.ToString()))
        {
            print(playerName + "-----");
            GameObject[] gose = GameObject.FindGameObjectsWithTag("Finish");
            foreach (GameObject go in gose)
            {
                go.GetComponent<playerController>().storyNow = aStory;
            }
        }
        if (aStory.Contains("SQ_"))
        {
            maxStage = questDeck.Find(x => x.getName() == aStory).getStageNum();
            File.Delete(pathSponsorAtk);
            File.Delete(pathPlayerStagebool);
        }
        if (aStory.Contains("ST_"))
        {
            File.Delete(pathTournATK);
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Finish");
            foreach (GameObject go in gos)
                go.GetComponent<playerController>().myMsg = "The new story is tournment choose cards and press T to play";
        }
        File.AppendAllText(pathLog, playerName + " just draw a story card, its " + aStory + Environment.NewLine);
        RpcDisplayStory(aStory, previousStory);
    }

    [ClientRpc]
    void RpcDisplayStory(string storyName, string previous)
    {
        if (previous != null)
        {
            GameObject goA = GameObject.Find(previous);
            Destroy(goA);
        }
        print(storyName);
        GameObject go = GameObject.Find(storyName);
        Vector3 pos = new Vector3(0, 0.8f, 1);
        iTween.MoveTo(go, pos, 1f);
        if (storyName[9] == 'Q')
        {
            Debug.Log("It's a Quest");
            CmdAskIfSponsor(true);
            return;
        }
        else if (storyName[9] == 'E')
        {
            Debug.Log("It's an event.");
            CmdStoryEvent();
            return;
        }
    }

    [Command]
    void CmdAskIfSponsor(bool start)
    { 
        askCount %= 4;
        
        string aName = "player" + askCount;
        sponsorNow = aName;
        RpcAskSponsor(aName);
        Debug.Log("CmdAskIfSponsor--- player asking sponsor now is :" + sponsorNow);
        File.AppendAllText(pathLog, "ask " + sponsorNow + " if sonsor" + Environment.NewLine);
    }

    [ClientRpc]
    void RpcAskSponsor(string playerNow)
    {
        askingSponsor = true;
        if (!isLocalPlayer)
        {
            return;
        }
        if (playerName == playerNow)
        {
            myMsg = "Would you like to sponsor this Quest? Press Y/N to sponsor or decline.";
        }
        else
        {
            myMsg = "Waiting for other player make decision...";
        }
    }

    [Command]
    void CmdSponsorDraw()
    {
        advString = File.ReadAllText(pathAdvent);
        int num = maxStage + sponsorUsedCard.Count;
        RpcSponsorDraw(pathCardnum, advString, num);
        File.AppendAllText(pathLog, "Sponsor get " + num + " cards " + Environment.NewLine);
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Finish");
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].GetComponent<playerController>().playerName == sponsorNow)
                gos[i].GetComponent<playerController>().myMsg = "choose card and press E to discard extra cards";
            else
                gos[i].GetComponent<playerController>().myMsg = "wait sponsor to discard cards";
        }
        
    }

    [ClientRpc]
    void RpcSponsorDraw(string path, string fileContent, int num)
    {
        if (!isLocalPlayer || !isServer) return;
        string[] tempArray = fileContent.Split('*');
        print("before draw a new card" + handCardPerPerson);
        for (int i = 0; i < num; i++)
        {
            handCardPerPerson = handCardPerPerson + " " + tempArray[i];
            fileContent = fileContent.Replace(tempArray[i] + "*", "");
            print("after draw" + playerName + handCardPerPerson);
        }
        CmdupdateCardNum(playerName, handCardPerPerson, path);
        CmdWriteAdvBack(fileContent);
        tempArray = handCardPerPerson.Split(' ');
        sortCard(tempArray);
        sponsorUsedCard.Clear();
    }



    [Command]
    void CmdStageFinish()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Finish");

        string[] temp1 = File.ReadAllLines(pathPlayerStagebool);
        string content = "this stage ends, ";
        int Sponosr = 0;

        for (int i = 0; i < NetworkServer.connections.Count; i++)
        {
            if (temp1[i] == "Sponsor")
                Sponosr = i + 1;
            if (temp1[i] == "Passed")
            {
                content += "player" + (i + 1) + " pass, ";
                temp1[i] = "havent";
            }
            if (temp1[i] == "Failed")
            {
                content += "player" + (i + 1) + " failed, ";
            }
        }
        File.WriteAllLines(pathPlayerStagebool, temp1);
        content = content + "player who pass continue play";
        foreach (GameObject go in gos)
            go.GetComponent<playerController>().myMsg = content;
        File.AppendAllText(pathLog, "this stage finish " + content + Environment.NewLine);
    }

    [Command]
    void CmdQuestFinsih()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Finish");

        string[] temp1 = File.ReadAllLines(pathPlayerStagebool);
        string content = "this quest ends, ";

        for (int i = 0; i < NetworkServer.connections.Count; i++)
        {
            if (temp1[i] == "Win")
            {
                content += "player" + (i + 1) + " win, ";
            }
            if (temp1[i] == "Failed")
            {
                content += "player" + (i + 1) + " failed, ";
            }
        }
        //File.WriteAllLines(pathPlayerStagebool, temp1);
        string b = content + "you are the sponsor, press A to draw card and discard";
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].GetComponent<playerController>().playerName == sponsorNow)
                gos[i].GetComponent<playerController>().myMsg = b;
            else
                gos[i].GetComponent<playerController>().myMsg = content;
        }
        File.AppendAllText(pathLog, "this quest finish " + content + Environment.NewLine);
    }

    [Command]
    void CmdPlayerDoNotJoinStage(string name)
    {
        if (playerName != name) return;
        string[] temp = { "havent", "havent", "havent", "havent" };
        if (!File.Exists(pathPlayerStagebool))
        {
            char c = sponsorNow[6];
            int index = Int32.Parse(c.ToString()) - 1;
            temp[index] = "Sponsor";
        }
        else
        {
            temp = File.ReadAllLines(pathPlayerStagebool);
        }
        int i = Int32.Parse(name[6].ToString()) - 1;
        temp[i] = "Not join";
        File.WriteAllLines(pathPlayerStagebool, temp);
        File.AppendAllText(pathLog, "player  " + playerName + " do not join the quest" + Environment.NewLine);
    }

    [Command]
    void CmdupdateShieldNum(string name, int shieldnum)    ////
    {
        string[] shieldNum = { "0", "0", "0", "0" };
        if (!File.Exists(pathShieldNum))
        {
            //File.WriteAllLines(pathShieldNum, temp);
        }
        else
            shieldNum = File.ReadAllLines(pathShieldNum);
        int index = Int32.Parse(name[6].ToString()) - 1;
        shieldNum[index] = shieldnum.ToString();
        File.WriteAllLines(pathShieldNum, shieldNum);
        RpcUpdateSheildNum(shieldNum);
    }

    [ClientRpc]
    void RpcUpdateSheildNum(string[] shieldArray)
    {
        char c = playerName[6];
        string ca = "numOfShieldsText";
        ca += c;
        GameObject go = GameObject.Find(ca);
        int index = Int32.Parse(c.ToString()) - 1;
        go.GetComponent<Text>().text = shieldArray[index] + "";
    }



    [Command]
    void CmdAddThisPlayerInQuest(string aName)
    {
        advString = File.ReadAllText(pathAdvent);
        File.AppendAllText(pathLog, playerName + " accept join the quest" + Environment.NewLine);
        RpcPlayerJoinedQuest(aName, pathCardnum, advString);
    }

    [ClientRpc]
    void RpcPlayerJoinedQuest(string name, string path2, string fileContent)
    {
        askingJoin = false;
        if (!isLocalPlayer)
        {
            return;
        }

        if (playerName == sponsorNow)
        {
            myMsg = "Players playing in Quest...";
        }
        else if (playerName == name)
        {
            myMsg = "Press 'P' to play cards in stag, press D to discard extra card";
            string[] tempArray = fileContent.Split('*');
            print("before draw a new card" + handCardPerPerson);
            handCardPerPerson = handCardPerPerson + " " + tempArray[0];
            CmdupdateCardNum(playerName, handCardPerPerson, path2);
            print("before delete the draw card" + fileContent);
            fileContent = fileContent.Replace(tempArray[0] + "*", "");
            CmdWriteAdvBack(fileContent);
            print("after delete" + fileContent);
            print("after draw" + playerName + handCardPerPerson);
            tempArray = handCardPerPerson.Split(' ');
            sortCard(tempArray);
        }
    }

    [Command]
    void CmdWriteAdvBack(string content)
    {
        File.WriteAllText(pathAdvent, content);
    }

    [Command]
    void CmdDiscard(string name, bool compare)
    {
        RpcDiscard(name, pathCardnum, compare);
        File.AppendAllText(pathLog, name + " discard cards " + Environment.NewLine);
    }

    [ClientRpc]
    void RpcDiscard(String name, string path, bool compare)
    {
        if (playerName != name || !isLocalPlayer)
            return;

        List<GameObject> Cards = new List<GameObject>();
        string[] handCard = handCardPerPerson.Split(' ');

        List<Card> chosedCard = new List<Card>();
        perStageSponsorCard = null;
        for (int i = 0; i < handCard.Length; i++)
        {
            Cards.Add(GameObject.Find(handCard[i]));
        }

        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i].GetComponent<TouchCard>().isChosen())
            {
                GameObject go = GameObject.Find(Cards[i].name);
                if (go.name == handCard[handCard.Length - 1])
                    handCardPerPerson = handCardPerPerson.Replace(go.name, "");
                else
                    handCardPerPerson = handCardPerPerson.Replace(go.name + " ", "");
                Destroy(go);
            }
        }
        if (handCardPerPerson[handCardPerPerson.Length - 1] == ' ')
        {
            handCardPerPerson = handCardPerPerson.Remove(handCardPerPerson.Length - 1);
        }
        CmdupdateCardNum(playerName, handCardPerPerson, path);
        string[] remainingHandCard;
        remainingHandCard = handCardPerPerson.Split(' ');
        sortCard(remainingHandCard);
        if (compare)
            CmdCompareATK(name, "empty", stageNow);

    }

    [Command]
    void CmdCompareATK(string name, string cards, int stage)
    {
		/*player play amour*/
		string[] amourAtk = File.ReadAllLines (pathAmour);
		int[] amourAtkInt = Array.ConvertAll (amourAtk,s=>int.Parse(s));
		char cname = name [6];
		int ppos = Int32.Parse (cname.ToString()) - 1;
		string[] handcards = cards.Split ('*');
		for(int i=0;i<handcards.Length;i++){
			if(handcards[i].Contains("Amour")){
				amourAtkInt [ppos] += 10;
			}
		}
		amourAtk [ppos] = "" + amourAtkInt [ppos];
		File.WriteAllLines (pathAmour,amourAtk);
		print (name+"'s amourAtk is "+amourAtkInt [ppos]);
		File.AppendAllText(pathLog, name+"'s amourAtk is "+amourAtkInt [ppos] + Environment.NewLine);


		/*player play ally*/
		string[] allyAtk = File.ReadAllLines (pathAmour);
		int[] alllyAtkInt = Array.ConvertAll (allyAtk,s=>int.Parse(s));
		for(int i=0;i<handcards.Length;i++){
			if(handcards[i].Contains("SirGawain")){
				if (storyNow.Contains ("TOTGK")) {
					alllyAtkInt [ppos] += 20;
					print (name+" play sir Gawain ally card in quest test of the green knight so atk is "+alllyAtkInt [ppos]);
					File.AppendAllText(pathLog, name+" play sir Gawain ally card in quest test of the green knight so atk is "+alllyAtkInt [ppos] + Environment.NewLine);
				} else {
					alllyAtkInt [ppos] += 10;
					print (name+" play sir Gawain ally card and atk is "+alllyAtkInt [ppos]);
					File.AppendAllText(pathLog, name+" play sir Gawain ally card and atk is "+alllyAtkInt [ppos] + Environment.NewLine);
				}
			}else if(handcards[i].Contains("SirPercival")){
				if (storyNow.Contains ("SFTHG")) {
					alllyAtkInt [ppos] += 20;
					print (name+" play sir percival ally card in quest search for the holy grail so atk is "+alllyAtkInt [ppos]);
					File.AppendAllText(pathLog, name+" play sir percival ally card in quest search for the holy grail so atk is "+alllyAtkInt [ppos] + Environment.NewLine);
				} else {
					alllyAtkInt [ppos] += 5;
					print (name+" play sir percival ally card and atk is "+alllyAtkInt [ppos]);
					File.AppendAllText(pathLog, name+" play sir percival ally card and atk is "+alllyAtkInt [ppos] + Environment.NewLine);
				}
			}else if(handcards[i].Contains("SirTristan")){
				alllyAtkInt [ppos] += 10;
				print (name+" play sir tristan ally card and atk is "+alllyAtkInt [ppos]);
				File.AppendAllText(pathLog, name+" play sir tristan ally card and atk is "+alllyAtkInt [ppos] + Environment.NewLine);
			}else if(handcards[i].Contains("SirLancelot")){
				if (storyNow.Contains ("DTQH")) {
					alllyAtkInt [ppos] += 25;
					print (name+" play sir lancelot ally card in quest defend the queen's honor so atk is "+alllyAtkInt [ppos]);
					File.AppendAllText(pathLog, name+" play sir lancelot ally card in quest defend the queen's honor so atk is "+alllyAtkInt [ppos] + Environment.NewLine);
				} else {
					alllyAtkInt [ppos] += 15;
					print (name+" play sir lancelot ally card and atk is "+alllyAtkInt [ppos]);
					File.AppendAllText(pathLog, name+" play sir lancelot ally card and atk is "+alllyAtkInt [ppos] + Environment.NewLine);
				}
			}else if(handcards[i].Contains("SirGalahad")){
				alllyAtkInt [ppos] += 15;
				print (name+" play sir galahad ally card and atk is "+alllyAtkInt [ppos]);
				File.AppendAllText(pathLog, name+" play sir galahad ally card and atk is "+alllyAtkInt [ppos] + Environment.NewLine);
			}else if(handcards[i].Contains("KingArthur")){
				alllyAtkInt [ppos] += 10;
				print (name+" play king arthur ally card and atk is "+alllyAtkInt [ppos]);
				File.AppendAllText(pathLog, name+" play king arthur ally card and atk is "+alllyAtkInt [ppos] + Environment.NewLine);
			}else if(handcards[i].Contains("KingPellinore")){
				alllyAtkInt [ppos] += 10;
				print (name+" play king pellinore ally card and atk is "+alllyAtkInt [ppos]);
				File.AppendAllText(pathLog, name+" play king pellinore ally card and atk is "+alllyAtkInt [ppos] + Environment.NewLine);
			}
		}
		allyAtk [ppos] = "" + alllyAtkInt [ppos];
		File.WriteAllLines (pathAlly,allyAtk);
		print (name+"'s allyAtk is "+alllyAtkInt [ppos]);
		File.AppendAllText(pathLog, name+"'s allyAtk is "+alllyAtkInt [ppos] + Environment.NewLine);

        string[] temp = { "havent", "havent", "havent", "havent" };
        if (!File.Exists(pathPlayerStagebool))
        {
            char c = sponsorNow[6];
            int index = Int32.Parse(c.ToString()) - 1;
            temp[index] = "Sponsor";
        }
        else
        {
            temp = File.ReadAllLines(pathPlayerStagebool);
        }
        string sponsorAtk = File.ReadAllText(pathSponsorAtk);
        string fileContent = File.ReadAllText(pathAdvent);
        File.AppendAllText(pathLog, playerName + " plays " + cards + " in this stage " + Environment.NewLine);
		RpcCompareATK(alllyAtkInt [ppos], amourAtkInt [ppos],name, cards, stage, temp, sponsorAtk, fileContent, pathCardnum, maxStage, NetworkServer.connections.Count, pathPlayerStagebool);
		if(stage == maxStage){
			string[] defaultAmoutAtk = {"0", "0", "0", "0" };
			File.WriteAllLines (pathAmour,defaultAmoutAtk);
			print ("storyQuest finish all players' amourAtk should be 0 now.");
			File.AppendAllText(pathLog,"storyQuest finish all players' amourAtk should be 0 now." + Environment.NewLine);
		}
    }

    [ClientRpc]
	void RpcCompareATK(int allyatk,int amouratk,string name, string cards, int stage, string[] temp, string sponsorAtk, string fileContent, string pathCardnum, int maxStage, int connect, string pathstagebool)
    {


        if (playerName == name && !isServer)
        {
            /*string[] temp = { "havent", "havent", "havent", "havent" };
            if (!File.Exists(pathPlayerStagebool))
            {
                char c = sponsorNow[6];
                int index = Int32.Parse(c.ToString()) - 1;
                temp[index] = "Sponsor";
            }
            else
            { 
                temp = File.ReadAllLines(pathPlayerStagebool);
            }
            /////////////////////////////////////////////////////////*/


            int playerAtk = 0;
            //string sponsorAtk = File.ReadAllText(pathSponsorAtk);
            sponsorAtk = sponsorAtk.Remove(sponsorAtk.Length - 1);
            string[] sponsor = sponsorAtk.Split('*');

            if (playerRank == Rank.SQUIRE)
                playerAtk += 5;
            else if (playerRank == Rank.KNIGHT)
                playerAtk += 10;
            else if (playerRank == Rank.CHAMPIONKNIGHT)
                playerAtk += 15;
            else playerAtk += 100;

            if (cards != "empty" || cards.Length > 0)
            {
                string[] playerCards = cards.Split('*');
                print(playerCards.Length);
                print("*" + playerCards[0] + "*");
                for (int i = 0; i < playerCards.Length; i++)
                {
                    if (playerCards[i].Length > 13)
                        playerAtk += Int32.Parse(playerCards[i].Substring(11, 2));
                }
            }
            print("player atk is " + playerAtk + "stagenum" + stage);
            if (stage < maxStage)
            {
                bool pass = false;
                char c = name[6];
                int index = Int32.Parse(c.ToString()) - 1;
                if (playerAtk < Int32.Parse(sponsor[stageNow - 1]))
                {
                    myMsg = "You did not pass this stage, wait for the end";
                    temp[index] = "Failed";
                }
                else
                {
                    myMsg = "Congratulation, you pass the stage,choose for next stage";
                    temp[index] = "Passed";
                    pass = true;
                    //string fileContent = File.ReadAllText(pathAdvent);
                    string[] tempArray = fileContent.Split('*');
                    handCardPerPerson += " " + tempArray[0];
                    CmdupdateCardNum(playerName, handCardPerPerson, pathCardnum);
                    fileContent = fileContent.Replace(tempArray[0] + "*", "");
                    CmdWriteAdvBack(fileContent);
                    tempArray = handCardPerPerson.Split(' ');
                    sortCard(tempArray);
                }
                stageNow++;

                //File.WriteAllLines(pathPlayerStagebool, temp);            ////////////////
                CmdQuestWrite(pathstagebool, temp);

                //string[] resultArray = File.ReadAllLines(pathPlayerStagebool);
                string[] resultArray = temp;
                int count = 0;
                for (int i = 0; i < connect; i++)
                {
                    if (resultArray[i] == "havent")
                        count++;
                }
                if (count == 0)
                {
                    if (pass)
                        myMsg = "Congratulation, you pass the stage,choose for next stage, Press W to continue";
                    else
                        myMsg = "you did not pass this stage, Press W to continue";

                    print("this stage finsh");
                    /*string[] temp1 = File.ReadAllLines(pathPlayerStagebool);
                    for (int i = 0; i < connect; i++)
                    {
                        if (temp1[i] == "Passed")
                            temp1[i] = "havent";
                        print(temp1[i]);
                    }
                    File.WriteAllLines(pathPlayerStagebool, temp1);*/

                }
            }
            else
            {
                print("max stage");
                char c = name[6];
                int index = Int32.Parse(c.ToString()) - 1;
                if (playerAtk < Int32.Parse(sponsor[stageNow - 1]))
                {

                    temp[index] = "Failed";
                    //File.WriteAllLines(pathPlayerStagebool, temp);
                    CmdQuestWrite(pathstagebool, temp);

                    //string[] resultArray = File.ReadAllLines(pathPlayerStagebool);
                    string[] resultArray = temp;
                    int count = 0;
                    for (int i = 0; i < connect; i++)
                    {
                        if (resultArray[i] == "havent")
                            count++;
                    }
                    if (count == 0)
                    {
                        print("all quest finish");
                        myMsg = "You did not pass this stage, this is the end, press R to continue...";
                    }
                    else
                    {
                        myMsg = "You did not pass this stage, this is the end";
                    }

                }
                else
                {
                    temp[index] = "Win";
                    //File.WriteAllLines(pathPlayerStagebool, temp);//
                    CmdQuestWrite(pathstagebool, temp);
                    shieldsNum += maxStage;
                    CmdupdateShieldNum(playerName, shieldsNum);

                    //string[] resultArray = File.ReadAllLines(pathPlayerStagebool);
                    string[] resultArray = temp;
                    int count = 0;
                    for (int i = 0; i < connect; i++)
                    {
                        if (resultArray[i] == "havent")
                            count++;
                    }
                    if (count == 0)
                    {
                        print("all quest finish");
                        myMsg = "Congratulation, you pass the quest, gain shields, press R to continue...";
                    }
                    else
                    {
                        myMsg = "Congratulation, you pass the quest, gain shields";
                    }
                }
                ///gain shield          
            }
        }
    }

    [Command]
    void CmdQuestWrite(string path, string[] content)
    {
        File.WriteAllLines(path, content);
    }

    [Command]
    void CmdAskJoin()
    {
        RpcAskJoin();
    }

    [ClientRpc]
    void RpcAskJoin()
    {
        //print(playerName);
        askingJoin = true;
        if (!isLocalPlayer)
        {
            //return;
        }
        if (playerName == sponsorNow)
        {
            myMsg = "Wait other players to make decision";
        }
        else
        {
            myMsg = "Would you like to join this Quest? Y/N";
        }
    }










 



    [Command]
    void CmdSE_PT()
    {
        advString = File.ReadAllText(pathAdvent);
        Debug.Log("CmdSE_PT--- " + playerName + " in here, and string is: " + advString);
        RpcSE_PT(pathCardnum, advString);
    }

    [ClientRpc]
    void RpcSE_PT(string path, string fileContent)
    {
        if (!isLocalPlayer) return;
        Debug.Log("RpcSE_PT--- " + playerName + " in here.");
        /*
        string[] tempArray = fileContent.Split('*');
        //print("before draw a new card" + handCardPerPerson);
        for (int i = 0; i < 2; i++)
        {
            handCardPerPerson = handCardPerPerson + " " + tempArray[i];
            fileContent = fileContent.Replace(tempArray[i] + "*", "");
            //print("after draw" + playerName + handCardPerPerson);          
        }
        CmdupdateCardNum(playerName, handCardPerPerson, path);
        CmdWriteAdvBack(fileContent);
        tempArray = handCardPerPerson.Split(' ');
        sortCard(tempArray);
        */
    }


    [Command]
    void CmdStoryEvent()
    {
        if (storyNow.Contains("SE_PL"))
        {
            RpcLoseTwoShields();
        }
        else if (storyNow.Contains("SE_KR"))
        {

        }
        else if (storyNow.Contains("SE_QF"))
        {

        }
        else if (storyNow.Contains("SE_KC"))
        {

        }
        else if (storyNow.Contains("SE_PT"))
        {
            CmdSE_PT();
        }
        else if (storyNow.Contains("SE_CD"))
        {

        }
        else if (storyNow.Contains("SE_PO"))
        {

        }
        else if (storyNow.Contains("SE_CC"))
        {
			string[] temp = {"0","0","0","0"};
			File.WriteAllLines (pathAlly,temp);
			print ("all allies in play have been discarded");
        }
        RpcStoryEvent();
    }

    [ClientRpc]
    void RpcStoryEvent()
    {
        myMsg = "It's an spcial event.";
    }

    [ClientRpc]
    void RpcLoseTwoShields()
    {
        if (!isLocalPlayer)
            return;
        print("CmdStoryEvent--- " + playerName + "  shieldsNum: " + shieldsNum);
        if (shieldsNum >= 2)
        {
            shieldsNum -= 2;
            CmdupdateShieldNum(playerName, shieldsNum);
        }
        print("CmdStoryEvent--- " + playerName + "  shieldsNum: " + shieldsNum);
    }
    [Client]
    void ClearMyMsg()
    {
        GameObject msg = GameObject.Find("MsgText");
        msg.GetComponent<Text>().text = "";

    }






    /*[Command]
    void CmdNoOnAskSponsor()
    {
        askCount++;
        CmdAskIfSponsor();
    }
    */
    [Command]
    void CmdYesOnAskSponsor()
    {
        //round +1
        //reset askCount for use in next time
        //Change msg
        roundNow++;
        //askCount = 1;

        RpcYesOnAskSponsor(sponsorNow, maxStage);
        File.AppendAllText(pathLog, playerName + " accept to sponsor the story" + Environment.NewLine);

    }


    [ClientRpc]
    void RpcYesOnAskSponsor(string name, int max)
    {

        print(playerName + " " + name);
        if (playerName == name /*&& isLocalPlayer*/)
        {
            myMsg = "Please place cards on stages, press C to play it on each stage";
            //END AT HERE...
        }
        else if (!isLocalPlayer)
        {
            myMsg = "Wait player sponsor cards...";
        }
    }

    [Command]
    void CmdChooseTournament()
    {
        RpcPlayerGetCardChosenTournament(pathTournATK);
        File.AppendAllText(pathLog, playerName + " choose card in Tournment " + Environment.NewLine);
    }

    [ClientRpc]
    void RpcPlayerGetCardChosenTournament(string path)
    {

        if (!isLocalPlayer)
            return;

        List<GameObject> playerCard = new List<GameObject>();
        string[] handCard = handCardPerPerson.Split(' ');

        List<string> chosedCard = new List<string>();
        tournPlayerCard = null;
        for (int i = 0; i < handCard.Length; i++)
        {
            playerCard.Add(GameObject.Find(handCard[i]));
        }

        for (int i = 0; i < playerCard.Count; i++)
        {
            if (playerCard[i].GetComponent<TouchCard>().isChosen())
            {
                chosedCard.Add(playerCard[i].name);
                if (playerCard[i].name.Contains("Foes") || playerCard[i].name.Contains("Test"))
                {
                    print("You can only play Ally, Amour and Weapon card.");
                    return;
                }
            }
        }
        if (chosedCard.Count == 0)
        {
            print("choosed 0 card");
        }
        for (int i = 0; i < chosedCard.Count; i++)
        {
            if (i == chosedCard.Count - 1)
            {
                tournPlayerCard += chosedCard[i];
            }
            else
            {
                tournPlayerCard += chosedCard[i] + "*";
            }
        }
        print("player chosed " + tournPlayerCard);

        string[] chosedArray = tournPlayerCard.Split('*');

        string[] tempA = handCardPerPerson.Split(' ');
        for (int i = 0; i < chosedArray.Length; i++)
        {
            GameObject go = GameObject.Find(chosedArray[i]);
            if (go.name == tempA[tempA.Length - 1])
                handCardPerPerson = handCardPerPerson.Replace(go.name, "");
            else
                handCardPerPerson = handCardPerPerson.Replace(go.name + " ", "");
            Destroy(go);
        }
        if (handCardPerPerson[handCardPerPerson.Length - 1] == ' ')
        {
            handCardPerPerson = handCardPerPerson.Remove(handCardPerPerson.Length - 1);
        }

        string[] remainingHandCard;
        remainingHandCard = handCardPerPerson.Split(' ');
        CmdupdateCardNum(playerName, handCardPerPerson, path);
        sortCard(remainingHandCard);
        CmdCompareTournamentATK(playerName, tournPlayerCard);
    }

    [Command]
    void CmdCompareTournamentATK(string name, string cards)
    {
        if (playerName != name) return;
        string[] temp = { "0", "0", "0", "0" };
        //pathTournATK
        if (!File.Exists(pathTournATK))
        {
            File.WriteAllLines(pathTournATK, temp);
        }
        else
        {
            temp = File.ReadAllLines(pathTournATK);
        }
        int playerAtk = 0;

        if (playerRank == Rank.SQUIRE)
            playerAtk += 5;
        else if (playerRank == Rank.KNIGHT)
            playerAtk += 10;
        else if (playerRank == Rank.CHAMPIONKNIGHT)
            playerAtk += 15;
        else playerAtk += 100;
        //////////////////////////////////////////////////////////////




        if (cards != null)
        {
            string[] playerCards = cards.Split('*');
            for (int i = 0; i < playerCards.Length; i++)
            {
                if (playerCards[i].Contains("Weap"))
                {
                    WeaponCard aW = (WeaponCard)adventureDeck.Find(x => x.getName() == playerCards[i]);
                    playerAtk += aW.getAtk();
                }
                if (playerCards[i].Contains("Ally"))
                {
                    AllyCard aAlly = (AllyCard)adventureDeck.Find(x => x.getName() == playerCards[i]);
                    playerAtk += aAlly.getAtk();
                }
                if (playerCards[i].Contains("Amou"))
                {
                    AmourCard aAmou = (AmourCard)adventureDeck.Find(x => x.getName() == playerCards[i]);
                    playerAtk += aAmou.getAtk();
                }
            }
        }
        print(playerName + "'s atk is " + playerAtk);
        File.AppendAllText(pathLog, playerName + " atk in this tournment is " + playerAtk + Environment.NewLine);

        char c = name[6];
        int index = Int32.Parse(c.ToString()) - 1;
        temp[index] = playerName + playerAtk.ToString();

        File.WriteAllLines(pathTournATK, temp);
        string[] temp1 = File.ReadAllLines(pathTournATK);
        List<String> playerNameJoinTour = new List<String>();
        List<int> playerAtkJoinTour = new List<int>();
        for (int i = 0; i < temp1.Length; i++)
        {
            if (temp1[i] != "0")
            {
                playerNameJoinTour.Add(temp1[i].Substring(0, 7));
            }
        }
        for (int i = 0; i < temp1.Length; i++)
        {
            if (temp1[i] != "0")
            {
                int l = temp1[i].Length;
                playerAtkJoinTour.Add(Int32.Parse(temp1[i].Substring(7, l - 7)));
            }
        }
        //finish write into files, now starting to compare
        //if now is the last player

        string[] a = File.ReadAllLines(pathTournATK);
        int count = 0;
        bool ready = false;
        for (int i = 0; i < NetworkServer.connections.Count; i++)
        {
            if (a[i] == "0")
                count++;
        }
        if (count == 0)
            ready = true;


        if (ready)
        {
            int d = 0;
            int addShields = 0;
            //string[] temp1 = File.ReadAllLines (pathTournATK);
            string[] b = File.ReadAllLines(pathTournATK);
            for (int i = 0; i < temp1.Length; i++)
            {
                if (b[i] != "0")
                {
                    d++;
                }
                else { print(i + b[i]); }
            }
            print("d" + d);
            int max = playerAtkJoinTour.Max();
            int pos = playerAtkJoinTour.IndexOf(max);
            string pName = playerNameJoinTour[pos];
            char winnerNum = pName[6];
            int winnerNumInt = (Int32.Parse(winnerNum.ToString()) - 1);
            //打出一行字playerxxx赢得了这次的tournament
            print(pName + " wins this tournament.");
            File.AppendAllText(pathLog, name + " is the winner in the Tournment, Environment.NewLine");
            //给赢家发盾牌

            if (storyNow.Contains("ST_AY"))
            {
                //加玩家数的盾牌
                addShields += d;
            }
            else if (storyNow.Contains("ST_AT"))
            {
                //+1
                addShields += d;
                addShields += 1;
            }
            else if (storyNow.Contains("ST_AO"))
            {
                //+2
                addShields += d;
                addShields += 2;
            }
            else if (storyNow.Contains("ST_AC"))
            {
                //+3
                addShields += d;
                addShields += 3;
            }
            //init the file to all 0
            print(pName + "/" + playerName + addShields);
            RpcTourAddShieldToWinner(pName, addShields);

            

        }
    }

    [ClientRpc]
    void RpcTourAddShieldToWinner(string name, int shields)
    {
        if (playerName == name)
        {
            print("------" + shieldsNum + " " + shields);
            shieldsNum += shields;
            CmdupdateShieldNum(name, shieldsNum);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Finish");
        foreach (GameObject go in gos)
            go.GetComponent<playerController>().myMsg = name + " is the winner in the Tournment, he gains " + shields + " shields";
        // 
    }

    [Command]
    void CmdChoose()
    {
        if (playerName != sponsorNow) return;
        RpcGetCardChosen();
        File.AppendAllText(pathLog, "sponsor choose card" + Environment.NewLine);
    }

    [ClientRpc]
    void RpcGetCardChosen()
    {
        if (!isLocalPlayer)
            return;
        print(playerName + handCardPerPerson);
        List<GameObject> sponsorCard = new List<GameObject>();
        string[] handCard = handCardPerPerson.Split(' ');

        List<string> chosedCard = new List<string>();
        perStageSponsorCard = null;
        for (int i = 0; i < handCard.Length; i++)
        {
            sponsorCard.Add(GameObject.Find(handCard[i]));
            //print ("handCard " + handCard[i]);
        }

        for (int i = 0; i < sponsorCard.Count; i++)
        {
            if (sponsorCard[i].GetComponent<TouchCard>().isChosen())
            {
                //Card aCard = adventureDeck.Find(x => x.getName() == sponsorCard[i].name);
                //if (aCard != null)
                chosedCard.Add(sponsorCard[i].name);
                //else print("card not find "+ aCard.getName());
            }
        }
        if (chosedCard.Count == 0)
        {
            print("should put at least one card in sponsor stage");
            return;
        }
        for (int i = 0; i < chosedCard.Count; i++)
        {
            if (i == chosedCard.Count - 1)
                perStageSponsorCard += chosedCard[i]; //
            else
                perStageSponsorCard += chosedCard[i] + "*"; //
        }
        print("sposnor chosed " + perStageSponsorCard);
        CmdAddsponsorToStage(perStageSponsorCard);
    }

    [Command]
    void CmdPlayerChoose()
    {
        RpcPlayerGetCardChosen(pathCardnum);
        File.AppendAllText(pathLog, playerName + " choose card foe quest " + Environment.NewLine);
    }

    [ClientRpc]
    void RpcPlayerGetCardChosen(string path)
    {
        if (!isLocalPlayer)
            return;
        //print(handCardPerPerson + "|");
        List<GameObject> playerCard = new List<GameObject>();
        string[] handCard = handCardPerPerson.Split(' ');

        List<string> chosedCard = new List<string>();
        perStagePlayerCard = null;
        for (int i = 0; i < handCard.Length; i++)
        {
            playerCard.Add(GameObject.Find(handCard[i]));
        }

        for (int i = 0; i < playerCard.Count; i++)
        {
            if (playerCard[i].GetComponent<TouchCard>().isChosen())
            {
                chosedCard.Add(playerCard[i].name);
                if (playerCard[i].name.Contains("Foes") || playerCard[i].name.Contains("Test"))
                {
                    print("dont play foe or test");
                    return;
                }
            }
        }
        if (chosedCard.Count == 0)
        {
            print("choosed 0 card");
        }
        for (int i = 0; i < chosedCard.Count; i++)
        {
            if (i == chosedCard.Count - 1)
            {
                perStagePlayerCard += chosedCard[i];
            }
            else
            {
                perStagePlayerCard += chosedCard[i] + "*";
            }
        }
        print("player chosed " + perStagePlayerCard);
        //CmdAddPlayerChosedToStage(perStagePlayerCard);

        string[] chosenArray = perStagePlayerCard.Split('*');   ////

        string[] tempA = handCardPerPerson.Split(' ');
        for (int i = 0; i < chosenArray.Length; i++)
        {
            GameObject go = GameObject.Find(chosenArray[i]);
            if (go.name == tempA[tempA.Length - 1])
                handCardPerPerson = handCardPerPerson.Replace(go.name, "");
            else
                handCardPerPerson = handCardPerPerson.Replace(go.name + " ", "");
            Destroy(go);
        }
        if (handCardPerPerson[handCardPerPerson.Length - 1] == ' ')
        {
            handCardPerPerson = handCardPerPerson.Remove(handCardPerPerson.Length - 1);
        }
        //print(handCardPerPerson+"*");
        string[] remainingHandCard;
        remainingHandCard = handCardPerPerson.Split(' ');
        CmdupdateCardNum(playerName, handCardPerPerson, path);
        sortCard(remainingHandCard);
        CmdCompareATK(playerName, perStagePlayerCard, stageNow);

    }

    [Command]
    void CmdAddsponsorToStage(string chosedCard)
    {
        List<Card> stage = new List<Card>();
        string[] chosenArray = chosedCard.Split('*');
        for (int i = 0; i < chosenArray.Length; i++)
        {
            Card aCard = adventureDeck.Find(x => x.getName() == chosenArray[i]);
            stage.Add(aCard);
        }
        if (checkValidPerStage(stage) == true)
        {

            for (int i = 0; i < stage.Count; i++)
            {
                sponsorUsedCard.Add(stage[i].getName());
            }
            RpcSponsorSortCard(chosenArray, stageNum, pathCardnum);
            File.AppendAllText(pathLog, "sponsor choose card " + chosedCard + Environment.NewLine);
        }
        else
        {
            print("false, error input to stage");
            return;
        }

        if (stageNum == (maxStage - 1))
        {
            print("sponsor set up finish");
            RpcAskIfJoin();
        }
        else
        {
            stageNum++;
        }
    }

    [ClientRpc]
    void RpcSponsorSortCard(string[] chosenArray, int stage, string path)
    {
        if (!isLocalPlayer) return;
        string[] tempA = handCardPerPerson.Split(' ');

        for (int i = 0; i < chosenArray.Length; i++)
        {
            GameObject go = GameObject.Find(chosenArray[i]);
            if (go.name == tempA[tempA.Length - 1])
                handCardPerPerson = handCardPerPerson.Replace(go.name, "");
            else
                handCardPerPerson = handCardPerPerson.Replace(go.name + " ", "");
            Vector3 sponsorCardNewPos = new Vector3((2.67f + 2 * stage), (1.2f - 0.5f * i), (i * 1.1f));
            iTween.MoveTo(go, sponsorCardNewPos, 0.5f);
        }
        if (handCardPerPerson[handCardPerPerson.Length - 1] == ' ')
        {
            handCardPerPerson = handCardPerPerson.Remove(handCardPerPerson.Length - 1);
        }
        //print(handCardPerPerson+"*");
        CmdupdateCardNum(playerName, handCardPerPerson, path);
        string[] remainingHandCard;
        remainingHandCard = handCardPerPerson.Split(' ');
        sortCard(remainingHandCard);

    }


    [ClientRpc]
    void RpcAskIfJoin()
    {
        Debug.Log("RpcAskIfJoin--- " + playerName + "in there");
        askingJoin = true;
        if (!isLocalPlayer)
            return;
        if (playerName == sponsorNow)
        {
            myMsg = "Please press 'S' to start Quest...";
        }
    }

    bool checkValidPerStage(List<Card> alist)
    {
        int foeCount = 0;
        bool test = false;
        List<Card> theList = alist;
        int ATK = 0;
        QuestCard questNow = questDeck.Find(x => x.getName() == storyNow);
        if (questNow == null) print("quest card not found " + storyNow);
        setUpSpecialATK(questNow, theList);

        if (theList.Count == 0)
        {
            print("you should put at least one card in per stage");
            return false;
        }
        for (int i = 0; i < theList.Count; i++)
        {
            if (theList[i].getKind() == Kind.TEST)
            {
                test = true;
                if (!File.Exists(pathSponsorAtk))
                {
                    ATK = 0;
                }
                else
                {
                    string temp = File.ReadAllText(pathSponsorAtk);
                    if (temp[temp.Length - 1] == '*')
                        temp = temp.Remove(temp.Length - 1);
                    string[] tempA = temp.Split('*');
                    ATK = Int32.Parse(tempA[tempA.Length - 1]) + 1;
                }
                if (theList.Count != 1)
                {
                    print("stage is not valid because of the test require");
                    theList.Clear();
                    return false;
                }
            }
            if (alist[i].getKind() == Kind.FOE)
            {
                foeCount += 1;
                FoeCard aFoe = (FoeCard)alist[i];
                ATK += aFoe.getAtk();
            }
            if (alist[i].getKind() == Kind.WEAPON)
            {
                WeaponCard aWeapon = (WeaponCard)alist[i];
                ATK += aWeapon.getAtk();
            }
        }
        if (foeCount != 1 && test != true)
        {
            print("stage  is not valid duo to foeCard num");
            alist.Clear();
            return false;
        }

        if (!File.Exists(pathSponsorAtk))
        {
            File.WriteAllText(pathSponsorAtk, ATK.ToString() + "*");
            print("first stage " + ATK);
        }
        else
        {
            int previous;
            string temp = File.ReadAllText(pathSponsorAtk);
            if (temp[temp.Length - 1] == '*')
                temp = temp.Remove(temp.Length - 1);
            string[] tempA = temp.Split('*');
            previous = Int32.Parse(tempA[tempA.Length - 1]);
            if (ATK <= previous)
            {
                print("atk is not valid " + ATK);
                return false;
            }
            else
            {
                File.AppendAllText(pathSponsorAtk, ATK.ToString() + "*");
                print("valid " + ATK);
            }

        }


        //CmdAddSponsor(sponsorStageATK);
        return true;
    }



    void setUpSpecialATK(QuestCard aQuestCard, List<Card> thelist)
    {
        List<Card> alist = thelist;
        if (alist[0] == null)
            return;
        for (int i = 0; i < alist.Count; i++)
        {
            if (alist[i].getKind() == Kind.FOE)
            {
                FoeCard theFoe = (FoeCard)alist[i];
                theFoe.initialTheATK();
                if (aQuestCard.getName().Contains("JTTEF"))
                    if (theFoe.getName().Contains("Evil"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("RTSR"))
                    if (theFoe.getName().Contains("Saxons"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("BH"))
                    if (theFoe.getName().Contains("Boar"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("DTQH"))
                    theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("STD"))
                    if (theFoe.getName().Contains("Dragon"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("RTFM"))
                    if (theFoe.getName().Contains("Black"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("SFTHG"))
                    theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("TOTGK"))
                    if (theFoe.getName().Contains("Green"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
            }
            else
            {

            }
        }
    }
}
