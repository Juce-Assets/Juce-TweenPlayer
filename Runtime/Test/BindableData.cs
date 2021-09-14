using UnityEngine;
using Juce.TweenPlayer.BindableData;

namespace Assets
{
    [BindableData("Test Bindable Data", "Test/Bindable Data", "a8ea3fa2-9e3b-11eb-a8b3-0242ac130003")]
    public class BindableData : IBindableData
    {
        public int intToBind;
        public float floatToBind;
        public string stringToBind;
        public Vector3 vectorToBind;
        public Transform transformToBind;
        // etc...
    }
}
