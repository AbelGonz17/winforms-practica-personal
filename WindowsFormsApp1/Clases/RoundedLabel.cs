using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedLabel : UserControl
{
    private int borderRadius = 15;
    private int borderSize = 2;
    private Color borderColor = Color.Gray;
    private ContentAlignment textAlign = ContentAlignment.MiddleCenter;
    private bool multiline = true;

    public RoundedLabel()
    {
        this.Size = new Size(150, 60);
        this.DoubleBuffered = true;
        this.BackColor = Color.MediumSlateBlue;
        this.ForeColor = Color.White;
    }

    // ================= PROPIEDADES =================

    [Category("Rounded Label")]
    public bool Multiline
    {
        get => multiline;
        set { multiline = value; Invalidate(); }
    }

    [Category("Rounded Label")]
    public int BorderRadius
    {
        get => borderRadius;
        set { borderRadius = value; Invalidate(); }
    }

    [Category("Rounded Label")]
    public int BorderSize
    {
        get => borderSize;
        set { borderSize = value; Invalidate(); }
    }

    [Category("Rounded Label")]
    public Color BorderColor
    {
        get => borderColor;
        set { borderColor = value; Invalidate(); }
    }

    [Category("Rounded Label")]
    public ContentAlignment TextAlign
    {
        get => textAlign;
        set { textAlign = value; Invalidate(); }
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
        get => base.Text;
        set { base.Text = value; Invalidate(); }
    }

    // ================= DIBUJO =================

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rectSurface = this.ClientRectangle;
        // Ajuste sutil para que el borde no se pixele en los límites del control
        Rectangle rectBorder = new Rectangle(rectSurface.X, rectSurface.Y, rectSurface.Width - 1, rectSurface.Height - 1);

        using (GraphicsPath pathSurface = GetRoundedPath(rectSurface, borderRadius))
        using (Pen penBorder = new Pen(borderColor, borderSize))
        {
            this.Region = new Region(pathSurface);
            g.Clear(this.BackColor);

            DrawText(g);

            if (borderSize >= 1)
            {
                penBorder.Alignment = PenAlignment.Inset;
                g.DrawPath(penBorder, pathSurface);
            }
        }
    }

    private void DrawText(Graphics g)
    {
        using (StringFormat stringFormat = new StringFormat())
        {
            // Alineación Horizontal
            if (textAlign == ContentAlignment.TopLeft || textAlign == ContentAlignment.MiddleLeft || textAlign == ContentAlignment.BottomLeft)
                stringFormat.Alignment = StringAlignment.Near;
            else if (textAlign == ContentAlignment.TopRight || textAlign == ContentAlignment.MiddleRight || textAlign == ContentAlignment.BottomRight)
                stringFormat.Alignment = StringAlignment.Far;
            else
                stringFormat.Alignment = StringAlignment.Center;

            // Alineación Vertical
            if (textAlign == ContentAlignment.TopLeft || textAlign == ContentAlignment.TopCenter || textAlign == ContentAlignment.TopRight)
                stringFormat.LineAlignment = StringAlignment.Near;
            else if (textAlign == ContentAlignment.BottomLeft || textAlign == ContentAlignment.BottomCenter || textAlign == ContentAlignment.BottomRight)
                stringFormat.LineAlignment = StringAlignment.Far;
            else
                stringFormat.LineAlignment = StringAlignment.Center;

            // Lógica Multiline
            if (!multiline)
                stringFormat.FormatFlags = StringFormatFlags.NoWrap;

            stringFormat.Trimming = StringTrimming.EllipsisCharacter;

            // Área de texto: reducimos un poco el ancho según el radio para que el texto no toque las curvas
            int margin = borderRadius / 2;
            Rectangle textRect = new Rectangle(margin, margin, this.Width - (margin * 2), this.Height - (margin * 2));

            using (Brush textBrush = new SolidBrush(this.ForeColor))
            {
                g.DrawString(this.Text, this.Font, textBrush, textRect, stringFormat);
            }
        }
    }

    private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        float size = radius * 2F;
        if (size > rect.Width) size = rect.Width;
        if (size > rect.Height) size = rect.Height;
        if (size <= 0) size = 1;

        path.StartFigure();
        path.AddArc(rect.X, rect.Y, size, size, 180, 90);
        path.AddArc(rect.Right - size, rect.Y, size, size, 270, 90);
        path.AddArc(rect.Right - size, rect.Bottom - size, size, size, 0, 90);
        path.AddArc(rect.X, rect.Bottom - size, size, size, 90, 90);
        path.CloseFigure();
        return path;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        this.Invalidate();
    }
}
