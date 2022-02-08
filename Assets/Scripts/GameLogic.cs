using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public ItemList itemList;
    public Transform ClosingAnimation;
    public GameObject Wheel, WheelInside;
    private Dictionary<string, int> Occurences = new Dictionary<string, int>();
    public Text EndingText;
    
    private Dictionary<string, float> SectorData = new Dictionary<string, float>(){
        {"Life 30 min", 22.5f},
        {"Brush 3X", 67.5f},
        {"Gems 35", 112.5f},
        {"Hammer 3X", 157.5f},
        {"Coins 750", 202.5f},
        {"Brush 1x", 247.5f},
        {"Gems 75", 292.5f},
        {"Hammer 1X", 337.5f},
    };

    void Start()
    {
        itemList.AddEntry("Life 30 min", 20);
        itemList.AddEntry("Brush 3X", 10);
        itemList.AddEntry("Gems 35", 10);
        itemList.AddEntry("Hammer 3X", 10);
        itemList.AddEntry("Coins 750", 5);
        itemList.AddEntry("Brush 1x", 20);
        itemList.AddEntry("Gems 75", 5);
        itemList.AddEntry("Hammer 1X", 20);
    }

    public void thousandSpinEmulation()
    {
        Occurences.Clear();
        for (int i=0;i<=1000;i++)
        {
            string itemName = itemList.GetItem();
            bool keyExists = Occurences.ContainsKey(itemName);
            if (keyExists)
            {
                Occurences[itemName] = Occurences[itemName]+1;
            }
            else
            {
                Occurences.Add(itemName, 0);
            }
        }
        foreach (KeyValuePair<string, int> pair in Occurences)
        {
            print(pair.Key.ToString() + "  -  " + pair.Value.ToString());
        }
    }

    public void checkEachSector(string sectorName)
    {
        if(sectorName =="")
        {
            string itemName = itemList.GetItem();
            float rotationValue = SectorData[itemName];
            StartCoroutine(Rotate(2.0f, 720+rotationValue, itemName));
        }
        else
        {
            float rotationValue = SectorData[sectorName];
            StartCoroutine(Rotate(2.0f, 720 + rotationValue, sectorName));
        }
    }

    IEnumerator Rotate(float duration, float angle, string itemName)
    {
        float startRotation = WheelInside.transform.eulerAngles.z;
        float endRotation = startRotation + angle;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            WheelInside.transform.eulerAngles = new Vector3(WheelInside.transform.eulerAngles.x,
                WheelInside.transform.eulerAngles.y, zRotation);
            yield return null;
        }
        var SelectedItem = (GameObject.Find("/Canvas/Wheel/WheelData/WheelInside/" + itemName));
        SelectedItem.transform.SetParent(ClosingAnimation);
        EndingText.text = "Claim";
        yield return new WaitForSeconds(1.5f);
        Wheel.SetActive(false);
        Vector3 startScale = SelectedItem.transform.localScale;
        Vector3 endScale = startScale + new Vector3(0.5f,0.5f,0.5f);
        Vector3 startposition = SelectedItem.transform.localPosition;
        Vector3 endposition = startposition + new Vector3(0.0f, -50.0f, 0.0f);
        float tt = 0.0f;
        while (tt < duration)
        {
            tt += Time.deltaTime;
            SelectedItem.transform.localScale = Vector3.Lerp(startScale, endScale, tt);
            SelectedItem.transform.localPosition = Vector3.Lerp(startposition, endposition, tt);
            yield return null;
        }
    }

}


