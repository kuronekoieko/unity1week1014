using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
public class SusiGetaController : MonoBehaviour {

    [SerializeField] SpriteRenderer netaSR;
    [SerializeField] ExplosionController explosion;
    NetaType netaType;
    CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    public void OnStart () {
        circleCollider2D = GetComponent<CircleCollider2D> ();
    }

    public void Init () {
        netaSR.gameObject.SetActive (true);
        explosion.gameObject.SetActive (false);
        circleCollider2D.enabled = true;
    }

    // Update is called once per frame
    public void OnUpdate () {

    }

    public void MoveStart (Vector2 offset) {

        Vector2 pos = (Vector2) transform.position + offset;
        float speed = StageData.i.list[Variables.stageIndex].speed;
        speed = Mathf.Clamp (speed, 0.1f, 0.6f);
        //1秒で座標（1,1,1）に移動
        transform.DOMove (
            pos, 　　 //移動後の座標
            speed　　　　　　 //時間
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
            AudioManager.i.PlayOneShot (0);
            UIManager.i.GetNeta (netaType);
            netaSR.gameObject.SetActive (false);
            circleCollider2D.enabled = false;
        } else {
            AudioManager.i.PlayOneShot (3);
            explosion.gameObject.SetActive (true);
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
        circleCollider2D.enabled = true;
    }
}