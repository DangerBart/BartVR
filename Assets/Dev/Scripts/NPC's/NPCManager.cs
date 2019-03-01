using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class NPCManager : MonoBehaviour {

    public string NPCPrefabsPath = "NPCTEST";
    public GameObject CheckpointContainer;
    private Object[] npcModels;

    private int amount;
    private System.Random rand = new System.Random();

    //Node attributes
    private readonly Node[] nodeList = new Node[59];
    private readonly Node[] spawnList = new Node[46];

    // Use this for initialization
    void Start () {

        Setup();

        // Create suspect
        string suspectModelName = CreateSuspect();

        List<int> civilianModelIndexes = GetModelsIndexesWithDifferentId(suspectModelName);

        // Create all civilians
        for (int i = 0; i < amount; i++) {

            int index = rand.Next(0, civilianModelIndexes.Count);
            index = civilianModelIndexes[index];
            InstantiateNPC(index, Roles.Civilian);
        }
    }

    private void Setup()
    {
        npcModels = Resources.LoadAll(NPCPrefabsPath, typeof(GameObject));
        amount = Gamemanager.amountOfNpcsToSpawn;
        InitializeNodes();
    }


    private string CreateSuspect()
    {
        List<int> suspectModelsIndexes = new List<int>();
        string usedModelName = "";

        // Make list with available suspect models
        for (int i=0; i < npcModels.Length; i++)
        {
            if (npcModels[i].name.Contains("S"))
                suspectModelsIndexes.Add(i);
        }

        if(suspectModelsIndexes.Count == 0)
        {
            throw new System.Exception("No valid models for suspect were found");
        }

        int randomindex = suspectModelsIndexes[rand.Next(0, suspectModelsIndexes.Count)];
        GameObject suspect = GetNPCModelByIndex(randomindex);

        InstantiateNPC(suspect, Roles.Suspect);
        usedModelName = suspect.name;

        return usedModelName;
    }

    private List<int> GetModelsIndexesWithDifferentId(string modelName)
    {
        // List that contains all indexes of models which do not match with the suspect
        List<int> civilianModelIndexes = new List<int>();

        string[] recievedModelName = SplitStringInRightFormat(modelName);

        for (int i = 0; i < npcModels.Length; i++)
        {
            string[] foundModelName = SplitStringInRightFormat(npcModels[i].name);

            // Check if model does not have the same identifications
            if (!(recievedModelName[0] == foundModelName[0] && recievedModelName[1] == foundModelName[1] && recievedModelName[2] == foundModelName[2]))
            {
                civilianModelIndexes.Add(i);
            }
        }

        return civilianModelIndexes;
    }

    private string[] SplitStringInRightFormat(string text)
    {
        string[] returnString = text.Split('-');
        //Make sure the number is removed so it can easily be compared
        returnString[0] = Regex.Replace(returnString[0], @"[\d]", string.Empty);

        return returnString;
    }

    private GameObject GetNPCModelByIndex(int index)
    {
        return (GameObject)npcModels[index];
    }


    private void InstantiateNPC(int modelIndex, Roles role)
    {
        GameObject npc = GetNPCModelByIndex(modelIndex);
        InstantiateNPC(npc, role);
    }

    private void InstantiateNPC(GameObject npc, Roles role)
    {
        SetIDProperties(npc, role);
        npc = Instantiate(npc);

        NPCBehaviour npcBehaviour = npc.GetComponent<NPCBehaviour>();
        npcBehaviour.checkpointContainer = CheckpointContainer;
        npcBehaviour.SetSpawnList(spawnList);
        npc.SetActive(true);
        npc.transform.SetParent(this.transform);
    }

    private void SetIDProperties(GameObject NPC, Roles role)
    {
        Identification idModel = NPC.GetComponent<Identification>();

        //properties
        string gender;
        string topPiece;
        string bottomPiece;

        string[] proporties = (NPC.name.Split('-'));
        gender = proporties[0];
        topPiece = proporties[1];
        bottomPiece = proporties[2];

        // Set role
        idModel.role = role;

        // Set all ID variables
        SetRightGender(gender, idModel);
        idModel.topPiece = GetColorBasedOfString(topPiece);
        idModel.bottomPiece = GetColorBasedOfString(bottomPiece);
    }

    private void SetRightGender(string genderCode, Identification idModel)
    {
        switch (genderCode.ToUpper())
        {
            case "F":
                idModel.gender = Genders.Female;
                break;
            default:
                idModel.gender = Genders.Male;
                break;
        }
    }

    private Colors GetColorBasedOfString(string colorCode)
    {
        Colors color;
        switch (colorCode.ToUpper())
        {

            case "B":
                color = Colors.BLack;
                break;
            case "G":
                color = Colors.Green;
                break;
            case "P":
                color = Colors.Pink;
                break;
            case "Y":
                color = Colors.Yellow;
                break;
            case "BL":
                color = Colors.Blue;
                break;
            case "BR":
                color = Colors.Brown;
                break;
            case "GR":
                color = Colors.Grey;
                break;
            case "PU":
                color = Colors.Purple;
                break;
            default:
                color = Colors.White;
                break;
        }

        return color;
    }

    private Transform GetCheckpointChild(int index) {
        return CheckpointContainer.gameObject.transform.GetChild(index);
    }


    private void InitializeNodes() {
        /*
        so the GameObject named Checkpoint (3) is equal to node3 and GetCheckpointChild(3)
        List goes by checkpoint index number in CheckpointContainer parent, 
        */
        Node node0 = new Node(GetCheckpointChild(0));
        Node node1 = new Node(GetCheckpointChild(1));
        Node node2 = new Node(GetCheckpointChild(2));
        Node node3 = new Node(GetCheckpointChild(3));
        Node node4 = new Node(GetCheckpointChild(4));
        Node node5 = new Node(GetCheckpointChild(5));
        Node node6 = new Node(GetCheckpointChild(6));
        Node node7 = new Node(GetCheckpointChild(7));
        Node node8 = new Node(GetCheckpointChild(8));
        Node node9 = new Node(GetCheckpointChild(9));
        Node node10 = new Node(GetCheckpointChild(10));
        Node node11 = new Node(GetCheckpointChild(11));
        Node node12 = new Node(GetCheckpointChild(12));
        Node node13 = new Node(GetCheckpointChild(13));
        Node node14 = new Node(GetCheckpointChild(14));
        Node node15 = new Node(GetCheckpointChild(15));
        Node node16 = new Node(GetCheckpointChild(16));
        Node node17 = new Node(GetCheckpointChild(17));
        Node node18 = new Node(GetCheckpointChild(18));
        Node node19 = new Node(GetCheckpointChild(19));
        Node node20 = new Node(GetCheckpointChild(20));
        Node node21 = new Node(GetCheckpointChild(21));
        Node node22 = new Node(GetCheckpointChild(22));
        Node node23 = new Node(GetCheckpointChild(23));
        Node node24 = new Node(GetCheckpointChild(24));
        Node node25 = new Node(GetCheckpointChild(25));
        Node node26 = new Node(GetCheckpointChild(26));
        Node node27 = new Node(GetCheckpointChild(27));
        Node node28 = new Node(GetCheckpointChild(28));
        Node node29 = new Node(GetCheckpointChild(29));
        Node node30 = new Node(GetCheckpointChild(30));
        Node node31 = new Node(GetCheckpointChild(31));
        Node node32 = new Node(GetCheckpointChild(32));
        Node node33 = new Node(GetCheckpointChild(33));
        Node node34 = new Node(GetCheckpointChild(34));
        Node node35 = new Node(GetCheckpointChild(35));
        Node node36 = new Node(GetCheckpointChild(36));
        Node node37 = new Node(GetCheckpointChild(37));
        Node node38 = new Node(GetCheckpointChild(38));
        Node node39 = new Node(GetCheckpointChild(39));
        Node node40 = new Node(GetCheckpointChild(40));
        Node node41 = new Node(GetCheckpointChild(41));
        Node node42 = new Node(GetCheckpointChild(42));
        Node node43 = new Node(GetCheckpointChild(43));
        Node node44 = new Node(GetCheckpointChild(44));
        Node node45 = new Node(GetCheckpointChild(45));
        Node node46 = new Node(GetCheckpointChild(46));
        Node node47 = new Node(GetCheckpointChild(47));
        Node node48 = new Node(GetCheckpointChild(48));
        Node node49 = new Node(GetCheckpointChild(49));
        Node node50 = new Node(GetCheckpointChild(50));
        Node node51 = new Node(GetCheckpointChild(51));
        Node node52 = new Node(GetCheckpointChild(52));
        Node node53 = new Node(GetCheckpointChild(53));
        Node node54 = new Node(GetCheckpointChild(54));
        Node node55 = new Node(GetCheckpointChild(55));
        Node node56 = new Node(GetCheckpointChild(56));
        Node node57 = new Node(GetCheckpointChild(57));
        Node node58 = new Node(GetCheckpointChild(58));

        node0.SetOptions(new Node[] { node1, node58 });
        node1.SetOptions(new Node[] { node0, node2, node52, node54, node55 });
        node2.SetOptions(new Node[] { node1, node3 });
        node3.SetOptions(new Node[] { node2, node4, node5 });
        node4.SetOptions(new Node[] { node3, node6 });
        node5.SetOptions(new Node[] { node3, node54, node20, node22 });
        node6.SetOptions(new Node[] { node4, node7, node16 });
        node7.SetOptions(new Node[] { node8, node6, node10 });
        node8.SetOptions(new Node[] { node7, node9 });
        node9.SetOptions(new Node[] { node8 });
        node10.SetOptions(new Node[] { node7, node11, node12 });
        node11.SetOptions(new Node[] { node10, node24 });
        node12.SetOptions(new Node[] { node10, node13, node15 });
        node13.SetOptions(new Node[] { node12 });
        node14.SetOptions(new Node[] { node15, node21, node20 });
        node15.SetOptions(new Node[] { node12, node14, node16 });
        node16.SetOptions(new Node[] { node15, node17, node19 });
        node17.SetOptions(new Node[] { node16, node18 });
        node18.SetOptions(new Node[] { node17, node19, node20 });
        node19.SetOptions(new Node[] { node16, node18 });
        node20.SetOptions(new Node[] { node14, node18, node22, node5, node54 });
        node21.SetOptions(new Node[] { node14, node22, node23, node24 });
        node22.SetOptions(new Node[] { node20, node21, node5 });
        node23.SetOptions(new Node[] { node21, node29, node27 });
        node24.SetOptions(new Node[] { node11, node21, node25 });
        node25.SetOptions(new Node[] { node24, node26, node27 });
        node26.SetOptions(new Node[] { node25, node30, node31 });
        node27.SetOptions(new Node[] { node23, node25, node28 });
        node28.SetOptions(new Node[] { node27, node29, node50 });
        node29.SetOptions(new Node[] { node23, node28 });
        node30.SetOptions(new Node[] { node26, node40, node38 });
        node31.SetOptions(new Node[] { node26, node35, node32 });
        node32.SetOptions(new Node[] { node33, node31 });
        node33.SetOptions(new Node[] { node32, node34 });
        node34.SetOptions(new Node[] { node33 });
        node35.SetOptions(new Node[] { node31, node36 });
        node36.SetOptions(new Node[] { node35, node37 });
        node37.SetOptions(new Node[] { node36, node38, node44 });
        node38.SetOptions(new Node[] { node30, node37, node39 });
        node39.SetOptions(new Node[] { node38, node40, node43 });
        node40.SetOptions(new Node[] { node30, node39, node41 });
        node41.SetOptions(new Node[] { node40, node42, node48 });
        node42.SetOptions(new Node[] { node41, node43, node45 });
        node43.SetOptions(new Node[] { node39, node42, node44 });
        node44.SetOptions(new Node[] { node37, node43, node45 });
        node45.SetOptions(new Node[] { node44, node42, node46 });
        node46.SetOptions(new Node[] { node45, node47, node48 });
        node47.SetOptions(new Node[] { node46, node51, node57 });
        node48.SetOptions(new Node[] { node41, node46, node49 });
        node49.SetOptions(new Node[] { node48, node50, node51 });
        node50.SetOptions(new Node[] { node28, node54, node53 });
        node51.SetOptions(new Node[] { node49, node47, node52 });
        node52.SetOptions(new Node[] { node53, node1 });
        node53.SetOptions(new Node[] { node52, node54, node50 });
        node54.SetOptions(new Node[] { node53, node50, node5, node1 });
        node55.SetOptions(new Node[] { node56 });
        node56.SetOptions(new Node[] { node57 });
        node57.SetOptions(new Node[] { node47 });
        node58.SetOptions(new Node[] { node0, node1, node2 });

        nodeList[0] = node0;
        nodeList[1] = node1;
        nodeList[2] = node2;
        nodeList[3] = node3;
        nodeList[4] = node4;
        nodeList[5] = node5;
        nodeList[6] = node6;
        nodeList[7] = node7;
        nodeList[8] = node8;
        nodeList[9] = node9;
        nodeList[10] = node10;
        nodeList[11] = node11;
        nodeList[12] = node12;
        nodeList[13] = node13;
        nodeList[14] = node14;
        nodeList[15] = node15;
        nodeList[16] = node16;
        nodeList[17] = node17;
        nodeList[18] = node18;
        nodeList[19] = node19;
        nodeList[20] = node20;
        nodeList[21] = node21;
        nodeList[22] = node22;
        nodeList[23] = node23;
        nodeList[24] = node24;
        nodeList[25] = node25;
        nodeList[26] = node26;
        nodeList[27] = node27;
        nodeList[28] = node28;
        nodeList[29] = node29;
        nodeList[30] = node30;
        nodeList[31] = node31;
        nodeList[32] = node32;
        nodeList[33] = node33;
        nodeList[34] = node34;
        nodeList[35] = node35;
        nodeList[36] = node36;
        nodeList[37] = node37;
        nodeList[38] = node38;
        nodeList[39] = node39;
        nodeList[40] = node40;
        nodeList[41] = node41;
        nodeList[42] = node42;
        nodeList[42] = node42;
        nodeList[43] = node43;
        nodeList[44] = node44;
        nodeList[45] = node45;
        nodeList[46] = node46;
        nodeList[47] = node47;
        nodeList[48] = node48;
        nodeList[49] = node49;
        nodeList[50] = node50;
        nodeList[51] = node51;
        nodeList[52] = node52;
        nodeList[53] = node53;
        nodeList[54] = node54;
        nodeList[55] = node55;
        nodeList[56] = node56;
        nodeList[57] = node57;
        nodeList[58] = node58;

        spawnList[0] = node0;
        spawnList[1] = node1;
        spawnList[2] = node2;
        spawnList[3] = node3;
        spawnList[4] = node4;
        spawnList[5] = node5;
        spawnList[6] = node7;
        spawnList[7] = node10;
        spawnList[8] = node11;
        spawnList[9] = node14;
        spawnList[10] = node17;
        spawnList[11] = node20;
        spawnList[12] = node22;
        spawnList[13] = node24;
        spawnList[14] = node25;
        spawnList[15] = node26;
        spawnList[16] = node27;
        spawnList[17] = node28;
        spawnList[18] = node29;
        spawnList[19] = node30;
        spawnList[20] = node31;
        spawnList[21] = node32;
        spawnList[22] = node33;
        spawnList[23] = node34;
        spawnList[24] = node35;
        spawnList[25] = node37;
        spawnList[26] = node38;
        spawnList[27] = node39;
        spawnList[28] = node40;
        spawnList[29] = node41;
        spawnList[30] = node42;
        spawnList[31] = node42;
        spawnList[32] = node43;
        spawnList[33] = node44;
        spawnList[34] = node45;
        spawnList[35] = node46;
        spawnList[36] = node47;
        spawnList[37] = node50;
        spawnList[38] = node51;
        spawnList[39] = node52;
        spawnList[40] = node53;
        spawnList[41] = node54;
        spawnList[42] = node55;
        spawnList[43] = node56;
        spawnList[44] = node57;
        spawnList[45] = node58;
        }
    }
