using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowInfoBySteps : MonoBehaviour
{
    private string final ;
    private string current_string;
    public bool isShowing = false;

    public void setText(string _new) {
        StopCoroutine("showtext");//结束上一个进程
        final = _new;
        StartCoroutine("showtext");//开始下一个进程
        isShowing = true;
    }

    public void qsetText() {
        StopCoroutine("showtext");
        GetComponent<Text>().text = final;
        isShowing=false;
    }
    IEnumerator showtext() {
        int current_index = 0;
        current_string = "";
        while(current_string != final) {
            current_string += final[current_index++];
            GetComponent<Text>().text = current_string;          
            yield return new WaitForSeconds(0.1f);        
        }
        isShowing = false;
    }
}
