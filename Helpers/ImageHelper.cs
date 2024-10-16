using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AvaloniaApplication1.Helpers;

public class ImageHelper
{
    public static Bitmap Load(Uri uri)
    {
        return new Bitmap(AssetLoader.Open(uri));
    }
}