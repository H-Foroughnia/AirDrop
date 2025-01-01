using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace EndPoint.ExtensionMethodes;

public static class ImageHash
{
    /// <summary>
    /// Computes a SHA256 hash for the given image.
    /// </summary>
    /// <param name="image">The image to hash.</param>
    /// <returns>The hash of the image as a hexadecimal string.</returns>
    public static string ComputeImageHash(this Image image)
    {
        if (image == null)
        {
            throw new ArgumentNullException(nameof(image), "Image cannot be null.");
        }

        using (var memoryStream = new MemoryStream())
        {
            // Save the image to a memory stream in a standardized format (e.g., PNG).
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

            // Get the byte array of the image.
            byte[] imageBytes = memoryStream.ToArray();

            // Compute the hash.
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(imageBytes);

                // Convert the hash to a hexadecimal string.
                var stringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}