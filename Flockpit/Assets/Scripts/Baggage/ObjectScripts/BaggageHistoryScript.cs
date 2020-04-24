using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script to take a history of bags, used to ensure little repitition occurs
public class BaggageHistoryScript : MonoBehaviour
{
    //Structure of bag parameters
   public struct BaggageItem
    {
        public Color bagCol;
        public string bagModel;
        public Texture bagTex;
    }
    //List of bags that have appeared in the scene
    public List<BaggageItem> bagHistory;
    //The amount of spaces from the top of the list down for which bags with those traits will not reappear
    public int invalidRange = 5;
    // Start is called before the first frame update
    void Start()
    {
        //Initialise list
        bagHistory = new List<BaggageItem>();
    }
    //Add a new bag to the list
    public void AddToHistory(BaggageItem bag)
    {
        bagHistory.Add(bag);
    }
    //Function to check if a big is in the history
    public bool IsInHistory(BaggageItem bag)
    {
        //Only checks for the bag within a certain range dictated by the invalid range parameter. This is to ensure that previous combinations can occur again, just that they don't occur until a certain
        //Quantity of bags have been spawned
        if (bagHistory.Count > invalidRange)
        {
            //Simple search returning true if the parameters of the bag match a bag within the history list
            for (int i = bagHistory.Count-1; i > bagHistory.Count - invalidRange; i--)
            {
                if ((bagHistory[i].bagCol == bag.bagCol) && (bagHistory[i].bagModel == bag.bagModel) && (bagHistory[i].bagTex == bag.bagTex))
                {
                    return true;
                }
            }
        } else
        {//Same procedure but for cases in which the history is smaller than the invalid range(usually at the start of the game)
            for (int i = bagHistory.Count-1; i > -1; i--)
            {
                if ((bagHistory[i].bagCol == bag.bagCol) && (bagHistory[i].bagModel == bag.bagModel) && (bagHistory[i].bagTex == bag.bagTex))
                {
                    return true;
                }
            }
        }//If it is not found at all, return false
        return false;
    }
}
