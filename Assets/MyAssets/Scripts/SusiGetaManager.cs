using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SusiGetaManager : MonoBehaviour {

    [SerializeField] SusiGetaController susiGetaPrefab;
    SusiGetaController[] susis;

    // Start is called before the first frame update
    public void OnStart () {
        SusigetaGenerator ();
    }

    // Update is called once per frame
    public void OnUpdate () {

    }

    void SusigetaGenerator () {
        susis = new SusiGetaController[6];
        Vector2 pos = new Vector2 (-9.6f, 0.67f);
        Vector2 offset = new Vector2 (3.2f, 0f);
        for (int i = 0; i < susis.Length; i++) {
            pos += offset;
            susis[i] = Instantiate (
                susiGetaPrefab,
                pos,
                Quaternion.identity,
                transform);
        }
    }
}