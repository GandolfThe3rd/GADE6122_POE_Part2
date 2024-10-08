using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Hero_Adventure
{
    public class Level
    {
        public Tile[,] tiles;
        private int width;
        private int height;
        private Random random = new Random();
        public HeroTile hero;
        public ExitTile exit;
        private EnemyTile[] enemies;
        private PickupTile[] pickupTiles;

        public PickupTile[] PickupTiles
        {
            get { return pickupTiles; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Level(int width, int height,int noOfEnemies, int noOfPickups, HeroTile aHero = null)
        {
            Position randomPlace;

            Width = width;
            Height = height;
            enemies = new EnemyTile[noOfEnemies];
            pickupTiles = new PickupTile[noOfPickups];

            tiles = new Tile[Width, Height];
            InitialiseTiles();

            randomPlace = GetRandomEmptyPosition();
            
            if (aHero == null)
            {
                CreateTile(TileType.Hero, randomPlace);
                aHero = new HeroTile(randomPlace);
                this.hero = aHero;
            }
            else
            {
                aHero.characterPosition = randomPlace;
                aHero = new HeroTile(randomPlace);
                this.hero = aHero;
                tiles[randomPlace.X, randomPlace.Y] = aHero;
            }

            randomPlace = GetRandomEmptyPosition();
            CreateTile(TileType.Exit, randomPlace);
            exit = new ExitTile(randomPlace);
            exit.X = randomPlace.X;
            exit.Y = randomPlace.Y;

            for (int i = 0; i < noOfEnemies; i++)
            {
                randomPlace = GetRandomEmptyPosition();
                enemies[i] = (EnemyTile)CreateTile(TileType.Enemy, randomPlace);
            }

            for (int j = 0; j < noOfPickups; j++)
            {
                randomPlace = GetRandomEmptyPosition();
                pickupTiles[j] = (PickupTile)CreateTile(TileType.Pickup, randomPlace);
            }

        }

        public enum TileType
        {
            Empty,
            Wall,
            Hero,
            Exit,
            Enemy,
            Pickup
        }

        private Tile CreateTile(TileType aTileType, Position aPosition)
        {
            switch (aTileType)
            {
                case TileType.Empty:
                    {
                        EmptyTile tile = new EmptyTile(aPosition);
                        tiles[aPosition.X, aPosition.Y] = tile;
                        return tile;
                    }
                case TileType.Wall:
                    {
                        WallTile tile = new WallTile(aPosition);
                        tiles[aPosition.X, aPosition.Y] = tile;
                        return tile;
                    }
                case TileType.Hero:
                    {
                        HeroTile tile = new HeroTile(aPosition);
                        tiles[aPosition.X, aPosition.Y] = tile;
                        return tile;
                    }
                case TileType.Exit:
                    {
                        ExitTile tile = new ExitTile(aPosition);
                        tiles[aPosition.X, aPosition.Y] = tile;
                        return tile;
                    }
                case TileType.Enemy:
                    {
                        GruntTile tile = new GruntTile(aPosition);
                        tiles[aPosition.X, aPosition.Y] = tile;
                        return tile;
                    }
                case TileType.Pickup:
                    {
                        HealthPickup tile = new HealthPickup(aPosition);
                        tiles[aPosition.X, aPosition.Y] = tile;
                        return tile;
                    }
                default:
                    {
                        EmptyTile tile = new EmptyTile(aPosition);
                        tiles[aPosition.X, aPosition.Y] = tile;
                        return tile;
                    }
            }
        }

        private Tile CreateTile(TileType aTileType, int aX, int aY)
        {
            Position position = new Position(aX, aY);
            return CreateTile(aTileType, position);
        }

        public void InitialiseTiles()
        {
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    if (w == 0 | w == Width - 1 | h == 0 | h == Height - 1)
                    {
                        CreateTile(TileType.Wall, w, h);
                    }
                    else
                    {
                        CreateTile(TileType.Empty, w, h);
                    }
                }
            }
        }

        public override string ToString()
        {
            string map = "";

            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    map += tiles[w, h].Display.ToString();
                }
                map += "\n";
            }

            return map;
        }

        public Tile[,] Tiles
        {
            get { return tiles; }

        }

        private Position GetRandomEmptyPosition()
        {
            Position value = null;
            int temp1;
            int temp2;

            while (value == null)
            {
                temp1 = random.Next(0, Height);
                temp2 = random.Next(0, Width);

                if (tiles[temp2, temp1] is EmptyTile) // if (tiles[temp1, temp2].Display == Convert.ToChar("▯")) || Old code just in case
                {
                    value = new Position(temp2, temp1);
                    return value;
                }
            }
            return value;
        }

        public HeroTile Hero
        {
            get { return hero; }
        }

        public void SwopTiles(Tile tile1, Tile tile2)
        {
            /*Tile tempTile1 = tile1;
            Tile tempTile2 = tile2;

            tile1.X = tempTile2.X; // Object 1 becomes Object 2
            tile1.Y = tempTile2.Y;
            tile1 = tempTile2;

            tile2.X = tempTile1.X; // Object 2 becomes the Temp Tile (which was Object 1)
            tile2.Y = tempTile1.Y;
            tile2 = tempTile1;

            tiles[tile1.X, tile1.Y] = tile1;
            tiles[tile2.X, tile2.Y] = tile2; */ //Snake Code

            Position tempTile = tile1.Position;

            tile1.Position = tile2.Position;
            tile2.Position = tempTile;

            tiles[tile1.Position.X, tile1.Position.Y] = tile1;
            tiles[tile2.Position.X, tile2.Position.Y] = tile2;
        }

        public void UpdateVision()
        {
            hero.UpdateVision(this);

            for (int y = 0; y < enemies.Length; y++)
            {
                enemies[y].UpdateVision(this);
            }
        }

        public ExitTile Exit
        { get { return exit; } }

        public HeroTile HeroTile
        {
            get { return hero; }
            set { hero = value; }
        }

        public EnemyTile[] Enemies
        {
            get { return enemies; }
        }
    }
}
