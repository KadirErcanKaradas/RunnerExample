using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour,IInteractable
{
    private void OnEnable()
    {
        GameEvent.GameStart += Jump;
    }

    private void OnDisable()
    {
        GameEvent.GameStart -= Jump;
    }

    public void Interact()
    {
        GameEvent.Collect();
        CollectSphere();
    }

    private void CollectSphere()
    {
        gameObject.SetActive(false);
    }

    private void Jump()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.DOMoveY(1, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            transform.DORotate(new Vector3(0,360,0),2 * 0.5f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Restart);
        }
    }
}
