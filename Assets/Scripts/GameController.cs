using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum Gamestate
{
    idle,
    playing,
    levelEnd
}

public class GameController : MonoBehaviour {

    public static GameController S;


    // Inspector fields

    public GameObject[] castles;
    public Vector3 castlePos;

    public Text gtLevel;
    public Text gtShotsFired;



    //internal fields
    private int level;
    private int levelMax;
    private int shotsFired;
    private GameObject currentCastle;
    private string showing = "Slingshot";
    private Gamestate state = Gamestate.idle;




    void Awake(){

        S = this;

        level = 0;
        levelMax = castles.Length;


        StartLevel();
    }

    void StartLevel()
    {
        // if there is a castle destroy it 

        // destroy all remaining bullets

        // instantiate new castle

        // switch view to both

        // clear all bullet trails

    }

    void Update()
    {
        // update our gui texts

        // check for level end

    }

    public void SwitchView(string view)
    {
        // Switch over all the possibilities(castle,both,slingshot)

            //set the followCam

    }
	
	}

