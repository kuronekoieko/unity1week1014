using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
public class SusiGetaController : MonoBehaviour {

    [SerializeField] SpriteRenderer netaSR;
    [SerializeField] ExplosionController explosion;
    NetaType netaType;

    // Start is called before the first frame update
    public void OnStart () {

    }

    public void Init () {
        netaSR.gameObject.SetActive (true);
    }

    // Update is called once per frame
    public void OnUpdate () {

    }

    public void MoveStart (Vector2 offset) {

        Vector2 pos = (Vector2) transform.position + offset;

        //1秒で座標（1,1,1）に移動
        transform.DOMove (
            pos, 　　 //移動後の座標
            0.5f　　　　　　 //時間
        ).OnComplete (() => {
            Variables.susiGetaState = SusiGetaState.MOVE_END;
        });

        /*   transform
                    .DOJump (pos, 1, 1, 0.5f)
                    .SetEase (Ease.Linear)
                    .OnComplete (() => {
                        Variables.susiGetaState = SusiGetaState.MOVE_END;
                    });*/

    }

    void OnTriggerEnter2D (Collider2D other) {
        if (TargetController.i.IsTarget (netaType)) {
            UIManager.i.GetNeta (netaType);
            netaSR.gameObject.SetActive (false);
        } else {
            explosion.Explosion ();
        }
    }

    public void SetNeta (NetaType netaType) {
        NetaProperty netaProperty = NetaData.i.netaProperties
            .Where (i => i.netaType == netaType)
            .FirstOrDefault ();

        if (netaProperty == null) { return; }
        this.netaType = netaType;
        netaSR.sprite = netaProperty.sprite;
        netaSR.gameObject.SetActive (true);
    }
}