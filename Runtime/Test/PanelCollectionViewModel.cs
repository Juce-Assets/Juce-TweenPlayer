using System.Collections.Generic;
using UnityEngine;

namespace JuceNew
{
    public class PanelCollectionViewModel : MonoBehaviour
    {
        [SerializeField] private Transform panelsParent = default;
        [SerializeField] private PanelViewModel panelPrefab = default;
        [SerializeField] private List<PanelData> dataToShow = default;

        private void Awake()
        {
            SpawnPanels();
        }

        public void SpawnPanels()
        {
            foreach (PanelData panelData in dataToShow)
            {
                GameObject instance = MonoBehaviour.Instantiate(panelPrefab.gameObject, panelsParent);
                PanelViewModel panelViewModel = instance.GetComponent<PanelViewModel>();

                panelViewModel.Init(panelData);
            }
        }
    }
}
