using _Source.Gameplay.Currency;
using Include;

namespace _Source.MetaGameplay.Transition
{
    public class WalletDataLoader
    {
        private CurrencyValueConfig _currencyValueConfig;
        private ISaveLoadService _saveLoadService;
        
        public WalletDataLoader(CurrencyValueConfig currencyValueConfig, ISaveLoadService saveLoadService)
        {
            _currencyValueConfig = currencyValueConfig;
            _saveLoadService = saveLoadService;
        }

        public void SaveWallet(MetaWallet wallet)
        {
            _saveLoadService.Save("WalletData", wallet);
        }

        public MetaWallet LoadWallet()
        {
            var data = _saveLoadService.Load<MetaWallet>("WalletData");
            if (data == null)
            {
                return new MetaWallet(new Currency(_currencyValueConfig.Gold));
            }

            return data;
        }
    }
}