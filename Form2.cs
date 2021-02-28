using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace computer_graphics_project
{
    public partial class algorithms : Form
    {
        float x1_xc, y1_yc, x2_rx, y2_ry;
        int dir = 1, dir2 = -1;
        int left = 0, right = 0, top = 0, buttom = 0;
        public algorithms()
        {
            InitializeComponent();
        }

        /*
         
        about the project :- 
        in first 4 textbox we can ues this textboxs to inter the data from the users
        if we chose from the groupbox dda algprithm the 4 textbox we can consider them the 2 point
        and if we chose from the groupbox circle algprithm we can consider the first 2 textbox the center of 
        the circle and the third textbox is the radius the same applies to the rest of Algorithms 

        the same thing with Translation, Scale, Rotation and Clipping

         */


        // DDA algorithm
        public void draw_1(Bitmap bp, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1, dy = y2 - y1;
            float step = Math.Max(Math.Abs(dx), Math.Abs(dy));
            float xinc = dx / step, yinc = dy / step;
            float x = x1, y = y1;
            for (int i = 0; i <= step; i++)
            {
                int paint_x = (int)Math.Round(x);
                int paint_y = (int)Math.Round(y);

                // That condition regards clipping
                if (paint_x >= left && paint_x <= right && paint_y >= top && paint_y <= buttom)
                    bp.SetPixel(paint_x, paint_y, Color.Red);

                x += xinc;
                y += yinc;
            }
            ptrbox.Image = bp;
        }

        // bresenham algorithm
        public void draw_2(Bitmap bp, int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            float slope = (float)dy / dx;

            // if the slope is lower than 1
            if (slope < 1)
            {
                int p = 2 * dy - dx;
                int x = x1, y = y1, xend = x2;
                if (x1 > x2)
                {
                    x = x2;
                    y = y2;
                    xend = x1;
                }
                // That condition regards clipping
                if (x >= left && x <= right && y >= top && y <= buttom)
                    bp.SetPixel(x, y, Color.Red);
                while (x <= xend)
                {
                    x++;
                    if (p < 0)
                        p += (2 * dy);
                    else
                    {
                        y++;
                        p += (2 * (dy - dx));
                    }
                    // That condition regards clipping
                    if (x >= left && x <= right && y >= top && y <= buttom)
                        bp.SetPixel(x, y, Color.Red);
                }
                ptrbox.Image = bp;
            }
            else
            {
                int p = 2 * dx - dy;
                int x = x1, y = y1, yend = y2;
                if (y1 > y2)
                {
                    x = x2;
                    y = y2;
                    yend = y1;
                }
                // That condition regards clipping
                if (x >= left && x <= right && y >= top && y <= buttom)
                    bp.SetPixel(x, y, Color.Red);
                //string s = "";
                while (y <= yend)
                {
                    y++;
                    if (p < 0)
                        p += (2 * dx);
                    else
                    {
                        x++;
                        p += (2 * (dx - dy));
                    }
                    // That condition regards clipping
                    if (x >= left && x <= right && y >= top && y <= buttom)
                        bp.SetPixel(x, y, Color.Red);
                    //s += (x + " " + y + "\n");
                }
                //MessageBox.Show(s);
                ptrbox.Image = bp;
            }
        }

        // circle algorithm
        public void draw_3(Bitmap bp, int xc, int yc, int r)
        {
            int p = 1 - r, x = 0, y = r;
            while (x <= y)
            {
                x++;
                if (p < 0)
                    p += 2 * x + 1;
                else
                {
                    y--;
                    p += 2 * x + 1 - 2 * y;
                }
                bp.SetPixel(xc + x, yc + y, Color.Red);
                bp.SetPixel(xc - x, yc - y, Color.Red);
                bp.SetPixel(xc + x, yc - y, Color.Red);
                bp.SetPixel(xc - x, yc + y, Color.Red);

                bp.SetPixel(xc + y, yc + x, Color.Red);
                bp.SetPixel(xc - y, yc - x, Color.Red);
                bp.SetPixel(xc + y, yc - x, Color.Red);
                bp.SetPixel(xc - y, yc + x, Color.Red);
            }
            ptrbox.Image = bp;
        }

        // ellipse algorithm
        public void draw_4(Bitmap bp, int xc, int yc, int rx, int ry)
        {
            // regoin 1
            double pp = (ry * ry) - (rx * rx * ry) + (rx * rx * 0.25);
            int p1 = (int)pp;
            int x = 0, y = ry;
            while ((2 * rx * rx * y) > (2 * ry * ry * x))
            {
                bp.SetPixel(xc + x, yc + y, Color.Red);
                bp.SetPixel(xc - x, yc - y, Color.Red);
                bp.SetPixel(xc - x, yc + y, Color.Red);
                bp.SetPixel(xc + x, yc - y, Color.Red);

                x++;
                if (p1 < 0)
                    p1 += (2 * ry * ry * x) + (ry * ry);
                else
                {
                    y--;
                    p1 += (2 * ry * ry * x) - (2 * rx * rx * y) + (ry * ry);
                }
            }

            // regoin 2
            pp = ((ry * ry) * (x + 0.5) * (x + 0.5)) + ((rx * rx) * (y - 1) * (y - 1)) - (rx * rx * ry * ry);
            int p2 = (int)pp;
            while (y >= 0)
            {
                bp.SetPixel(xc + x, yc + y, Color.Red);
                bp.SetPixel(xc - x, yc - y, Color.Red);
                bp.SetPixel(xc - x, yc + y, Color.Red);
                bp.SetPixel(xc + x, yc - y, Color.Red);

                y--;
                if (p2 > 0)
                    p2 -= (2 * rx * rx * y) + (rx * rx);
                else
                {
                    x++;
                    p2 += (2 * ry * ry * x) - (2 * rx * rx * y) + (rx * rx);
                }
            }
            ptrbox.Image = bp;
        }

        // rotat
        public void rotat(double ceta)
        {
            double[,] rotate = { { Math.Cos(ceta), Math.Sin(ceta) * dir, 0 },
                                 { Math.Sin(ceta) * dir2, Math.Cos(ceta), 0 },
                                 { 0, 0, 1 } };
            double[,] p1_zer0 = tras_to_zero((x1_xc + x2_rx) / 2, (y1_yc + y2_ry) / 2);

            double newddx1 = x1_xc + p1_zer0[0, 2];
            double newddx2 = x2_rx + p1_zer0[0, 2];

            double newddy1 = y1_yc + p1_zer0[1, 2];
            double newddy2 = y2_ry + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((x1_xc + x2_rx) / -2, (y1_yc + y2_ry) / -2);

            double[,] test = matrix33_X_matrix33(come_back, rotate);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { x1_xc, y1_yc, 1 };
            double[] point_2_before = { x2_rx, y2_ry, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            x1_xc = (int)point_1_after_rotate[0];
            y1_yc = (int)point_1_after_rotate[1];
            x2_rx = (int)point_2_after_rotate[0];
            y2_ry = (int)point_2_after_rotate[1];
        }
        double[,] tras_to_zero(double x, double y)
        {
            double[,] trans = { { 1, 0, -1 * x }, { 0, 1, -1 * y }, { 0, 0, 1 } };
            return trans;
        }
        double[,] matrix33_X_matrix33(double[,] v1, double[,] v2)
        {
            double[,] res = new double[3, 3];
            for (int i = 0; i < 9; i++)
                for (int col = 0; col < 3; col++)
                    res[i / 3, i % 3] += v1[i / 3, col] * v2[col, i % 3];
            return res;
        }
        double[] matrix33_X_matrix31(double[,] v1, double[] v2)
        {
            double[] res = new double[3];
            for (int i = 0; i < 9; i++)
                res[i / 3] += v1[i / 3, i % 3] * v2[i % 3];
            return res;
        }

        // Clipping
        void clipping(int x_min, int y_min, int x_max, int y_max)
        {
            string code1 = "", code2 = "";
            code1 += (y1_yc > y_max ? "1" : "0");
            code1 += (y1_yc < y_min ? "1" : "0");
            code1 += (x1_xc > x_max ? "1" : "0");
            code1 += (x1_xc < x_min ? "1" : "0");

            code2 += (y1_yc > y_max ? "1" : "0");
            code2 += (y1_yc < y_min ? "1" : "0");
            code2 += (x1_xc > x_max ? "1" : "0");
            code2 += (x1_xc < x_min ? "1" : "0");

            // or operation
            int result1 = 0;
            for (int i = 0; i < 4; i++)
                result1 += (code1[i] - '0' + code2[i] - '0') > 0 ? 1 : 0;
            if (result1 == 0)
                return;

            // and operation
            int result2 = 0;
            for (int i = 0; i < 4; i++)
                result2 += ((code1[i] - '0') * (code2[i] - '0')) > 0 ? 1 : 0;
            if (result2 != 0)
            {
                x1_xc = 0;
                y1_yc = 0;
                x2_rx = 0;
                y2_ry = 0;
                return;
            }

            int new_x1 = 0, new_x2 = 0, new_y1 = 0, new_y2 = 0;
            solve1(code1, ref new_x1, ref new_y1, x_min, y_min, x_max, y_max);
            solve1(code2, ref new_x2, ref new_y2, x_min, y_min, x_max, y_max);
            x1_xc = new_x1;
            y1_yc = new_y1;
            x2_rx = new_x2;
            y2_ry = new_y2;
        }
        public void solve1(string code1, ref int x, ref int y, int x_min, int y_min, int x_max, int y_max)
        {
            double slope = (double)(y2_ry - y1_yc) / (x2_rx - x1_xc);

            for (int i = 3; i >= 0; i--)
            {
                if (code1[i] - '0' == 1)
                {
                    x = x_min;
                    y = (int)Math.Round((slope * (x - x1_xc)) + y1_yc);
                    break;
                }
                else
                if (code1[i] - '0' == 1)
                {
                    x = x_max;
                    y = (int)Math.Round((slope * (x - x1_xc)) + y1_yc);
                    break;
                }
                else
                if (code1[i] - '0' == 1)
                {
                    y = y_min;
                    x = (int)Math.Round(((y - y1_yc) / slope) + x1_xc);
                    break;
                }
                else
                if (code1[i] - '0' == 1)
                {
                    y = y_max;
                    x = (int)Math.Round(((y - y1_yc) / slope) + x1_xc);
                    break;
                }
            }
        }


        //********************* Draw 1 **************************//
        private void button2_Click(object sender, EventArgs e)
        {
            x1_xc = int.Parse(txt1.Text);
            y1_yc = int.Parse(txt2.Text);
            x2_rx = int.Parse(txt3.Text);
            y2_ry = int.Parse(txt4.Text);
            right = ptrbox.Width;
            buttom = ptrbox.Height;
            left = 0;
            top = 0;
            try
            {
                Bitmap bp = new Bitmap(ptrbox.Width, ptrbox.Height);
                if (radioB_1.Checked)
                    draw_1(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx, (int)y2_ry);
                else
                if (radioB_2.Checked)
                    draw_2(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx, (int)y2_ry);
                else
                if (radioB_3.Checked)
                    draw_3(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx);
                else
                if (radioB_4.Checked)
                    draw_4(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx, (int)y2_ry);
            }
            catch (Exception ex)
            {
                MessageBox.Show("please inter correct data");
            }
        }

        //********************* Redraw **************************//
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                right = ptrbox.Width;
                buttom = ptrbox.Height;
                left = 0;
                top = 0;
                // Translation
                float tx = float.Parse(txt5.Text);
                float ty = float.Parse(txt6.Text);

                // Rotation
                float ceta = (float)(double.Parse(txt9.Text) * Math.PI / 180.0);

                // Scaling
                float sx = tx;
                float sy = ty;
                if (sx <= 0) sx = 1;
                if (sy <= 0) sy = 1;

                if (chbox1.Checked)
                {
                    if (radioB_3.Checked || radioB_4.Checked)
                    {
                        x1_xc += tx;
                        y1_yc += ty;
                    }
                    else
                    {
                        x1_xc += tx;
                        x2_rx += tx;
                        y1_yc += ty;
                        y2_ry += ty;
                    }
                }
                else
                if (chbox2.Checked)
                {
                    if (radioB_3.Checked || radioB_4.Checked)
                    {
                        x2_rx *= sx;
                        y2_ry *= sy;
                    }
                    else
                    {
                        x1_xc *= sx;
                        x2_rx *= sx;
                        y1_yc *= sy;
                        y2_ry *= sy;
                    }
                }
                else
                if (chbox3.Checked)
                    rotat(ceta);


                Bitmap bp = new Bitmap(ptrbox.Width, ptrbox.Height);
                if (radioB_1.Checked)
                    draw_1(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx, (int)y2_ry);
                else
                if (radioB_2.Checked)
                    draw_2(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx, (int)y2_ry);
                else
                if (radioB_3.Checked)
                    draw_3(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx);
                else
                if (radioB_4.Checked)
                    draw_4(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx, (int)y2_ry);

            }
            catch (Exception ex)
            {
                MessageBox.Show("please inter correct data");
            }
        }

        //********************* Draw 2 **************************//
        private void button3_Click_1(object sender, EventArgs e)
        {

            try
            {
                int xmin = int.Parse(txtb1.Text);
                int ymin = int.Parse(txtb2.Text);
                int xmax = int.Parse(txtb3.Text);
                int ymax = int.Parse(txtb4.Text);
                Bitmap bp = new Bitmap(ptrbox.Width, ptrbox.Height);
                //clipping(xmin, ymin, xmax, ymax);
                left = xmin;
                right = xmax;
                top = ymax;
                buttom = ymin;

                if (radioB_1.Checked)
                    draw_1(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx, (int)y2_ry);
                else
                if (radioB_2.Checked)
                    draw_2(bp, (int)x1_xc, (int)y1_yc, (int)x2_rx, (int)y2_ry);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // back button
        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Intro alg = new Intro();
            alg.Show();
        }

    }
}