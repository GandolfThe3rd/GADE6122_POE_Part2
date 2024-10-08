using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public class GameEngine
    {
        private Level level;
        private int noOfLevels;
        private Random random;

        private int currentLevel;
        bool callEnemies = false;
        private int currentLevelNumber = 1;

        const int MIN_SIZE = 10;
        const int MAX_SIZE = 20;

        GameState gameState = GameState.InProgress;

        // Temporary
        public string coords;

        public GameEngine(int aNoOfLevels)
        {
            int tempW;
            int tempH;

            noOfLevels = aNoOfLevels;
            random = new Random();
            tempW = random.Next(MIN_SIZE, MAX_SIZE);
            tempH = random.Next(MIN_SIZE, MAX_SIZE);
            level = new Level(tempH, tempW, currentLevelNumber);
        }

        public override string ToString()
        {
            switch(gameState)
            {
                case GameState.Complete:
                    {
                        return "Congratulations, You Have Completed The Game!";
                    }
                case GameState.InProgress:
                    {

                        return level.ToString();
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        private bool MoveHero(Direction aDirection)
        {
            Tile targetTile = level.Hero.vision[(int)aDirection];
            
            //targetTile.X = targetTile.X;
            //targetTile.Y = targetTile.Y;

            if (targetTile is ExitTile && currentLevel == noOfLevels)
            {
                gameState = GameState.Complete;
                return false;
            }
            else if(targetTile is ExitTile)
            {
                NextLevel();
                return true;
            }
            else if (targetTile is EmptyTile)
            {
                level.SwopTiles(level.hero, targetTile);
                Level.UpdateVision(level);

                if (callEnemies == false)
                {
                    callEnemies = true;
                }
                else
                {
                    callEnemies = false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void TriggerMovement(Direction aDirection)
        {
            level.hero.UpdateVision(level);
            MoveHero(aDirection);

            // every secind call:
            if (callEnemies == true)
            {
                MoveEnemies(Level.Enemies);
            }

            coords = $"X:{level.hero.X}\nY:{level.hero.Y}";
        }

        public Level Level
        {
            get { return level; }
            set { level = value; }
        }

        public enum GameState
        {
            InProgress,
            Complete,
            GameOver
        }

        public void NextLevel()
        {
            currentLevel++;

            int tempW;
            int tempH;
            HeroTile tempHero;
            tempHero = level.hero;

            tempW = random.Next(MAX_SIZE, MAX_SIZE);
            tempH = random.Next(MAX_SIZE, MAX_SIZE);
            level = new Level(tempH, tempW, currentLevelNumber, tempHero);

            currentLevelNumber++;
        }

        private void MoveEnemies(EnemyTile[] enemys)
        {
            Tile targetTile;

            for (int y = 0; y < enemys.Length; y++)
            {
                if (enemys[y].isDead == true || enemys[y].GetMove(out targetTile) == false)
                {
                    return;
                }
                else
                {
                    Level.SwopTiles(enemys[y], targetTile);
                    Level.UpdateVision(level);
                }
            }
        }

        private bool HeroAttack(Direction direction)
        {
            Tile attackTarget = level.hero.vision[(int)direction];

            if (attackTarget is CharacterTile)
            {
                level.hero.Attack((CharacterTile)attackTarget);
                return true;
            }
            else
            {
                return false;
            }

        }

        public void TriggerAttack(Direction direction)
        {
            bool successful;

            successful = HeroAttack(direction);

            if (successful)
            {
                EnemiesAttack();
            }
        }

        private void EnemiesAttack()
        {
            CharacterTile[] targets;

            for (int i = 0; i < level.Enemies.Length; i++)
            {
                if (level.Enemies[i].IsDead == false)
                {
                    targets = level.Enemies[i].GetTargets();

                    for (int j = 0; j < targets.Length; j++)
                    {
                        level.Enemies[i].Attack(targets[j]);
                    }
                }
            }
        }

    }
        public enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3,
            None = 4
        }

    
}
