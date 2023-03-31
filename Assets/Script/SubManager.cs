using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;
using System.IO;

using System; 

[System.Serializable]
public class AllSub
{
    public List<SubEntry> list = new List<SubEntry>();
}

public class SubEntry
{
    public string name;
    public int point;
    public string faction;
    public DateTime lastJoinDate;
    public DateTime toltalTimeSub;
}

public class SubManager : MonoBehaviour
{
    List<SubEntry> currentSub;
    public AllSub allSub;

    FactionManager currentFaction;

    // Start is called before the first frame update
    void Awake()
    {
        currentSub = LoadSub();
        currentFaction = GetComponent<FactionManager>();
    }

    public void SaveSub()
    {
        allSub.list = currentSub;
        XmlSerializer serializer = new XmlSerializer(typeof(AllSub));
        FileStream stream = new FileStream(Application.persistentDataPath + "/Sub.xml", FileMode.Create);
        serializer.Serialize(stream, allSub);
        stream.Close();
        currentFaction.SaveFaction();
    }

    public void UpdateSub(string entryName)
    {
        int gainPoint = 10;
        Debug.Log(currentSub);
        SubEntry tempCurrentSub = currentSub.Find((x) => x.name == entryName);

        if (tempCurrentSub == null)
        {
            tempCurrentSub = new SubEntry { name = entryName, point = 0, faction = "Teauorent", lastJoinDate = DateTime.Now, toltalTimeSub = new DateTime(0)};
            currentSub.Add(tempCurrentSub);

        }
        currentFaction.UpdateFaction(tempCurrentSub.faction);

        tempCurrentSub.point += gainPoint;
        SaveSub();
    }

    public List<SubEntry> LoadSub()
    {
        if (File.Exists(Application.persistentDataPath + "/Sub.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AllSub));
            FileStream stream = new FileStream(Application.persistentDataPath + "/Sub.xml", FileMode.Open);
            allSub = serializer.Deserialize(stream) as AllSub;
            stream.Close();
        }
        return allSub.list;
    }
}
