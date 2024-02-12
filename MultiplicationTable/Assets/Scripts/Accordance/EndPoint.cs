using UnityEngine;

public class EndPoint : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.TryGetComponent(out MovingObject obj)) {
            obj.gameObject.SetActive(false);
        }
    }
}
