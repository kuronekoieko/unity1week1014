using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MyGame/Create StageData", fileName = "StageData")]
public class StageData : ScriptableObject {

    public List<StageProperty> list = new List<StageProperty> ();
    private static StageData _i;
    public static StageData i {
        get {
            string PATH = "ScriptableObjects/" + nameof (StageData);
            //初アクセス時にロードする
            if (_i == null) {
                _i = Resources.Load<StageData> (PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_i == null) {
                    Debug.LogError (PATH + " not found");
                }
            }

            return _i;
        }
    }
}

[System.Serializable]
public class StageProperty {
    public string memo;
    public  float speed;
    public NetaType[] targets;
    public NetaType[] getas;

}