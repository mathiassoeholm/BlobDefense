using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    internal class GameManager : Singleton<GameManager>
    {
        private const int InitialCurrencyAmount = 100;
        
        /// <summary>
        /// Prevents a default instance of the <see cref="GameManager"/> class from being created.
        /// </summary>
        private GameManager()
        {
            this.Currency = InitialCurrencyAmount;
        }

        /// <summary>
        /// This method is called to create the instance.
        /// </summary>
        public void InitializeManager()
        {
        }

        /// <summary>
        /// Gets the amount of currency that player has.
        /// </summary>
        public int Currency { get; private set; }

        /// <summary>
        /// Subtracts the price from the players currency, if the player has enough.
        /// </summary>
        /// <param name="price">
        /// The price of the item being bought.
        /// </param>
        /// <returns>
        /// A value indicating whether the player has enough currency to buy the item.
        /// </returns>
        public bool TryBuy(int price)
        {
            if (this.Currency >= price)
            {
                this.Currency -= price;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gives currency to the player.
        /// </summary>
        /// <param name="amount">
        /// The amount of currency to give.
        /// </param>
        public void GiveCurrency(int amount)
        {
            this.Currency += amount;
        }
    }
}
