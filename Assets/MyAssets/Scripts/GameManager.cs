using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] SusiGetaManager susiGetaManager;
    [SerializeField] HandController handController;
    [SerializeField] UIManager uIManager;
    [SerializeField] TargetController targetController;
    // Start is called before the first frame update
    void Start () {
        Variables.gameState = GameState.START;
        susiGetaManager.OnStart ();
        uIManager.OnStart ();
        targetController.OnStart ();
        handController.OnStart ();
    }

    void Init () {
        susiGetaManager.Init ();
        uIManager.Init ();
        targetController.Init ();
    }

    // Update is called once per frame
    void Update () {
        Debug.Log (Variables.gameState);
        switch (Variables.gameState) {
            case GameState.START:
                Init ();
                Variables.gameState = GameState.GAME;
                break;
            case GameState.GAME:
                susiGetaManager.OnUpdate ();
                if (Input.GetMouseButtonDown (0)) {
                    handController.MoveHand ();
                }
                break;
            case GameState.CLEAR:
                uIManager.ShowResultText ("クリア");
                if (Input.GetMouseButtonDown (0)) {
                    if (StageData.i.list.Count == Variables.stageIndex + 1) {
                        Debug.Log ("全クリ");
                        Variables.gameState = GameState.RESULT;
                    } else {
                        Variables.gameState = GameState.START;
                        Variables.stageIndex++;
                    }
                }
                break;
            case GameState.ANIMATION:
                DOVirtual.DelayedCall (1f, () => Variables.gameState = GameState.RESULT);
                Variables.gameState = GameState.DEFAULT;
                break;
            case GameState.RESULT:
                uIManager.SetActiveButtons (isActive: true);
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking (Variables.stageIndex + 1);
                Variables.gameState = GameState.DEFAULT;
                break;
            default:
                break;
        }

    }
}