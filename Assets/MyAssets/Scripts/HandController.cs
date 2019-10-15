using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandController : MonoBehaviour {
    Vector2 startPos = new Vector3 (1.96f, -2.15f);
    Vector2 endPos = new Vector3 (1.96f, 0.25f);
    bool isMoving;
    // Start is called before the first frame update
    public void OnStart () {

    }

    // Update is called once per frame
    public void OnUpdate () {

    }

    public void MoveHand () {
        if (isMoving) { return; }
        isMoving = true;
        transform.DOMoveY (
            0.25f, 　　 //移動後の座標
            0.05f　　　　　　 //時間
        ).OnComplete (() => {
            ReturnHand ();

        });
    }

    void ReturnHand () {
        transform.DOMoveY (-2.15f, 　　 //移動後の座標
            0.5f　　　　　　 //時間
        ).OnComplete (() => {
            isMoving = false;
        });
    }
}