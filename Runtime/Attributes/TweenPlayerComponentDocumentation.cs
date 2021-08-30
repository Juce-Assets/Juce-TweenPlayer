using System;

namespace Juce.TweenPlayer.Components
{
    public class TweenPlayerComponentDocumentation : Attribute
    {
        public string Documentation { get; }

        public TweenPlayerComponentDocumentation(string documentation)
        {
            Documentation = documentation;
        }
    }
}
