using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    class StorageMediator
    {
        private static StorageMediator instance;

        public StorageManager StorageManager { get; set; }

        private StorageMediator()
        {
            this.StorageManager = StorageManager.Instance();
        }
        public static StorageMediator Instance()
        {
            if (instance == null)
            {
                instance = new StorageMediator();
            }
            return instance;
        }

        public void ReadProductsFromFile(string path)
        {
            StorageManager.ReadProductsFromTextFile(path);
        }

        public Product GetProductById(int prodID)
        {
            return StorageManager.GetProductByID(prodID);
        }
    }
}
