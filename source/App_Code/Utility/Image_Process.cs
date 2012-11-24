using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

/// <summary>
/// Summary description for Image_Process
/// </summary>
public class Image_Process
{
	public Image_Process()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    
      public static Bitmap CreateThumbnail(string lcFilename,int targetSize)
      {
          Bitmap loBMP = null;
          Bitmap bmpOut = null;
          try
          {
              loBMP = new Bitmap(lcFilename);
              ImageFormat loFormat = loBMP.RawFormat;
              Size newSize = CalculateDimensions(loBMP.Size, targetSize);
              bmpOut = new Bitmap(newSize.Width, newSize.Height);
              Graphics canvas = Graphics.FromImage(bmpOut);
              canvas.SmoothingMode = SmoothingMode.AntiAlias;
              canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
              canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
              canvas.DrawImage(loBMP, new Rectangle(new Point(0, 0), newSize));
          }
          catch (Exception ex)
          {
              
          }
          finally
          {
              loBMP.Dispose();
          }

          return bmpOut;
      }

      public static Bitmap CreateThumbnail(string lcFilename, int width, int height)
      {
          Bitmap loBMP = null;
          Bitmap bmpOut = null;
          try
          {
              loBMP = new Bitmap(lcFilename);
              ImageFormat loFormat = loBMP.RawFormat;
              Size newSize = new Size();
              newSize.Height = height;
              newSize.Width = width;
              bmpOut = new Bitmap(newSize.Width, newSize.Height);
              Graphics canvas = Graphics.FromImage(bmpOut);
              canvas.SmoothingMode = SmoothingMode.AntiAlias;
              canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
              canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
              canvas.DrawImage(loBMP, new Rectangle(new Point(0, 0), newSize));
          }
          catch (Exception ex)
          {

          }
          finally
          {
              loBMP.Dispose();
          }

          return bmpOut;
      }
        
      private static  Size CalculateDimensions(Size oldSize,int targetSize)
    {
         Size newSize = new Size();
        if (oldSize.Height > oldSize.Width)
        {
            newSize.Width = (int)(oldSize.Width * ( (float)targetSize / (float)oldSize.Height));
            newSize.Height = targetSize;
        }
        else
        {
            newSize.Width = targetSize;
            newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
   
        }
        return newSize;
    }
  
      public static void SaveJpeg(string path, Bitmap img, long quality)
    {
        // Encoder parameter for image quality
        EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

        // Jpeg image codec
        ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

        if (jpegCodec == null)
            return;

        EncoderParameters encoderParams = new EncoderParameters(1);
        encoderParams.Param[0] = qualityParam;

        img.Save(path, jpegCodec, encoderParams);
    }

      private static ImageCodecInfo getEncoderInfo(string mimeType)
    {
        // Get image codecs for all image formats
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

        // Find the correct image codec
        for (int i = 0; i < codecs.Length; i++)
            if (codecs[i].MimeType == mimeType)
                return codecs[i];
        return null;
    }

      // Generate thumbs and midthumbs from photos
      public static string Generate_Thumbs(string path, string thumbpath, string midthumbpath, int thumbwidth, int midthumbwidth)
      {
          Bitmap mp = null;
          Bitmap midmp = null;
          Copy_Photo(path, thumbpath);
          Copy_Photo(path, midthumbpath);
          // Generate Normal Thumb
          mp = Image_Process.CreateThumbnail(path, thumbwidth);
          midmp = Image_Process.CreateThumbnail(path, midthumbwidth);
          if (mp == null)
          {
              return "";
          }
          mp.Save(thumbpath);
          midmp.Save(midthumbpath);

          //***********************
          // Compress Thumb Photo
          //***********************
          // Mid thumbs
          Image_Process.SaveJpeg(midthumbpath, midmp, 60);
          // Thumbs
          Image_Process.SaveJpeg(thumbpath, mp, 90);

          mp.Dispose();
          midmp.Dispose();
          return "1";
      }

      private static void Copy_Photo(string original_path, string new_path)
      {
          FileInfo TheFile = new FileInfo(original_path);
          if (TheFile.Exists)
          {
              File.Copy(original_path, new_path);
          }
          else
          {
              throw new FileNotFoundException();
          }
      }

     
}
