using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    interface IOperationAdministratorAddRemoveProduct
    {
        void AddProduct(int storageType, Product prod, int amount);
        void RemoveProduct(int storageType, int prodID, int amount);
    }
    interface IOperationChangeCustomerStatus
    {
        void ChangeCustomerStatus(int custID, UserStatus status);
    }


    class Administrator: Employee, IOperationChangeCustomerStatus, IOperationAdministratorAddRemoveProduct
    {
        //відповідає за id
        private static int administratorNextUniqueId = 1;

        public int AdministratorId { get; private set; }

        public Administrator():base()
        {
            this.AdministratorId = administratorNextUniqueId++;
        }

        //послати повідомлення у медіатор і там воно обробиться-----------------

        //змінити статус покупця
        public void ChangeCustomerStatus(int custID, UserStatus status)
        {
            ShopMediator.Instance().AdministratorChangeCustomerStatus(custID,status);
        }
        //додати продукт у сховище, тип сховища визначається цифрою
        // 1 - М'ясний, 2- Молочний, 3- Предмети для дому
        //Краща логіка добавлання продуктів буду у наступних версіях програми
        public void AddProduct(int storageType, Product prod, int amount= 1)
        {
            //дадати вказану кількість продуктів
            for(int i =0; i < amount; i++)
            {
                ShopMediator.Instance().AdministratorAddProduct(storageType, prod);
            }
        }

        //Зменшити кількість продукту або видалити продукт
        public void RemoveProduct(int storageType, int prodID, int amount=1)
        {
            //відняти вказану кількість
            for(int i =0; i < amount; i ++)
            {
                ShopMediator.Instance().AdministratorRemoveProduct(storageType, prodID);

            }
        }
    }
}
