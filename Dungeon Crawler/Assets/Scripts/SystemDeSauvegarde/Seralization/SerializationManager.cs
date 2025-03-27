using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SystemDeSauvegarde.Seralization
{
    [System.Serializable]
    public class SerializationManager
    {
        public static bool Save(string _saveName, object _saveData)
        {
            BinaryFormatter formatter = GetBinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            string path = Application.persistentDataPath + "/saves/" + _saveName + ".save";

        
            FileStream file = File.Create(path);
            formatter.Serialize(file, _saveData);
            file.Close();
        
            Debug.Log("File Saved succesfully !");
            Debug.Log(Application.persistentDataPath);

            return true;
        
        }

        public static object Load(string _FileName)
        {
            string path = Application.persistentDataPath + "/saves/" + _FileName + ".save";
        
            if (!File.Exists(path))
            {
                Debug.LogError(" No file found !");
                return null;
            }

            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            try
            {
                object save = formatter.Deserialize(file);
                file.Close();
            
                Debug.Log("File Loaded succesfully !");
            
                return save;
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("File not found at : {0} / with exception code : {1}", path, e);
                file.Close();
                return null;
            }

        
        }


        public static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter;
        
        }
    
    }
}