using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Kenaz
{
    public static class Utility {

        //containerSize = gameObject.GetComponentInParent<CanvasScaler>().referenceResolution + GetComponent<RectTransform>().sizeDelta;
        static public void SetImageWithContainerSize(ref Image img, ref Sprite spr, Vector2 containerSize)
        {
            img.sprite = spr;
            img.SetNativeSize();
            var scaleX = containerSize.x / img.rectTransform.sizeDelta.x;
            var scaleY = containerSize.y / img.rectTransform.sizeDelta.y;
            if (scaleX < scaleY)
            {
                img.rectTransform.sizeDelta = new Vector2(containerSize.x, img.rectTransform.sizeDelta.y * scaleX);
            }
            else
            {
                img.rectTransform.sizeDelta = new Vector2(img.rectTransform.sizeDelta.x * scaleY, containerSize.y);
            }
        }

        //orgSize = img.rectTransform.sizeDelta;
        static public void SetImageWithOriginSize(ref Image img, ref Sprite spr, Vector2 orgSize)
        {
            var x = spr.bounds.size.x * 100f;
            var y = spr.bounds.size.y * 100f;

            img.sprite = spr;
            var scaleX = orgSize.x / x;
            var scaleY = orgSize.y / y;

            if (scaleX < scaleY)
            {
                img.rectTransform.sizeDelta = new Vector2(orgSize.x, y * scaleX);
            }
            else
            {
                img.rectTransform.sizeDelta = new Vector2(x * scaleY, orgSize.y);
            }
        }

        /// <summary>
        /// 判斷是否為數字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        static public bool IsNumeric(object Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;
            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;
            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = double.TryParse(System.Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }

        static public string UnicodeToString(string srcText)
        {
            string dst = "";
            string src = srcText;
            int len = srcText.Length / 6;

            for (int i = 0; i <= len - 1; i++)
            {
                string str = "";
                str = src.Substring(0, 6).Substring(2);
                src = src.Substring(6);

                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());

                bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());

                dst += System.Text.Encoding.Unicode.GetString(bytes);
            }

            return dst;
        }
        
    }

}
