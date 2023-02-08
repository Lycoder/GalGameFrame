using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace gal {
    public class GameManger : MonoBehaviour {
        public Text _name;
        public Text talk;
        public Image background;
        public Image left;
        public Image right;

        // Use this for initialization
        void Start() {
            LoadScripts.instance.loadScripts("first.txt");
            handleData(LoadScripts.instance.loadNext());
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                handleData(LoadScripts.instance.loadNext());
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


        public void handleData(ScriptDataBase data) {
            if (data == null)
                return;
            if (data.type == 0) {
                setImage(background, data.picName);
                print(data.picName);
                handleData(LoadScripts.instance.loadNext());
            } else {

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
                setText(talk, data.talk);


            }
        }
    }

}