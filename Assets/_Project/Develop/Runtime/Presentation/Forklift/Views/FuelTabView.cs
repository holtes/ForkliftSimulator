using UnityEngine;
using TMPro;

namespace _Project.Develop.Runtime.Presentation.Forklift.Views
{
    public class FuelTabView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fuelProgress;

        public void SetProgress(int value)
        {
            _fuelProgress.text = $"{value}%";
        }
    }
}