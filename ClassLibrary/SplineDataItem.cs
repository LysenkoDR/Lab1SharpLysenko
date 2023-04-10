namespace ClassLibrary
{
    public struct SplineDataItem
    {
        public double Coord { get; set; }
        public double Spline { get; set; }
        public double SplineDeriv { get; set; }
        public double SplineDoubleDeriv { get; set; }

        public SplineDataItem(double coord, double spline, double splineDeriv, double splineDoubleDeriv)
        {
            this.Coord = coord;
            this.Spline = spline;
            this.SplineDeriv = splineDeriv;
            this.SplineDoubleDeriv = splineDoubleDeriv;
        }

        public override string ToString()
        {
            return string.Format("Coord={0}, Spline={1}, Der'={2}",
                                 Coord.ToString("F2"),
                                 Spline.ToString("F2"),
                                 SplineDeriv.ToString("F2"));
        }

        public string ToString(string format)
        {
            return string.Format("Coord={0}, Spline={1}, Der'={2}, Der''={3}",
                                 Coord.ToString(format),
                                 Spline.ToString(format),
                                 SplineDeriv.ToString(format),
                                 SplineDoubleDeriv.ToString(format));
        }
        //public override string ToString()
        //{
        //    return $"X = {Coord}, Y = {Spline}, Y1 = {SplineDeriv}, Y2 = {SplineDoubleDeriv}";
        //}

        //public string ToString(string format)
        //{
        //    return $"X = {Coord.ToString(format)}, Y = {Spline.ToString(format)}, Y1 = {SplineDeriv.ToString(format)}, Y2 = {SplineDoubleDeriv.ToString(format)}";
        //}

        //public string R
        //{
        //    get { return this.ToString(); }
        //}
    }
}
