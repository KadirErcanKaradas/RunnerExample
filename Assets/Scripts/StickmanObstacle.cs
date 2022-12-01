using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanObstacle : Obstacle
{
    private void Start()
    {
        PlayAnimator();
    }

    protected override void Explosion()
    {
        StickExpo();
    }

    private void PlayAnimator()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            Animator anim = transform.GetChild(i).GetComponent<Animator>();
            int randomNumber = Random.Range(1, 4);
            anim.SetBool(randomNumber.ToString(),true);
        }
    }
    private void StickExpo()
    {
        StartCoroutine(StickExpoTime());
    }

    private IEnumerator StickExpoTime()
    {            
        //boxCollider.enabled = false;
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().enabled = false;
        }
        GameEvent.Expo();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
