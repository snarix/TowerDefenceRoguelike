using _Source.Gameplay.Currency;
using Include;
using UnityEngine;

namespace _Source.Testing.DoTweenTesting.Scripts
{
    public class AddCurrencyTesting : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var data = ServiceLocator.GetService<MetaWallet>();
                data.Gold.Add(50);
            }
        }
    }
}