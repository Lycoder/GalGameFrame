using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TxtEditor : EditorWindow
{
    [MenuItem("EditorWindow/TxTEditorWindow")]
    public static void ShowWindow() {
        var window = EditorWindow.GetWindow(typeof(TxtEditor));
    }

    public TxTType type;
    public pos c_position;


    public string backgroudName;
    public string talkName;
    public string talkText;
    public string talkIcon;
    public string nextchapter;
    public string target_txt;
    string output;
    

    public enum TxTType {
        background=0 ,
        character=1,
        chapter=2,
    }

    public enum pos {
        left,
        right,
    }
    private void OnGUI() {
        type = (TxTType)EditorGUILayout.EnumPopup(type);
        target_txt = EditorGUILayout.TextField("请输入目标txt名称:",target_txt);
        if(type == TxTType.background) {
            EditorGUILayout.LabelField("编辑器类型为背景图:");
            EditorGUILayout.LabelField("请输入背景图的名称:(例:backgroud.jpg)");
            backgroudName = EditorGUILayout.TextField("",backgroudName);
        }
        else if (type == TxTType.character) {
            EditorGUILayout.LabelField("编辑器类型为角色:");
            c_position = (pos)EditorGUILayout.EnumPopup("请选择人物位置:",c_position);
            talkName = EditorGUILayout.TextField("请输入说话人的名字", talkName);
            talkText = EditorGUILayout.TextField("请输入说话内容:", talkText);
            talkIcon = EditorGUILayout.TextField("请输入说话人的立绘:",talkIcon);
        }
        else if(type == TxTType.chapter) {
            EditorGUILayout.LabelField("编辑器类型为背景图:");
            EditorGUILayout.LabelField("请输入新章节的名称:(例:next.txt)");
            nextchapter = EditorGUILayout.TextField("", nextchapter);
        }

        if (GUILayout.Button("生成txt")) {
            if(type == TxTType.background) {
                output = $"{(int)type}|{backgroudName}";
            }
            else if(type == TxTType.character) {
                output = $"{(int)type}|{c_position.ToString()}|{talkName}|{talkText}|{talkIcon}";
            }
            else if(type == TxTType.chapter) {
                output = $"{(int)type}|{nextchapter}";
            }
        }
        EditorGUILayout.LabelField("生成的行语句为:", output);
        if (GUILayout.Button($"压入{target_txt}中")) {
            StreamWriter sw;
            FileInfo fi = new FileInfo("Assets/Gal/Texts/" + target_txt);
            sw = fi.AppendText();
            sw.WriteLine(output);
            sw.Close();
            sw.Dispose();
        }
    }
}
