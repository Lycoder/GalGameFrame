

using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace gal {
    public class LoadScripts : MonoBehaviour {

        public static LoadScripts instance;
        int index;
        List<string> txt;
        // Use this for initialization
        void Awake() {
            instance = this;
            index = 0;
        }

        void Start() {

        }

        public void loadScripts(string txtFileName) {
            index = 0;
            txt = new List<string>();
            StreamReader stream = new StreamReader("Assets/Gal/Texts/" + txtFileName);
            while (!stream.EndOfStream) {
                txt.Add(stream.ReadLine());

            }
            stream.Close();
        }

        public ScriptDataBase loadNext() {
            if (index < txt.Count) {
                string[] datas = txt[index].Split('|');
                int type = int.Parse(datas[0]);
                if (type == 0) {
                    string picName = datas[1];
                    index++;
                    return new ScriptDataBase(type, picName);
                } else {
                    string pos = datas[1];
                    string name = datas[2];
                    string talk = datas[3];
                    string picName = datas[4];
                    index++;
                    return new ScriptDataBase(type, pos, name, talk, picName);
                }

            } else {
                return null;
            }
        }
    }

}
