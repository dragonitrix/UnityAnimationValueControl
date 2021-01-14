using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[ExecuteAlways]
public class SimpleSetAnimFrame_Runtime : MonoBehaviour
{
    Animator anim;
    [Range(0f, 1f)]
    public float normalizedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpToTime(currentAnimationName(), normalizedTime);
    }
    void jumpToTime(string name, float nTime)
    {
        //anim.Play(name, 0, nTime);

        anim.speed = 0f;
        anim.Play(currentAnimationName(), 0, nTime);
        anim.Update(Time.deltaTime);

    }
    string currentAnimationName()
    {
        var currAnimName = "";
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                currAnimName = clip.name.ToString();
            }
        }

        return currAnimName;

    }
}
