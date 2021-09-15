#if JUCE_TIMELINE_EXTENSIONS

using UnityEngine;
using UnityEngine.Playables;

namespace Juce.TweenPlayer.Timeline
{
    [System.Serializable]
    public class TweenPlayerPlayControlBehaviour : PlayableBehaviour
    {
        private bool firstFrame;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if(!Application.isPlaying)
            {
                return;
            }

            if(!firstFrame)
            {
                firstFrame = true;

                TweenPlayer tweenPlayer = playerData as TweenPlayer;

                if (tweenPlayer == null)
                {
                    return;
                }

                tweenPlayer.Kill();
                tweenPlayer.Play();

                float durationToMatch = (float)playable.GetDuration();

                float currentDuration = tweenPlayer.GetDuration();

                float newTimeScale = (currentDuration * tweenPlayer.TimeScale) / durationToMatch;

                tweenPlayer.SetTimeScale(newTimeScale);
            }
        }
    }
}

#endif
