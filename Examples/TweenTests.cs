using Juce.Tween;
using UnityEngine;

public class TweenTests : MonoBehaviour
{
    [SerializeField] private Transform toTween1 = default;
    [SerializeField] private Transform toTween2 = default;

    private ISequenceTween groupTween;

    void Start()
    {
        ITween tween1 = toTween1.TweenPosition(new Vector3(0, 0, 0), 4.0f);
        ITween tween2 = toTween2.TweenPosition(new Vector3(0, 0, 0), 2.0f);
        ITween tween3 = new ResetableCallbackTween(() => toTween2.gameObject.SetActive(true), () => toTween2.gameObject.SetActive(false));
        ITween tween4 = new WaitTimeTween(1);

        ITween tween5 = toTween1.TweenPosition(new Vector3(1000, 0, 0), 4.0f);
        ITween tween6 = toTween2.TweenPosition(new Vector3(1000, 0, 0), 2.0f);
        ITween tween7 = new ResetableCallbackTween(() => toTween2.gameObject.SetActive(true), () => toTween2.gameObject.SetActive(false));
        ITween tween8 = new WaitTimeTween(1);

        ISequenceTween groupTween2 = JuceTween.Sequence();
        groupTween2.Append(tween5);
        groupTween2.Append(tween6);
        groupTween2.Append(tween7);
        groupTween2.Append(tween8);

        groupTween = JuceTween.Sequence();
        groupTween.Append(tween1);
        groupTween.Append(tween4);
        groupTween.Append(tween3);
        groupTween.Append(tween2);
        groupTween.Append(groupTween2);

        groupTween.SetLoops(5, LoopResetMode.InitialValues);

        groupTween.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("k"))
        {
            groupTween.Kill();
        }

        if (Input.GetKeyDown("c"))
        {
            groupTween.Complete();
        }

        if (Input.GetKeyDown("r"))
        {
            groupTween.Reset();
        }

        if (Input.GetKeyDown("p"))
        {
            groupTween.Play();
        }

        if (Input.GetKeyDown("o"))
        {
            groupTween.Replay();
        }

        if (Input.GetKeyDown("d"))
        {
            MonoBehaviour.Destroy(toTween1.gameObject);
        }

        UnityEngine.Debug.Log($"Progress: { groupTween.GetNormalizedProgress() } " +
            $"| Duration: {groupTween.GetDuration()} " +
            $"| Elapsed: {groupTween.GetElapsed()}" +
            $"| Tweens: {groupTween.GetTweensCount()}" +
            $"| Playing Tweens {groupTween.GetPlayingTweensCount()}");
    }
}
