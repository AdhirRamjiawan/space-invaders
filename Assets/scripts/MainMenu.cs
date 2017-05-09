using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


    private int selectedOption;
    //private GameObject player;
    //private float playerStartY;

	// Use this for initialization
	void Start () {
        selectedOption = 0;
        //player = GameObject.Find("player");
        //playerStartY = player.transform.position.y;
	}
	
    private void setSelectedText(int selectedOption)
    {
        GameObject.Find("txtNewGame").GetComponent<Text>().color = Color.white;
        GameObject.Find("txtOptions").GetComponent<Text>().color = Color.white;
        GameObject.Find("txtQuit").GetComponent<Text>().color = Color.white;

        switch (selectedOption)
        {
            case 0:
                GameObject.Find("txtNewGame").GetComponent<Text>().color = Color.blue;
                break;
            case 1:
                GameObject.Find("txtOptions").GetComponent<Text>().color = Color.blue;
                break;
            case 2:
                GameObject.Find("txtQuit").GetComponent<Text>().color = Color.blue;
                break;
        }
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedOption++;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedOption--;
        }

        if (selectedOption < 0)
            selectedOption = 0;

        if (selectedOption > 2)
            selectedOption = 2;

        setSelectedText(selectedOption);

        //player.transform.position = new Vector3(player.transform.position.x, playerStartY - (selectedOption * 0.8f), player.transform.position.z);


        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedOption == 0)
                SceneManager.LoadScene("Level1");
            else if (selectedOption == 2)
                Application.Quit();
        }
    }
}

