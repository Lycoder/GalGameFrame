using System.Collections.Generic;

namespace gal {
    public class SingleCharacter : ScriptData {
        public string pos;
        public string name;
        public string talk;
        public string picName;
        public string clip;
    }

    public class MultipleCharacter:ScriptData{
        
        public SingleCharacter talk_character;//正在说话的人

        public Dictionary<string, string> othercharacter = new Dictionary<string, string>();    //其他人物,只需要人物的pos和picname
    }
}
