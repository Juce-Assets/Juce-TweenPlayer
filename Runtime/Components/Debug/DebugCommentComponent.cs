using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Attributes;
using UnityEngine;

namespace Juce.TweenComponent.Components
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
