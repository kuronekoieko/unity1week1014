using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] SusiGetaManager susiGetaManager;
    [SerializeField] HandController handController;
    // Start is called before the first frame update
    void Start () {
        Variables.gameState = GameState.GAME;
        susiGetaManager.OnStart ();
    }

    // Update is called once per frame
    void Update () {

        switch (Variables.gameState) {
            case GameState.START:
                break;
            case GameState.GAME:
                susiGetaManager.OnUpdate ();
                if (Input.GetMouseButtonDown (0)) {
                    handController.MoveHand ();
                }
                break;
            case GameState.RESULT:
                break;
            default:
                break;
        }

    }
}