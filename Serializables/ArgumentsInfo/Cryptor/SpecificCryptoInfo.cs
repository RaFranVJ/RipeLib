using System.IO;
using System.Security.Cryptography;

namespace RipeLib.Serializables.ArgumentsInfo.Cryptor
{
/// <summary> Groups Info related to specific Params used for Ciphering Data. </summary>

public class SpecificCryptoInfo : GenericCryptoInfo
{
/** <summary> Gets or Sets the Provider that will Perform the Cipher Operation. </summary>
<returns> The Provider Name. </returns> */

public string ProviderName{ get; set; }

/** <summary> Gets or Sets the <c>CipherMode</c> used for Encrypting or Decrypting Data. </summary>
<returns> The Ciphering Mode. </returns> */

public CipherMode CipheringMode{ get; set; }

/** <summary> Gets or Sets the <c>PaddingMode</c> used for Encrypting or Decrypting Data. </summary>
<returns> The Padding Mode. </returns> */

public PaddingMode DataPadding{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Random IVS should be Derived. </summary>

<remarks> In Order to generate Stronger IVS, this Field is Recommended to be always Set as <b>true</b>. </remarks>

<returns> <b>true</b if IVS should be Randomized; otherwise, <b>false</b>. </returns> */

public bool RandomizeIVS{ get; set; }

/** <summary> Gets or Sets the Init Vector used for Ciphering Data. </summary>

<remarks> This Field is Optional. If <c>null</c> a new IV will be Set from the Cipher Key or randomly </remarks>

<returns> The IV. </returns> */

public byte[] IV{ get; set; }

/** <summary> Gets or Sets a Path to the Folder that Contains the Cipher Key. </summary>
<returns> The Key Container. </returns> */

public string KeyContainer{ get; set; }

/** <summary> Gets or Sets a Path to the Folder that Contains the IV. </summary>
<returns> The IV Container. </returns> */

public string IVContainer{ get; set; }

/// <summary> Creates a new Instance of the <c>SpecificCryptoInfo</c>. </summary>

public SpecificCryptoInfo()
{
ProviderName = "Aes";
CipheringMode = CipherMode.CBC;

DataPadding = PaddingMode.Zeros;
RandomizeIVS = true;

KeyContainer = Path.Combine(GetBaseDir(), "CipherKeys");
IVContainer = Path.Combine(GetBaseDir(), "IVS");
}

/// <summary> Checks each nullable Field of the <c>SpecificCryptoInfo</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
SpecificCryptoInfo defaultInfo = new();

#region ======== Set default Values to Null Fields ========

ProviderName ??= defaultInfo.ProviderName;
KeyContainer ??= defaultInfo.KeyContainer;
IVContainer ??= defaultInfo.IVContainer;

#endregion

PathHelper.CheckExistingPath(KeyContainer, CipherKey == null);
PathHelper.CheckExistingPath(IVContainer, RandomizeIVS);
}

protected static string GetBaseDir() => Path.Combine(LibInfo.CurrentDllDirectory, "Storage", "CryptoInfo");
}

}