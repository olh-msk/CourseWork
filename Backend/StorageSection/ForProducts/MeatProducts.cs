using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Meat Products]
    enum MeatCategories { High_sort, I_sort, II_sort };
    class MeatProduct : Product
    {
        public MeatCategories MeatCategory { get; set; }

        public MeatProduct():base()
        {
            MeatCategory = MeatCategories.High_sort;
        }
    }

    class UkrainianMeatProduct: MeatProduct
    {
        public UkrainianMeatProduct():base()
        {

        }
    }

    class EuropeanMeatProduct: MeatProduct
    {
        public EuropeanMeatProduct():base()
        {

        }
    }
    #endregion
}
