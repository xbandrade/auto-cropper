using System;
using System.Drawing;
using System.Drawing.Imaging;

public static class ImageCropper
{
    private static int _tolerance;
    private static Color _baseColor;
    public static int Tolerance
    {
        get { return _tolerance; }
        set { _tolerance = value; }
    }
    public static Color BaseColor
    {
        get { return _baseColor; }
        set { _baseColor = value; }
    }
    public static Image CropImage(string imagePath, Label label, int tolerance)
    {
        Bitmap bitmap;
        _tolerance = tolerance;
        try
        {
            bitmap = new(imagePath);
            FindBaseColor(bitmap);
            if (_baseColor == Color.Empty)
            {
                label.Text = "No borders found!";
                return bitmap;
            }
            label.Text = $"New size: {bitmap.Width}x{bitmap.Height}";
            bitmap = ReduceImageBorders(bitmap,  label);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading image: {e.Message}");
            label.Text = $"Error loading image: {e.Message}";
            return new Bitmap(1, 1);
        }
        return bitmap;
    }

    static void FindBaseColor(Bitmap image)
    {
        Color topLeft = image.GetPixel(0, 0);
        Color bottomRight = image.GetPixel(image.Width - 1, image.Height - 1);

        _baseColor = topLeft;
        Color firstRow = GetRowColColorUniform(image, 0, 0, "row");
        if (firstRow != Color.Empty)
        {
            return;
        }
        Color firstCol = GetRowColColorUniform(image, 0, 0, "col");
        if (firstCol != Color.Empty)
        {
            return;
        }
        _baseColor = bottomRight;
        Color lastRow = GetRowColColorUniform(image, image.Height - 1, 0, "row");
        if (lastRow != Color.Empty)
        {
            return;
        }
        _baseColor = GetRowColColorUniform(image, 0, image.Width - 1, "col");
    }

    static Color GetRowColColorUniform(Bitmap image, int row, int col, string direction)
    {
        for (int i = 0; i < (direction == "row" ? image.Width : image.Height); i++)
        {
            Color currentColor = (direction == "row") ? image.GetPixel(i, row) : image.GetPixel(col, i);
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

    static Bitmap ReduceImageBorders(Bitmap image, Label label)
    {
        int width = image.Width;
        int height = image.Height;
        int topRowsToRemove = 0;
        int bottomRowsToRemove = 0;
        int leftColsToRemove = 0;
        int rightColsToRemove = 0;

        for (int r = 0; r < height; r++)
        {
            if (GetRowColColorUniform(image, r, 0, "row") == _baseColor)
            {
                topRowsToRemove++;
            }
            else
            {
                break;
            }
        }

        for (int r = height - 1; r >= 0; r--)
        {
            if (GetRowColColorUniform(image, r, 0, "row") == _baseColor)
            {
                bottomRowsToRemove++;
            }
            else
            {
                break;
            }
        }

        for (int c = 0; c < width; c++)
        {
            if (GetRowColColorUniform(image, 0, c, "col") == _baseColor)
            {
                leftColsToRemove++;
            }
            else
            {
                break;
            }
        }

        for (int c = width - 1; c >= 0; c--)
        {
            if (GetRowColColorUniform(image, 0, c, "col") == _baseColor)
            {
                rightColsToRemove++;
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
            image = image.Clone(new Rectangle(leftColsToRemove, topRowsToRemove, newWidth, newHeight), image.PixelFormat);
        }
        return image;
    }
}