using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace gal {
    public class GalDataRoot : MonoBehaviourSingleton<GalDataRoot>
    {
        public int line;
        public string chapter;

        public bool save_data(string data_name) {
            SaveData save = new SaveData(chapter, line);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Create(Application.dataPath + "/Gal/SaveData/GalData" + data_name);
            bf.Serialize(fileStream, save);
            fileStream.Close();
            if (File.Exists(Application.dataPath + "/Gal/SaveData/GalData" + data_name)) {
                return true;
            }
            return false;
        }
        public bool load_data(string data_name) {
            if(File.Exists(Application.dataPath + "/Gal/SaveData/GalData" + data_name)) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fileStream = File.Open(Application.dataPath + "/Gal/SaveData/GalData" + data_name, FileMode.Open);
                SaveData save = (SaveData)bf.Deserialize(fileStream);

                fileStream.Close();
                line = save.line ;//if (save.line -1<0) TBD
                chapter = save.chapter;
                return true;
            }
            return false;
        }

    }
    [System.Serializable]
    public class SaveData {
        public string chapter;
        public int line;

        public SaveData(string chapter, int line) {
            this.chapter = chapter;
            this.line = line;
        }
    }
}

