using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TargetController : MonoBehaviour {
    [SerializeField] SpriteRenderer targetSR;
    NetaType targetNetaType;
    int targetIndex;

    public static TargetController i;
    public void OnStart () {
        i = this;
    }

    public void Init () {
        targetIndex = 0;
        targetNetaType = StageData.i.list[Variables.stageIndex].targets[targetIndex];
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
                Variables.gameState = GameState.CLEAR;
                return true;
            }
            targetIndex++;
            targetNetaType = StageData.i.list[Variables.stageIndex].targets[targetIndex];
            SetTargetSprite ();
            return true;
        } else {
            Variables.gameState = GameState.EXP_ANIMATION;
            return false;
        }
    }

}