using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;

namespace Application.Extensions
{
    public static class ImageHash
    {
        /// <summary>
        /// Computes a SHA256 hash for the given image with consistent encoding and no metadata.
        /// </summary>
        /// <param name="image">The image to hash.</param>
        /// <returns>The hash of the image as a hexadecimal string.</returns>
        public static string ComputeImageHash(this Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "Image cannot be null.");
            }

            // Remove EXIF metadata if present (to ensure consistency)
            RemoveExifMetadata(image);

            using (var memoryStream = new MemoryStream())
            {
                // Save the image to a memory stream in a lossless PNG format (ImageSharp's PNG encoder)
                image.Save(memoryStream, new PngEncoder());

                // Get the byte array of the image.
                byte[] imageBytes = memoryStream.ToArray();

                // Compute the hash
                using (var sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(imageBytes);

                    // Convert the hash to a hexadecimal string
                    var stringBuilder = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        stringBuilder.Append(b.ToString("x2"));
                    }

                    return stringBuilder.ToString();
                }
            }
        }

        /// <summary>
        /// Removes EXIF metadata from the image to ensure consistent hashing.
        /// </summary>
        private static void RemoveExifMetadata(Image image)
        {
            // ImageSharp does not have EXIF metadata manipulation, 
            // but we can strip all metadata before hashing to ensure consistency
            image.Metadata.ExifProfile = null;
            image.Metadata.IccProfile = null; // Remove ICC profile as well to prevent variations in hash
        }
    }
}
