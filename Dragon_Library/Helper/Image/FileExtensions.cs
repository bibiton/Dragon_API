using System;
using System.Drawing;
using System.IO;

namespace Dragon_Library.Helper.Image
{
    public static class FileExtensions
	{
		/// <summary>
		/// 處理圖片函式，參數：name來源圖面路徑，Path_s輸出圖面路徑，Width指定要縮的寬度，Hight指定要縮的高度，Texts版權文字
		/// </summary>
		/// <param name="Path">來源圖面路徑</param>
		/// <param name="Path_s">輸出圖面路徑</param>
		/// <param name="width">指定要縮的寬度</param>
		/// <param name="height">指定要縮的高度</param>
		/// <param name="Texts">版權文字</param>
		public static void GenerateThumbnailImage(Stream File, string Path_s, int width, int height, string Texts)
		{
			MemoryStream ms = new MemoryStream();
			File.CopyTo(ms);
			System.Drawing.Image originalImage = System.Drawing.Image.FromStream(ms);
            
			int x = 0;
			int y = 0;
			int ow = originalImage.Width;
			int oh = originalImage.Height;
            int towidth = ow;
            int toheight = oh;

            if (ow > width || oh > height)
            {
                if (ow > oh)
                {
                    toheight = originalImage.Height * width / originalImage.Width;
                    towidth = width;
                }
                else
                {
                    towidth = originalImage.Width * height / originalImage.Height;
                    toheight = height;
                }
            }

			System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			g.Clear(System.Drawing.Color.Transparent);

			g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);

			if (!string.IsNullOrEmpty(Texts))
			{
				//自動調整文字最適大小 
				Graphics canvas = Graphics.FromImage(bitmap);
				int[] sizes = new int[] {
			16,
			14,
			12,
			10,
			8,
			6,
			4
		};
				System.Drawing.Font crFont = null;
				SizeF crSize = new SizeF();

				for (int i = 0; i <= 6; i++)
				{
					crFont = new System.Drawing.Font("arial", sizes[i], FontStyle.Bold);
					crSize = canvas.MeasureString(Texts, crFont);

					if (Convert.ToUInt16(crSize.Width) < Convert.ToUInt16(bitmap.Width))
					{
						break; // TODO: might not be correct. Was : Exit For
					}
				}

				// 指定文字位置 
				int yPixlesFromBottom = Convert.ToInt32((bitmap.Height * 0.05));
				float yPosFromBottom = ((bitmap.Height - yPixlesFromBottom) - (crSize.Height / 2));
				float xCenterOfImg = (bitmap.Width / 2);

				// 指定文字格式(置中對齊) 
				StringFormat StrFormat = new StringFormat();
				StrFormat.Alignment = StringAlignment.Center;

				// 指定文字筆刷(製造陰影效果) 
				SolidBrush semiTransBrush = new SolidBrush(System.Drawing.Color.FromArgb(153, 255, 255, 255));
				SolidBrush semiTransBrush2 = new SolidBrush(System.Drawing.Color.FromArgb(153, 0, 0, 0));

				canvas.DrawString(Texts, crFont, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), StrFormat);
				canvas.DrawString(Texts, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);
			}


			try
			{
				bitmap.Save(Path_s, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			catch (System.Exception e)
			{
				throw e;
			}
			finally
			{
				originalImage.Dispose();
				bitmap.Dispose();
				g.Dispose();
			}
		}

        /// <summary>
        /// 處理圖片函式，並回傳串流，參數：name來源圖面路徑，Path_s輸出圖面路徑，Width指定要縮的寬度，Hight指定要縮的高度，Texts版權文字
        /// </summary>
        /// <param name="File"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="Texts"></param>
        /// <returns></returns>
        public static Stream GenerateThumbnailImage(Stream File, int width, int height, string Texts)
        {
            MemoryStream ms = new MemoryStream();
            File.CopyTo(ms);
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(ms);
            
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            int towidth = ow;
            int toheight = oh;

            if (ow > width || oh > height)
            {
                if (ow > oh)
                {
                    toheight = originalImage.Height * width / originalImage.Width;
                    towidth = width;
                }
                else
                {
                    towidth = originalImage.Width * height / originalImage.Height;
                    toheight = height;
                }
            }

            System.Drawing.Image bitmap = new Bitmap(towidth, toheight);

            Graphics g = Graphics.FromImage(bitmap);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.Clear(Color.Transparent);

            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);

            if (!string.IsNullOrEmpty(Texts))
            {
                //自動調整文字最適大小 
                Graphics canvas = Graphics.FromImage(bitmap);
                int[] sizes = new int[] {
            16,
            14,
            12,
            10,
            8,
            6,
            4
        };
                Font crFont = null;
                SizeF crSize = new SizeF();

                for (int i = 0; i <= 6; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);
                    crSize = canvas.MeasureString(Texts, crFont);

                    if (Convert.ToUInt16(crSize.Width) < Convert.ToUInt16(bitmap.Width))
                    {
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

                // 指定文字位置 
                int yPixlesFromBottom = Convert.ToInt32((bitmap.Height * 0.05));
                float yPosFromBottom = ((bitmap.Height - yPixlesFromBottom) - (crSize.Height / 2));
                float xCenterOfImg = (bitmap.Width / 2);

                // 指定文字格式(置中對齊) 
                StringFormat StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Center;

                // 指定文字筆刷(製造陰影效果) 
                SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
                SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

                canvas.DrawString(Texts, crFont, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), StrFormat);
                canvas.DrawString(Texts, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);
            }


            try
            {
                //bitmap.Save(Path_s, Imaging.ImageFormat.Jpeg);
                var stream = new MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;
                return stream;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }
}
