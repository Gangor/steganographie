﻿using System.Drawing;

namespace Steganographie
{
    public static class Utils
    {
        public static bool IsImage(string filePath)
        {
            try
            {
                using (var image = Image.FromFile(filePath))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
