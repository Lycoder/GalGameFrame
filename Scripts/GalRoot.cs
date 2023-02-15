using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace gal {
    public class GalRoot : MonoBehaviour {

        public Text _name;
        public Text talk;
        public Image background;
        public Image left;
        public Image right;
        
        private string current_chapter;
        private int current_line;

        private string pre_chapter;
        private int pre_line;


        List<string> txt;

        private void Start() {
            load_scripts();
            handleData(loadNext());
        }
        public void load_scripts() {
            txt = new List<string>();
            
            current_line = GalDataRoot.instance.line;
            current_chapter = GalDataRoot.instance.chapter;

            pre_chapter = current_chapter;
            pre_line = current_line;

            StreamReader stream = new StreamReader("Assets/Gal/Texts/" + current_chapter);
            while (!stream.EndOfStream) {
                txt.Add(stream.ReadLine());
            }
            stream.Close();
        }
        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                if(talk.GetComponent<ShowInfoBySteps>().isShowing == true) {
                    talk.GetComponent<ShowInfoBySteps>().qsetText();
                    return;
                }
                if (EventSystem.current.currentSelectedGameObject==null)
                    handleData(loadNext());
            }
        }
        public ScriptDataBase loadNext() {
            if (current_line < txt.Count) {
                string[] datas = txt[current_line].Split('|');
                int type = int.Parse(datas[0]);
                if (type == 0) {
                    string picName = datas[1];
                    pre_line = current_line;
                    current_line++;
                    return new ScriptDataBase(type, picName);
                } else if(type ==1){
                    string pos = datas[1];
                    string name = datas[2];
                    string talk = datas[3];
                    string picName = datas[4];
                    pre_line = current_line;
                    current_line++;
                    return new ScriptDataBase(type, pos, name, talk, picName);
                } else if(type==2){
                    pre_line = current_line;
                    pre_chapter = current_chapter;
                    current_line = 0;
                    current_chapter = datas[1];
                    txt = new List<string>();
                    StreamReader stream = new StreamReader("Assets/Gal/Texts/" + current_chapter);
                    while (!stream.EndOfStream) {
                        txt.Add(stream.ReadLine());
                    }
                    stream.Close();
                    return loadNext();
                } else {
                    return null;
                }

            } else {
                //goto next chapter
                return null;
            }
        }
        public void handleData(ScriptDataBase data) {
            if (data == null)
                return;
            if (data.type == 0) {
                setImage(background, data.picName);
                handleData(loadNext());
            } else if (data.type == 1) {

                if (data.pos.CompareTo("left") == 0) {
                    left.gameObject.SetActive(true);
                    setImage(left, data.picName);
                    right.gameObject.SetActive(false);
                } else {
                    right.gameObject.SetActive(true);
                    setImage(right, data.picName);
                    left.gameObject.SetActive(false);
                }
                setText(_name, data.name);
                talk.GetComponent<ShowInfoBySteps>().setText(data.talk);
            } else if(data.type == 2) {

            }
        }
        public void setText(Text text, string content) {
            text.text = content;
        }

        public void setImage(Image image, string picName) {
            image.sprite = loadPicture("Assets/Gal/Pictures/" + picName);
        }


        public Sprite loadPicture(string picPath) {

            //创建文件读取流
            FileStream fileStream = new FileStream(picPath, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);
            //创建文件长度缓冲区
            byte[] bytes = new byte[fileStream.Length];
            //读取文件
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            //释放文件读取流
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;

            //创建Texture
            int width = Screen.width;
            int height = Screen.height;
            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(bytes);

            //创建Sprite
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }

        public void gal_save_data() {
            GalDataRoot.instance.chapter = pre_chapter;
            GalDataRoot.instance.line = pre_line;
            GalDataRoot.instance.save_data("Default.txt");
        }
        public void gal_load_data() {
            GalDataRoot.instance.load_data("Default.txt");
            load_scripts();
            handleData(loadNext());
        }
    }

}
