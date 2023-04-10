using ClassLibrary;
using System;
using System.Windows;

namespace WpfAppLab1
{
    public class ViewData
    {
        //параметры для объекта RawData
        public double[] BindLeftAndRightEndPoint { get; set; }
        //double LeftEndPoint = (double)bindLeftAndRightEndPoint[0];
        //public double RightEndPoint { get; set; }
        public int BindNumberGridNodes { get; set; }
        public bool BindIsUniform { get; set; }

        public FRawEnum BindFunctionFRawEnum { get; set; }
        public FRaw F { get; set; }
        //параметры для объекта SplineData
        public int BindNumberSplineGridNodes { get; set; }
        public double BindLeftDeriv { get; set; }
        public double BindRightDeriv { get; set; }

        // Ссылки на объекты RawData и SplineData
        public RawData? RawData;
        public SplineData? SplineData { get; set; }
        
        public ViewData() { }
     
        public ViewData(RawData raw)
        {
            //LeftEndPoint= raw.LeftEndPoint;
            //RightEndPoint= raw.RightEndPoint;
            BindLeftAndRightEndPoint[0] = raw.LeftEndPoint;
            BindLeftAndRightEndPoint[1] = raw.RightEndPoint;


            BindNumberGridNodes = raw.NumberGridNodes;
            BindIsUniform = raw.IsUniform;
            F = raw.F;
            //bindFunctionFRawEnum = FRawEnum.Linear;
            //NumberSplineGridNodes = 20;
            //LeftDeriv = 0;
            //RightDeriv = 1;
        }
        public void CalculateSplines()
        {
            try
            {
                //// для способа 1
                switch (BindFunctionFRawEnum)
                {
                    case FRawEnum.Linear:
                        F = RawData.Linear;
                        break;
                    case FRawEnum.Cubic:
                        F = RawData.Cubic;
                        break;
                    case FRawEnum.Random:
                        F = RawData.Random;
                        break;
                }
                //switch (Ftype)
                //{
                //    case FRawEnum.Linear:
                //        F = RawData.Linear;
                //        break;
                //    case FRawEnum.Cubic:
                //        F = RawData.Cubic;
                //        break;
                //    case FRawEnum.Random:
                //        F = RawData.Random;
                //        break;
                //}
                // Создаем объекты RawData и SplineData
                RawData = new RawData(BindLeftAndRightEndPoint[0], BindLeftAndRightEndPoint[1], BindNumberGridNodes, BindIsUniform, F);
                SplineData = new SplineData(RawData, BindLeftDeriv, BindRightDeriv, BindNumberSplineGridNodes);
                SplineData.MakeMKLSpline();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"!!!!!!!!!!!!!! {ex}",
                        "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void Save(string filename)
        {
            try
            {
                //CalculateSplines();
                if (RawData != null)
                {
                    RawData.Save(filename);
                }
                else
                {
                    MessageBox.Show("Ошибка! Нет RawData");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные в файле: " + ex.Message, "Ошибка");
            }

        }
        public void Load(string filename)
        {
            try
            {
                RawData.Load(filename, out RawData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить данные из файла" + ex.Message, "Ошибка");
            }
        }
    }
}
