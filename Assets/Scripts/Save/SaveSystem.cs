using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;

public static class SaveSystem {

    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SyncDB();
    #endif
    
    public static void SavePlayer(int slot, PlayerData player) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player" + slot + ".save";

        FileStream fileStream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(player);
        formatter.Serialize(fileStream, data);

        fileStream.Close();

        #if UNITY_WEBGL && !UNITY_EDITOR
            SyncDB();
        #endif
    }

    public static PlayerData LoadPlayer(int slot) {
        string path = Application.persistentDataPath + "/player" + slot + ".save";

        if (File.Exists(path)) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerData data = (PlayerData)binaryFormatter.Deserialize(fileStream);
            data.timestamp = File.GetLastWriteTime(path).ToString();
            fileStream.Close();

            return data;
        }

        else {
            //Debug.LogError("File not found in " + path);
            return null;
        }
    }

}
