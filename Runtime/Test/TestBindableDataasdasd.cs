using Juce.TweenPlayer.BindableData;
using UnityEngine;

namespace JuceNew
{
    [BindableData("Test2", "Test/Test2", "a8ea3fa2-9e3b-11eb-a8b3-0242ac130003")]
    public class TestBindableDataasdasd : IBindableData
    {
        public float bindableFloat;
        public float bindableFloat2;
        public int bindableInt;
        public string bindableString;
        //public string bindableString2;
        public Transform randomTransform;
        public Vector3 vector;
    }
}
