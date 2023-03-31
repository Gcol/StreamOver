using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Xml.Serialization;
using System.IO;

using TMPro;

[System.Serializable]
public class AllFaction
{
    public List<FactionEntry> list = new List<FactionEntry>();
}

public class FactionEntry
{
    public string name;
    public int point;
}

public class FactionManager : MonoBehaviour
{

    List<FactionDisplay> allFactionPannel;
    List<FactionEntry> allCurrentFaction;
    public AllFaction allFaction;

    public Animator factionAnimator;
    public Image factionAnimationSprite;

    // Start is called before the first frame update
    void Awake()
    {
        allCurrentFaction = LoadFaction();
        allFactionPannel = new List<FactionDisplay>(Object.FindObjectsOfType<FactionDisplay>());
    }
    
    void Start()
    {
        SortRank();
    }

    void SortRank()
    {
        allFactionPannel.Sort((x, y) => y.currentPoint.CompareTo(x.currentPoint));

        int currentRank = 0;
        foreach (FactionDisplay tempFactionPannel in allFactionPannel)
        {
            currentRank += 1;
            //Debug.LogFormat("Current Rank : {0}, Point : {1}, Name {2}", currentRank, tempFactionPannel.currentPoint, tempFactionPannel.currentFaction.name);
            tempFactionPannel.UpdateRank(currentRank);
        }

    }

    public void SaveFaction()
    {
        allFaction.list = allCurrentFaction;
        XmlSerializer serializer = new XmlSerializer(typeof(AllFaction));
        FileStream stream = new FileStream(Application.persistentDataPath + "/Faction.xml", FileMode.Create);
        serializer.Serialize(stream, allFaction);
        stream.Close();
    }

    public void UpdateFaction(string entryName)
    {
        int gainPoint = 10;
        FactionEntry tempCurrentFaction = getFaction(entryName);
        FactionDisplay tempFactionPanel = allFactionPannel.Find((x) => x.currentFaction.name== entryName);
        tempCurrentFaction.point += gainPoint;

        StartCoroutine(CoRoutineAnimation(tempFactionPanel, tempCurrentFaction.point));
    }

    public FactionEntry getFaction(string entryName)
    {
        FactionEntry tempCurrentFaction = allCurrentFaction.Find((x) => x.name == entryName);

        if (tempCurrentFaction == null)
        {
            tempCurrentFaction = new FactionEntry { name = entryName, point = 0 };
            allCurrentFaction.Add(tempCurrentFaction);

        }
        return tempCurrentFaction;
    }

    public List<FactionEntry> LoadFaction()
    {
        if (File.Exists(Application.persistentDataPath + "/Faction.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AllFaction));
            FileStream stream = new FileStream(Application.persistentDataPath + "/Faction.xml", FileMode.Open);
            allFaction = serializer.Deserialize(stream) as AllFaction;
            stream.Close();
        }
        return allFaction.list;
    }

    IEnumerator CoRoutineAnimation(FactionDisplay tempFactionPanel, int newPoint)
    {
        factionAnimationSprite.sprite = tempFactionPanel.currentSprite.sprite;
        factionAnimator.Play("DropDown");
        yield return new WaitForSeconds(1f);
        tempFactionPanel.UpdatePoint(newPoint);
        SortRank();
        factionAnimator.Play("DropUp");
    }
}
