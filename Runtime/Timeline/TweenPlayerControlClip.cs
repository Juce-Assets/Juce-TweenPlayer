﻿using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Juce.TweenComponent.Timeline
{
    public class TweenPlayerControlClip : PlayableAsset, ITimelineClipAsset
    {
        [SerializeField] private TweenPlayerPlayControlBehaviour template = new TweenPlayerPlayControlBehaviour();

        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<TweenPlayerPlayControlBehaviour>.Create(graph, template);
        }
    }
}
