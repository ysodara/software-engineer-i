using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.Mvc;
using System.Diagnostics;

namespace HW4.Controllers
{
    public class ColorInterpolatorController : Controller
    {  
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string FirstNum, string SecondNum, int? NumColors)
        {

            if (ModelState.IsValid)
            {


                int NotNullNum = 0;


                if (NumColors.HasValue)
                {
                    NotNullNum = NumColors.Value;
                }
                else
                {
                    return View();
                }

                NotNullNum = NotNullNum - 1;

                Color original1 = ColorTranslator.FromHtml(FirstNum);
                Color original2 = ColorTranslator.FromHtml(SecondNum);

                double HUE1;
                double SAT1;
                double VAL1;
                //ViewBag.TheMessage = "Got it!";

                ColorToHSV(original1, out HUE1, out SAT1, out VAL1);

                Color Copy1 = ColorFromHSV(HUE1, SAT1, VAL1);
                /*double HF = original1.GetHue();        // 212.0
                double SF = original1.GetSaturation(); // 0.6
                double VF = original1.GetBrightness(); // 0.49*/

                double HUE2;
                double SAT2;
                double VAL2;


                ColorToHSV(original2, out HUE2, out SAT2, out VAL2);

                Color Copy2 = ColorFromHSV(HUE2, SAT2, VAL2);

                /*double HSec = original2.GetHue();        
                double SSec = original2.GetSaturation(); 
                double VSec = original2.GetBrightness();*/

                //Initialize Step Size
                double stepH;
                double stepS;
                double stepV;

                if (HUE2 > HUE1)
                {
                    stepH = Math.Abs(HUE2 - HUE1) / (NotNullNum);
                }
                else
                {
                    stepH = Math.Abs(HUE2 - HUE1) / (NotNullNum) * -1;
                }

                if (SAT2 > SAT1)
                {
                    stepS = Math.Abs(SAT2 - SAT1) / (NotNullNum);
                }
                else
                {
                    stepS = Math.Abs(SAT2 - SAT1) / (NotNullNum) * -1;
                }

                if (VAL2 > VAL1)
                {
                    stepV = Math.Abs(VAL2 - VAL1) / (NotNullNum);
                }
                else
                {
                    stepV = Math.Abs(VAL2 - VAL1) / (NotNullNum) * -1;
                }

                //Debug.WriteLine(HUE1 + " " + SAT1 + " " + VAL1);
                //Debug.WriteLine(HSec + " " + SSec + " " + VSec);
                //Debug.WriteLine(stepH + " " + stepS + " " + stepV);
                //Create a list
                IList<ColorStrut> output = new List<ColorStrut>();
                string temphtmlColor;/* = ColorTranslator.ToHtml(Copy1);
                output.Add(new ColorStrut { htmlColor = temphtmlColor });*/

                for (int i = 0; i < NumColors; i++)
                {
                    double HueVal = HUE1 + (i * stepH);
                    double SatVal = SAT1 + (i * stepS);
                    double ValVal = VAL1 + (i * stepV);

                    Color tempColor = ColorFromHSV(HueVal, SatVal, ValVal);

                    temphtmlColor = ColorTranslator.ToHtml(tempColor);

                    output.Add(new ColorStrut { htmlColor = temphtmlColor });



                }

                /*temphtmlColor = ColorTranslator.ToHtml(Copy2);
                output.Add(new ColorStrut { htmlColor = temphtmlColor });*/

                ViewBag.ColorLists = output;
                ViewBag.Success = true;
                return View();

            }

            return View();
        }
        public struct ColorStrut
        {
            public string htmlColor { get; set; }
        }
        // From Greg's answer: https://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv/1626175
        // And Wikipedia: https://en.wikipedia.org/wiki/HSL_and_HSV
        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}