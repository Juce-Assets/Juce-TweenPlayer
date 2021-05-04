using Juce.TweenPlayer.BindableData;

namespace JuceNew.Example1
{
    [System.Serializable]
    [BindableData("Panel Stats", "Examples/1/PanelStats", "a4b718b0-9e3b-11eb-a8b3-0242ac1")]
    public class Example1PanelStatsDataBinding : IBindableData
    {
        public float AttackProgressBarSize;
        public float DefenseProgressBarSize;
        public float SuperPowerProgressBarSize;
    }

}
