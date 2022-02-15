using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGame
{
    public List<int> npcHelpCounter = new List<int>();

    // [MF] still don't know if bool or list of strings would be better
    //public bool isOrryRecruited;
    //public bool isGaehlRecruited;
    //public bool isEmbreRecruited;
    public List<string> alliesRecruited = new List<string>();
    public int npcHelped;
    public int HP;

}
