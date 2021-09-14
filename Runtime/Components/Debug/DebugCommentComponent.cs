using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Attributes;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Debug Comment", "Debug/Comment")]
    [TweenPlayerComponentColor(0.5f, 0.192f, 0.721f, useAsBackground: true)]
    public class DebugCommentComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private StringBinding comment = new StringBinding();

        public override string GenerateTitle()
        {
            return comment.FallbackValue;
        }
    }
}
