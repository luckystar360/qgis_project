using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comacExport
{
    public static class CustomPosition
    {
        static Point rotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (double)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (double)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        /// <summary>
        /// Tìm 1 điểm H giữa 1 đoạn thẳng cách điểm thứ nhất 1 đoạn s
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="sencondPoint"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        static Point findPointonLine(Point firstPoint, Point sencondPoint, double s)
        {
            double xD = firstPoint.X;
            double yD = firstPoint.Y;
            double xC = sencondPoint.X;
            double yC = sencondPoint.Y;

            double constant = s * (yC - yD)  / Math.Sqrt((xD - xC) * (xD - xC) + (yC - yD) * (yC - yD));

            double yH1 = yD - constant;
            double xH1 = ((xD * yC - xC * yD) - (xD - xC) * yH1) / (yC - yD);

            if ((yH1 - yD) * (yC - yD) < 0)
            {
                yH1 = yD + constant;
                xH1 = ((xD * yC - xC * yD) - (xD - xC) * yH1) / (yC - yD);
            }

            return new Point(xH1, yH1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="sencondPoint"></param>
        /// <param name="angle"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Point findPointwithAngleandDistance(Point firstPoint, Point sencondPoint,double angle, double s)
        {
            double xD = firstPoint.X;
            double yD = firstPoint.Y;
            double xC = sencondPoint.X;
            double yC = sencondPoint.Y;

            Point AB = findPointonLine(firstPoint, sencondPoint, s);

            if (xC>xD)
            {
                return rotatePoint(AB, firstPoint, angle);
            }    
            else
            {
                return rotatePoint(AB, firstPoint, -angle);
            }    
        }
    }
}
