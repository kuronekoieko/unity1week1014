using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TargetController : MonoBehaviour {
    [SerializeField] SpriteRenderer targetSR;
    NetaType targetNetaType;
    int stageIndex;
    int targetIndex;

    public static TargetController i;
    public void OnStart () {
        i = this;
        targetNetaType = StageData.i.list[stageIndex].netas[targetIndex];
        SetTargetSprite ();
    }

    void SetTargetSprite () {
        NetaProperty netaProperty = NetaData.i.netaProperties
            .Where (i => i.netaType == targetNetaType)
            .FirstOrDefault ();
        if (netaProperty == null) { return; }
        targetSR.sprite = netaProperty.sprite;
    }

    public bool IsTarget (NetaType netaType) {

        if (targetNetaType == netaType) {
            if (targetIndex == 2) {
                Variables.gameState = GameState.RESULT;
                return true;
            }
            targetIndex++;
            targetNetaType = StageData.i.list[stageIndex].netas[targetIndex];
            SetTargetSprite ();
            return true;
        } else {
            return false;
        }
    }

}