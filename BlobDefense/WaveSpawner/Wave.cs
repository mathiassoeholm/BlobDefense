namespace BlobDefense.WaveSpawner
{
    internal class Wave<T1> : IEnemyWave
        where T1 : Enemy, new()
    {
        private int amountOfEnemyOne;

        public Wave(int amountOfEnemyOne = 0)
        {
            this.amountOfEnemyOne = amountOfEnemyOne;
        }

        public Enemy SpawnEnemy()
        {
            if (this.amountOfEnemyOne > 0)
            {
                this.amountOfEnemyOne--;
                return new T1();
            }

            // End of wave return null
            return null;
        }
    }
    
    internal class Wave<T1, T2> : IEnemyWave
        where T1 : Enemy, new()
        where T2 : Enemy, new()
    {
        private int amountOfEnemyOne;
        private int amountOfEnemyTwo;

        public Wave(int amountOfEnemyOne = 0, int amountOfEnemyTwo = 0)
        {
            this.amountOfEnemyOne = amountOfEnemyOne;
            this.amountOfEnemyTwo = amountOfEnemyTwo;
        }

        public Enemy SpawnEnemy()
        {
            if (this.amountOfEnemyOne > 0)
            {
                this.amountOfEnemyOne--;
                return new T1();
            }

            if (this.amountOfEnemyTwo > 0)
            {
                this.amountOfEnemyTwo--;
                return new T2();
            }

            // End of wave return null
            return null;
        }
    }
    
    internal class Wave<T1, T2, T3> : IEnemyWave
        where T1 : Enemy, new()
        where T2 : Enemy, new()
        where T3 : Enemy, new()
    {
        private int amountOfEnemyOne;
        private int amountOfEnemyTwo;
        private int amountOfEnemyThree;

        public Wave(int amountOfEnemyOne = 0, int amountOfEnemyTwo = 0, int amountOfEnemyThree = 0)
        {
            this.amountOfEnemyOne = amountOfEnemyOne;
            this.amountOfEnemyTwo = amountOfEnemyTwo;
            this.amountOfEnemyThree = amountOfEnemyThree;
        }

        public Enemy SpawnEnemy()
        {
            if (this.amountOfEnemyOne > 0)
            {
                this.amountOfEnemyOne--;
                return new T1();
            }

            if (this.amountOfEnemyTwo > 0)
            {
                this.amountOfEnemyTwo--;
                return new T2();
            }

            if (this.amountOfEnemyThree > 0)
            {
                this.amountOfEnemyThree--;
                return new T3();
            }

            // End of wave return null
            return null;
        }
    }
}
