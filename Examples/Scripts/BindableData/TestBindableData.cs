using Juce.TweenPlayer.BindableData;
using UnityEngine;

[BindableData("Test Bindable Data", "Test/BindableData", "TestBindableData")]
public class TestBindableData : IBindableData
{
    public Vector2 ValueToBind;
}
