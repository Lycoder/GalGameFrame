using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gal {
    public class TextView : BaseView<Text>{

        private string final_string;
        private string current_string;
        public float text_speed = 0.1f;


        public void SetView(string content) {
            StopCoroutine(settext());
            final_string = content;
            current_string = string.Empty;
            StartCoroutine(settext());
        }

        public void quickSet() {
            StopCoroutine(settext());
            view.text = final_string;
        }

        IEnumerator settext() {
            int current_index = 0;
            current_string = "";
            while (current_string != final_string) {
                current_string += final_string[current_index++];
                view.text = current_string;
                yield return new WaitForSeconds(text_speed);
            }
        }
    }
}
