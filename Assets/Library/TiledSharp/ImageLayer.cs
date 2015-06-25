// Distributed as part of TiledSharp, Copyright 2012 Marshall Ward
// Licensed under the Apache License, Version 2.0
// http://www.apache.org/licenses/LICENSE-2.0
using System.Xml.Linq;

namespace TiledSharp
{
    public class ImageLayer : IElement
    {
        public string Name {get; private set;}
        public int Width {get; private set;}
        public int Height {get; private set;}

        public bool Visible {get; private set;}
        public double Opacity {get; private set;}

        public Image Image {get; private set;}

        public PropertyDict Properties {get; private set;}

        public ImageLayer(XElement xImageLayer)
        {
            Name = (string) xImageLayer.Attribute("name");
            Width = (int) xImageLayer.Attribute("width");
            Height = (int) xImageLayer.Attribute("height");
            Visible = (bool?) xImageLayer.Attribute("visible") ?? true;
            Opacity = (double?) xImageLayer.Attribute("opacity") ?? 1.0;

            Image = new Image(xImageLayer.Element("image"));

            Properties = new PropertyDict(xImageLayer.Element("properties"));
        }
    }
}
