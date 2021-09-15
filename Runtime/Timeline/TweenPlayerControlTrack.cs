#if JUCE_TIMELINE_EXTENSIONS

using UnityEngine.Timeline;

namespace Juce.TweenPlayer.Timeline
{
    [TrackColor(0.2f, 0.2f, 0.2f)]
    [TrackBindingType(typeof(TweenPlayer))]
    [TrackClipType(typeof(TweenPlayerControlClip))]
    public class TweenPlayerControlTrack : TrackAsset
    {
 
    }
}

#endif
