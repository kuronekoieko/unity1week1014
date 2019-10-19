using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandController : MonoBehaviour {

    [SerializeField] Transform sushiGetaPos;
    float startPos;
    float endPos { get { return sushiGetaPos.position.y - 1f; } }
    bool isMoving;
    // Start is called before the first frame update
    public void OnStart () {
        startPos = transform.position.y;
    }

    // Update is called once per frame
    public void OnUpdate () {

    }

    public void MoveHand () {
        if (isMoving) { return; }
        isMoving = true;
        transform.DOMoveY (
            endPos, 　　 //移動後の座標
            0.05f　　　　　　 //時間
        ).OnComplete (() => {
            ReturnHand ();
        });
    }

    void ReturnHand () {
        transform.DOMoveY (startPos, 　　 //移動後の座標
            0.1f　　　　　　 //時間
        ).OnComplete (() => {
            isMoving = false;
        });
    }
}