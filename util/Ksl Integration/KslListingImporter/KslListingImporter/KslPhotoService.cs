using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Rentler.Facades;
using System.Text.RegularExpressions;
using System.Xml;

namespace KslListingImporter
{
	public static class KslPhotoService
	{
		//http://stage-v2.ksl.com/api/xml.php?k=Uf5fJ972x7843CZ&sid=17509839

		public static void DownloadPhotos(int buildingId, int sid)
		{
			Console.WriteLine("Building Id: {0}, sid: {1}", buildingId, sid);
			var client = new WebClient();

			//grab the xml doc
			string xmlString = "";

			WebRequest request = WebRequest.Create("http://stage-v2.ksl.com/api/xml.php?k=Uf5fJ972x7843CZ&sid=" + sid.ToString());
			request.Timeout = 30 * 60 * 1000;
			WebResponse response = null;
			try
			{
				response = (WebResponse)request.GetResponse();
			}
			catch(Exception exc)
			{
				Console.WriteLine(exc.Message);
				return;
			}

			using(var s = response.GetResponseStream())
			{
				var stream = new StreamReader(s, Encoding.UTF8);
				var reader = XmlReader.Create(stream, new XmlReaderSettings());

				try
				{
					reader.Read();
					xmlString = reader.ReadOuterXml();
				}
				catch(XmlException exc)
				{
					Console.WriteLine(exc.Message);
					Console.WriteLine("Skipping listing...");
					xmlString = "err /Story_Not_Found\n";
				}
			}

			//no images
			if(xmlString.Equals("err /Story_Not_Found\n"))
				return;

			List<string> urls = new List<string>();
			var xmlPhotos = XDocument.Parse(xmlString)
				.Element("story").Element("body");

			xmlPhotos = xmlPhotos.Element("media");
			if(xmlPhotos == null)
				return;
			xmlPhotos = xmlPhotos.Element("photos");
			if(xmlPhotos == null)
				return;
			var photos = xmlPhotos.Elements("item");

			//make sure the media element is actually there,
			//since sometimes Ksl leaves empty photo elements floating around.
			foreach(var item in photos)
				if(item.Descendants("media").Any())
					urls.Add(item.Element("media").Attribute("src").Value);

			//need to support jpg, gif, probably png
			//story.body.media.photos -> item.media.src

			//load images
			Bitmap bitmap = null;
			foreach(var item in urls)
			{
				//download image
				try
				{
					Stream stream = client.OpenRead(item);
					bitmap = new Bitmap(stream);
					stream.Flush();
					stream.Close();
				}
				catch(Exception exc)
				{
					//usually a 404, just skip the image.
					Console.WriteLine(exc.Message);
					continue;
				}

				//figure out what kind of image it is
				ImageFormat format = null;
				if(bitmap.RawFormat.Equals(ImageFormat.Jpeg))
					format = ImageFormat.Jpeg;
				if(bitmap.RawFormat.Equals(ImageFormat.Png))
					format = ImageFormat.Png;
				if(bitmap.RawFormat.Equals(ImageFormat.Gif))
					format = ImageFormat.Gif;

				if(format != null)
					//convert bitmap to byte[], now that we have the image format
					using(MemoryStream ms = new MemoryStream())
					{
						bool worked = true;
						try
						{
							bitmap.Save(ms, format);
							ms.Position = 0;
						}
						catch(Exception exc) 
						{
							worked = false;
							Console.WriteLine(exc.Message);
						}

						if(worked)
						{
							//clean up
							bitmap.Dispose();
							bitmap = null;

							//upload it!
							ImageHelper.ProcessPhoto(
								buildingId, ms.GetBuffer(), format,
								Path.GetExtension(item).ToLower(), new BuildingFacade(), new PhotoFacade());
						}
					}
			}
		}
	}
}
