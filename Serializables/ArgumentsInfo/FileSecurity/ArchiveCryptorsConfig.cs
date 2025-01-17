using RipeLib.Serializables.ArgumentsInfo.Cryptor;

namespace RipeLib.Serializables.ArgumentsInfo.FileSecurity
{
/// <summary> The Info used in the <c>FileCryptors</c> Tasks. </summary>

public class ArchiveCryptorsConfig : ParamGroupInfo
{
/** <summary> Gets or Sets the Crypto Info related to the XOR Algorithm. </summary>
<returns> The XOR Digest Info. </returns> */

public GenericCryptoInfo XorCryptoInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the .NET Cryptors. </summary>
<returns> The .NET Crypto Info. </returns> */

public SpecificCryptoInfo DotNetCryptoInfo{ get; set; }

/// <summary> Creates a new Instance of the <c>FileCryptors/c>. </summary>

public ArchiveCryptorsConfig()
{
XorCryptoInfo = new GenericCryptoInfo();
DotNetCryptoInfo = new SpecificCryptoInfo();
}

/// <summary> Checks each nullable Field of this instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
ArchiveCryptorsConfig defaultConfig = new();

#region ======== Set default Values to Null Fields ========

XorCryptoInfo ??= defaultConfig.XorCryptoInfo;
DotNetCryptoInfo ??= defaultConfig.DotNetCryptoInfo;

#endregion
}

}

}