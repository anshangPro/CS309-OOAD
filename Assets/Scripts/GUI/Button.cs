using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameStatus mode = GameStatus.Default;

    private void OnGUI()
    {
        switch (mode)
        {
            case GameStatus.Default:
                /* show nothing here*/
                break;
            case GameStatus.Menu:
                if (GUI.Button(new Rect(100, 100, 100, 20), "Switch Unit"))
                {
                    /* api: switch current unit */
                    /* api: run into the Menu status again */
                }

                if (GUI.Button(new Rect(300, 100, 100, 20), "Attack"))
                {
                    /* api: run into the attack status*/
                }

                if (GUI.Button(new Rect(500, 100, 100, 20), "Move"))
                {
                    /* api: run into the move status */
                }

                break;
            case GameStatus.Fight:
                GUI.TextArea(new Rect(100, 100, 200, 20), "Choose an enemy to attack: ");
                // GUI.TextArea(new Rect(200, 100, 200, 20), "name of game object");

                /* api: select an energy here */

                if (GUI.Button(new Rect(100, 300, 100, 20), "Confirm"))
                {
                    /* api: select an energy here */
                    /* api: switch current unit */
                    /* api: run into the Menu status again */
                }

                break;
            case GameStatus.Move:
                GUI.TextArea(new Rect(100, 100, 200, 20), "Choose a block to go");
                if (GUI.Button(new Rect(100, 300, 100, 20), "Confirm"))
                {
                    /* api: run into the Moving status */
                }

                break;


            case GameStatus.Moving:
                /* get the target block */
                // GUI.TextArea(new Rect(100, 100, 200, 20), "Moving to the block: " + GameObject);
                /* showing moving animation*/
                break;

            default:
                break;
        }
    }
}