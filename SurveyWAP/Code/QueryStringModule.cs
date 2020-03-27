namespace Votations.NSurvey.WebAdmin.Code
{

#region Using

using System;
using System.IO;
using System.Web;
using System.Text;
using System.Security.Cryptography;

#endregion

/// <summary>
/// Summary description for QueryStringModule
/// </summary>
public class QueryStringModule : IHttpModule
{

  #region IHttpModule Members

  public void Dispose()
  {
    // Nothing to dispose
  }

  public void Init(HttpApplication context)
  {
    context.BeginRequest += new EventHandler(context_BeginRequest);
    //context.EndRequest += new EventHandler(context_EndRequest);

        }

  #endregion

  private const string PARAMETER_NAME = "enc=";
  private const string ENCRYPTION_KEY = "key";

  void context_BeginRequest(object sender, EventArgs e)
  {
    HttpContext context = HttpContext.Current;
    if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?"))
    {
      string query = ExtractQuery(context.Request.RawUrl);
      string path = GetVirtualPath();

      if (query.StartsWith(PARAMETER_NAME, StringComparison.OrdinalIgnoreCase))
      {
        // Decrypts the query string and rewrites the path.
        string rawQuery = query.Replace(PARAMETER_NAME, string.Empty);
        string decryptedQuery = Decrypt(rawQuery);
        context.RewritePath(path, string.Empty, decryptedQuery);
      }
      else if (context.Request.HttpMethod == "GET")
      {
        // Encrypt the query string and redirects to the encrypted URL.
        // Remove if you don't want all query strings to be encrypted automatically.
        string encryptedQuery = Encrypt(query);
        context.Response.Redirect(path + encryptedQuery);
      }
    }
  }

        // NOT USED
        void context_EndRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Response.IsRequestBeingRedirected)
            {
                HttpContext.Current.Response.RedirectLocation = Encrypt(HttpContext.Current.Response.RedirectLocation);
            }
            else
            {
            }
        }


        /// <summary>
        /// Parses the current URL and extracts the virtual path without query string.
        /// </summary>
        /// <returns>The virtual path of the current URL.</returns>
        private static string GetVirtualPath()
  {
    string path = HttpContext.Current.Request.RawUrl;
    path = path.Substring(0, path.IndexOf("?"));
    path = path.Substring(path.LastIndexOf("/") + 1);
    return path;
  }

  /// <summary>
  /// Parses a URL and returns the query string.
  /// </summary>
  /// <param name="url">The URL to parse.</param>
  /// <returns>The query string without the question mark.</returns>
  private static string ExtractQuery(string url)
  {
    int index = url.IndexOf("?") + 1;
    return url.Substring(index);
  }

  #region Encryption/decryption

  /// <summary>
  /// The salt value used to strengthen the encryption.
  /// </summary>
  private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());

  /// <summary>
  /// Encrypts any string using the Rijndael algorithm.
  /// Used by HttpModule web.config
  /// </summary>
  /// <param name="inputText">The string to encrypt.</param>
  /// <returns>A Base64 encrypted string.</returns>
  public static string Encrypt(string inputText)
  {
    RijndaelManaged rijndaelCipher = new RijndaelManaged();
    byte[] plainText = Encoding.Unicode.GetBytes(inputText);
    PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

    using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        {
          cryptoStream.Write(plainText, 0, plainText.Length);
          cryptoStream.FlushFinalBlock();
          return "?" + PARAMETER_NAME + Convert.ToBase64String(memoryStream.ToArray());
        }
      }
    }
  }

        /// <summary>
        /// Encrypts any string using the Rijndael algorithm.
        /// Used to encrypt voterid in url queries - UInavigator
        /// </summary>
        /// <param name="inputText">The string to encrypt.</param>
        /// <returns>A Base64 encrypted string.</returns>
        public static string EncryptString(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }


        /// <summary>
        /// Decrypts a previously encrypted string.
        /// Used by HttpModule - web.confi
        /// </summary>
        /// <param name="inputText">The encrypted string to decrypt.</param>
        /// <returns>A decrypted string.</returns>
        public static string Decrypt(string inputText)
  {
    RijndaelManaged rijndaelCipher = new RijndaelManaged();
           byte[] encryptedData = Convert.FromBase64String(inputText);

            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

    using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
    {
      using (MemoryStream memoryStream = new MemoryStream(encryptedData))
      {
        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
        {
          byte[] plainText = new byte[encryptedData.Length];
          int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
          return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
        }
      }
    }
  }

        /// <summary>
        /// Decrypts a previously encrypted string.
        /// Used to encrypt voterid in url queries - UInavigator
        /// </summary>
        /// <param name="inputText">The encrypted string to decrypt.</param>
        /// <returns>A decrypted string.</returns>
        public static string DecryptString(string inputText)
        {
            string text = HttpUtility.UrlDecode(inputText);
            text = text.Replace(" ", "+");

            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(text);

            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }

        #endregion

    }

}
