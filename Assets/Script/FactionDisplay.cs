using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using TMPro;

public class FactionDisplay : MonoBehaviour
{
    public Faction currentFaction;

    public SpriteRenderer currentSprite;

    public TMP_Text currentNameText;
    public TMP_Text currentSloganText;
    public TMP_Text currentPointText;

    public int currentPoint;

    public FactionManager currentFactionManager;

    // Afichage du rang dans la compétition
    //public SpriteRenderer currentRank;
    //Temporaire en attendant les images
    public TMP_Text currentRankText;


    // Start is called before the first frame update
    void Start()
    {
        currentFactionManager = GameObject.FindWithTag("GameController").GetComponent<FactionManager>();

        currentNameText.text = currentFaction.name;
        currentSloganText.text = currentFaction.slogan;
        currentSprite.sprite = currentFaction.image;
        currentPoint = currentFactionManager.getFaction(currentFaction.name).point;
        currentPointText.text = currentPoint.ToString();
    }

    public void UpdatePoint(int newPoint)
    {
        currentPoint = newPoint;
        currentPointText.text = currentPoint.ToString();
    }

    public void UpdateRank(int newrank)
    {
        currentRankText.text = newrank.ToString();
    }

}
