using System;

namespace Juce.TweenComponent.Attributes
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
