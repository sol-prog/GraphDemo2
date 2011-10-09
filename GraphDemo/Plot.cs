using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;


namespace GraphDemo
{
    class Plot
    {
        public Plot(Read rr, ComboBox xBox, ComboBox yBox, Chart chart)
        {
            int indX = xBox.SelectedIndex;
            int indY = yBox.SelectedIndex;
            float[,] data = rr.get_Data();
            int nLines = rr.get_nLines();
            int nColumns = rr.get_nColumns();
            string []header = rr.get_Header();

            chart.Series.Clear(); //ensure that the chart is empty
            chart.Series.Add("Series0");
            chart.Series[0].ChartType = SeriesChartType.Line;
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{F2}";
            chart.ChartAreas[0].AxisX.Title = header[indX];
            chart.ChartAreas[0].AxisY.Title = header[indY];

            float x_grids = 6;
            float[] ext = get_Extrema(data, nLines, nColumns, indX);
            chart.ChartAreas[0].AxisX.MajorGrid.Interval = (ext[1] - ext[0]) / x_grids;
            chart.ChartAreas[0].AxisX.LabelStyle.Interval = (ext[1] - ext[0]) / x_grids;

            chart.Legends.Clear();
            for (int j = 0; j < nLines; j++)
            {
                chart.Series[0].Points.AddXY(data[j, indX], data[j, indY]);
            }
        }

        private float []get_Extrema(float[,] data,int nL,int nC,int idx)
        {
            float min = data[0, idx],max = data[0, idx];
            float[] res = new float[2];
            for (int i = 1; i < nL; i++)
            {
                if (min > data[i, idx]) min = data[i, idx];
                if (max < data[i, idx]) max = data[i, idx];
            }
            res[0] = min; res[1] = max;
            return res;
        }
    }
}
