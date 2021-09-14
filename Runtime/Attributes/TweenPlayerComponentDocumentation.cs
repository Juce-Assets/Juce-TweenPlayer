using System;

namespace Juce.TweenPlayer.Attributes
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
