using System;
using UnityEngine;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class Binding
    {
        [SerializeField] [HideInInspector] private bool bindingEnabled;
        [SerializeField] [HideInInspector] private bool wantsToBeBinded;
        [SerializeField] [HideInInspector] private bool binded;
        [SerializeField] [HideInInspector] private string bindedVariableName;

        public bool BindingEnabled { get => bindingEnabled; set => bindingEnabled = value; }
        public bool WantsToBeBinded { get => wantsToBeBinded; set => wantsToBeBinded = value; }
        public bool Binded { get => binded; set => binded = value; }
        public string BindedVariableName { get => bindedVariableName; set => bindedVariableName = value; }

        public virtual Type BindingType { get; }
        public virtual void SetBindedValue(object objectValue) { }
    }
}
