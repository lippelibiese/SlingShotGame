using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState
{
    idle,
    playing,
    levelEnd
}

public class GameController : MonoBehaviour
{

    public static GameController S; // Singleton

    // Fields set in Unity Inspector pane
    public GameObject[] castles; // An array with all castles
    public Text gtLevel; // Level GUI Text
    public Text gtScore; // Score GUI Text
    public Vector3 castlePos; // Place to put castles
    public float velThresh;

    // Dynamic fields
    public int level; // Current level
    public int levelMax; // Number of levels
    public int shotsTaken;
    public GameObject castle; // The current castle
    public GameState state = GameState.idle;
    public string showing = "both"; // FollowCam mode

    

    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
        velThresh = 0.5f;
    }

    void StartLevel()
    {
        // If a castle exists, get rid of it
        if (castle != null)
        {
            Destroy(castle);
        }

        // Destroy the old projectiles
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("bullet");
        foreach (GameObject p in projectiles)
        {
            Destroy(p);
        }

        // Instantiate the new castle
        castle = Instantiate(castles[level]) as GameObject;
        castle.transform.position = castlePos;
        shotsTaken = 0;

        // Reset the camera
        SwitchView("both");


        // Reset the Goal
        Goal.goalMet = false;
        UpdateGT();

        state = GameState.playing;

    }

    void UpdateGT()
    {
        gtLevel.text = "Level:" + (level + 1) + " of " + levelMax;
        gtScore.text = "Shots Taken: " + shotsTaken;
    }

    void Update()
    {
        UpdateGT();

        // Check for level end
        if (state == GameState.playing && Goal.goalMet)
        {
            if (FollowCam.S.poi.tag == "bullet" && FollowCam.S.poi.GetComponent<Rigidbody>().velocity.magnitude <= velThresh)
            {
                // Change state to stop checking for level end
                state = GameState.levelEnd;
                // Zoom out
                SwitchView("both");
                // Start next level in 2 seconds
                Invoke("NextLevel", 2f);
            }
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    // Static to change the view point
    public void SwitchView(string view)
    {
        S.showing = view;
        switch (S.showing)
        {
            case "Slingshot":
                FollowCam.S.poi = null;
                break;
            case "Castle":
                FollowCam.S.poi = S.castle;
                break;
            case "both":
                FollowCam.S.poi = GameObject.Find("poiBoth");
                break;
        }
    }

    // Static function that allows to increment the score
    public static void ShotFired()
    {
        S.shotsTaken++;
    }


}