using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator animator;

    public float voltage = 100f;
    public float voltage_max = 1000f;

    private float normalizedDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    bool isPlayingAnimation = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlayingAnimation)
        {
            voltage = Mathf.Clamp(voltage, 0, voltage_max);
            normalizedDuration = map(voltage, 0f, voltage_max, 0f, 0.5f);
            StartCoroutine(playShockAnimation(normalizedDuration));
        }
    }

    IEnumerator playShockAnimation(float normalizedDuration)
    {
        isPlayingAnimation = true;

        var oldStateName = currentAnimationName();

        //ขาไป
        var duration = 0f;
        do
        {
            jumpToTime("shock", duration);
            duration += Time.deltaTime;
            yield return null;
        } while (duration < normalizedDuration);

        //ขากลับ
        do
        {
            jumpToTime("shock", duration);
            duration -= Time.deltaTime;
            yield return null;
        } while (duration > 0);

        animator.Play(oldStateName, 0, 0);
        animator.speed = 1f;
        isPlayingAnimation = false;
    }

    void jumpToTime(string name, float nTime)
    {
        //anim.Play(name, 0, nTime);
        animator.speed = 0f;
        animator.Play(name, 0, nTime);
        animator.Update(Time.deltaTime);
    }
    string currentAnimationName()
    {
        var currAnimName = "";
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                currAnimName = clip.name.ToString();
            }
        }
        return currAnimName;
    }

    // c#
    float map(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

}
