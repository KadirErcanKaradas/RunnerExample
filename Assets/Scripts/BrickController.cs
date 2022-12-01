using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : Obstacle
{
    protected override void Explosion()
    {
        ExplosionBrick();
    }
    private void ExplosionBrick()
    {
        StartCoroutine(ExplosionTime());
    }
    private IEnumerator ExplosionTime()
    {
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            transform.GetChild(1).GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
        }
        for (int i = 2; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().enabled = false;
        }
        GameEvent.Expo();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
