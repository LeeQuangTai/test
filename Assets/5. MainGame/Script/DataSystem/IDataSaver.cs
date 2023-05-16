using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSaver
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
