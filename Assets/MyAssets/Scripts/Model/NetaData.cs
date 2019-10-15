using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MyGame/Create NetaData ", fileName = "NetaData ")]
public class NetaData : ScriptableObject {
    public NetaProperty[] netaProperties;

    private static NetaData _i;
    public static NetaData i {
        get {
            string PATH = "ScriptableObjects/" + nameof (NetaData);
            //初アクセス時にロードする
            if (_i == null) {
                _i = Resources.Load<NetaData> (PATH);

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
public class NetaProperty {
    public NetaType netaType;
    public Sprite sprite;

}