using UnityEngine;
namespace Gal {
    public class BaseView<T> : MonoBehaviour {
        public T view;
        public virtual void SetView(T _view) {
            view = _view;
        }
    }
}
