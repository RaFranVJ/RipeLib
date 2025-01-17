namespace RipeLib.Serializables.ArgumentsInfo.FileSecurity
{
/// <summary> The used in the <c>FileHashers</c> Tasks. </summary>

public class ArchiveHashersConfig : ParamGroupInfo
{
/** <summary> Gets or Sets the Provider that will Perform the Cipher Operation. </summary>
<returns> The Provider Name. </returns> */

public string ProviderName{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if the Strings should be Hashed with HMAC
(Hash-based Message Authentication Code). </summary>

<returns> The Auth Mode. </returns> */

public bool UseHmac{ get; set; }

/** <summary> Gets or Sets the Auth Code used for Generating HMAC Digest. </summary>
<returns> The Auth Code. </returns> */

public byte[] AuthCode{ get; set; }

/** <summary> Gets or Sets the Case to Apply to Hashed Strings. </summary>
<returns> The Hashed String Case. </returns> */

public StringCase HashedStringCase{ get; set; }

/// <summary> Creates a new Instance of the <c>FileHashersArgs/c>. </summary>

public ArchiveHashersConfig()
{
ProviderName = "MD5";
HashedStringCase = StringCase.Lower;
}

/// <summary> Checks each nullable Field of the <c>ArchiveHashersConfig</c> instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
ArchiveHashersConfig defaultConfig = new();

#region ======== Set default Values to Null Fields ========

ProviderName ??= defaultConfig.ProviderName;

#endregion
}

}

}