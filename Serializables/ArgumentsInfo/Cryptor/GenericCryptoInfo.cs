using System;

namespace RipeLib.Serializables.ArgumentsInfo.Cryptor
{
/// <summary> Groups Info related to common Params used for Ciphering Data. </summary>

public class GenericCryptoInfo : DataBlockInfo
{
/** <summary> Gets or Sets the Cipher Key used for Encrypting or Decrypting Data. </summary>
<returns> The Cipher Key. </returns> */

public byte[] CipherKey{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Cipher Keys should be Derived. </summary>

<remarks> In Order to generate Stronger Keys, this Field is Recommended to be always Set as <b>true</b>. </remarks>

<returns> <b>true</b if Keys should be Derived; otherwise, <b>false</b>. </returns> */

public bool DeriveKeys{ get; set; }

/** <summary> Gets or Sets the Salt Value used for Reinforcing the Cipher Key. </summary>
<returns> The Salt Value. </returns> */

public byte[] SaltValue{ get; set; }

/** <summary> Gets or Sets the Hash Type used for Protecting the Cipher Key. </summary>
<returns> The Hash Type. </returns> */

public string HashType{ get; set; }

/** <summary> Gets or Sets the number of Iterations perfomed when Generating strongest Keys. </summary>
<returns> The number of Iterations. </returns> */

public uint? IterationsCount{ get; set; }

/// <summary> Creates a new Instance of the <c>GenericCryptoInfo</c>. </summary>

public GenericCryptoInfo()
{
CipherKey = Console.InputEncoding.GetBytes("<Enter a Cipher Key>");
SaltValue = Console.InputEncoding.GetBytes("<Enter a Salt Value>");

HashType = "MD5";
IterationsCount = 1;

DeriveKeys = true;
}

///<summary> Checks each nullable Field of the <c>GenericCryptoInfo</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
GenericCryptoInfo defaultInfo = new();

#region ======== Set default Values to Null Fields ========

CipherKey ??= defaultInfo.CipherKey;

#endregion

BufferSizeForIOTasks = BufferSizeRange.CheckParamRange(BufferSizeForIOTasks);
}

}

}