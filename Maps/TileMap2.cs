using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SideScroller
{
    class TileMap2
    {
        public int pc1, pc2, pc3, pc4, pc5, pc6, pc7, pc8, pc9, pc10, totalpc, ls;
        Point currentFrame;
        public Rectangle source;
        float interval;
        float time;
        Point frameSize;
        Player player = new Player();

        public int[,] tilemap;
        public int tilesize;
        public int mapSizeX;
        public int mapSizeY;

        int tileindex;
        Texture2D tex, shardtex, animtex, fjtex, gjtex, singleSlowT, pickuptex, pillar, stalagmite, shardleveltex;
        List<Texture2D> texlis;

        public List<Rectangle> spikes;
        public List<Rectangle> spikesTop;
        public List<Rectangle> spikesLeft;
        public List<Rectangle> spikesRight;
        public List<Rectangle> spikesBot;

        public List<Rectangle> bounce;
        public List<Rectangle> bounceTop;
        public List<Rectangle> bounceLeft;
        public List<Rectangle> bounceRight;
        public List<Rectangle> bounceBot;

        public List<Rectangle> pickup;
        public List<Rectangle> pickupTop;
        public List<Rectangle> pickupLeft;
        public List<Rectangle> pickupRight;
        public List<Rectangle> pickupBot;
        public List<Rectangle> pickup2;
        public List<Rectangle> pickup2Top;
        public List<Rectangle> pickup2Left;
        public List<Rectangle> pickup2Right;
        public List<Rectangle> pickup2Bot;
        public List<Rectangle> pickup3;
        public List<Rectangle> pickup3Top;
        public List<Rectangle> pickup3Left;
        public List<Rectangle> pickup3Right;
        public List<Rectangle> pickup3Bot;
        public List<Rectangle> pickup4;
        public List<Rectangle> pickup4Top;
        public List<Rectangle> pickup4Left;
        public List<Rectangle> pickup4Right;
        public List<Rectangle> pickup4Bot;
        public List<Rectangle> pickup5;
        public List<Rectangle> pickup5Top;
        public List<Rectangle> pickup5Left;
        public List<Rectangle> pickup5Right;
        public List<Rectangle> pickup5Bot;
        public List<Rectangle> pickup6;
        public List<Rectangle> pickup6Top;
        public List<Rectangle> pickup6Left;
        public List<Rectangle> pickup6Right;
        public List<Rectangle> pickup6Bot;
        public List<Rectangle> pickup7;
        public List<Rectangle> pickup7Top;
        public List<Rectangle> pickup7Left;
        public List<Rectangle> pickup7Right;
        public List<Rectangle> pickup7Bot;
        public List<Rectangle> pickup8;
        public List<Rectangle> pickup8Top;
        public List<Rectangle> pickup8Left;
        public List<Rectangle> pickup8Right;
        public List<Rectangle> pickup8Bot;
        public List<Rectangle> pickup9;
        public List<Rectangle> pickup9Top;
        public List<Rectangle> pickup9Left;
        public List<Rectangle> pickup9Right;
        public List<Rectangle> pickup9Bot;
        public List<Rectangle> pickup10;
        public List<Rectangle> pickup10Top;
        public List<Rectangle> pickup10Left;
        public List<Rectangle> pickup10Right;
        public List<Rectangle> pickup10Bot;

        public List<Rectangle> bridge;
        public List<Rectangle> bridgeTop;

        public List<Rectangle> slow;
        public List<Rectangle> slowTop;
        public List<Rectangle> slowLeft;
        public List<Rectangle> slowRight;
        public List<Rectangle> slowBot;

        public List<Rectangle> shards;
        public List<Rectangle> shardTop;
        public List<Rectangle> shardLeft;
        public List<Rectangle> shardRight;
        public List<Rectangle> shardBot;
        public List<Rectangle> levelshards;
        public List<Rectangle> levelshardTop;
        public List<Rectangle> levelshardLeft;
        public List<Rectangle> levelshardRight;
        public List<Rectangle> levelshardBot;

        public List<Rectangle> blocks;
        public List<Rectangle> blocksTop;
        public List<Rectangle> blocksLeft;
        public List<Rectangle> blocksRight;
        public List<Rectangle> blocksBot;
        public List<Rectangle> tile;
        public int mapX;
        public int MapW;
        public int MapH;

        public bool hit = false, pickuphit = false, pickup2hit = false, pickup3hit = false, pickup4hit = false, pickup5hit = false;
        public bool pickup6hit = false, pickup7hit = false, pickup8hit = false, pickup9hit = false, pickup10hit = false, shardhit = false;

        public TileMap2()
        {
            tilemap = new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,19,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,15,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,9,1,0,0,0,17,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,9,6,2,2,9,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,9,4,9,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,1,9,9,9,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,13,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,16,0,0,0,0,0,0,0,0,0,0,0,1,4,4,4,4,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,18,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,9,9,9,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,22,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,23},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,9,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,1,1,1},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,20,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,5,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,9,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,6,2,2,6,2,2,6,2,2,6,2,2,6,2,2,6,2,2,6,2,2,3,3,4,4,4,4,4,4,6,2,2,6,2,2,6,2,2,6,2,2,3,3,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            };

            tilesize = 40;
            texlis = new List<Texture2D>();

            mapSizeX = tilemap.GetLength(1);
            mapSizeY = tilemap.GetLength(0);
            spikes = new List<Rectangle>();
            spikesTop = new List<Rectangle>();
            spikesLeft = new List<Rectangle>();
            spikesRight = new List<Rectangle>();
            spikesBot = new List<Rectangle>();

            bounce = new List<Rectangle>();
            bounceTop = new List<Rectangle>();
            bounceLeft = new List<Rectangle>();
            bounceRight = new List<Rectangle>();
            bounceBot = new List<Rectangle>();

            pickup = new List<Rectangle>();
            pickupTop = new List<Rectangle>();
            pickupLeft = new List<Rectangle>();
            pickupRight = new List<Rectangle>();
            pickupBot = new List<Rectangle>();
            pickup2 = new List<Rectangle>();
            pickup2Top = new List<Rectangle>();
            pickup2Left = new List<Rectangle>();
            pickup2Right = new List<Rectangle>();
            pickup2Bot = new List<Rectangle>();
            pickup3 = new List<Rectangle>();
            pickup3Top = new List<Rectangle>();
            pickup3Left = new List<Rectangle>();
            pickup3Right = new List<Rectangle>();
            pickup3Bot = new List<Rectangle>();
            pickup4 = new List<Rectangle>();
            pickup4Top = new List<Rectangle>();
            pickup4Left = new List<Rectangle>();
            pickup4Right = new List<Rectangle>();
            pickup4Bot = new List<Rectangle>();
            pickup5 = new List<Rectangle>();
            pickup5Top = new List<Rectangle>();
            pickup5Left = new List<Rectangle>();
            pickup5Right = new List<Rectangle>();
            pickup5Bot = new List<Rectangle>();
            pickup6 = new List<Rectangle>();
            pickup6Top = new List<Rectangle>();
            pickup6Left = new List<Rectangle>();
            pickup6Right = new List<Rectangle>();
            pickup6Bot = new List<Rectangle>();
            pickup7 = new List<Rectangle>();
            pickup7Top = new List<Rectangle>();
            pickup7Left = new List<Rectangle>();
            pickup7Right = new List<Rectangle>();
            pickup7Bot = new List<Rectangle>();
            pickup8 = new List<Rectangle>();
            pickup8Top = new List<Rectangle>();
            pickup8Left = new List<Rectangle>();
            pickup8Right = new List<Rectangle>();
            pickup8Bot = new List<Rectangle>();
            pickup9 = new List<Rectangle>();
            pickup9Top = new List<Rectangle>();
            pickup9Left = new List<Rectangle>();
            pickup9Right = new List<Rectangle>();
            pickup9Bot = new List<Rectangle>();
            pickup10 = new List<Rectangle>();
            pickup10Top = new List<Rectangle>();
            pickup10Left = new List<Rectangle>();
            pickup10Right = new List<Rectangle>();
            pickup10Bot = new List<Rectangle>();

            bridge = new List<Rectangle>();
            bridgeTop = new List<Rectangle>();

            shards = new List<Rectangle>();
            shardTop = new List<Rectangle>();
            shardLeft = new List<Rectangle>();
            shardRight = new List<Rectangle>();
            shardBot = new List<Rectangle>();
            levelshards = new List<Rectangle>();
            levelshardTop = new List<Rectangle>();
            levelshardLeft = new List<Rectangle>();
            levelshardRight = new List<Rectangle>();
            levelshardBot = new List<Rectangle>();

            slow = new List<Rectangle>();
            slowTop = new List<Rectangle>();
            slowLeft = new List<Rectangle>();
            slowRight = new List<Rectangle>();
            slowBot = new List<Rectangle>();

            blocks = new List<Rectangle>();
            blocksTop = new List<Rectangle>();
            blocksLeft = new List<Rectangle>();
            blocksRight = new List<Rectangle>();
            blocksBot = new List<Rectangle>();
            tile = new List<Rectangle>();
            mapX = 0;

            currentFrame = new Point(0, 0);
            time = 0f;
            interval = 200f;

            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    if (tilemap[y, x] == 1)
                        blocks.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 2)
                        slow.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 3)
                        slow.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 4)
                        spikes.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 5)
                        bounce.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 6)
                        slow.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 7)
                        shards.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 8)
                        bounce.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 9)
                        bridge.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 10)
                        pickup.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 11)
                        pickup2.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 12)
                        pickup3.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 13)
                        pickup4.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 14)
                        pickup5.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 15)
                        pickup6.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 16)
                        pickup7.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 17)
                        pickup8.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 18)
                        pickup9.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 19)
                        pickup10.Add(new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20));
                    if (tilemap[y, x] == 22)
                        levelshards.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                    if (tilemap[y, x] == 23)
                        shards.Add(new Rectangle(x * tilesize - mapX, y * tilesize, 35, 30));
                }
            }

            foreach (Rectangle block in blocks)
            {
                blocksTop.Add(new Rectangle(block.Center.X, block.Top, block.Width, block.Height));
                blocksLeft.Add(new Rectangle(block.Left, block.Y, 1, block.Height));
                blocksRight.Add(new Rectangle(block.Right, block.Y, block.Width / 2, block.Height));
                blocksBot.Add(new Rectangle(block.Center.X, block.Bottom, block.Width, block.Height));
            }

            foreach (Rectangle pick in pickup)
            {
                pickupTop.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickupLeft.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickupRight.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickupBot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }
            foreach (Rectangle pick in pickup2)
            {
                pickup2Top.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickup2Left.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickup2Right.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickup2Bot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }
            foreach (Rectangle pick in pickup3)
            {
                pickup3Top.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickup3Left.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickup3Right.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickup3Bot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }
            foreach (Rectangle pick in pickup4)
            {
                pickup4Top.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickup4Left.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickup4Right.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickup4Bot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }
            foreach (Rectangle pick in pickup5)
            {
                pickup5Top.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickup5Left.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickup5Right.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickup5Bot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }
            foreach (Rectangle pick in pickup6)
            {
                pickup6Top.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickup6Left.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickup6Right.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickup6Bot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }
            foreach (Rectangle pick in pickup7)
            {
                pickup7Top.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickup7Left.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickup7Right.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickup7Bot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }
            foreach (Rectangle pick in pickup8)
            {
                pickup8Top.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickup8Left.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickup8Right.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickup8Bot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }
            foreach (Rectangle pick in pickup9)
            {
                pickup9Top.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickup9Left.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickup9Right.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickup9Bot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }
            foreach (Rectangle pick in pickup10)
            {
                pickup10Top.Add(new Rectangle(pick.Center.X, pick.Top, pick.Width, pick.Height));
                pickup10Left.Add(new Rectangle(pick.Left, pick.Y, 1, pick.Height));
                pickup10Right.Add(new Rectangle(pick.Right, pick.Y, pick.Width / 2, pick.Height));
                pickup10Bot.Add(new Rectangle(pick.Center.X, pick.Bottom, pick.Width, pick.Height));
            }

            foreach (Rectangle bri in bridge)
            {
                bridgeTop.Add(new Rectangle(bri.Center.X, bri.Top, bri.Width, 10));
            }

            foreach (Rectangle spike in spikes)
            {
                spikesTop.Add(new Rectangle(spike.Center.X, spike.Top, spike.Width, spike.Height));
                spikesLeft.Add(new Rectangle(spike.Left, spike.Y, 1, spike.Height));
                spikesRight.Add(new Rectangle(spike.Right, spike.Y, spike.Width / 2, spike.Height));
                spikesBot.Add(new Rectangle(spike.Center.X, spike.Bottom, spike.Width, spike.Height));
            }

            foreach (Rectangle bo in bounce)
            {
                bounceTop.Add(new Rectangle(bo.Center.X, bo.Top, bo.Width, bo.Height));
                bounceLeft.Add(new Rectangle(bo.Left, bo.Y, 1, bo.Height));
                bounceRight.Add(new Rectangle(bo.Right, bo.Y, bo.Width / 2, bo.Height));
                bounceBot.Add(new Rectangle(bo.Center.X, bo.Bottom, bo.Width, bo.Height));
            }
            foreach (Rectangle slo in slow)
            {
                slowTop.Add(new Rectangle(slo.Center.X, slo.Top, slo.Width, slo.Height));
                slowLeft.Add(new Rectangle(slo.Left, slo.Y, 1, slo.Height));
                slowRight.Add(new Rectangle(slo.Right, slo.Y, slo.Width / 2, slo.Height));
                slowBot.Add(new Rectangle(slo.Center.X, slo.Bottom, slo.Width, slo.Height));
            }
            foreach (Rectangle sha in shards)
            {
                shardTop.Add(new Rectangle(sha.Center.X, sha.Top, sha.Width, sha.Height));
                shardLeft.Add(new Rectangle(sha.Left, sha.Y, 1, sha.Height));
                shardRight.Add(new Rectangle(sha.Right, sha.Y, sha.Width / 2, sha.Height));
                shardBot.Add(new Rectangle(sha.Center.X, sha.Bottom, sha.Width, sha.Height));
            }
            foreach (Rectangle sha in levelshards)
            {
                levelshardTop.Add(new Rectangle(sha.Center.X, sha.Top, sha.Width, sha.Height));
                levelshardLeft.Add(new Rectangle(sha.Left, sha.Y, 1, sha.Height));
                levelshardRight.Add(new Rectangle(sha.Right, sha.Y, sha.Width / 2, sha.Height));
                levelshardBot.Add(new Rectangle(sha.Center.X, sha.Bottom, sha.Width, sha.Height));
            }

            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    tile.Add(new Rectangle(x * tilesize, y * tilesize, tilesize, tilesize));
                }
            }

            MapW = tilesize * mapSizeX;
            MapH = tilesize * mapSizeY;
        }

        public void LoadContent(Texture2D tex1, Texture2D tex2, Texture2D tex3, Texture2D tex4, Texture2D tex5, Texture2D tex6, Texture2D tex7, Texture2D tex8, Texture2D tex9, Texture2D tex10, Texture2D tex11,
            Texture2D tex12, Texture2D tex13, Texture2D tex14, Texture2D tex15)
        {
            texlis.Add(tex1); // transparent tile
            texlis.Add(tex2); // basic level tile
            texlis.Add(tex1); // slow tile
            texlis.Add(tex3); // anim tex
            texlis.Add(tex5); //invisible bounce tile
            texlis.Add(tex1); //floating jump tilef
            texlis.Add(tex7); // slow tile
            texlis.Add(tex1); /// shard texture
            texlis.Add(tex1);  //ground jump tile
            texlis.Add(tex11); //bridge tile
            texlis.Add(tex1); //pickup1 tile
            texlis.Add(tex1); //pickup2 tile
            texlis.Add(tex1); //pickup3 tile
            texlis.Add(tex1); //pickup4 tile
            texlis.Add(tex1); //pickup5 tile
            texlis.Add(tex1); //pickup6 tile
            texlis.Add(tex1); //pickup7 tile
            texlis.Add(tex1); //pickup8 tile
            texlis.Add(tex1); //pickup9 tile
            texlis.Add(tex1); //pickup10 tile
            texlis.Add(tex1); //pillar
            texlis.Add(tex1); //stalagmite
            texlis.Add(tex1); //level shard
            texlis.Add(tex1); //inv portal
            shardtex = tex8; /// shard texture
            animtex = tex9; // anim tex
            fjtex = tex6;  //floating jump tile
            gjtex = tex10; //ground jump tile
            singleSlowT = tex3; /// single slow tile
            pickuptex = tex12; // pickup1 tile
            pillar = tex13;
            stalagmite = tex14;
            shardleveltex = tex15; // level shard
        }

        public void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Animation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    tileindex = tilemap[y, x];
                    tex = texlis[tileindex];

                    if (hit == false)
                    {
                        if (tilemap[y, x] == 7)
                            spriteBatch.Draw(shardtex, new Rectangle(x * tilesize - mapX, y * tilesize, tilesize, tilesize * 2), Color.White);
                    }
                    if (shardhit == false)
                    {
                        if (tilemap[y, x] == 22)
                            spriteBatch.Draw(shardleveltex, new Rectangle(x * tilesize - mapX, y * tilesize, tilesize, tilesize), source, Color.White);
                    }
                    if (pickuphit == false)
                    {
                        if (tilemap[y, x] == 10)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    if (pickup2hit == false)
                    {
                        if (tilemap[y, x] == 11)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    if (pickup3hit == false)
                    {
                        if (tilemap[y, x] == 12)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    if (pickup4hit == false)
                    {
                        if (tilemap[y, x] == 13)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    if (pickup5hit == false)
                    {
                        if (tilemap[y, x] == 14)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    if (pickup6hit == false)
                    {
                        if (tilemap[y, x] == 15)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    if (pickup7hit == false)
                    {
                        if (tilemap[y, x] == 16)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    if (pickup8hit == false)
                    {
                        if (tilemap[y, x] == 17)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    if (pickup9hit == false)
                    {
                        if (tilemap[y, x] == 18)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    if (pickup10hit == false)
                    {
                        if (tilemap[y, x] == 19)
                            spriteBatch.Draw(pickuptex, new Rectangle((x * tilesize - mapX) + 10, y * tilesize, 20, 20), source, Color.White);
                    }
                    spriteBatch.Draw(tex, new Rectangle(x * tilesize - mapX, y * tilesize, tilesize, tilesize), Color.White);

                    if (tilemap[y, x] == 3)
                        spriteBatch.Draw(singleSlowT, new Rectangle(x * tilesize - mapX, y * tilesize, tilesize, tilesize), Color.White);
                    if (tilemap[y, x] == 4)
                        spriteBatch.Draw(animtex, new Rectangle(x * tilesize - mapX, y * tilesize, tilesize, tilesize), source, Color.White);
                    if (tilemap[y, x] == 5)
                        spriteBatch.Draw(fjtex, new Rectangle(x * tilesize - mapX, y * tilesize, tilesize, tilesize), source, Color.White);
                    if (tilemap[y, x] == 6)
                        spriteBatch.Draw(tex, new Rectangle(x * tilesize - mapX, y * tilesize, 120, tilesize), Color.White);
                    if (tilemap[y, x] == 8)
                        spriteBatch.Draw(gjtex, new Rectangle(x * tilesize - mapX, y * tilesize, tilesize, tilesize), source, Color.White);
                    if (tilemap[y, x] == 20)
                        spriteBatch.Draw(pillar, new Rectangle(x * tilesize - mapX, y * tilesize, tilesize, 80), Color.White);
                    if (tilemap[y, x] == 21)
                        spriteBatch.Draw(stalagmite, new Rectangle(x * tilesize - mapX, y * tilesize, tilesize, 80), Color.White);
                }
            }
            spriteBatch.End();
        }

        public void Animation(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            currentFrame.Y = 0;

            frameSize = new Point(150, 150);
            if (time > interval)
            {
                currentFrame.X++;
                if (currentFrame.X > 5)
                {
                    currentFrame.X = 0;
                }

                source = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
                time = 0f;
            }
        }
    }
}