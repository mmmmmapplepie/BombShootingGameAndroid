using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
  public static void saveSettings() {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/settings.kek";
    FileStream stream = new FileStream(path, FileMode.Create);
    PlayerData data = new PlayerData();
    formatter.Serialize(stream, data);
    stream.Close();
  }
  public static PlayerData loadSettings() {
    string path = Application.persistentDataPath + "/settings.kek";
    if (File.Exists(path)) {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);
      PlayerData data = formatter.Deserialize(stream) as PlayerData;
      stream.Close();
      return data;
    } else {
      Debug.Log("File does not exist in path: " + path);
      return null;
    }
  }
  public static void resetSaveData() {
    string path = Application.persistentDataPath + "/settings.kek";
    File.Delete(path);
  }
}
