// Distributed as part of TiledSharp, Copyright 2012 Marshall Ward
// Licensed under the Apache License, Version 2.0
// http://www.apache.org/licenses/LICENSE-2.0

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

namespace TiledSharp
{
	public struct Tuple<T, U>
	{
		public T First;
		public U Second;
	}

	public struct Tuple
	{
		public static Tuple<T, U> Create<T, U>(T first, U second)
		{
			return new Tuple<T, U> {First = first, Second = second};
		}
	}

	public interface IElement
	{
		string Name { get; }
	}

	public class PropertyDict : Dictionary<string, string>
	{
		public PropertyDict(XElement xmlProp)
		{
			if (xmlProp == null) return;

			foreach (var p in xmlProp.Elements("property"))
			{
				var pname = p.Attribute("name").Value;
				var pval = p.Attribute("value").Value;
				Add(pname, pval);
			}
		}
	}

	public class Image
	{
		public string Source { get; private set; }
		public string Format { get; private set; }
		public Stream Data { get; private set; }
		public Color Trans { get; private set; }
		public int? Width { get; private set; }
		public int? Height { get; private set; }

		public Image(XElement xImage)
		{
			if (xImage == null) return;

			var xSource = xImage.Attribute("source");

			if (xSource != null)
				// Append directory if present
				Source = (string)xSource;
			else {
				Format = (string)xImage.Attribute("format");
				var xData = xImage.Element("data");
				var decodedStream = new Base64Data(xData);
				Data = decodedStream.Data;
			}

			Trans = new Color(xImage.Attribute("trans"));
			Width = (int?)xImage.Attribute("width");
			Height = (int?)xImage.Attribute("height");
		}
	}

	public struct Color
	{
		public int R;
		public int G;
		public int B;

		public Color(XAttribute xColor)
		{
			if (xColor == null)
			{
				R = G = B = 0;
				return;
			}

			var colorStr = ((string)xColor).TrimStart("#".ToCharArray());

			R = int.Parse(colorStr.Substring(0, 2), NumberStyles.HexNumber);
			G = int.Parse(colorStr.Substring(2, 2), NumberStyles.HexNumber);
			B = int.Parse(colorStr.Substring(4, 2), NumberStyles.HexNumber);
		}
	}

	public class Base64Data
	{
		public Stream Data { get; private set; }

		public Base64Data(XElement xData)
		{
			if ((string)xData.Attribute("encoding") != "base64")
				throw new Exception(
					"TmxBase64Data: Only Base64-encoded data is supported.");

			var rawData = Convert.FromBase64String((string)xData.Value);
			Data = new MemoryStream(rawData, false);

			var compression = (string)xData.Attribute("compression");
			if (compression == "gzip")
				Data = new GZipStream(Data, CompressionMode.Decompress, false);
			else if (compression == "zlib")
				Data = new Ionic.Zlib.ZlibStream(Data,
						Ionic.Zlib.CompressionMode.Decompress, false);
			else if (compression != null)
				throw new Exception("TmxBase64Data: Unknown compression.");
		}
	}
}
