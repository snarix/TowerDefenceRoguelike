using System;
using System.Collections.Generic;
using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.BonusSystem.Data;
using Include;
using UnityEngine;

namespace _Source.MetaGameplay.Transition
{
    public class BonusPriceDataLoader
    {
        private BonusConfigHolder _bonusConfigHolder;
        private ISaveLoadService _saveLoadService;
        
        public BonusPriceDataLoader(BonusConfigHolder bonusConfigHolder, ISaveLoadService saveLoadService)
        {
            _bonusConfigHolder = bonusConfigHolder;
            _saveLoadService = saveLoadService;
        }

        public void SaveBonusPrice()
        {
            var data = new BonusPriceData();
            foreach (var bonusData in _bonusConfigHolder.Bonuses)
            {
                data.Prices.Add(new BonusPriceData.BonusPrice
                {
                    Type = bonusData.BonusType,
                    Price = bonusData.BonusGoldPrice
                });
            }
            _saveLoadService.Save("BonusPriceData", data);
        }

        public void LoadBonusPrice()
        {
            var data = _saveLoadService.Load<BonusPriceData>("BonusPriceData");
            if (data != null && data.Prices != null)
            {
                foreach (var bonusPrice in data.Prices)
                {
                    var bonusData = _bonusConfigHolder.Bonuses.Find(b => b.BonusType == bonusPrice.Type);
                    bonusData?.SetPrice(bonusPrice.Price);
                }
            }
            else
            {
                foreach (var bonusData in _bonusConfigHolder.Bonuses)
                {
                    bonusData.InitializePrice();
                }
            }
        }
    }
    
    [Serializable]
    public class BonusPriceData
    {
        [SerializeField] private List<BonusPrice> _prices = new List<BonusPrice>();
        public List<BonusPrice> Prices => _prices;
        
        [Serializable]
        public struct BonusPrice
        {
            [SerializeField] private BonusType _type;
            [SerializeField] private int _price;

            public BonusType Type
            {
                get => _type;
                set => _type = value;
            }

            public int Price
            {
                get => _price;
                set => _price = value;
            }
        }
    }
}