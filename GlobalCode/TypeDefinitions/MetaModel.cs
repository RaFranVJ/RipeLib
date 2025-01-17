using RipeLib;
using RipeLib.Serializables.ArgumentsInfo.FileManager.Metadata;

/// <summary> Represents an Object that Handles the Importation or Exportation of Metadata from Files. </summary>

public class MetaModel<T> : SerializableClass<T> where T : class
{
/** <summary> Gets a Path used for Data Importaton. </summary>

<param name = "basePath"> The Path to the File where the Info should be Loaded from. </param>

<returns> The Path to the Info. </returns> */

public static string ResolvePath(string basePath, MetadataImportParams importCfg = default)
{
importCfg ??= new();

if(!string.IsNullOrEmpty(importCfg.PathToMetadataFile) )
return importCfg.PathToMetadataFile;

return PathHelper.BuildPathFromDir(importCfg.MetadataContainerPath, basePath, SerializedFileExt, ObjTypeName);
}

/** <summary> Gets a Path used for Data Exportation. </summary>

<param name = "basePath"> The Path to the File where the Info should be Saved. </param>

<returns> The Path to the Info. </returns> */

public static string ResolvePath(string basePath, MetadataExportParams exportCfg = default)
{
exportCfg ??= new();

if(!string.IsNullOrEmpty(exportCfg.PathToMetadataFile) )
return exportCfg.PathToMetadataFile;

return PathHelper.BuildPathFromDir(exportCfg.MetadataContainerPath, basePath, SerializedFileExt, ObjTypeName);
}

}