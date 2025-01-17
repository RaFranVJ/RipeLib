using System;
using System.Collections.Generic;
using System.IO;
using RipeLib.Modules;

/// <summary> Serves as a Tool for Serializing other Classes inherited from this. </summary>

[Serializable]

public abstract class SerializableClass<T> where T : class
{
/// <summary> The Object TypeName </summary>

protected static readonly string ObjTypeName = typeof(T).Name;

/// <summary> The Extension for Serialized Files </summary>

protected const string SerializedFileExt = ".json";

/** <summary> Gets the Parent Dir where Serialized Files will be Saved. </summary>
<returns> The Parent Dir. </returns> */

protected virtual string ParentDir => Path.Combine(LibInfo.CurrentDllDirectory, "Storage", "Serializables");

/** <summary> Gets a Reference to the Current Parent Dir. </summary>
<returns> A reference to the Parent Dir. </returns> */

public string GetParentDir() => ParentDir;

/** <summary> Gets the Path where the Serialized File should be Saved. </summary>
<returns> The File Path. </returns> */

protected virtual string FilePath => Path.Combine(ParentDir, ObjTypeName + SerializedFileExt);

/** <summary> Gets a Reference to the Current File Path. </summary>
<returns> A reference to the File Path. </returns> */

public string GetFilePath() => FilePath;

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

protected virtual void CheckForNullFields()
{
return;
}

/** <summary> Serializes the Instance into a JSON String. </summary>
<returns> The Class Serialized into a String. </returns> */

public virtual string SerializeToJson() => JsonSerializer.SerializeObject(this);

/// <summary> Deserializes the given JSON String as a C# Object. </summary>

public virtual T DeserializeFromJson(string targetStr) => JsonSerializer.DeserializeObject<T>(targetStr);

// Handle Missing Folders

protected void CreateDefaultDir(string sourcePath)
{
DirManager.CheckMissingFolder( Path.GetDirectoryName(sourcePath) );

WriteObject();
}

/** <summary> Reads a JSON File and Deserializes its Content. </summary>

<param name = "sourcePath"> The Path where to Read the File (default is already Set). </param>

<returns> The Object Read. </returns> */

public virtual T ReadObject(string sourcePath = default)
{
sourcePath ??= FilePath;

DirManager.CheckMissingFolder(Path.GetDirectoryName(sourcePath) );

if(!File.Exists(sourcePath) || FileManager.FileIsEmpty(sourcePath) )
WriteObject(sourcePath);

T targetObj = DeserializeFromJson(File.ReadAllText(sourcePath) );
CheckForNullFields();

return targetObj;
}

/** <summary> Reads a Container of JSON Files and Deserializes the Content of each one. </summary>

<param name = "sourcePath"> The Path to the Container (default is already Set). </param>

<returns> A List of Objects Read. </returns> */

public List<T> ReadObjects(string sourcePath = default)
{
sourcePath ??= ParentDir;

List<T> objsList = new();

if(!Directory.Exists(sourcePath) || DirManager.FolderIsEmpty(sourcePath) )
CreateDefaultDir(sourcePath);

string[] filesAvailable = DirManager.GetFileSystems(sourcePath, true, new("*.json") );

foreach(string singleFile in filesAvailable)
{
T objectRead = ReadObject(singleFile);

objsList.Add(objectRead);
}

return objsList;
}

/** <summary> Reads a Container of JSON Files and Deserializes the Content of each one. </summary>

<param name = "sourcePath"> The Path to the Container (default is already Set). </param>

<returns> A Dictionary that Maps the Objects Read with their Paths. </returns> */

public Dictionary<string, T> ReadObjectsAsMap(string sourcePath = default)
{
sourcePath ??= ParentDir;

Dictionary<string, T> mappedObjs = new();

if(!Directory.Exists(sourcePath) || DirManager.FolderIsEmpty(sourcePath) )
CreateDefaultDir(sourcePath);

string[] filesAvailable = DirManager.GetFileSystems(sourcePath, true, new("*.json") );

foreach(string singleFile in filesAvailable)
{
T objectRead = ReadObject(singleFile);

mappedObjs.Add(singleFile, objectRead);
}

return mappedObjs;
}

/// <summary> Serializes the Providen Object and Writes it to a JSON File. </summary>

public virtual void WriteObject(string targetPath = default)
{
targetPath ??= FilePath;

CheckForNullFields();

DirManager.CheckMissingFolder( Path.GetDirectoryName(targetPath) );

File.WriteAllText(targetPath, SerializeToJson() );
}

}