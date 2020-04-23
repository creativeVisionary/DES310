using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaggageHistoryScript : MonoBehaviour
{
   public struct BaggageItem
    {
        public Color bagCol;
        public string bagModel;
        public Texture bagTex;
    }
    public List<BaggageItem> bagHistory;
    //The amount of spaces from the top of the list down for which bags with those traits will not reappear
    public int invalidRange = 5;
    // Start is called before the first frame update
    void Start()
    {
        bagHistory = new List<BaggageItem>();
    }

    public void AddToHistory(BaggageItem bag)
    {
        bagHistory.Add(bag);
    }

    public bool IsInHistory(BaggageItem bag)
    {
        if (bagHistory.Count > invalidRange)
        {
            for (int i = bagHistory.Count-1; i > bagHistory.Count - invalidRange; i--)
            {
                if ((bagHistory[i].bagCol == bag.bagCol) && (bagHistory[i].bagModel == bag.bagModel) && (bagHistory[i].bagTex == bag.bagTex))
                {
                    return true;
                }
            }
        } else
        {
            for (int i = bagHistory.Count-1; i > -1; i--)
            {
                if ((bagHistory[i].bagCol == bag.bagCol) && (bagHistory[i].bagModel == bag.bagModel) && (bagHistory[i].bagTex == bag.bagTex))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
