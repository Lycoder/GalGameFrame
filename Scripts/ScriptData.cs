

namespace gal {
    public class ScriptDataBase {

        public int type;
        public string pos;
        public string name;
        public string talk;
        public string picName;
        public string nextChapterName;

        public ScriptDataBase(int type, string pos, string name, string talk, string picName) {
            this.type = type;
            this.pos = pos;
            this.name = name;
            this.talk = talk;
            this.picName = picName;
        }

        public ScriptDataBase(int type, string str) {
            this.type = type;
            if (type == 0) {
                this.picName = str;
            }
            else if (type == 2) {
                nextChapterName = str;
            }
        }
    }

    public enum DataType {
        charcter = 0,
        many_charcters = 1,
        background = 2,
        chapter = 3,
    }
    public class ScriptData {
        public DataType type;
    }
}
