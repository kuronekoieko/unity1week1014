using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class SusiGetaController : MonoBehaviour {

    [SerializeField] SpriteRenderer netaSR;
    NetaType netaType;

    // Start is called before the first frame update
    public void OnStart () {

    }

    // Update is called once per frame
    public void OnUpdate () {

    }

    public void MoveStart (Vector2 offset) {

        Vector2 pos = (Vector2) transform.position + offset;
        //1秒で座標（1,1,1）に移動
        transform.DOMove (
            pos, 　　 //移動後の座標
            1.0f　　　　　　 //時間
        ).OnComplete (() => {
            Variables.susiGetaState = SusiGetaState.MOVE_END;
        });
    }

    void OnTriggerEnter2D (Collider2D other) {
        netaSR.gameObject.SetActive (false);
        UIManager.i.GetNeta (netaType);
    }
}