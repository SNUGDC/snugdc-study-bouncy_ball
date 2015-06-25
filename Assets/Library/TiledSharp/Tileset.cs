/* Distributed as part of TiledSharp, Copyright 2012 Marshall Ward
 * Licensed under the Apache License, Version 2.0
 * http://www.apache.org/licenses/LICENSE-2.0 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace TiledSharp
{
    // TODO: The design here is all wrong. A Tileset should be a list of tiles,
    //       it shouldn't force the user to do so much tile ID management

    public class Tileset : IElement
    {
        public int FirstGid {get; private set;}
        public string Name {get; private set;}
        public int TileWidth {get; private set;}
        public int TileHeight {get; private set;}
        public int Spacing {get; private set;}
        public int Margin {get; private set;}

        public List<TilesetTile> Tiles {get; private set;}
        public TmxTileOffset TileOffset {get; private set;}
        public PropertyDict Properties {get; private set;}
        public Image Image {get; private set;}
        public List<Terrain> Terrains {get; private set;}

        // TMX tileset element constructor
        public Tileset(XElement xTileset, Func<string, TextReader> resolver)
        {
            var xFirstGid = xTileset.Attribute("firstgid");
            var source = (string) xTileset.Attribute("source");

            if (source != null)
            {
                // source is always preceded by firstgid
                FirstGid = (int) xFirstGid;

                // Everything else is in the TSX file
                var xDocTileset = XDocument.Load(resolver(source));
                var ts = new Tileset(xDocTileset.Root, resolver);

                Name = ts.Name;
                TileWidth = ts.TileWidth;
                TileHeight = ts.TileHeight;
                Spacing = ts.Spacing;
                Margin = ts.Margin;
                TileOffset = ts.TileOffset;
                Image = ts.Image;
                Terrains = ts.Terrains;
                Tiles = ts.Tiles;
                Properties = ts.Properties;
            }
            else
            {
                // firstgid is always in TMX, but not TSX
                if (xFirstGid != null)
                    FirstGid = (int) xFirstGid;

                Name = (string) xTileset.Attribute("name");
                TileWidth = (int) xTileset.Attribute("tilewidth");
                TileHeight = (int) xTileset.Attribute("tileheight");
                Spacing = (int?) xTileset.Attribute("spacing") ?? 0;
                Margin = (int?) xTileset.Attribute("margin") ?? 0;

                TileOffset = new TmxTileOffset(xTileset.Element("tileoffset"));
                Image = new Image(xTileset.Element("image"));

                Terrains = new List<Terrain>();
                var xTerrainType = xTileset.Element("terraintypes");
                if (xTerrainType != null) {
                    foreach (var e in xTerrainType.Elements("terrain"))
                        Terrains.Add(new Terrain(e));
                }

                Tiles = new List<TilesetTile>();
                foreach (var xTile in xTileset.Elements("tile"))
                {
                    var tile = new TilesetTile(xTile, Terrains);
                    Tiles.Add(tile);
                }

                Properties = new PropertyDict(xTileset.Element("properties"));
            }
        }
    }

    public class TmxTileOffset
    {
        public int X {get; private set;}
        public int Y {get; private set;}

        public TmxTileOffset(XElement xTileOffset)
        {
            if (xTileOffset == null) {
                X = 0;
                Y = 0;
            } else {
                X = (int)xTileOffset.Attribute("x");
                Y = (int)xTileOffset.Attribute("y");
            }
        }
    }

    public class Terrain : IElement
    {
        public string Name {get; private set;}
        public int Tile {get; private set;}

        public PropertyDict Properties {get; private set;}

        public Terrain(XElement xTerrain)
        {
            Name = (string)xTerrain.Attribute("name");
            Tile = (int)xTerrain.Attribute("tile");
            Properties = new PropertyDict(xTerrain.Element("properties"));
        }
    }

    public class TilesetTile
    {
        public int Id {get; private set;}
        public List<Terrain> TerrainEdges {get; private set;}
        public double Probability {get; private set;}

        public PropertyDict Properties {get; private set;}
        public Image Image {get; private set;}
        public List<ObjectGroup> ObjectGroups {get; private set;}
        public List<TmxAnimationFrame> AnimationFrames {get; private set;}

        // Human-readable aliases to the Terrain markers
        public Terrain TopLeft {
            get { return TerrainEdges[0]; }
        }

        public Terrain TopRight {
            get { return TerrainEdges[1]; }
        }

        public Terrain BottomLeft {
            get { return TerrainEdges[2]; }
        }
        public Terrain BottomRight {
            get { return TerrainEdges[3]; }
        }

        public TilesetTile(XElement xTile, List<Terrain> Terrains)
        {
            Id = (int)xTile.Attribute("id");

            TerrainEdges = new List<Terrain>(4);

            int result;
            Terrain edge;

            var strTerrain = (string)xTile.Attribute("terrain") ?? ",,,";
            foreach (var v in strTerrain.Split(',')) {
                var success = int.TryParse(v, out result);
                if (success)
                    edge = Terrains[result];
                else
                    edge = null;
                TerrainEdges.Add(edge);
            }

            Probability = (double?)xTile.Attribute("probability") ?? 1.0;
            Image = new Image(xTile.Element("image"));

            ObjectGroups = new List<ObjectGroup>();
            foreach (var e in xTile.Elements("objectgroup"))
                ObjectGroups.Add(new ObjectGroup(e));

            AnimationFrames = new List<TmxAnimationFrame>();
            if (xTile.Element("animation") != null) {
                foreach (var e in xTile.Element("animation").Elements("frame"))
                    AnimationFrames.Add(new TmxAnimationFrame(e));
            }

            Properties = new PropertyDict(xTile.Element("properties"));
        }
    }

    public class TmxAnimationFrame
    {
        public int Id {get; private set;}
        public int Duration {get; private set;}

        public TmxAnimationFrame(XElement xFrame)
        {
            Id = (int)xFrame.Attribute("tileid");
            Duration = (int)xFrame.Attribute("duration");
        }
    }


}
