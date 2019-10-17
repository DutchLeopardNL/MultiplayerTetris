using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.Data
{
	public class FileIO
	{
		public static Dictionary<string, int> Read(string path, string file)
		{
			try
			{
				using (Stream stream = File.Open($"{path}/{file}", FileMode.Open))
				{
					var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					return (Dictionary<string, int>)binaryFormatter.Deserialize(stream);
				}
			}
			catch (DirectoryNotFoundException)
			{
				Directory.CreateDirectory(path);
				return Read(path, file);
			}
			catch (FileNotFoundException)
			{
				using (File.Create($"{path}/{file}"))
				{
					return new Dictionary<string, int>();
				}
			}
		}

		public static void Write(string path, string file, Dictionary<string, int> objectToWrite)
		{
			using (Stream stream = File.Open($"{path}/{file}", FileMode.Append))
			{
				var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				binaryFormatter.Serialize(stream, objectToWrite);
			}
		}

	}
}
