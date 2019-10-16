using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SusiGetaManager : MonoBehaviour {

    [SerializeField] SusiGetaController susiGetaPrefab;
    SusiGetaController[] susis;
    Vector2 offset = new Vector2 (3.2f, 0f);
    Vector2 startPos = new Vector2 (-9.6f, 0.67f);

    // Start is called before the first frame update
    public void OnStart () {
        SusigetaGenerator ();
        Variables.susiGetaState = SusiGetaState.MOVE_START;
    }

    public void Init () {
        for (int i = 0; i < susis.Length; i++) {

            susis[i].Init ();
        }
    }

    // Update is called once per frame
    public void OnUpdate () {
        switch (Variables.susiGetaState) {
            case SusiGetaState.MOVE_START:
                MoveStartSusis ();
                break;
            case SusiGetaState.MOVING:
                break;
            case SusiGetaState.MOVE_END:
                BorderCheck ();
                Variables.susiGetaState = SusiGetaState.MOVE_START;
                break;
            default:
                break;
        }
    }

    void SusigetaGenerator () {
        susis = new SusiGetaController[6];
        Vector2 pos = startPos;

        for (int i = 0; i < susis.Length; i++) {
            pos += offset;
            susis[i] = Instantiate (
                susiGetaPrefab,
                pos,
                Quaternion.identity,
                transform);
            susis[i].OnStart ();
            int index = Random.Range (0, NetaData.i.netaProperties.Length);
            NetaType netaType = NetaData.i.netaProperties[index].netaType;
            susis[i].SetNeta (netaType);
        }
    }

    void MoveStartSusis () {
        Variables.susiGetaState = SusiGetaState.MOVING;
        for (int i = 0; i < susis.Length; i++) {

            susis[i].MoveStart (-offset);
        }
    }

    void BorderCheck () {
        int index = Random.Range (0, NetaData.i.netaProperties.Length);
        NetaType netaType = NetaData.i.netaProperties[index].netaType;
        Vector2 loopPos = new Vector2 (-startPos.x, startPos.y);
        for (int i = 0; i < susis.Length; i++) {
            if (susis[i].transform.position.x < -7f) {
                susis[i].transform.position = loopPos;
                susis[i].SetNeta (netaType);
            }

        }
    }
}