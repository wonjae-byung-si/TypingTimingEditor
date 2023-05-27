using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataFile<T> where T : new()
{
	protected string path;
	protected FileStream fileStream;

	protected T data; // on memory
	public T Data
	{
		get => data;
		set
		{
			data = value;
		}
	}

	public DataFile()
	{
		this.path = null;
	}

	public DataFile(string path)
	{
		this.path = path;

		do
		{
			FileStream fileStream = File.Create(path);
			fileStream.Close();
		} while (!File.Exists(path));
	}

	public static DataFile<T> From(string path)
	{
		DataFile<T> file = new DataFile<T>();
		file.path = path;
		file.Load();

		return file;
	}

	// overwrite file contents to var data
	public virtual void Save()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		using(FileStream fileStream = File.OpenWrite(path))
		{
			if(Data == null)
			{
				Data = new T();
				Debug.LogWarning("Set file contents to default");
			}

			formatter.Serialize(fileStream, Data);
		}
	}

	// overwrite var data to file contents
	public virtual void Load()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		using (FileStream fileStream = File.OpenRead(path))
		{
			Data = (T)formatter.Deserialize(fileStream);
		}
	}
}
