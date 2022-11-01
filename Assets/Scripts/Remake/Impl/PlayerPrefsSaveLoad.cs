using UnityEngine;

namespace Impl
{
    class PlayerPrefsSaveLoad : MonoBehaviour, IProgressSaver, IProgressLoader
    {
        public bool isDone => true;
        public ProgressData data { get; private set; }
        public void Load()
        {
            data = new ProgressData();
            foreach (var x in ProgressConfig.instance.products)
            {
                var pr = new ProductData();
                pr.code = x.code;
                pr.amount = PlayerPrefs.GetInt(x.key, x.defaultAmount);
                data.products.Add(pr);
            }
        }

        public void SaveAll(ProgressData progressData)
        {
            foreach (var x in progressData.products)
            {
                Save(x);
            }
            PlayerPrefs.Save();
        }

        public void Save(ProductData product)
        {
            var key = ProgressConfig.instance.GetProduct(product.code).key;
            PlayerPrefs.SetInt(key, product.amount);
            
        }
    }
}