using System;
using System.Drawing;
using System.Drawing.Imaging;

public static class ImageCropper
{
    private static int _tolerance;
    private static Color _baseColor;
    private static Bitmap _baseImage;
    private static Bitmap _image;
    public static int Tolerance
    {
        get { return _tolerance; }
    }
    public static Color BaseColor
    {
        get { return _baseColor; }
    }
    public static Bitmap Image
    {
        get { return _image; }
    }

    static ImageCropper()
    {
        _image = new(1, 1);
        _baseImage = new(1, 1);
    }

    public static (Bitmap, Bitmap) CropImage(Bitmap image, Label label, int tolerance)
    {
        _tolerance = tolerance;
        _image = image;
        _baseImage = image;
        Bitmap transparentCrop;
        try
        {
            FindBaseColor();
            if (_baseColor == Color.Empty)
            {
                label.Text = "No borders found!";
                return (_image, _image);
            }
            transparentCrop = ReduceImageBorders();
            label.Text = $"New size: {_image.Width}x{_image.Height}";
        }
        catch (Exception e)
        {
            label.Text = $"Error loading image: {e.Message}";
            return (new Bitmap(1, 1), new Bitmap(1, 1));
        }
        return (_image, transparentCrop);
    }

    static void FindBaseColor()
    {
        Color topLeft = _image.GetPixel(0, 0);
        Color bottomRight = _image.GetPixel(_image.Width - 1, _image.Height - 1);

        _baseColor = topLeft;
        Color firstRow = GetRowColColorUniform(0, 0, "row");
        if (firstRow != Color.Empty)
        {
            return;
        }
        Color firstCol = GetRowColColorUniform(0, 0, "col");
        if (firstCol != Color.Empty)
        {
            return;
        }
        _baseColor = bottomRight;
        Color lastRow = GetRowColColorUniform(_image.Height - 1, 0, "row");
        if (lastRow != Color.Empty)
        {
            return;
        }
        _baseColor = GetRowColColorUniform( 0, _image.Width - 1, "col");
    }

    static Color GetRowColColorUniform(int row, int col, string direction)
    {
        for (int i = 0; i < (direction == "row" ? _image.Width : _image.Height); ++i)
        {
            Color currentColor = (direction == "row") ? _image.GetPixel(i, row) : _image.GetPixel(col, i);
            if (!AreColorsSimilar(currentColor, _baseColor))
            {
                return Color.Empty;
            }
        }
        return _baseColor;
    }

    static bool AreColorsSimilar(Color color1, Color color2)
    {
        int deltaR = Math.Abs(color1.R - color2.R);
        int deltaG = Math.Abs(color1.G - color2.G);
        int deltaB = Math.Abs(color1.B - color2.B);
        return deltaR <= _tolerance && deltaG <= _tolerance && deltaB <= _tolerance;
    }

    static Bitmap ReduceImageBorders()
    {
        int width = _image.Width;
        int height = _image.Height;
        int topRowsToRemove = 0;
        int bottomRowsToRemove = 0;
        int leftColsToRemove = 0;
        int rightColsToRemove = 0;

        for (int r = 0; r < height; ++r)
        {
            if (GetRowColColorUniform(r, 0, "row") == _baseColor)
            {
                ++topRowsToRemove;
            }
            else
            {
                break;
            }
        }

        for (int r = height - 1; r >= 0; --r)
        {
            if (GetRowColColorUniform(r, 0, "row") == _baseColor)
            {
                ++bottomRowsToRemove;
            }
            else
            {
                break;
            }
        }

        for (int c = 0; c < width; ++c)
        {
            if (GetRowColColorUniform(0, c, "col") == _baseColor)
            {
                ++leftColsToRemove;
            }
            else
            {
                break;
            }
        }

        for (int c = width - 1; c >= 0; --c)
        {
            if (GetRowColColorUniform(0, c, "col") == _baseColor)
            {
                ++rightColsToRemove;
            }
            else
            {
                break;
            }
        }
        int newWidth = width - leftColsToRemove - rightColsToRemove;
        int newHeight = height - topRowsToRemove - bottomRowsToRemove;
        if (newWidth > 0 && newHeight > 0)
        {
            _image = _image.Clone(new Rectangle(leftColsToRemove, topRowsToRemove, newWidth, newHeight), _image.PixelFormat);
        }
        return AddTransparency(leftColsToRemove, topRowsToRemove, newWidth, newHeight);
    }

    static Bitmap AddTransparency(int leftColsToRemove, int topRowsToRemove, int newWidth, int newHeight)
    {
        if (newWidth > 0 && newHeight > 0)
        {
            Bitmap modifiedImage = new(_baseImage);
            Rectangle interiorRegion = new(leftColsToRemove, topRowsToRemove, newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(modifiedImage))
            {
                using Region region = new(interiorRegion);
                g.ExcludeClip(region);
                using SolidBrush transparentBrush = new(Color.FromArgb(128, 255, 255, 255));
                g.FillRectangle(transparentBrush, 0, 0, modifiedImage.Width, modifiedImage.Height);
            }
            return modifiedImage;
        }
        return _baseImage;
    }
}